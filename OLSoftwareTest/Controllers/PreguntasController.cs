using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLSoftwareTest.Models;
using OLSoftwareTest.Models.DTO;
using System.Text;

namespace OLSoftwareTest.Controllers
{
    public class PreguntasController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7011/api");
        private readonly HttpClient _client;

        public PreguntasController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            List<PreguntasViewModel> listPreguntas = new List<PreguntasViewModel>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Preguntas").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listPreguntas = JsonConvert.DeserializeObject<List<PreguntasViewModel>>(data);
            }
            return View(listPreguntas);
        }
        public IActionResult Details(int id)
        {
            try
            {
                ViewData["LenguajesProgramacion"] = getLenguajesProgramacion();
                ViewData["NivelesConocimiento"] = getNivelesConocimiento();
                Preguntas itemPreguntas = new Preguntas();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/Preguntas/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    itemPreguntas = JsonConvert.DeserializeObject<Preguntas>(data);
                }
                return View(itemPreguntas);
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
            ViewData["LenguajesProgramacion"] = getLenguajesProgramacion();
            ViewData["NivelesConocimiento"] = getNivelesConocimiento();
            return View();
        }


        [HttpPost]
        public IActionResult Create(Preguntas model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(baseAddress + "/Preguntas", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Pregunta creada.";
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
        public IActionResult Edit(int id)
        {
            ViewData["LenguajesProgramacion"] = getLenguajesProgramacion();
            ViewData["NivelesConocimiento"] = getNivelesConocimiento();
            Preguntas itemPreguntas = new Preguntas();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Preguntas/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                itemPreguntas = JsonConvert.DeserializeObject<Preguntas>(data);
            }
            return View(itemPreguntas);
        }


        [HttpPost]
        public IActionResult Edit(int id, Preguntas model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(baseAddress + "/Preguntas/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Pregunta modificada.";
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
                ViewData["LenguajesProgramacion"] = getLenguajesProgramacion();
                ViewData["NivelesConocimiento"] = getNivelesConocimiento();                
                Preguntas itemPreguntas = new Preguntas();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/Preguntas/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    itemPreguntas = JsonConvert.DeserializeObject<Preguntas>(data);
                }
                return View(itemPreguntas);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public IActionResult DeleteConfirm(Preguntas model)
        {
            try
            {
                int id = model.id_pregunta;
                HttpResponseMessage response = _client.DeleteAsync(baseAddress + "/Preguntas/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Pregunta eliminada.";
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
    }
}
