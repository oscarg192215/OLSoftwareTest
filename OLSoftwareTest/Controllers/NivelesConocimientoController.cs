using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLSoftwareTest.Models.DTO;
using System.Text;

namespace OLSoftwareTest.Controllers
{
    public class NivelesConocimientoController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7011/api");
        private readonly HttpClient _client;

        public NivelesConocimientoController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }


        public IActionResult Index()
        {
            List<NivelesConocimiento> listNivelesConocimiento = new List<NivelesConocimiento>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/NivelesConocimiento").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listNivelesConocimiento = JsonConvert.DeserializeObject<List<NivelesConocimiento>>(data);
            }
            return View(listNivelesConocimiento);
        }

        public IActionResult Details(int id)
        {
            try
            {
                NivelesConocimiento nivelesConocimiento = new NivelesConocimiento();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/NivelesConocimiento/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    nivelesConocimiento = JsonConvert.DeserializeObject<NivelesConocimiento>(data);
                }
                return View(nivelesConocimiento);
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
        public IActionResult Create(NivelesConocimiento model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(baseAddress + "/NivelesConocimiento", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Nivel de conocimiento creado.";
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
            NivelesConocimiento nivelesConocimiento = new NivelesConocimiento();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/NivelesConocimiento/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                nivelesConocimiento = JsonConvert.DeserializeObject<NivelesConocimiento>(data);
            }
            return View(nivelesConocimiento);
        }


        [HttpPost]
        public IActionResult Edit(int id, NivelesConocimiento model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(baseAddress + "/NivelesConocimiento/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Nivel de conocimiento modificado.";
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
                NivelesConocimiento nivelesConocimiento = new NivelesConocimiento();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/NivelesConocimiento/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    nivelesConocimiento = JsonConvert.DeserializeObject<NivelesConocimiento>(data);
                }
                return View(nivelesConocimiento);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public IActionResult DeleteConfirm(NivelesConocimiento model)
        {
            try
            {
                int id = model.id_nivel;
                HttpResponseMessage response = _client.DeleteAsync(baseAddress + "/NivelesConocimiento/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Nivel de conocimiento eliminado.";
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
