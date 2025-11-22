using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using PrimaEmitida.Entidades.Core;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace PrimaEmitida.ClienteWeb.Controllers
{
    public class PrimaController : Controller
    {
        private readonly string RutaApi = ConfigurationManager.AppSettings["RutaApi"];
        private const string JsonMediaType = "application/json";

        // GET: Prima
        public ActionResult Index()
        {
            List<DatosReferencia> datos = new List<DatosReferencia>();

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = JsonMediaType;
                    client.Encoding = UTF8Encoding.UTF8;

                    string url = RutaApi + "prima/referencia";
                    var respuesta = client.DownloadString(new Uri(url));
                    datos = JsonConvert.DeserializeObject<List<DatosReferencia>>(respuesta);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cargar datos: " + ex.Message;
            }

            return View(datos);
        }

        // GET: Prima/Cargar
        public ActionResult Cargar()
        {
            return View();
        }

        // POST: Prima/ProcesarArchivo
        [HttpPost]
        public ActionResult ProcesarArchivo(/* Aquí recibiremos el archivo Excel */)
        {
            try
            {
                // TODO: Implementar lectura de Excel
                // Por ahora datos de prueba
                List<DatosTransaccionales> datos = new List<DatosTransaccionales>
                {
                    new DatosTransaccionales { PolTxtPresentacion = "SOBREVIVENCIA", PolNumPrima = 24567289.33M },
                    new DatosTransaccionales { PolTxtPresentacion = "INVALIDEZ", PolNumPrima = 100000M },
                    new DatosTransaccionales { PolTxtPresentacion = "INVALIDEZ PARCIAL", PolNumPrima = 100000M },
                    new DatosTransaccionales { PolTxtPresentacion = "INVALIDEZ TOTAL", PolNumPrima = 91456.39M },
                    new DatosTransaccionales { PolTxtPresentacion = "", PolNumPrima = 50000M }, // Este se omitirá
                };

                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = JsonMediaType;
                    client.Encoding = UTF8Encoding.UTF8;

                    var jsonDatos = JsonConvert.SerializeObject(datos);
                    string url = RutaApi + "prima/cargar";
                    var respuesta = client.UploadString(new Uri(url), jsonDatos);
                }

                TempData["Mensaje"] = "Datos cargados exitosamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al procesar archivo: " + ex.Message;
                return View("Cargar");
            }
        }

        // POST: Prima/Comparar
        [HttpPost]
        public ActionResult Comparar()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = JsonMediaType;
                    client.Encoding = UTF8Encoding.UTF8;

                    string url = RutaApi + "prima/procesar";
                    var respuesta = client.UploadString(new Uri(url), "POST", "");
                    var datos = JsonConvert.DeserializeObject<List<DatosReferencia>>(respuesta);

                    return View("Index", datos);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al comparar datos: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}