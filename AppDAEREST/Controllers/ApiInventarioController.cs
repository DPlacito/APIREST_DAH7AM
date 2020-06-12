using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AppDAEREST.Models;
using AppDAEREST.Data;

namespace AppDAEREST.Controllers
{
    [Produces("application/json")]
    public class ApiInventarioController : Controller
    {
        private readonly DBContext LoDBContext;

        public ApiInventarioController(DBContext PaDBContext)
        {
            LoDBContext = PaDBContext;
        }//Constructor
        #region APIS_Clase
        /*//Api's acumulados
        [HttpGet]
        [Route("api/inventarios/inv_acumulados_list")]
        public IEnumerable<zt_inventarios_acumulados> ApiGetInv_Acumulados_List([FromQuery] int IdInventario)
        {
            var resultado = (
                            from acu in LoDBContext.zt_inventarios_acumulados
                            where acu.IdInventario == IdInventario
                            select acu
                            ).ToList();

            return (resultado);
        }

        //Api's conteos
        [HttpGet]
        [Route("api/inventarios/inv_conteos_list")]
        public IEnumerable<zt_inventarios_conteos> ApiGetInv_Conteos_List([FromQuery] int IdInventario)
        {
            var resultado = (
                            from cont in LoDBContext.zt_inventarios_conteos
                            where cont.IdInventario == IdInventario
                            select cont
                            ).ToList();
            return (resultado);
        }

        [HttpGet]
        [Route("api/inventarios/inv_conteos_sku_list")]
        public IEnumerable<zt_inv_temporal_conacu> ApiGetInv_Conteos_List([FromQuery] int IdInventario, string SKU)
        {
            //Se obtienen todos los conteos de un mismo SKU y mismo inventario
            var resultado = (
                           from cont in LoDBContext.zt_inventarios_conteos
                           join acu in LoDBContext.zt_inventarios_acumulados
                           on new { cont.IdInventario, cont.IdSKU } equals new { acu.IdInventario, acu.IdSKU }
                           where cont.IdInventario == IdInventario &&
                           cont.IdSKU == SKU
                           select new zt_inv_temporal_conacu
                           {
                               IdInventario = cont.IdInventario,
                               IdAlmacen = cont.IdAlmacen,
                               IdSKU = cont.IdSKU,
                               IdUbicacion = cont.IdUbicacion,
                               IdUnidadMedida = cont.IdUnidadMedida,
                               NumConteo = cont.NumConteo,
                               CantidadTeorica = acu.CantidadTeorica,
                               CantidadTeoricaCJA = acu.CantidadTeoricaCJA,
                               Diferencia = acu.CantidadTeorica - acu.CantidadFisica,
                               CantidadFisica = cont.CantidadFisica,
                               CantidadFisicaPZA = cont.CantidadPZA
                           }).ToList();
            //Crear clase temporal con los campos que van arriba y ponerla en lugar de IEnumerable
            //Solo en models, no DBContext
            return (resultado);
        }

        //Realizar una API que obtenga la información completa de acumulado y conteo dado un IdInventario y un IdSKU
        [HttpGet]
        [Route("api/inventarios/invList_conteos_acum")]
        public IEnumerable<zt_inv_temporal_conacu2> ApiGetInv_Conteos_Acum([FromQuery] int IdInventario, string SKU)
        {
            //Se obtienen todos los conteos de un mismo SKU y mismo inventario
            var resultado = (
                           from cont in LoDBContext.zt_inventarios_conteos
                           join acu in LoDBContext.zt_inventarios_acumulados
                           on new { cont.IdInventario, cont.IdSKU } equals new { acu.IdInventario, acu.IdSKU }
                           where cont.IdInventario == IdInventario &&
                           cont.IdSKU == SKU 
                           select new zt_inv_temporal_conacu2
                           {
                               IdInventario = cont.IdInventario,
                               IdSKU = cont.IdSKU,
                               IdUnidadMedida = cont.IdUnidadMedida,
                               IdAlmacen = cont.IdAlmacen,
                               IdUbicacion = cont.IdUbicacion,
                               NumConteo = cont.NumConteo,
                               CantidadTeorica = acu.CantidadTeorica,
                               CantidadTeoricaCJA = acu.CantidadTeoricaCJA,
                               CantidadFisica = acu.CantidadFisica,
                               CantidadFisicaCJA = acu.CantidadFisicaCJA,
                               Diferencia = acu.CantidadTeorica-acu.CantidadFisica,
                               DiferenciaCJA = acu.CantidadTeoricaCJA - acu.CantidadFisicaCJA,
                               Reconteo = acu.Reconteo,
                               Activo = acu.Activo,
                               Borrado = acu.Borrado,
                               FechaReg = acu.FechaReg,
                               UsuarioReg = acu.UsuarioReg,
                               FechaUltMod = acu.FechaUltMod,
                               UsuarioMod = acu.UsuarioMod,
                               CodigoBarras = cont.CodigoBarras,
                               Lote = cont.Lote                               
                           }).ToList();
            return (resultado);
        }

        /*[HttpPost]
        [Route("api/inventarios/nuevoAcu")]
        public async Task<IActionResult> ApiPostInv_acumulado([FromQuery] int IdInventario, [FromQuery]string IdSKU, [FromQuery]string IdUnidadMedida, [FromQuery] float CantitadTeorica, [FromQuery] float CantitadTeoricaCJA, [FromQuery] float CantitadFisica, [FromQuery] float CantitadFisicaCJA,
            [FromQuery] float Diferencia, [FromQuery] float DiferenciaCJA, [FromQuery] string Reconteo, [FromQuery] string Activo, [FromQuery] string Borrado, [FromQuery] DateTime FechaReg, [FromQuery] string UsuarioReg, [FromQuery] DateTime FechaUltMod, [FromQuery] string UsuarioMod)
        {
            try
            {
                zt_inventarios_acumulados acu = new zt_inventarios_acumulados();
                acu.IdInventario = IdInventario;
                acu.IdSKU = IdSKU;
                acu.IdUnidadMedida = IdUnidadMedida;
                acu.CantidadTeorica = CantitadTeorica;
                acu.CantidadTeoricaCJA = CantitadTeoricaCJA;
                acu.CantidadFisica = CantitadFisica;
                acu.CantidadFisicaCJA = CantitadFisicaCJA;
                acu.Diferencia = Diferencia;
                acu.DiferenciaCJA = DiferenciaCJA;
                acu.Reconteo = Reconteo;
                acu.Activo = Activo;
                acu.Borrado = Borrado;
                acu.FechaReg = FechaReg;
                acu.UsuarioReg = UsuarioReg;
                acu.FechaUltMod = FechaUltMod;
                acu.UsuarioMod = UsuarioMod;                    
                LoDBContext.zt_inventarios_acumulados.Add(acu);
                LoDBContext.SaveChanges();
                return Ok("Inventario Registrado");
            }
            catch (Exception e)
            {
                Dictionary<String, String> err = new Dictionary<string, string>();
                err.Add("err", "Post");
                return Ok(err);
            }


        }

        [HttpPost]
        [Route("api/inventarios/nuevoCont")]
        public async Task<IActionResult> ApiPostInv_conteo([FromQuery] int IdInventario, [FromQuery]string IdSKU, [FromQuery]string IdUnidadMedida, [FromQuery]string IdAlmacen, [FromQuery]string IdUbicacion, [FromQuery]int NumConteo, 
            [FromQuery] string CodigoBarras, [FromQuery] float CantidadFisica, [FromQuery] float CantidadPZA,
            [FromQuery] string Lote, [FromQuery] DateTime FechaReg, [FromQuery] string UsuarioReg, [FromQuery] string Activo, [FromQuery] string Borrado)
        {
            try
            {
                zt_inventarios_conteos cont = new zt_inventarios_conteos();
                cont.IdInventario = IdInventario;
                cont.IdSKU = IdSKU;
                cont.IdUnidadMedida = IdUnidadMedida;
                cont.IdAlmacen = IdAlmacen;
                cont.IdUbicacion = IdUbicacion;
                cont.NumConteo = NumConteo;
                cont.CodigoBarras = CodigoBarras;
                cont.CantidadFisica = CantidadFisica;
                cont.CantidadPZA = CantidadPZA;
                cont.Lote = Lote;
                cont.FechaReg = FechaReg;
                cont.UsuarioReg = UsuarioReg;
                cont.Activo = Activo;
                cont.Borrado = Borrado;
                LoDBContext.zt_inventarios_conteos.Add(cont);
                LoDBContext.SaveChanges();
                return Ok("Inventario Registrado");
            }
            catch (Exception e)
            {
                Dictionary<String, String> err = new Dictionary<string, string>();
                err.Add("err", "Post");
                return Ok(err);
            }
        }

        [HttpPost]
        [Route("api/inventarios/nuevoAcu_item")]
        public async Task<IActionResult> ApiPostInvAcumulado_Item([FromBody] zt_inventarios_acumulados InvAcuItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LoDBContext.zt_inventarios_acumulados.Add(InvAcuItem);
            await LoDBContext.SaveChangesAsync();

            return CreatedAtAction("ApiGetConteosList", new { id = InvAcuItem.IdSKU });
        }

        [HttpPost]
        [Route("api/inventarios/nuevoCon_item")]
        public async Task<IActionResult> ApiPostInvConteo_Item([FromBody] zt_inventarios_conteos InvConItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LoDBContext.zt_inventarios_conteos.Add(InvConItem);
            await LoDBContext.SaveChangesAsync();

            return CreatedAtAction("ApiGetConteosList", new { id = InvConItem.IdSKU });
        }

        [HttpPut]
        [Route("api/inventarios/acumulados_update")]
        public async Task<IActionResult> ApiPutInv_AcumuladosB([FromQuery] int IdInventarioR, [FromQuery] string IdSKUR, 
            [FromQuery] string IdUnidadMedidaR, [FromBody] zt_inventarios_acumulados acu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var temp = LoDBContext.zt_inventarios_acumulados.Find(IdInventarioR, IdSKUR, IdUnidadMedidaR);
            LoDBContext.zt_inventarios_acumulados.Update(acu);
            //LoDBContext.zt_inventarios_acumulados[IdInventarioR] = acu;
            //LoDBContext.zt_inventarios_acumulados.Add(acu);
            await LoDBContext.SaveChangesAsync();
            return CreatedAtAction("ApiGetAcumuladosConteosList", new { id = acu.IdSKU });
        }

        /*[HttpDelete]
        [Route("api/inventarios/delete_conteo")]
        public async Task<IActionResult> ApiDeleteConteoItem([FromQuery]int IdInventario, [FromQuery] string SKU, [FromQuery] string Unidad,
            [FromQuery] string Almacen, [FromQuery] string Ubicacion, [FromQuery] int Conteo, [FromBody] zt_inventarios_conteos cont)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = LoDBContext.zt_inventarios_conteos.FindAsync(IdInventario, SKU, Unidad, Almacen, Ubicacion, Conteo);
            if (item == null)
            {
                return NotFound();
            }

            var item2 = LoDBContext.zt_inventarios_conteos.Find(IdInventario);
            LoDBContext.zt_inventarios_conteos.Remove(item2);
            await LoDBContext.SaveChangesAsync();

            return Ok(item2);
        }

        /*[HttpDelete]
        [Route("api/inventarios/borrar")]
        public async Task<IActionResult> ApiDeleteInventarioConteos([FromQuery] int idInventario, [FromQuery] string SKU,
            [FromQuery] string Unidad, [FromQuery] string Almacen, [FromQuery] string Ubicacion, [FromQuery] int Conteo)
        {
            zt_inventarios_conteos cont = new zt_inventarios_conteos();
            cont.IdInventario = idInventario;
            cont.IdSKU = SKU;
            cont.IdUnidadMedida = Unidad;
            cont.IdAlmacen = Almacen;
            cont.IdUbicacion = Ubicacion;
            cont.NumConteo = Conteo;
            try
            {
                LoDBContext.zt_inventarios_conteos.Remove(cont);
                LoDBContext.SaveChanges();
                return Ok(cont);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                Dictionary<String, String> err = new Dictionary<string, string>();
                err.Add("err", "No se encontraron registros");
                return Ok(err);
            }
        }*/

        /*[HttpDelete]
        [Route("api/inventarios/inventario_deleteC")]
        public async Task<IActionResult> ApiDeleteConteoItem([FromQuery] int IdInventario, [FromQuery] string SKU, [FromQuery]
        string Unidad, [FromQuery] string Almacen, [FromQuery] string Ubicacion, [FromQuery] int Conteo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await LoDBContext.zt_inventarios_conteos.FirstOrDefaultAsync(cont => cont.IdInventario == IdInventario 
            && cont.IdSKU == SKU && cont.IdUnidadMedida == Unidad && cont.IdAlmacen == Almacen && cont.IdUbicacion == Ubicacion 
            && cont.NumConteo == Conteo);
         
            if (item == null)
            {
                return NotFound();
            }

            LoDBContext.zt_inventarios_conteos.Remove(item);
            await LoDBContext.SaveChangesAsync();
            return Ok(item);
        }*/

        /*//Api que elimine un SKU de acumulados dado un Inventario y SKU como parametros y sus conteos
        [HttpDelete]
        [Route("api/inventarios/inventario_deleteCA")]
        public async Task<IActionResult> ApiDeleteConteoAcumItem([FromQuery] int IdInventario, [FromQuery] string SKU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await LoDBContext.zt_inventarios_acumulados.FirstOrDefaultAsync(acu => acu.IdInventario == IdInventario
            && acu.IdSKU == SKU);

            //var item2 = await LoDBContext.zt_inventarios_conteos.ForEachAsync()
            var item2 = (
                            from cont in LoDBContext.zt_inventarios_conteos
                            where cont.IdInventario == item.IdInventario &&
                            cont.IdSKU == item.IdSKU
                            select new zt_inventarios_conteos
                            {
                                IdInventario = cont.IdInventario,
                                IdAlmacen = cont.IdAlmacen,
                                IdSKU = cont.IdSKU,
                                IdUbicacion = cont.IdUbicacion,
                                IdUnidadMedida = cont.IdUnidadMedida,
                                NumConteo = cont.NumConteo,
                                CodigoBarras = cont.CodigoBarras,
                                CantidadFisica = cont.CantidadFisica,
                                CantidadPZA = cont.CantidadPZA,
                                Lote = cont.Lote,
                                FechaReg = cont.FechaReg,
                                UsuarioReg = cont.UsuarioReg,
                                Activo = cont.Activo,
                                Borrado = cont.Borrado
                            }).ToList();

            if (item == null)
            {
                return NotFound();
            }
            if (item2 == null)
            {
                return NotFound();
            }

            LoDBContext.zt_inventarios_conteos.RemoveRange(item2);
            LoDBContext.zt_inventarios_acumulados.Remove(item);
            await LoDBContext.SaveChangesAsync();
            return Ok(item);
        }*/

        /*[HttpPost]
        [Route("api/inventarios/inv_conteo_acumulado")]
        public async Task<IActionResult> ApiPostInvConteosAcumulado([FromBody] zt_inventarios_conteos ConteoNvo)
        {
            //insertar el conteo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LoDBContext.zt_inventarios_conteos.Add(ConteoNvo);
            await LoDBContext.SaveChangesAsync();

            //discriminar conteos no validos
           var contItem = await LoDBContext.zt_inventarios_conteos.LastOrDefaultAsync(c => c.IdInventario == ConteoNvo.IdInventario &&
           
                   c.IdSKU == ConteoNvo.IdSKU && c.IdUnidadMedida == ConteoNvo.IdUnidadMedida);

            //actualizar el acumulado
            var acuItem = await LoDBContext.zt_inventarios_acumulados.FirstOrDefaultAsync(c => c.IdInventario == ConteoNvo.IdInventario && c.IdSKU == ConteoNvo.IdSKU);

            if (acuItem == null)
            {
                return NotFound();
            }

            acuItem.CantidadFisica = acuItem.CantidadFisica + ConteoNvo.CantidadPZA;
            acuItem.Diferencia = acuItem.CantidadTeorica - acuItem.CantidadFisica;

            LoDBContext.zt_inventarios_acumulados.Update(acuItem);
            await LoDBContext.SaveChangesAsync();


            return CreatedAtAction("ApiGetInv_Conteos_Acum", new { acuItem.IdInventario, acuItem.IdSKU });

        }

        //Api que inserte conteos y que calcule y actualice por cada conteo los campos de CantidadFisica y Diferencia

        [HttpPost]
        [Route("api/inventarios/inv_conteos_post_tarea")]
        public async Task<IActionResult> ApiPostInv_Conteos_post_lunes([FromQuery] int IdInventario, [FromQuery] string IdSKU,
            [FromQuery] string IdUnidadMedida,
            [FromQuery] string IdAlmacen, [FromQuery] string IdUbicacion, [FromBody] zt_inventarios_conteos con)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = (
                      from conteos in LoDBContext.zt_inventarios_conteos
                      where conteos.IdInventario == IdInventario &&
                      conteos.IdSKU == IdSKU &&
                      conteos.IdUnidadMedida == IdUnidadMedida &&
                      conteos.IdAlmacen == IdAlmacen &&
                      conteos.IdUbicacion == IdUbicacion
                      select conteos
                      ).ToList();
            if (!resultado.Any())
            {
                con.NumConteo = 1;
                LoDBContext.zt_inventarios_conteos.Add(con);
                await LoDBContext.SaveChangesAsync();
                var temp = await LoDBContext.zt_inventarios_acumulados.FirstOrDefaultAsync(a => a.IdInventario == con.IdInventario && a.IdSKU == con.IdSKU);
                temp.CantidadFisica = temp.CantidadFisica + con.CantidadPZA;
                temp.Diferencia = temp.CantidadTeorica - temp.CantidadFisica;
                LoDBContext.zt_inventarios_acumulados.Update(temp);
                await LoDBContext.SaveChangesAsync();
            }
            else
            {
                var ultimo = resultado.Last();
                con.NumConteo = ultimo.NumConteo + 1;
                LoDBContext.zt_inventarios_conteos.Add(con);
                await LoDBContext.SaveChangesAsync();
                var temp = await LoDBContext.zt_inventarios_acumulados.FirstOrDefaultAsync(a => a.IdInventario == con.IdInventario && a.IdSKU == con.IdSKU);
                temp.CantidadFisica = temp.CantidadFisica + con.CantidadPZA;
                temp.Diferencia = temp.CantidadTeorica - temp.CantidadFisica;
                LoDBContext.zt_inventarios_acumulados.Update(temp);
                await LoDBContext.SaveChangesAsync();
            }
            return CreatedAtAction("ApiGetAcumuladosConteosList", new { id = con.IdSKU });

        }*/

        /*[HttpPost]
        [Route("api/inventarios/api_post_conteo_item")]
        public async Task<IActionResult> ApiPostInvConteos_Item([FromBody] zt_inventarios_conteos PaConteoItem)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //buscar la cantidad de conteos que tienen la misma clave que el que se va a insertar para calcular el número del conteo a insertar
            var buscarConteo = (from cont in LoDBContext.zt_inventarios_conteos
                                where cont.IdInventario == PaConteoItem.IdInventario && cont.IdSKU == PaConteoItem.IdSKU && cont.IdAlmacen == PaConteoItem.IdAlmacen
                                && cont.IdUbicacion == PaConteoItem.IdUbicacion && cont.IdUnidadMedida == PaConteoItem.IdUnidadMedida
                                select cont.NumConteo).ToList();
            var nc = buscarConteo.Count;
            PaConteoItem.NumConteo = nc + 1;
            //insertar conteo
            LoDBContext.zt_inventarios_conteos.Add(PaConteoItem);
            await LoDBContext.SaveChangesAsync();

            double totalPZA = 0;

            //buscar conteos validos
            var resultadoConteo = (from cont in LoDBContext.zt_inventarios_conteos
                                   where cont.IdInventario == PaConteoItem.IdInventario && cont.IdSKU == PaConteoItem.IdSKU
                                   select cont).GroupBy(p => new { p.IdInventario, p.IdSKU, p.IdAlmacen, p.IdUbicacion, p.IdUnidadMedida })
                                   .Select(g => g.LastOrDefault());

            //sumar la cantidad en piezas de todos los conteos validos
            foreach (var element in resultadoConteo)
            {

                totalPZA = totalPZA + (double)element.CantidadPZA;
            }

            //actualizar el acumulado
            var objAcumulado = await LoDBContext.zt_inventarios_acumulados.FirstOrDefaultAsync(c => c.IdInventario == PaConteoItem.IdInventario && c.IdSKU == PaConteoItem.IdSKU);
            if (objAcumulado == null)
            {
                return NotFound();
            }

            objAcumulado.CantidadFisica = totalPZA;
            objAcumulado.Diferencia = objAcumulado.CantidadTeorica - objAcumulado.CantidadFisica;

            LoDBContext.zt_inventarios_acumulados.Update(objAcumulado);
            await LoDBContext.SaveChangesAsync();


            return CreatedAtAction("Reg acumulado", new { objAcumulado });


        }*/

        #endregion

        #region APIS_Proyecto

        /*[HttpGet]
        [Route ("api/productos/lista")]
        public IEnumerable<zt_cat_productos> ApiGetProductos([FromQuery] string IdSKU)
         {
             var lista = (
                  from ls in LoDBContext.zt_cat_productos
                  where ls.IdSKU == IdSKU
                  select ls
                 ).ToList();

             return (lista);
         }*/

        /*//retorno de SKU especifico
        [HttpGet]
        [Route("api/inventarios/sku")]
        public IEnumerable<zt_cat_sku> ApiGetDatosProductos([FromQuery] string IdSKU, string IdUnidadMedida)
        {
            var resultado = (
                            from ls in LoDBContext.zt_lista_precio_sku_um
                            join um in LoDBContext.zt_cat_unidad_medidas on ls.IdUnidadMedida equals um.IdUnidadMedida
                            join prod in LoDBContext.zt_cat_productos on ls.IdSKU equals prod.IdSKU
                            where ls.IdSKU == IdSKU && ls.IdUnidadMedida == IdUnidadMedida
                            select new zt_cat_sku
                            {
                                IdSKU =ls.IdSKU,
                                DesSKU = prod.DesSKU,
                                DesUMedida = um.DesUMedida,
                                Precio = ls.Precio,
                                Imagen = ls.imgURL,
                                UniMedPre = um.DesUMedida + ": $" + ls.Precio
                            }
                            ).ToList();

            return (resultado);
        }*/

        //retornoTODO
        [HttpGet]
        [Route("api/inventarios/sku_lista")]
        public IEnumerable<zt_cat_cedis> ApiGetDatosProductosLista()
        {
            var resultado = (
                            from ls in LoDBContext.zt_cat_cedis
                            select ls).ToList();

            return (resultado);
        }

        #endregion
        public IActionResult Index()
        {
            return View();
        }
    }
}