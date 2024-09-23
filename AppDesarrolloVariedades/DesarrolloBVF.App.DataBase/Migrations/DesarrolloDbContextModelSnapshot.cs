﻿// <auto-generated />
using System;
using DesarrolloBVF.App.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DesarrolloBVF.App.DataBase.Migrations
{
    [DbContext(typeof(DesarrolloDbContext))]
    partial class DesarrolloDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.CatProductosApp", b =>
                {
                    b.Property<int>("Tipo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Producto")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Abreviacion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DescripcionIngles")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NombreInterno")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Organico")
                        .HasColumnType("INTEGER");

                    b.HasKey("Tipo", "Producto");

                    b.ToTable("CatVariedades");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.CatTiposProdApp", b =>
                {
                    b.Property<int>("Tipo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CodigoSAT")
                        .HasColumnType("TEXT");

                    b.Property<string>("DescIngles")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Tipo");

                    b.ToTable("CatCultivos");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.DesVarImagenesApp", b =>
                {
                    b.Property<int>("IdApp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comentario")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Descargada")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IdCultivo")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("IdDesarrollo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdImagen")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdVariedad")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tipo")
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoAccion")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlImagen")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdApp");

                    b.HasIndex("IdDesarrollo");

                    b.ToTable("DesVarImagenes");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.DesVarMovimientosApp", b =>
                {
                    b.Property<int>("IdApp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Accion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Comentario")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Descargada")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("IdDesarrollo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdMovimiento")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdTipo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TipoAccion")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlImagen")
                        .HasColumnType("TEXT");

                    b.HasKey("IdApp");

                    b.ToTable("DesVarMovimientos");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.DesVarMovimientosTiposApp", b =>
                {
                    b.Property<int>("IdMovimientoTipo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdMovimientoTipo");

                    b.ToTable("DesVarMovimientosTipos");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.DesVarUbicacionRelativaApp", b =>
                {
                    b.Property<int>("IdUbicacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("IdDesarrollo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoAccion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdUbicacion");

                    b.HasIndex("IdDesarrollo");

                    b.ToTable("DesVarUbicacionRelativas");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.HistDesarrolloVariedadesApp", b =>
                {
                    b.Property<long>("IdDesarrollo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Consecutivo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdPresentacion")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdProducto")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("IdReferenciaPadre")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdTipoDesarrollo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Individuo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Lote")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoAccion")
                        .HasColumnType("TEXT");

                    b.HasKey("IdDesarrollo");

                    b.HasIndex("IdPresentacion");

                    b.HasIndex("IdProducto");

                    b.HasIndex("IdReferenciaPadre");

                    b.HasIndex("IdTipoDesarrollo");

                    b.ToTable("HistDesarrolloVariedades");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.LogModel", b =>
                {
                    b.Property<string>("Accion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<string>("IdAfectado")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Tabla")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UsuarioName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("LogModel");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.ProcCatProductosApp", b =>
                {
                    b.Property<int>("CodProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Abreviatura")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CodProdServSAT")
                        .HasColumnType("TEXT");

                    b.Property<string>("CodUniMedSAT")
                        .HasColumnType("TEXT");

                    b.Property<string>("CodigoSap")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Producto")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("SolicitudFinanciamiento")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Tipo")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TipoPlanta")
                        .HasColumnType("INTEGER");

                    b.HasKey("CodProducto");

                    b.HasIndex("TipoPlanta");

                    b.HasIndex("Tipo", "Producto");

                    b.ToTable("ProcCatProductos");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.ProcPresentacionEmpApp", b =>
                {
                    b.Property<int>("CodPreEmp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Factor")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IdPresentacion")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IdUnidad")
                        .HasColumnType("INTEGER");

                    b.HasKey("CodPreEmp");

                    b.HasIndex("IdPresentacion");

                    b.HasIndex("IdUnidad");

                    b.ToTable("ProcPresentacionEmps");
                });

            modelBuilder.Entity("Season.DesarrolloBVF.Domain.Entities.CatCentrosCostos", b =>
                {
                    b.Property<int>("Codigo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubCodigo")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Afectable")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cod_Empresa")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Estatus")
                        .HasColumnType("INTEGER");

                    b.HasKey("Codigo", "SubCodigo");

                    b.ToTable("CatCentrosCostos");
                });

            modelBuilder.Entity("Season.DesarrolloBVF.Domain.Entities.DesVarCatSitios", b =>
                {
                    b.Property<int>("IdSitio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Abreviatura")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdSitio");

                    b.ToTable("DesVarCatSitios");
                });

            modelBuilder.Entity("Season.DesarrolloBVF.Domain.Entities.DesVarTipos", b =>
                {
                    b.Property<int>("IdTipoDesarrollo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Abreviatura")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdTipoDesarrollo");

                    b.ToTable("DesVarTipos");
                });

            modelBuilder.Entity("Season.DesarrolloBVF.Domain.Entities.PresentacionesAux", b =>
                {
                    b.Property<int>("IdPresentacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdPresentacion");

                    b.ToTable("TiposPresentacion");
                });

            modelBuilder.Entity("Season.DesarrolloBVF.Domain.Entities.ProcCatProductosSegmentos", b =>
                {
                    b.Property<int>("CodProducto")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Codigo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("TEXT");

                    b.Property<int>("SubCodigo")
                        .HasColumnType("INTEGER");

                    b.HasKey("CodProducto");

                    b.ToTable("ProcCatProductosSegmentos");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ProcCatProductosSegmentos");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Season.DesarrolloBVF.Domain.Entities.ProcCatTipoPlanta", b =>
                {
                    b.Property<int>("TipoPlanta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TipoPlanta");

                    b.ToTable("CatTipoPlanta");
                });

            modelBuilder.Entity("Season.DesarrolloBVF.Domain.Entities.UnidadesAux", b =>
                {
                    b.Property<int>("IdUnidad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdUnidad");

                    b.ToTable("UnidadesPresentacion");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.ProcCatProductosSegmentosApp", b =>
                {
                    b.HasBaseType("Season.DesarrolloBVF.Domain.Entities.ProcCatProductosSegmentos");

                    b.HasIndex("Codigo", "SubCodigo");

                    b.HasDiscriminator().HasValue("ProcCatProductosSegmentosApp");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.CatProductosApp", b =>
                {
                    b.HasOne("DesarrolloBVF.App.Domain.Entities.CatTiposProdApp", "Cultivo")
                        .WithMany("Variedades")
                        .HasForeignKey("Tipo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cultivo");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.DesVarImagenesApp", b =>
                {
                    b.HasOne("DesarrolloBVF.App.Domain.Entities.HistDesarrolloVariedadesApp", "Desarrollo")
                        .WithMany("Imagenes")
                        .HasForeignKey("IdDesarrollo");

                    b.Navigation("Desarrollo");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.DesVarUbicacionRelativaApp", b =>
                {
                    b.HasOne("DesarrolloBVF.App.Domain.Entities.HistDesarrolloVariedadesApp", "Desarrollo")
                        .WithMany("Ubicaciones")
                        .HasForeignKey("IdDesarrollo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Desarrollo");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.HistDesarrolloVariedadesApp", b =>
                {
                    b.HasOne("DesarrolloBVF.App.Domain.Entities.ProcPresentacionEmpApp", "Presentacion")
                        .WithMany()
                        .HasForeignKey("IdPresentacion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DesarrolloBVF.App.Domain.Entities.ProcCatProductosApp", "Producto")
                        .WithMany()
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DesarrolloBVF.App.Domain.Entities.HistDesarrolloVariedadesApp", "ReferenciaPadre")
                        .WithMany()
                        .HasForeignKey("IdReferenciaPadre");

                    b.HasOne("Season.DesarrolloBVF.Domain.Entities.DesVarTipos", "TipoDesarrollo")
                        .WithMany()
                        .HasForeignKey("IdTipoDesarrollo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Presentacion");

                    b.Navigation("Producto");

                    b.Navigation("ReferenciaPadre");

                    b.Navigation("TipoDesarrollo");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.ProcCatProductosApp", b =>
                {
                    b.HasOne("Season.DesarrolloBVF.Domain.Entities.ProcCatTipoPlanta", "PlantaTipo")
                        .WithMany()
                        .HasForeignKey("TipoPlanta")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DesarrolloBVF.App.Domain.Entities.CatProductosApp", "Variedad")
                        .WithMany("Produtos")
                        .HasForeignKey("Tipo", "Producto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlantaTipo");

                    b.Navigation("Variedad");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.ProcPresentacionEmpApp", b =>
                {
                    b.HasOne("Season.DesarrolloBVF.Domain.Entities.PresentacionesAux", "TipoPresetacion")
                        .WithMany()
                        .HasForeignKey("IdPresentacion")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Season.DesarrolloBVF.Domain.Entities.UnidadesAux", "Unidad")
                        .WithMany()
                        .HasForeignKey("IdUnidad")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("TipoPresetacion");

                    b.Navigation("Unidad");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.ProcCatProductosSegmentosApp", b =>
                {
                    b.HasOne("DesarrolloBVF.App.Domain.Entities.ProcCatProductosApp", null)
                        .WithOne("Segmento")
                        .HasForeignKey("DesarrolloBVF.App.Domain.Entities.ProcCatProductosSegmentosApp", "CodProducto")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Season.DesarrolloBVF.Domain.Entities.CatCentrosCostos", "CentroCosto")
                        .WithMany()
                        .HasForeignKey("Codigo", "SubCodigo")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("CentroCosto");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.CatProductosApp", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.CatTiposProdApp", b =>
                {
                    b.Navigation("Variedades");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.HistDesarrolloVariedadesApp", b =>
                {
                    b.Navigation("Imagenes");

                    b.Navigation("Ubicaciones");
                });

            modelBuilder.Entity("DesarrolloBVF.App.Domain.Entities.ProcCatProductosApp", b =>
                {
                    b.Navigation("Segmento");
                });
#pragma warning restore 612, 618
        }
    }
}
