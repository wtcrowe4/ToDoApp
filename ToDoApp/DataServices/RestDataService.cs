using AVFoundation;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using ToDoApp.Models;

namespace ToDoApp.DataServices
{
    internal class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataService()
        {
            _httpClient = new HttpClient();
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5288" : "https://localhost:7155";
            _url = $"{_baseAddress}/api";
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };



        }

        //Lists
        public async Task<List<ToDo>> GetToDoListsAsync()
        {
            List<ToDo> toDoLists = new();
            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet access detected.");
                return toDoLists;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/todo");
                if(response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    toDoLists = JsonSerializer.Deserialize<List<ToDo>>(content, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("Http response not 2xx");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
            return toDoLists;
            //throw new NotImplementedException();
        }
        
        public async Task<ToDo> GetToDoListAsync(int id)
        {
            ToDo toDoList = new();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet access detected.");
                return toDoList;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/todo/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    toDoList = JsonSerializer.Deserialize<ToDo>(content, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("Http response not 2xx");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }

            return toDoList;
        
            //throw new NotImplementedException();
        }

        public async Task<ToDo> AddToDoListAsync(ToDo toDoList)
        {
            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet access detected.");
                return null;
            }
            try
            {  
                string jsonToDo = JsonSerializer.Serialize<ToDo>(toDoList, _jsonSerializerOptions);
                StringContent content= new(jsonToDo, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("{_url}/todo", content);
                
                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Created Todo: {content}");
                }
                else
                {
                    Debug.WriteLine("Http response not 2xx");
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }

            return toDoList;
        }
        public Task DeleteToDoList(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ToDo> UpdateToDoList(int id, ToDo toDoList)
        {
            throw new NotImplementedException();
        }

        //Items
        public Task<List<Item>> GetItems(ToDo toDoList, int Id)
        {
            throw new NotImplementedException();
        }
        public Task<List<Item>> AddItem(Item item, ToDo toDoList, int id)
        {
            throw new NotImplementedException();
        }
        public Task<List<Item>> DeleteItem(Item item, ToDo toDoList, int id)
        {
            throw new NotImplementedException();
        }
        public Task<List<Item>> UpdateItem(Item item, ToDo toDoList, int id)
        {
            throw new NotImplementedException();
        }


    }


}
