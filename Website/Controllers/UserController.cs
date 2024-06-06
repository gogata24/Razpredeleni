using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Website.ViewModel;
using System.Text.Json;
using Website.HelpTool;

namespace Website.Controllers
{
    public class UserController : Controller
    {
        Uri uri = new("https://localhost:44338/api");
        private readonly HttpClient _client;
        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = uri;
        }
        [HttpGet]
        public IActionResult Index(string searchEmail)
        {
            List<UserViewModel> usersList = new List<UserViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Users/GetAllUsers").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                usersList = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
            }
            var userByEmail = usersList;
            if(!string.IsNullOrEmpty(searchEmail))
            {
                userByEmail = userByEmail.Where(x => x.Email.ToLower().Contains(searchEmail.ToLower())).ToList();
                return View(userByEmail);
            }
            return View(usersList);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserViewModel model)
        {
            
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Users/AddUser", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }
            return View();

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            UserViewModel user = new UserViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Users/GetAllUsers" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<UserViewModel>(data);
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(UserViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Users/UpdateUser", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Car");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            UserViewModel user = new UserViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Users/GetAllUsers" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<UserViewModel>(data);
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult Delete(UserViewModel model)
        {
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Users/DeleteUser/id?id=" + model.Id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel user)
        {
            List<UserViewModel> usersList = new List<UserViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Users/GetAllUsers").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                usersList = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
            }
            for(int i = 0;i<usersList.Count;i++)
            {
                if(user.Username == usersList[i].Username && user.Password == usersList[i].Password)
                {
                    Logged.Id = usersList[i].Id;
                    Logged.Username = usersList[i].Username;
                    Logged.Password = usersList[i].Password;
                    return RedirectToAction("Index","Car");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            Logged.Id = 0;
            Logged.Username = null;
            Logged.Password = null;
            return RedirectToAction("Login", "User");

        }



    }
}
