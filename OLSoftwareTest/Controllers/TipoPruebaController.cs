using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OLSoftwareTest.Models.DTO;
using System.Text;
using System.Text.Json.Serialization;

namespace OLSoftwareTest.Controllers
{
    public class TipoPruebaController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7011/api");
        private readonly HttpClient _client;

        public TipoPruebaController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }


        public IActionResult Index()
        {
            List<TiposPruebas> listTiposPruebas = new List<TiposPruebas>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/TiposPruebas").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listTiposPruebas = JsonConvert.DeserializeObject<List<TiposPruebas>>(data);
            }
            return View(listTiposPruebas);
        }

        public IActionResult Details(int id)
        {
            try
            {
                TiposPruebas tiposPruebas = new TiposPruebas();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/TiposPruebas/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    tiposPruebas = JsonConvert.DeserializeObject<TiposPruebas>(data);
                }
                return View(tiposPruebas);
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
        public IActionResult Create(TiposPruebas model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(baseAddress + "/TiposPruebas", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Tipo de prueba creado.";
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
            TiposPruebas tiposPruebas = new TiposPruebas();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/TiposPruebas/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                tiposPruebas = JsonConvert.DeserializeObject<TiposPruebas>(data);
            }
            return View(tiposPruebas);
        }


        [HttpPost]
        public IActionResult Edit(int id, TiposPruebas model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(baseAddress + "/TiposPruebas/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Tipo de prueba modificado.";
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
                TiposPruebas tiposPruebas = new TiposPruebas();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/TiposPruebas/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    tiposPruebas = JsonConvert.DeserializeObject<TiposPruebas>(data);
                }
                return View(tiposPruebas);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }            
        }

       
        [HttpPost]
        public IActionResult DeleteConfirm(TiposPruebas model)
        {
            try
            {
                int id = model.id_tipo_prueba;
                HttpResponseMessage response = _client.DeleteAsync(baseAddress + "/TiposPruebas/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Tipo de prueba eliminado.";
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
