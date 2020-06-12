using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppDAEREST.Models;

namespace AppDAEREST.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {

        }//Constructor

        protected async override void OnConfiguring(DbContextOptionsBuilder PaOptionsBuilder)
        {
            try
            {

            }
            catch (Exception e)
            {

            }
        }

        //Modelos
        #region Inventarios
        //Gestión de inventarios
        public DbSet<zt_inventarios_acumulados> zt_inventarios_acumulados { get; set; }
        public DbSet<zt_inventarios_conteos> zt_inventarios_conteos { get; set; }

        public DbSet<zt_cat_cedis> zt_cat_cedis { get; set; }
        public DbSet<zt_cat_productos> zt_cat_productos { get; set; }
        public DbSet<zt_cat_productos_medidas> zt_cat_productos_medidas { get; set; }
        public DbSet<zt_cat_unidad_medidas> zt_cat_unidad_medidas { get; set; }
        public DbSet<zt_lista_precio_sku_um> zt_lista_precio_sku_um { get; set; }
        public DbSet<zt_cat_grupos_sku> zt_cat_grupos_sku { get; set; }
        #endregion

        //Para la inserción de datos
        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                #region Inventarios
                //Creación de llaves primarias
                modelBuilder.Entity<zt_inventarios_conteos>()
                    .HasKey(c => new { c.NumConteo, c.IdInventario, c.IdAlmacen, c.IdSKU, c.IdUnidadMedida, c.IdUbicacion });
                modelBuilder.Entity<zt_cat_cedis>().HasKey(pk => new { pk.IdCedi });
                modelBuilder.Entity<zt_inventarios_conteos>().HasIndex(x => x.NumConteo).IsUnique(false);
                modelBuilder.Entity<zt_inventarios_conteos>().HasIndex(x => x.IdInventario).IsUnique(false);
                modelBuilder.Entity<zt_inventarios_conteos>().HasIndex(x => x.IdAlmacen).IsUnique(false);
                modelBuilder.Entity<zt_inventarios_conteos>().HasIndex(x => x.IdSKU).IsUnique(false);
                modelBuilder.Entity<zt_inventarios_conteos>().HasIndex(x => x.IdUnidadMedida).IsUnique(false);
                modelBuilder.Entity<zt_inventarios_conteos>().HasIndex(x => x.IdUbicacion).IsUnique(false);

                modelBuilder.Entity<zt_inventarios_acumulados>()
                    .HasKey(c => new { c.IdInventario, c.IdSKU, c.IdUnidadMedida });
                #endregion

                #region Proyecto_DAE

                modelBuilder.Entity<zt_cat_productos>().HasKey(c => new { c.IdSKU });
                modelBuilder.Entity<zt_cat_unidad_medidas>().HasKey(c => new { c.IdUnidadMedida });
                modelBuilder.Entity<zt_cat_grupos_sku>().HasKey(c => new { c.IdGrupoSKU });
                modelBuilder.Entity<zt_lista_precio_sku_um>().HasKey(c => new { c.IdLista });
                modelBuilder.Entity<zt_cat_productos_medidas>().HasKey(c => new { c.IdProdMedidas });

                //Llaves foráneas Productos
                modelBuilder.Entity<zt_cat_productos>()
                .HasOne(s => s.zt_cat_grupos_sku).
                WithMany().HasForeignKey(s => new { s.IdGrupoSKU });

                modelBuilder.Entity<zt_cat_productos>()
                .HasOne(s => s.zt_cat_unidad_medidas).
                WithMany().HasForeignKey(s => new { s.IdUnidadMedidaBase });

                //Llaves foráneas Lista_SKU
                modelBuilder.Entity<zt_lista_precio_sku_um>()
                .HasOne(s => s.zt_cat_productos).
                WithMany().HasForeignKey(s => new { s.IdSKU });

                modelBuilder.Entity<zt_lista_precio_sku_um>()
                .HasOne(s => s.zt_cat_unidad_medidas).
                WithMany().HasForeignKey(s => new { s.IdUnidadMedida });

                //Llaves foráneas Productos_Medidas
                modelBuilder.Entity<zt_cat_productos_medidas>()
                .HasOne(s => s.zt_cat_productos).
                WithMany().HasForeignKey(s => new { s.IdSKU });

                modelBuilder.Entity<zt_cat_productos_medidas>()
                .HasOne(s => s.zt_cat_unidad_medidas).
                WithMany().HasForeignKey(s => new { s.IdUnidadMedida });



                #endregion


            }
            catch (Exception e)
            {

            }//catch()
        }//onModelCreating
    }//class
}//namespace
