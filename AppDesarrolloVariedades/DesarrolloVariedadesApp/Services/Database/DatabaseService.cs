using DesarrolloBVF.App.DataBase;
using DesarrolloBVF.App.Domain.Entities;
using DesarrolloBVF.App.Domain.Enums;
using DesarrolloVariedadesApp.Helpers;
using DesarrolloVariedadesApp.Services.ApiDatabase;
using DesarrolloVariedadesApp.Services.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Season.DesarrolloBVF.Domain.Entities;
using Season.DesarrolloBVF.Domain.Entities.VariantEntities;
using Season.DesarrolloBVF.Domain.Responses;

namespace DesarrolloVariedadesApp.Services.Database
{
    public class DatabaseService : IDataBaseService
    {
        private readonly IApiDatabaseService apiDatabaseService;
        private readonly IFilesServices filesServices;
        public DatabaseService(IApiDatabaseService apiDatabaseService, IFilesServices filesServices)
        {
            this.apiDatabaseService = apiDatabaseService;
            this.filesServices = filesServices;
            InicializarBD();
        }

        public DateTime UltimaActualizacion { get => Preferences.Get(nameof(UltimaActualizacion), new DateTime()); set => Preferences.Set(nameof(UltimaActualizacion), value); }
        public EEstatusBD DbStatus { get => RevisarEstatusBD(); }

        private async void InicializarBD()
        {
            var db = Get();
            //var migrations = db.GetAppliedMigrations();
            try
            {
                //Si hay migraciones pendientes aplicar las migraciones pendientes
                var pendingMigrations = db.Database.GetPendingMigrations();
                if (pendingMigrations != null)
                {
                    await db.Database.MigrateAsync();
                }
            }
            catch
            {
                //Eliminar todas la tablas para volver a crearlas en la nueva migracion
                await db.Database.EnsureDeletedAsync();
                await db.Database.MigrateAsync();
            }

            db.Database.EnsureCreated();
        }
        public DesarrolloDbContext Get()
        {
            //TODO cambiar nombre de la base de datos
            string path = Path.Combine(FileSystem.AppDataDirectory, $"DesarrolloVariedades.db");
            return new DesarrolloDbContext(path);
        }

        #region Estatus Pendientes
        private EEstatusBD RevisarEstatusBD()
        {
            var db = Get();

            return db.HistDesarrolloVariedades.Any(x => x.TipoAccion != null) ? EEstatusBD.ConPendientes
            : db.DesVarUbicacionRelativas.Any(x => x.TipoAccion != null) ? EEstatusBD.ConPendientes
            : db.DesVarImagenes.Any(x => x.TipoAccion != null) ? EEstatusBD.ConPendientes
            : db.DesVarMovimientos.Any(x => x.TipoAccion != null) ? EEstatusBD.ConPendientes
            : EEstatusBD.Actualizada;
        }
        public int NuevosRegistros()
        {
            int datos = 0;
            var db = Get();
            datos += db.HistDesarrolloVariedades.Count(x => x.TipoAccion == "A");
            datos += db.DesVarUbicacionRelativas.Count(x => x.TipoAccion == "A");
            datos += db.DesVarImagenes.Count(x => x.TipoAccion == "A");
            datos += db.DesVarMovimientos.Count(x => x.TipoAccion == "A");
            return datos;
        }
        public int RegistrosActualizados()
        {
            int datos = 0;
            var db = Get();
            datos += db.HistDesarrolloVariedades.Count(x => x.TipoAccion == "M" || x.TipoAccion == "D");
            datos += db.DesVarUbicacionRelativas.Count(x => x.TipoAccion == "M");
            datos += db.DesVarImagenes.Count(x => x.TipoAccion == "D" || x.TipoAccion == "M");
            return datos;
        }
        #endregion

        #region Actualizar
        public async Task ActualizarBD()
        {
            var catalogos = await RequestHandler.TaskRequestHandler(ActualizarCatalogos);
            if (!catalogos.success)
            {
                Application.Current?.MainPage?.DisplayAlert(catalogos.titulo, $"No se pudieron Actualizar lo Catalogos:\nMensaje:\n{catalogos.mensaje}", "Entendido");
                return;
            }
            UltimaActualizacion = DateTime.Now;
            var desarrollo = await RequestHandler.TaskRequestHandler(ActualizarPendientes);
            if (!desarrollo.success)
            {
                Application.Current?.MainPage?.DisplayAlert("Sincronizacion incompleta", $"{catalogos.mensaje}.\nDesarrollos y Ubicaciones:\n{desarrollo.mensaje}", "Entendido");
                return;
            }
            var imagenes = await RequestHandler.TaskRequestHandler(ActualizarImagenes);
            if (!imagenes.success)
            {
                Application.Current?.MainPage?.DisplayAlert("Sincronizacion incompleta", $"{catalogos.mensaje}.\n{desarrollo.mensaje}.\nImagenes:\n{imagenes.mensaje}", "Entendido");
                return;
            }
            //var movimientos = await RequestHandler.TaskRequestHandler(ActualizarMovimiento);
            //if (!movimientos.success)
            //{
            //    Application.Current?.MainPage?.DisplayAlert("Sincronizacion incompleta", $"{catalogos.mensaje}.\n{desarrollo.mensaje}.\nImagenes:\n{imagenes.mensaje}.\nMovimientos:\n{movimientos.mensaje}\"", "Entendido");
            //    return;
            //}
            //Application.Current?.MainPage?.DisplayAlert("Sincronizacion Completa", $"{catalogos.mensaje}.\n{desarrollo.mensaje}\n{imagenes.mensaje}\n{movimientos.mensaje}", "Entendido");
        }
        public async Task<string> ActualizarCatalogos()
        {
            var cultivos = this.apiDatabaseService.GetRequest<GetCatCultivosQueryResponse>("CatCultivos/GetCultivos");
         
            await Task.WhenAll(cultivos);

            var variedadesList = cultivos.Result.Variedades.Select(x => new CatProductosApp(x));
            var cultivosList = cultivos.Result.Cultivos.Select(x => new CatTiposProdApp(x));

            var db = Get();
            db.Database.ExecuteSqlRaw("PRAGMA foreign_keys=OFF;");
            await db.CatVariedades.ExecuteDeleteAsync();
            await db.CatCultivos.ExecuteDeleteAsync();
            await db.ProcPresentacionEmps.IgnoreAutoIncludes().ExecuteDeleteAsync();
            await db.ProcCatProductos.IgnoreAutoIncludes().ExecuteDeleteAsync();
            await db.CatCentrosCostos.ExecuteDeleteAsync();
            await db.DesVarTipos.IgnoreAutoIncludes().ExecuteDeleteAsync();
            await db.DesVarCatSitios.ExecuteDeleteAsync();
            await db.DesVarMovimientosTipos.ExecuteDeleteAsync();
            await db.CatTipoPlanta.ExecuteDeleteAsync();
            await db.UnidadesPresentacion.ExecuteDeleteAsync();
            await db.TiposPresentacion.ExecuteDeleteAsync();
            await db.ProcCatProductosSegmentos.ExecuteDeleteAsync();

            var llenarDB = new List<Task>()
            {
                db.CatCultivos.AddRangeAsync(cultivosList),
                db.CatVariedades.AddRangeAsync(variedadesList),
            };
            await Task.WhenAll(llenarDB);
            try
            {
                db.Database.ExecuteSqlRaw("PRAGMA foreign_keys=ON;");
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var error = ex.InnerException.Message;
                throw new DbUpdateException($"Error en la base de datos local:{error}");
            }
            return "Catalogos Actualizados Correctamente";

        }

        private async Task<string> ActualizarPendientes()
        {
            return "Desarrollos y Ubicaciones Actualizados Correctamente";
        }

        private async Task<string> ActualizarImagenes()
        {
            var response = await this.apiDatabaseService.GetRequest<GetImagenesDbQueryResponse>("DesarrolloVariedades/GetImagenesDb");
            var imagenes = response.DesVarImagenes.Select(x => new DesVarImagenesApp(x, AppSettings.ImagesUrl)).ToList();
            var db = Get();
            db.DesVarImagenes.Where(x => x.TipoAccion == null).ExecuteDelete();
            db.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE name = 'DesVarImagenes';");
            await db.DesVarImagenes.AddRangeAsync(imagenes);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var error = ex.InnerException.Message;
                throw new DbUpdateException($"Error en la base de datos local, {error}");
            }
            await RequestHandler.TaskRequestHandler(
                async () =>
                {
                    var urls = response.DesVarImagenes.Select(x => x.UrlImagen).ToList();
                    await this.filesServices.DescargarImagenesDelServidor(urls);
                    return "Imagenes descargadas correctamente";
                });

            return "Imagenes Actualizadas Correctamente";


        }

        #endregion

        #region Subir
        public async Task SubirData()
        {
            var mensaje = "Desarrollos y ubicacioenes:\n";
            mensaje += (await RequestHandler.TaskRequestHandler(SubirDesarrollos)).mensaje;
            mensaje += "\n\nImagenes:\n";
            mensaje += (await RequestHandler.TaskRequestHandler(SubirImagenes)).mensaje;
            mensaje += "\n\nMovimientos:\n";
            mensaje += (await RequestHandler.TaskRequestHandler(SubirMovimientos)).mensaje;
            //var logs = await GetJsonLogs();
            //if (!logs.IsNullOrEmpty())
            //{
            //    await RequestHandler.TaskRequestHandler(
            //    async () => {
            //        await apiDatabaseService.PostRequest("", logs);
            //        return "Logs Subidos";
            //    });
            //}            
            Application.Current?.MainPage?.DisplayAlert("Resultado al subir los registros", mensaje, "Entendido");
        }

        private async Task<string> SubirImagenes()
        {
            var mensaje = "";
            var db = Get();
            var imagenesList = db.DesVarImagenes.Where(x => x.TipoAccion == "A" || x.TipoAccion == "M").ToList();
            var imagenesEliminar = db.DesVarImagenes.Where(x => x.TipoAccion == "D");

            if (imagenesEliminar.Count() <= 0 && imagenesList.Count() <= 0)
                return "No hay datos para subir";

            var command = new Dictionary<string, IEnumerable<DesVarImagenes>>
                    {
                        { "imagenesDb", imagenesEliminar.ToList() }
                    };

            int problemas = 0;
            foreach (var imagen in imagenesList)
            {
                try
                {
                    await this.filesServices.SubirImagenAlServidor(imagen);
                    imagen.TipoAccion = null;
                }
                catch
                {
                    problemas++;
                }
            }
            await this.apiDatabaseService.PostRequest("DesarrolloVariedades/DeleteImagenDB", JsonConvert.SerializeObject(command));
            imagenesEliminar.ExecuteDelete();
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DbUpdateException($"Hubo problemas en la base de datos local al intentar subir las imagenes:{ex.InnerException.Message}");
            }
            if (problemas > 0)
            {
                mensaje = $"Hubo problemas para subir {problemas} imagenes";
            }
            else
            {
                mensaje = "La imagenes fueron agregadas existosamente";
            }
            return mensaje;
        }
        private async Task<string> SubirMovimientos()
        {
            var mensaje = "";
            var db = Get();
            var movimientosImagen = db.DesVarMovimientos.Where(x => x.TipoAccion == "A" && x.UrlImagen != null).ToList();
            var movimientosNoImagen = db.DesVarMovimientos.Where(x => x.TipoAccion == "A" && x.UrlImagen == null);
            if (movimientosImagen.Count() <= 0 && movimientosNoImagen.Count() <= 0)
                return "No hay datos para subir";

            int problemas = 0;
            foreach (var moviminto in movimientosImagen)
            {
                try
                {
                    await this.filesServices.SubirImagenAlServidor(moviminto);
                    moviminto.TipoAccion = null;
                }
                catch
                {
                    problemas++;
                }
            }
            if (movimientosNoImagen.Count() > 0)
            {
                var command = new Dictionary<string, IEnumerable<DesVarMovimientos>>
                    {
                        { "desVarMovimientos", movimientosNoImagen.ToList() }
                    };
                await this.apiDatabaseService.PostRequest("DesarrolloVariedades/AddMovimientosRange", JsonConvert.SerializeObject(command));
                movimientosNoImagen.ExecuteDelete();
            }
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DbUpdateException($"Hubo problemas en la base de datos local al intentar subir los movimientos {ex.InnerException.Message}");
            }
            if (problemas > 0)
            {
                mensaje = $"Hubo problemas para subir {problemas} movimientos";
            }
            else
            {
                mensaje = "Los movimientos fueron agregados existosamente";
            }
            return mensaje;
        }
        private async Task<string> SubirDesarrollos()
        {
            string mensaje = "";
            if (DbStatus != EEstatusBD.ConPendientes)
            {
                return "No hay datos para subir";
            }
            var desarrolloACommand = GetJsonDesarrolloUbicaciones("A");
            var desarrolloMCommand = GetJsonDesarrollo("M");
            var ubicacionesMCommand = GetJsonUbicaciones("M");
            var ubicacionesACommand = GetJsonUbicaciones("A");
            var desarrollosDCommand = GetJsonDesarrollo("D");

            await Task.WhenAll(desarrolloACommand, desarrolloMCommand, ubicacionesMCommand, ubicacionesACommand, desarrollosDCommand);

            if (desarrolloMCommand.Result == null && ubicacionesMCommand.Result == null && desarrolloACommand.Result == null && ubicacionesACommand.Result == null && desarrollosDCommand.Result == null)
                return "No hay datos para subir";

            var tasks = new List<Task>();
            Task<string> desarrolloMTask = null;
            Task<string> ubicacionesMTask = null;
            Task<string> desarrollosDTask = null;
            Task<AddDesarrolloRangeCommandResponse> desarrolloATask = null;
            Task ubicacionesATask = null;
            if (desarrolloMCommand.Result != null)
            {
                desarrolloMTask = apiDatabaseService.PostRequest("DesarrolloVariedades/UpdateDesarrolloRange", desarrolloMCommand.Result);
                tasks.Add(desarrolloMTask);
            }

            if (ubicacionesMCommand.Result != null)
            {
                ubicacionesMTask = apiDatabaseService.PostRequest("DesarrolloVariedades/UpdateUbicacionRange", ubicacionesMCommand.Result);
                tasks.Add(ubicacionesMTask);
            }

            if (desarrollosDCommand.Result != null)
            {
                desarrollosDTask = apiDatabaseService.PostRequest("DesarrolloVariedades/DeleteDesarrollo", desarrollosDCommand.Result);
                tasks.Add(desarrollosDTask);
            }

            if (desarrolloACommand.Result != null)
            {
                desarrolloATask = apiDatabaseService.PostRequest<AddDesarrolloRangeCommandResponse>("DesarrolloVariedades/AddDesarrolloRange", desarrolloACommand.Result);
                tasks.Add(desarrolloATask);
            }
            else
            {
                if (ubicacionesACommand.Result != null)
                {
                    ubicacionesATask = apiDatabaseService.PostRequest("DesarrolloVariedades/AddUbicacionRange", ubicacionesACommand.Result);
                    tasks.Add(ubicacionesATask);
                }
            }


            var db = Get();
            var ubicacionesError = new List<DesVarUbicacionRelativaApp>();
            var desarrollosError = new List<HistDesarrolloVariedadesApp>();
            try
            {
                await Task.WhenAll(tasks);
                var idError = desarrolloATask?.Result.Errores;
                if (idError != null && idError.Any())
                {
                    ubicacionesError = db.DesVarUbicacionRelativas.Where(x => idError.Contains(x.IdDesarrollo)).ToList();
                    desarrollosError = db.HistDesarrolloVariedades.Where(x => idError.Contains(x.IdDesarrollo)).ToList();
                    mensaje += $"\nHubo problemas para agregar {idError.Count()} registros nuevos";
                }
                else
                {
                    mensaje += "\nTodos los registros nuevos de Plantas Desarrollo fueron registrados exitosamente";
                }
                mensaje += "\nTodos los registros modificados de Plantas Desarrollo fueron actualizados exitosamente";
                mensaje += "\nTodos los registros modificados de Ubicaciones fueron registrados exitosamente";
                mensaje += "\nTodos los registros deshabilitados de Desarrollos fueron desactivados exitosamente";

            }
            catch (Exception ex)
            {
                if (desarrolloATask != null && !desarrolloATask.IsCompletedSuccessfully)
                {
                    ubicacionesError.AddRange(db.DesVarUbicacionRelativas.Where(x => x.TipoAccion == "A").ToList());
                    desarrollosError.AddRange(db.HistDesarrolloVariedades.Where(x => x.TipoAccion == "A").ToList());
                    mensaje += "\nHubo problemas para agregar los registros nuevos de Plantas Desarrollo";
                }
                else
                {
                    var idError = desarrolloATask?.Result.Errores;
                    if (idError != null && idError.Any())
                    {
                        ubicacionesError = db.DesVarUbicacionRelativas.Where(x => idError.Contains(x.IdDesarrollo)).ToList();
                        desarrollosError = db.HistDesarrolloVariedades.Where(x => idError.Contains(x.IdDesarrollo)).ToList();
                        mensaje += $"\nHubo problemas para agregar {idError.Count()} registros nuevos";
                    }
                    else
                    {
                        mensaje += "\nTodos los registros nuevos de Plantas Desarrollo fueron registrados exitosamente";
                    }
                }
                if (ubicacionesATask != null && !ubicacionesATask.IsCompletedSuccessfully)
                {
                    ubicacionesError.AddRange(db.DesVarUbicacionRelativas.Where(x => x.TipoAccion == "A").ToList());
                    mensaje += "\nHubo problemas para agregar los registros nuevos de Ubicaciones";
                }
                else
                {
                    mensaje += "\nTodos los registros modificados de Ubicaciones fueron registrados exitosamente";
                }
                if (desarrolloMTask != null && !desarrolloMTask.IsCompletedSuccessfully)
                {
                    desarrollosError.AddRange(db.HistDesarrolloVariedades.Where(x => x.TipoAccion == "M").ToList());
                    mensaje += "\nHubo problemas para agregar los registros modificados de Plantas Desarrollo";
                }
                else
                {
                    mensaje += "\nTodos los registros modificados de Plantas Desarrollo fueron actualizados exitosamente";
                }
                if (ubicacionesMTask != null && !ubicacionesMTask.IsCompletedSuccessfully)
                {
                    ubicacionesError.AddRange(db.DesVarUbicacionRelativas.Where(x => x.TipoAccion == "M").ToList());
                    mensaje += "\nHubo problemas para agregar los registros modificados de Ubicaciones";
                }
                else
                {
                    mensaje += "\nTodos los registros modificados de Ubicaciones fueron registrados exitosamente";
                }
                if (desarrollosDTask != null && !desarrollosDTask.IsCompletedSuccessfully)
                {
                    desarrollosError.AddRange(db.HistDesarrolloVariedades.Where(x => x.TipoAccion == "D").ToList());
                    mensaje += $"\nHubo problemas para deshabilitar los desarrollos: {ex.Message}";
                }
                else
                {
                    mensaje += "\nTodos los registros deshabilitados de Desarrollos fueron desactivados exitosamente";
                }
            }

            var desarrollosSubidos = db.HistDesarrolloVariedades.Where(x => !desarrollosError.Contains(x) && x.IdTipoDesarrollo != 0);
            var ubicacionesSubidas = db.DesVarUbicacionRelativas.Where(x => !ubicacionesError.Contains(x) && x.Tipo != null);

            foreach (var desarrollo in desarrollosSubidos)
            {
                desarrollo.TipoAccion = null;
            }
            foreach (var ubicacion in ubicacionesSubidas)
            {
                ubicacion.TipoAccion = null;
            }
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DbUpdateException($"Problemas en la base de datos local al cargar los desarrollos, {ex.InnerException.Message}");
            }
            return mensaje;
        }

        #endregion

        #region Generador de JSON
        private async Task<string?> GetJsonLogs()
        {
            var db = Get();
            var logs = await db.LogModel.ToListAsync();

            if (!logs.Any())
                return null;
            return JsonConvert.SerializeObject(logs);
        }
        public async Task<string?> GetJsonDesarrollo(string tipoAccion)
        {
            var db = Get();
            var desarrollo = await db.HistDesarrolloVariedades.Where(x => x.TipoAccion == tipoAccion).ToListAsync();

            if (!desarrollo.Any())
                return null;

            var desarrollosCommand = new Dictionary<string, IEnumerable<HistDesarrolloVariedades>>
            {
                { "desarrollos", desarrollo }
            };
            return JsonConvert.SerializeObject(desarrollosCommand);


        }

        public async Task<string?> GetJsonDesarrolloUbicaciones(string tipoAccion)
        {
            var db = Get();
            var desarrollo = await db.HistDesarrolloVariedades.Where(x => x.TipoAccion == tipoAccion).ToListAsync();

            var ubicaciones = await db.DesVarUbicacionRelativas.Where(x => x.TipoAccion == tipoAccion).ToListAsync();

            if (!desarrollo.Any())
                return null;

            var desarrollosCommand = new Dictionary<string, IEnumerable<object>>
            {
                { "desarrollos", desarrollo },
                { "ubicaciones", ubicaciones }
            };
            return JsonConvert.SerializeObject(desarrollosCommand);
        }

        public async Task<string?> GetJsonUbicaciones(string tipoAccion)
        {
            var db = Get();
            var ubicaciones = await db.DesVarUbicacionRelativas.Where(x => x.TipoAccion == tipoAccion).ToListAsync();

            if (!ubicaciones.Any())
                return null;

            var ubicacionesCommand = new Dictionary<string, IEnumerable<DesVarUbicacionRelativa>>
            {
                { "ubicaciones", ubicaciones }
            };
            return JsonConvert.SerializeObject(ubicacionesCommand);
        }

        public async Task<string?> GetJsonImagenes(string tipoAccion)
        {
            var db = Get();
            var imagenes = await db.DesVarImagenes.Where(x => x.TipoAccion == tipoAccion).ToListAsync();

            if (!imagenes.Any())
                return null;

            var ImagenesCommand = new Dictionary<string, IEnumerable<DesVarImagenes>>
            {
                { "desVarImagenes", imagenes }
            };
            return JsonConvert.SerializeObject(ImagenesCommand);
        }

        public string? SetPresentacionJSON(ProcPresentacionEmpApp presentacion, char accion)
        {
            if (accion.Equals('N'))
            {
                var presentacionCommand = new Dictionary<string, ProcPresentacionEmp>
                {
                    { "presentacion", presentacion }
                };
                return JsonConvert.SerializeObject(presentacionCommand);
            }
            var modificado = new
            {
                descripcion = presentacion.Descripcion,
                factor = presentacion.Factor,
                codPreEmp = presentacion.CodPreEmp
            };
            return JsonConvert.SerializeObject(modificado);
        }

        public string? SetProductoJSON(ProcCatProductosApp producto, char accion)
        {
            if (accion.Equals('N'))
            {
                var productoCommand = new Dictionary<string, ProcCatProductos>
                {
                    { "producto", producto }
                };
                return JsonConvert.SerializeObject(productoCommand);
            }
            var modificado = new
            {
                abreviatura = producto.Abreviatura,
                descripcion = producto.Descripcion,
                codProducto = producto.CodProducto
            };
            return JsonConvert.SerializeObject(modificado);
        }

        public string SetCultivoJSON(CatTiposProdApp cultivo)
        {
            var cultivoCommand = new Dictionary<string, CatTiposProd>
            {
                { "cultivo", cultivo }
            };
            return JsonConvert.SerializeObject(cultivoCommand);
        }

        public string? SetVariedadJSON(CatProductosApp variedad, char accion)
        {
            if (accion.Equals('N'))
            {
                var variedadCommand = new Dictionary<string, CatProductos>
                {
                    { "variedad", variedad }
                };
                return JsonConvert.SerializeObject(variedadCommand);
            }
            var modificado = new
            {
                abreviatura = variedad.Abreviacion,
                organica = variedad.Organico,
                idVariedad = variedad.Producto,
                idCultivo = variedad.Tipo
            };
            return JsonConvert.SerializeObject(modificado);
        }
        #endregion
    }
}
