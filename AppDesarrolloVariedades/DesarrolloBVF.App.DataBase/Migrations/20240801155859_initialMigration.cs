using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesarrolloBVF.App.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatCentrosCostos",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "INTEGER", nullable: false),
                    SubCodigo = table.Column<int>(type: "INTEGER", nullable: false),
                    Cod_Empresa = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Afectable = table.Column<bool>(type: "INTEGER", nullable: false),
                    Estatus = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatCentrosCostos", x => new { x.Codigo, x.SubCodigo });
                });

            migrationBuilder.CreateTable(
                name: "CatCultivos",
                columns: table => new
                {
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    DescIngles = table.Column<string>(type: "TEXT", nullable: true),
                    CodigoSAT = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatCultivos", x => x.Tipo);
                });

            migrationBuilder.CreateTable(
                name: "CatTipoPlanta",
                columns: table => new
                {
                    TipoPlanta = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatTipoPlanta", x => x.TipoPlanta);
                });

            migrationBuilder.CreateTable(
                name: "DesVarCatSitios",
                columns: table => new
                {
                    IdSitio = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Abreviatura = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesVarCatSitios", x => x.IdSitio);
                });

            migrationBuilder.CreateTable(
                name: "DesVarMovimientos",
                columns: table => new
                {
                    IdApp = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TipoAccion = table.Column<string>(type: "TEXT", nullable: true),
                    Descargada = table.Column<bool>(type: "INTEGER", nullable: false),
                    FullPath = table.Column<string>(type: "TEXT", nullable: false),
                    IdMovimiento = table.Column<int>(type: "INTEGER", nullable: false),
                    IdDesarrollo = table.Column<long>(type: "INTEGER", nullable: false),
                    UrlImagen = table.Column<string>(type: "TEXT", nullable: true),
                    Comentario = table.Column<string>(type: "TEXT", nullable: true),
                    IdTipo = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Accion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesVarMovimientos", x => x.IdApp);
                });

            migrationBuilder.CreateTable(
                name: "DesVarMovimientosTipos",
                columns: table => new
                {
                    IdMovimientoTipo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesVarMovimientosTipos", x => x.IdMovimientoTipo);
                });

            migrationBuilder.CreateTable(
                name: "DesVarTipos",
                columns: table => new
                {
                    IdTipoDesarrollo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Abreviatura = table.Column<string>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesVarTipos", x => x.IdTipoDesarrollo);
                });

            migrationBuilder.CreateTable(
                name: "LogModel",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "TEXT", nullable: false),
                    UsuarioName = table.Column<string>(type: "TEXT", nullable: false),
                    Accion = table.Column<string>(type: "TEXT", nullable: false),
                    Tabla = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    IdAfectado = table.Column<string>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TiposPresentacion",
                columns: table => new
                {
                    IdPresentacion = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPresentacion", x => x.IdPresentacion);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesPresentacion",
                columns: table => new
                {
                    IdUnidad = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesPresentacion", x => x.IdUnidad);
                });

            migrationBuilder.CreateTable(
                name: "CatVariedades",
                columns: table => new
                {
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Producto = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    DescripcionIngles = table.Column<string>(type: "TEXT", nullable: false),
                    Organico = table.Column<bool>(type: "INTEGER", nullable: false),
                    Abreviacion = table.Column<string>(type: "TEXT", nullable: true),
                    NombreInterno = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatVariedades", x => new { x.Tipo, x.Producto });
                    table.ForeignKey(
                        name: "FK_CatVariedades_CatCultivos_Tipo",
                        column: x => x.Tipo,
                        principalTable: "CatCultivos",
                        principalColumn: "Tipo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcPresentacionEmps",
                columns: table => new
                {
                    CodPreEmp = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdPresentacion = table.Column<int>(type: "INTEGER", nullable: true),
                    IdUnidad = table.Column<int>(type: "INTEGER", nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Factor = table.Column<int>(type: "INTEGER", nullable: false),
                    Activo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcPresentacionEmps", x => x.CodPreEmp);
                    table.ForeignKey(
                        name: "FK_ProcPresentacionEmps_TiposPresentacion_IdPresentacion",
                        column: x => x.IdPresentacion,
                        principalTable: "TiposPresentacion",
                        principalColumn: "IdPresentacion",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProcPresentacionEmps_UnidadesPresentacion_IdUnidad",
                        column: x => x.IdUnidad,
                        principalTable: "UnidadesPresentacion",
                        principalColumn: "IdUnidad",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProcCatProductos",
                columns: table => new
                {
                    CodProducto = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Producto = table.Column<int>(type: "INTEGER", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoPlanta = table.Column<int>(type: "INTEGER", nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Abreviatura = table.Column<string>(type: "TEXT", nullable: false),
                    CodProdServSAT = table.Column<string>(type: "TEXT", nullable: true),
                    CodUniMedSAT = table.Column<string>(type: "TEXT", nullable: true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    SolicitudFinanciamiento = table.Column<bool>(type: "INTEGER", nullable: false),
                    SKU = table.Column<string>(type: "TEXT", nullable: false),
                    CodigoSap = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcCatProductos", x => x.CodProducto);
                    table.ForeignKey(
                        name: "FK_ProcCatProductos_CatTipoPlanta_TipoPlanta",
                        column: x => x.TipoPlanta,
                        principalTable: "CatTipoPlanta",
                        principalColumn: "TipoPlanta",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProcCatProductos_CatVariedades_Tipo_Producto",
                        columns: x => new { x.Tipo, x.Producto },
                        principalTable: "CatVariedades",
                        principalColumns: new[] { "Tipo", "Producto" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistDesarrolloVariedades",
                columns: table => new
                {
                    IdDesarrollo = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TipoAccion = table.Column<string>(type: "TEXT", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Consecutivo = table.Column<int>(type: "INTEGER", nullable: false),
                    IdProducto = table.Column<int>(type: "INTEGER", nullable: false),
                    IdPresentacion = table.Column<int>(type: "INTEGER", nullable: false),
                    Lote = table.Column<string>(type: "TEXT", nullable: false),
                    IdTipoDesarrollo = table.Column<int>(type: "INTEGER", nullable: false),
                    IdReferenciaPadre = table.Column<long>(type: "INTEGER", nullable: true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Individuo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistDesarrolloVariedades", x => x.IdDesarrollo);
                    table.ForeignKey(
                        name: "FK_HistDesarrolloVariedades_DesVarTipos_IdTipoDesarrollo",
                        column: x => x.IdTipoDesarrollo,
                        principalTable: "DesVarTipos",
                        principalColumn: "IdTipoDesarrollo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistDesarrolloVariedades_HistDesarrolloVariedades_IdReferenciaPadre",
                        column: x => x.IdReferenciaPadre,
                        principalTable: "HistDesarrolloVariedades",
                        principalColumn: "IdDesarrollo");
                    table.ForeignKey(
                        name: "FK_HistDesarrolloVariedades_ProcCatProductos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "ProcCatProductos",
                        principalColumn: "CodProducto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistDesarrolloVariedades_ProcPresentacionEmps_IdPresentacion",
                        column: x => x.IdPresentacion,
                        principalTable: "ProcPresentacionEmps",
                        principalColumn: "CodPreEmp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcCatProductosSegmentos",
                columns: table => new
                {
                    CodProducto = table.Column<int>(type: "INTEGER", nullable: false),
                    Codigo = table.Column<int>(type: "INTEGER", nullable: false),
                    SubCodigo = table.Column<int>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 34, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcCatProductosSegmentos", x => x.CodProducto);
                    table.ForeignKey(
                        name: "FK_ProcCatProductosSegmentos_CatCentrosCostos_Codigo_SubCodigo",
                        columns: x => new { x.Codigo, x.SubCodigo },
                        principalTable: "CatCentrosCostos",
                        principalColumns: new[] { "Codigo", "SubCodigo" },
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProcCatProductosSegmentos_ProcCatProductos_CodProducto",
                        column: x => x.CodProducto,
                        principalTable: "ProcCatProductos",
                        principalColumn: "CodProducto",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DesVarImagenes",
                columns: table => new
                {
                    IdApp = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TipoAccion = table.Column<string>(type: "TEXT", nullable: true),
                    Descargada = table.Column<bool>(type: "INTEGER", nullable: false),
                    FullPath = table.Column<string>(type: "TEXT", nullable: false),
                    IdImagen = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCultivo = table.Column<int>(type: "INTEGER", nullable: false),
                    IdVariedad = table.Column<int>(type: "INTEGER", nullable: false),
                    IdDesarrollo = table.Column<long>(type: "INTEGER", nullable: true),
                    UrlImagen = table.Column<string>(type: "TEXT", nullable: false),
                    Comentario = table.Column<string>(type: "TEXT", nullable: true),
                    Tipo = table.Column<string>(type: "TEXT", nullable: true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesVarImagenes", x => x.IdApp);
                    table.ForeignKey(
                        name: "FK_DesVarImagenes_HistDesarrolloVariedades_IdDesarrollo",
                        column: x => x.IdDesarrollo,
                        principalTable: "HistDesarrolloVariedades",
                        principalColumn: "IdDesarrollo");
                });

            migrationBuilder.CreateTable(
                name: "DesVarUbicacionRelativas",
                columns: table => new
                {
                    IdUbicacion = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TipoAccion = table.Column<string>(type: "TEXT", nullable: true),
                    IdDesarrollo = table.Column<long>(type: "INTEGER", nullable: false),
                    Valor = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesVarUbicacionRelativas", x => x.IdUbicacion);
                    table.ForeignKey(
                        name: "FK_DesVarUbicacionRelativas_HistDesarrolloVariedades_IdDesarrollo",
                        column: x => x.IdDesarrollo,
                        principalTable: "HistDesarrolloVariedades",
                        principalColumn: "IdDesarrollo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesVarImagenes_IdDesarrollo",
                table: "DesVarImagenes",
                column: "IdDesarrollo");

            migrationBuilder.CreateIndex(
                name: "IX_DesVarUbicacionRelativas_IdDesarrollo",
                table: "DesVarUbicacionRelativas",
                column: "IdDesarrollo");

            migrationBuilder.CreateIndex(
                name: "IX_HistDesarrolloVariedades_IdPresentacion",
                table: "HistDesarrolloVariedades",
                column: "IdPresentacion");

            migrationBuilder.CreateIndex(
                name: "IX_HistDesarrolloVariedades_IdProducto",
                table: "HistDesarrolloVariedades",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_HistDesarrolloVariedades_IdReferenciaPadre",
                table: "HistDesarrolloVariedades",
                column: "IdReferenciaPadre");

            migrationBuilder.CreateIndex(
                name: "IX_HistDesarrolloVariedades_IdTipoDesarrollo",
                table: "HistDesarrolloVariedades",
                column: "IdTipoDesarrollo");

            migrationBuilder.CreateIndex(
                name: "IX_ProcCatProductos_Tipo_Producto",
                table: "ProcCatProductos",
                columns: new[] { "Tipo", "Producto" });

            migrationBuilder.CreateIndex(
                name: "IX_ProcCatProductos_TipoPlanta",
                table: "ProcCatProductos",
                column: "TipoPlanta");

            migrationBuilder.CreateIndex(
                name: "IX_ProcCatProductosSegmentos_Codigo_SubCodigo",
                table: "ProcCatProductosSegmentos",
                columns: new[] { "Codigo", "SubCodigo" });

            migrationBuilder.CreateIndex(
                name: "IX_ProcPresentacionEmps_IdPresentacion",
                table: "ProcPresentacionEmps",
                column: "IdPresentacion");

            migrationBuilder.CreateIndex(
                name: "IX_ProcPresentacionEmps_IdUnidad",
                table: "ProcPresentacionEmps",
                column: "IdUnidad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesVarCatSitios");

            migrationBuilder.DropTable(
                name: "DesVarImagenes");

            migrationBuilder.DropTable(
                name: "DesVarMovimientos");

            migrationBuilder.DropTable(
                name: "DesVarMovimientosTipos");

            migrationBuilder.DropTable(
                name: "DesVarUbicacionRelativas");

            migrationBuilder.DropTable(
                name: "LogModel");

            migrationBuilder.DropTable(
                name: "ProcCatProductosSegmentos");

            migrationBuilder.DropTable(
                name: "HistDesarrolloVariedades");

            migrationBuilder.DropTable(
                name: "CatCentrosCostos");

            migrationBuilder.DropTable(
                name: "DesVarTipos");

            migrationBuilder.DropTable(
                name: "ProcCatProductos");

            migrationBuilder.DropTable(
                name: "ProcPresentacionEmps");

            migrationBuilder.DropTable(
                name: "CatTipoPlanta");

            migrationBuilder.DropTable(
                name: "CatVariedades");

            migrationBuilder.DropTable(
                name: "TiposPresentacion");

            migrationBuilder.DropTable(
                name: "UnidadesPresentacion");

            migrationBuilder.DropTable(
                name: "CatCultivos");
        }
    }
}
