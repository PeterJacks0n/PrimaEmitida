using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrimaEmitida.Entidades.Core;
using PrimaEmitida.LogicaNegocio.Core;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebServicesPrimaEmitida.Controllers
{
    [RoutePrefix("api/prima")]
    public class PrimaController : ApiController
    {
        // GET: api/prima/referencia
        [HttpGet]
        [Route("referencia")]
        public IHttpActionResult ObtenerDatosReferencia()
        {
            try
            {
                var datos = new DatosReferenciaLN().ObtenerTodos();
                return Ok(datos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/prima/cargar
        [HttpPost]
        [Route("cargar")]
        public IHttpActionResult CargarDatosTransaccionales([FromBody] List<DatosTransaccionales> datos)
        {
            try
            {
                if (datos == null || datos.Count == 0)
                {
                    return BadRequest("No se recibieron datos para procesar");
                }

                bool resultado = new DatosTransaccionalesLN().CargarDatos(datos);

                if (resultado)
                {
                    return Ok(new { mensaje = "Datos cargados exitosamente", total = datos.Count });
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/prima/procesar
        [HttpPost]
        [Route("procesar")]
        public IHttpActionResult ProcesarYComparar()
        {
            try
            {
                var resultados = new DatosReferenciaLN().ProcesarYComparar();
                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
