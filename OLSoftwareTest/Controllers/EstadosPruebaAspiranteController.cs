using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLSoftwareTest.Models.DTO;
using System.Text;

namespace OLSoftwareTest.Controllers
{
    public class EstadosPruebaAspiranteController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7011/api");
        private readonly HttpClient _client;

        public EstadosPruebaAspiranteController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }


        public IActionResult Index()
        {
            List<EstadosPruebaAspirante> listEstadosPruebaAspirante = new List<EstadosPruebaAspirante>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/EstadosPruebaAspirante").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listEstadosPruebaAspirante = JsonConvert.DeserializeObject<List<EstadosPruebaAspirante>>(data);
            }
            return View(listEstadosPruebaAspirante);
        }

        public IActionResult Details(int id)
        {
            try
            {
                EstadosPruebaAspirante itemEstadosPruebaAspirante = new EstadosPruebaAspirante();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/EstadosPruebaAspirante/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    itemEstadosPruebaAspirante = JsonConvert.DeserializeObject<EstadosPruebaAspirante>(data);
                }
                return View(itemEstadosPruebaAspirante);
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
        public IActionResult Create(EstadosPruebaAspirante model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(baseAddress + "/EstadosPruebaAspirante", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Estado de Prueba creado.";
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
            EstadosPruebaAspirante itemEstadosPruebaAspirante = new EstadosPruebaAspirante();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/EstadosPruebaAspirante/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                itemEstadosPruebaAspirante = JsonConvert.DeserializeObject<EstadosPruebaAspirante>(data);
            }
            return View(itemEstadosPruebaAspirante);
        }


        [HttpPost]
        public IActionResult Edit(int id, EstadosPruebaAspirante model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(baseAddress + "/EstadosPruebaAspirante/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Estado de prueba modificado.";
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
                EstadosPruebaAspirante itemEstadosPruebaAspirante = new EstadosPruebaAspirante();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/EstadosPruebaAspirante/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    itemEstadosPruebaAspirante = JsonConvert.DeserializeObject<EstadosPruebaAspirante>(data);
                }
                return View(itemEstadosPruebaAspirante);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public IActionResult DeleteConfirm(EstadosPruebaAspirante model)
        {
            try
            {
                int id = model.id_estado_prueba_aspirante;
                HttpResponseMessage response = _client.DeleteAsync(baseAddress + "/EstadosPruebaAspirante/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Estados de prueba eliminado.";
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
