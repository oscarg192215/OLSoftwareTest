using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OLSoftwareTest.Models;
using OLSoftwareTest.Models.DTO;
using System.Reflection.Metadata;
using System.Text;

namespace OLSoftwareTest.Controllers
{
    public class PruebasController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7011/api");
        private readonly HttpClient _client;
        private readonly string listPreguntas;

        public PruebasController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }


        public IActionResult Index()
        {
            try
            {
                List<PruebasViewModel> listPruebas = new List<PruebasViewModel>();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/Pruebas").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    listPruebas = JsonConvert.DeserializeObject<List<PruebasViewModel>>(data);
                }
                ViewData["EstadosPruebas"] = GetEstadosPruebas();
                return View(listPruebas);
            }
            catch (Exception ex)
            {
                return View();
            }            
        }

        public IActionResult Details(int id)
        {
            try
            {
                PruebasViewModel itemPruebas = new PruebasViewModel();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/Pruebas/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    itemPruebas = JsonConvert.DeserializeObject<PruebasViewModel>(data);
                }

                ViewData["TiposPruebas"] = GetTiposPruebas();
                ViewData["LenguajesProgramacion"] = getLenguajesProgramacion();
                ViewData["NivelesConocimiento"] = getNivelesConocimiento();
                ViewData["EstadosPruebas"] = GetEstadosPruebas();
                ViewData["Preguntas"] = GetPreguntas(itemPruebas.id_lenguaje, itemPruebas.id_nivel);
                return View(itemPruebas);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }        

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["EstadosPruebas"] = GetEstadosPruebas();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pruebas model)
        {
            try
            {
                var idsPreguntas = TempData["Id_preguntas"]?.ToString();
                if (!string.IsNullOrEmpty(idsPreguntas) && !string.IsNullOrWhiteSpace(idsPreguntas))
                {
                    int n;
                    int[] preguntasId = idsPreguntas.Split(',').Select(s => int.TryParse(s, out n) ? n : 0).ToArray();
                    model.id_pregunta = preguntasId;
                }
                
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(baseAddress + "/Pruebas", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["Id_preguntas"] = null;
                    TempData["successMessage"] = "Prueba creada.";                    
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["Id_preguntas"] = null;
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["TiposPruebas"] = GetTiposPruebas();
            ViewData["LenguajesProgramacion"] = getLenguajesProgramacion();
            ViewData["NivelesConocimiento"] = getNivelesConocimiento();
            ViewData["EstadosPruebas"] = GetEstadosPruebas();

            PruebasViewModel itemPruebas = new PruebasViewModel();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Pruebas/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                itemPruebas = JsonConvert.DeserializeObject<PruebasViewModel>(data);
            }

            ViewData["Preguntas"] = GetPreguntas(itemPruebas.id_lenguaje, itemPruebas.id_nivel);

            return View(itemPruebas);
        }


        [HttpPost]
        public IActionResult Edit(int id, Pruebas model)
        {
            try
            {
                var idsPreguntas = TempData["Id_preguntas"]?.ToString();
                if (!string.IsNullOrEmpty(idsPreguntas) && !string.IsNullOrWhiteSpace(idsPreguntas))
                {
                    int n;
                    int[] preguntasId = idsPreguntas.Split(',').Select(s => int.TryParse(s, out n) ? n : 0).ToArray();
                    model.id_pregunta = preguntasId;
                }
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(baseAddress + "/Pruebas/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Prueba modificada.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                PruebasViewModel itemPruebas = new PruebasViewModel();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/Pruebas/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    itemPruebas = JsonConvert.DeserializeObject<PruebasViewModel>(data);
                }
                ViewData["TiposPruebas"] = GetTiposPruebas();
                ViewData["LenguajesProgramacion"] = getLenguajesProgramacion();
                ViewData["NivelesConocimiento"] = getNivelesConocimiento();
                ViewData["EstadosPruebas"] = GetEstadosPruebas();
                ViewData["Preguntas"] = GetPreguntas(itemPruebas.id_lenguaje, itemPruebas.id_nivel);
                return View(itemPruebas);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public IActionResult DeleteConfirm(Pruebas model)
        {
            try
            {
                int id = model.id_prueba;
                HttpResponseMessage response = _client.DeleteAsync(baseAddress + "/Pruebas/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Prueba eliminada.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        public Object GetTiposPruebas()
        {
            List<TiposPruebas> listTiposPruebas = new List<TiposPruebas>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/TiposPruebas").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listTiposPruebas = JsonConvert.DeserializeObject<List<TiposPruebas>>(data);
            }
            return listTiposPruebas;
        }

        public Object getNivelesConocimiento()
        {
            List<NivelesConocimiento> listNivelesConocimiento = new List<NivelesConocimiento>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/NivelesConocimiento").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listNivelesConocimiento = JsonConvert.DeserializeObject<List<NivelesConocimiento>>(data);
            }
            return listNivelesConocimiento;
        }

        public Object getLenguajesProgramacion()
        {
            List<LenguajesProgramacion> listLenguajesProgramacion = new List<LenguajesProgramacion>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/LenguajesProgramacion").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listLenguajesProgramacion = JsonConvert.DeserializeObject<List<LenguajesProgramacion>>(data);
            }
            return listLenguajesProgramacion;
        }
        public Object GetPreguntas(int IdLenguaje, int IdNivel)
        {
            List<Preguntas> listPreguntas = new List<Preguntas>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Preguntas").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listPreguntas = JsonConvert.DeserializeObject<List<Preguntas>>(data);
            }

            return listPreguntas.OrderBy(x => x.id_pregunta).Where(l => l.id_lenguaje == IdLenguaje && l.id_nivel == IdNivel);
        }

        public Object GetEstadosPruebas()
        {
            List<EstadosPruebaAspirante> listEstados = new List<EstadosPruebaAspirante>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/EstadosPruebaAspirante").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listEstados = JsonConvert.DeserializeObject<List<EstadosPruebaAspirante>>(data);
            }

            return listEstados.OrderBy(x => x.id_estado_prueba_aspirante);
        }
        public void SetPreguntas(string IdPregunta)
        {
            TempData["Id_preguntas"] = IdPregunta;
        }
    }
}
