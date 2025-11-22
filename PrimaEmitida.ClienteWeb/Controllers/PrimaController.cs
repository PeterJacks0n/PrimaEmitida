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

                    string url = RutaApi + "prima/referencia";  // ← Correcto
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

                    string url = RutaApi + "prima/procesar";  // ← Debe ser "procesar", NO "cargar"
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