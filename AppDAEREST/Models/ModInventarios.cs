using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDAEREST.Models
{
    public class zt_cat_cedis
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 IdCedi { get; set; }
        [StringLength(50)]
        public string DesCedi { get; set; }
        public DateTime? FechaReg { get; set; }
        public DateTime? FechaUltMod { get; set; }
        [StringLength(20)]
        public string UsuarioReg { get; set; }
        [StringLength(20)]
        public string UsuarioMod { get; set; }
        [StringLength(1)]
        public string Activo { get; set; }
        [StringLength(1)]
        public string Borrado { get; set; }

    }

    public class zt_inventarios_acumulados
    {
        public int IdInventario { get; set; }
        //public zt_inventarios zt_inventarios { get; set; }
        [StringLength(20)]
        public string IdSKU { get; set; }
        //public zt_cat_productos zt_cat_productos { get; set; }
        //[StringLength(50)]
        //public string DesSKU { get; set; }
        [StringLength(10)]
        public string IdUnidadMedida { get; set; }
        //public zt_cat_unidad_medidas zt_cat_unidad_medidas { get; set; }
        public Nullable<double> CantidadTeorica { get; set; }
        public double CantidadTeoricaCJA { get; set; }
        public Nullable<double> CantidadFisica { get; set; }
        public double CantidadFisicaCJA { get; set; }
        public Nullable<double> Diferencia { get; set; }
        public Nullable<double> DiferenciaCJA { get; set; }
        public DateTime FechaReg { get; set; }
        [StringLength(20)]
        public string UsuarioReg { get; set; }
        public Nullable<DateTime> FechaUltMod { get; set; }
        [StringLength(20)]
        public string UsuarioMod { get; set; }
        [StringLength(1)]
        public string Activo { get; set; }
        [StringLength(1)]
        public string Borrado { get; set; }
        [StringLength(1)]
        public string Reconteo { get; set; }
    }
    public class zt_inventarios_conteos
    {
        public int IdInventario { get; set; }
        //public zt_inventarios zt_inventarios { get; set; }
        [StringLength(20)]
        public string IdAlmacen { get; set; }
        //public zt_cat_almacenes zt_cat_almacenes { get; set; }
        public int NumConteo { get; set; }
        [StringLength(20)]
        public string IdSKU { get; set; }
        //public zt_cat_productos zt_cat_productos { get; set; }
        [StringLength(10)]
        public string IdUnidadMedida { get; set; }
        //public zt_cat_unidad_medidas zt_cat_unidad_medidas { get; set; }
        [StringLength(20)]
        public string CodigoBarras { get; set; }
        [StringLength(20)]
        public string IdUbicacion { get; set; }
        //public zt_cat_ubicaciones zt_cat_ubicaciones { get; set; }
        public Nullable<double> CantidadFisica { get; set; }
        public Nullable<double> CantidadPZA { get; set; }
        [StringLength(30)]
        public string Lote { get; set; }
        public Nullable<DateTime> FechaReg { get; set; }
        [StringLength(20)]
        public string UsuarioReg { get; set; }
        [StringLength(1)]
        public string Activo { get; set; }
        [StringLength(1)]
        public string Borrado { get; set; }
    }

    public class zt_cat_productos
    {
        [StringLength(20)]
        public string IdSKU { get; set; }

        [StringLength(20)]
        public string IdGrupoSKU { get; set; }//fk
        public zt_cat_grupos_sku zt_cat_grupos_sku { get; set; }

        [StringLength(10)]
        public string IdUnidadMedidaBase { get; set; }//fk
        public zt_cat_unidad_medidas zt_cat_unidad_medidas { get; set; }

        [StringLength(20)]
        public string CodigoBarras { get; set; }

        [StringLength(50)]
        public string DesSKU { get; set; }

        public DateTime FechaReg { get; set; }

        [StringLength(20)]
        public string UsuarioReg { get; set; }

        public Nullable<DateTime> FechaUltMod { get; set; }

        [StringLength(20)]
        public string UsuarioMod { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }

        [StringLength(1)]
        public string Borrado { get; set; }
    }//Ok

    public class zt_cat_unidad_medidas
    {
        [StringLength(10)]
        public string IdUnidadMedida { get; set; }

        [StringLength(20)]
        public string DesUMedida { get; set; }

        public DateTime FechaReg { get; set; }

        [StringLength(20)]
        public string UsuarioReg { get; set; }

        public Nullable<DateTime> FechaUltMod { get; set; }

        [StringLength(20)]
        public string UsuarioMod { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }

        [StringLength(1)]
        public string Borrado { get; set; }
    }//Ok

    public class zt_cat_productos_medidas
    {
        [StringLength(20)]
        public string IdProdMedidas { get; set; }

        [StringLength(20)]
        public string IdSKU { get; set; }
        public zt_cat_productos zt_cat_productos { get; set; }

        [StringLength(10)]
        public string IdUnidadMedida { get; set; }
        public zt_cat_unidad_medidas zt_cat_unidad_medidas { get; set; }

        public float CantidadPZA { get; set; }

        public DateTime FechaReg { get; set; }

        [StringLength(20)]
        public string UsuarioReg { get; set; }

        public Nullable<DateTime> FechaUltMod { get; set; }

        [StringLength(20)]
        public string UsuarioMod { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }

        [StringLength(1)]
        public string Borrado { get; set; }
    }//Ok

    public class zt_cat_grupos_sku
    {
        [StringLength(20)]
        public string IdGrupoSKU { get; set; }//pk

        [StringLength(50)]
        public string DesGrupoSKU { get; set; }

        public DateTime FechaReg { get; set; }

        [StringLength(20)]
        public string UsuarioReg { get; set; }

        public Nullable<DateTime> FechaUltMod { get; set; }

        [StringLength(20)]
        public string UsuarioMod { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }

        [StringLength(1)]
        public string Borrado { get; set; }
    }//Ok

    public class zt_lista_precio_sku_um
    {
        [StringLength(20)]
        public string IdLista { get; set; }

        [StringLength(20)]
        public string IdSKU { get; set; } //fk
        public zt_cat_productos zt_cat_productos { get; set; }

        [StringLength(10)]
        public string IdUnidadMedida { get; set; }
        public zt_cat_unidad_medidas zt_cat_unidad_medidas { get; set; }

        public double Precio { get; set; }

        [MaxLength]
        public string imgURL { get; set; }
    }

    public class zt_cat_sku
    {

        [StringLength(20)]
        public string IdSKU { get; set; } //fk

        [StringLength(50)]
        public string DesSKU { get; set; }

        [StringLength(20)]
        public string DesUMedida { get; set; }

        public double Precio { get; set; }

        [MaxLength]
        public string Imagen { get; set; }

        public string UniMedPre { get { return DesUMedida + ": $" + Precio; } }

    }


    public class zt_inv_temporal_conacu
    {
        public int IdInventario { get; set; }
        [StringLength(20)]
        public string IdAlmacen { get; set; }
        [StringLength(20)]
        public string IdUbicacion { get; set; }
        [StringLength(20)]
        public string IdUnidadMedida { get; set; }
        [StringLength(20)]
        public string IdSKU { get; set; }
        public int NumConteo { get; set; }
        public Nullable<double> CantidadTeorica { get; set; }
        public double CantidadTeoricaCJA { get; set; }
        public Nullable<double> Diferencia { get; set; }
        public double DiferenciaCJA { get; set; }
        public Nullable<double> CantidadFisica { get; set; }
        public Nullable<double> CantidadFisicaPZA { get; set; }
    }

    public class zt_inv_temporal_conacu2
    {
        public int IdInventario { get; set; }
        [StringLength(20)]
        public string IdAlmacen { get; set; }
        [StringLength(20)]
        public string IdUbicacion { get; set; }
        [StringLength(20)]
        public string IdUnidadMedida { get; set; }
        [StringLength(20)]
        public string IdSKU { get; set; }
        public int NumConteo { get; set; }
        public Nullable<double> CantidadTeorica { get; set; }
        public double CantidadTeoricaCJA { get; set; }
        public Nullable<double> Diferencia { get; set; }
        public double DiferenciaCJA { get; set; }
        public Nullable<double> CantidadFisica { get; set; }
        public Nullable<double> CantidadFisicaCJA { get; set; }
        public Nullable<double> CantidadPZA { get; set; }
        public string Reconteo { get; set; }
        public string Activo { get; set; }
        public string Borrado { get; set; }
        public Nullable<DateTime> FechaReg { get; set; }
        public string UsuarioReg { get; set; }
        public Nullable<DateTime> FechaUltMod { get; set; }
        public string UsuarioMod { get; set; }
        public string CodigoBarras { get; set; }
        public string Lote { get; set; }
    }
}
