using BSCars2.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Website.HelpTool;
using Website.ViewModel;

namespace Website.Controllers
{
    public class CarController : Controller
    {
        Uri uri = new("https://localhost:44338/api");
        private readonly HttpClient _client;
        public CarController()
        {
            _client = new HttpClient();
            _client.BaseAddress = uri;
        }

        private UserViewModel SearchUser(int id)
        {
            List<UserViewModel> usersList = new List<UserViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Users/GetAllUsers").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                usersList = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
            }
            return usersList.FirstOrDefault(x => x.Id == id);
        }

        [HttpGet]
        public IActionResult Index(string searchBrand)
        {
            List<CarViewModel> carsList = new List<CarViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Cars/GetAllCars").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                carsList = JsonConvert.DeserializeObject<List<CarViewModel>>(data);
            }
            var carsByBrand = carsList;
            if(!string.IsNullOrEmpty(searchBrand))
            {
                carsByBrand = carsByBrand.Where(x => x.Brand.ToLower().Contains(searchBrand.ToLower())).ToList();
                return View(carsByBrand);
            }
            return View(carsList);
        }

        public IActionResult Details(int id)
        {
            CarViewModel car = new CarViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Cars/GetCarDetails?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string st = response.Content.ReadAsStringAsync().Result;
                car = JsonConvert.DeserializeObject<CarViewModel>(st);
            }

            UserViewModel user = SearchUser((int)car.UserId);
            car.UserPhone = user.Phone;
            car.UserName = user.Username;
            return View(car);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarViewModel car)
        {
            car.UserId = Logged.Id;
            string data = JsonConvert.SerializeObject(car);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Cars/AddCar", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            CarViewModel user = new CarViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Cars/GetAllCars" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<CarViewModel>(data);
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(CarViewModel car)
        {
            string data = JsonConvert.SerializeObject(car);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Cars/UpdateCars", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            CarViewModel user = new CarViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Cars/GetAllCars" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<CarViewModel>(data);
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult Delete(CarViewModel car)
        {
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Cars/DeleteCar/?id=" + car.Id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        
    }

}
