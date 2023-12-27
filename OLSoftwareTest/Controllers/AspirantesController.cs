using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OLSoftwareTest.Models;
using OLSoftwareTest.Models.Domain;
using OLSoftwareTest.Models.DTO;
using System.Collections;
using System.Globalization;
using System.Security.Claims;
using System.Text;

namespace OLSoftwareTest.Controllers
{
    [Authorize]
    public class AspirantesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7011/api");
        private readonly HttpClient _client;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;


        public AspirantesController(UserManager<ApplicationUser> userManager, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Generate(int id)
        {
            try
            {
                string contenidoDetalle = "";
                JObject jso = new JObject();
                List<DetalleViewModel> listDetalles = new List<DetalleViewModel>();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/Detalle/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;

                    jso = JObject.Parse(data);
                    contenidoDetalle = jso.ToString();
                }

                var fechaFormat = DateTime.Now.ToString().Replace('/', '-').Replace(' ', '_').Replace('.', '_').Replace(':', '_').ToString();
                var fileName = "Detalle_" + fechaFormat + jso["detalle"]["documento"].ToString()+ ".txt";  
                string directoryPath = this._hostingEnvironment.WebRootPath + "\\Archivos\\";
               
                if (Directory.Exists(directoryPath) == false)
                {
                    Directory.CreateDirectory(directoryPath);
                }
                
                string filepath = directoryPath + fileName;
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    writer.WriteLine(contenidoDetalle);
                }

                TempData["successMessage"] = "Archivo creado.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                throw;
            }
            
        }

        public IActionResult Index()
        {
            try
            {
                ViewBag.Usuarios = new SelectList(_userManager.Users.ToList(), "Id", "UserName");

                List<AspirantesViewModel> listAspirantes = new List<AspirantesViewModel>();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/Aspirantes").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    listAspirantes = JsonConvert.DeserializeObject<List<AspirantesViewModel>>(data);
                }
                return View(listAspirantes);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            try
            {
                AspirantesViewModel itemAspirantes = new AspirantesViewModel();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/Aspirantes/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    itemAspirantes = JsonConvert.DeserializeObject<AspirantesViewModel>(data);
                }
                ViewBag.UsuarioLogin = _userManager.Users.Where(x => x.Id.Equals(itemAspirantes.id_login)).FirstOrDefault().UserName;
                return View(itemAspirantes);
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
            ViewBag.Usuarios = new SelectList(_userManager.Users.ToList(), "Id", "UserName");
            List<PruebasViewModel> listPruebas = new List<PruebasViewModel>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Pruebas").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listPruebas = JsonConvert.DeserializeObject<List<PruebasViewModel>>(data);
            }

            ViewBag.Pruebas = new SelectList(listPruebas, "id_prueba", "nombre_prueba");
            return View();
        }


        [HttpPost]
        public IActionResult Create(Aspirantes model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(baseAddress + "/Aspirantes", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Aspirante creado.";
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

        public List<PruebasViewModel> GetPruebas()
        {
            List<PruebasViewModel> listPruebas = new List<PruebasViewModel>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Pruebas").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listPruebas = JsonConvert.DeserializeObject<List<PruebasViewModel>>(data);
            }
            return listPruebas;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            AspirantesViewModel itemPruebas = new AspirantesViewModel();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Aspirantes/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                itemPruebas = JsonConvert.DeserializeObject<AspirantesViewModel>(data);
            }

            var lista = new SelectList(_userManager.Users.ToList(), "Id", "UserName");
            SelectListItem selected = lista.FirstOrDefault(x => x.Value.ToUpper().Contains(itemPruebas.id_login.ToString()));
            if (selected != null)
            {
                lista = new SelectList(lista.ToList(), "value", "text", selected.Value);
            }
            ViewBag.Usuarios = lista;
            ViewBag.Pruebas = GetPruebas();

            return View(itemPruebas);
        }


        [HttpPost]
        public IActionResult Edit(int id, AspirantesViewModel model)
        {
            try
            {
                model.id_prueba_aspirante = model.id_prueba_aspirante == null || model.id_prueba_aspirante == 0 ? id : model.id_prueba_aspirante;

                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(baseAddress + "/Aspirantes/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Aspirante modificado.";
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
                AspirantesViewModel itemAspirantes = new AspirantesViewModel();
                HttpResponseMessage response = _client.GetAsync(baseAddress + "/Aspirantes/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    itemAspirantes = JsonConvert.DeserializeObject<AspirantesViewModel>(data);
                }
                ViewBag.UsuarioLogin = _userManager.Users.Where(x => x.Id.Equals(itemAspirantes.id_login)).FirstOrDefault().UserName;
                return View(itemAspirantes);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public IActionResult DeleteConfirm(AspirantesViewModel model)
        {
            try
            {
                int id = model.id_prueba_aspirante;
                HttpResponseMessage response = _client.DeleteAsync(baseAddress + "/Aspirantes/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Aspirante eliminado.";
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
