using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLSoftwareTest.Models.DTO;
using System.Text;
using System.Text.Json.Serialization;

namespace OLSoftwareTest.Controllers
{
    public class LenguajesProgramacionController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7011/api");
        private readonly HttpClient _client;

        public LenguajesProgramacionController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }


        public IActionResult Index()
        {
            List<LenguajesProgramacion> listLenguajesProgramacion = new List<LenguajesProgramacion>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/LenguajesProgramacion").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listLenguajesProgramacion = JsonConvert.DeserializeObject<List<LenguajesProgramacion>>(data);
            }
            return View(listLenguajesProgramacion);
        }

        public IActionResult Details(int id)
        {
            try
            {
                LenguajesProgramacion lenguajesProgramacion = new LenguajesProgramacion();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/LenguajesProgramacion/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    lenguajesProgramacion = JsonConvert.DeserializeObject<LenguajesProgramacion>(data);
                }
                return View(lenguajesProgramacion);
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
            return View();
        }


        [HttpPost]
        public IActionResult Create(LenguajesProgramacion model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(baseAddress + "/LenguajesProgramacion", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Lenguaje de Programacion creado.";
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
            LenguajesProgramacion lenguajesProgramacion = new LenguajesProgramacion();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/LenguajesProgramacion/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                lenguajesProgramacion = JsonConvert.DeserializeObject<LenguajesProgramacion>(data);
            }
            return View(lenguajesProgramacion);
        }


        [HttpPost]
        public IActionResult Edit(int id, LenguajesProgramacion model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(baseAddress + "/LenguajesProgramacion/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Lenguaje de Programacion modificado.";
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
                LenguajesProgramacion lenguajesProgramacion = new LenguajesProgramacion();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/LenguajesProgramacion/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    lenguajesProgramacion = JsonConvert.DeserializeObject<LenguajesProgramacion>(data);
                }
                return View(lenguajesProgramacion);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public IActionResult DeleteConfirm(LenguajesProgramacion model)
        {
            try
            {
                int id = model.id_lenguaje;
                HttpResponseMessage response = _client.DeleteAsync(baseAddress + "/LenguajesProgramacion/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Lenguaje de Programacion eliminado.";
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
    }
}
