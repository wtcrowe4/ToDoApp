using System.Diagnostics;
using System.Net.Http;
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
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet access detected.");
                return toDoLists;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/todo");
                if (response.IsSuccessStatusCode)
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
        }

        public async Task<ToDo> AddToDoListAsync(ToDo toDoList)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet access detected.");
                return null;
            }
            try
            {
                string jsonToDo = JsonSerializer.Serialize<ToDo>(toDoList, _jsonSerializerOptions);
                StringContent content = new(jsonToDo, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("{_url}/todo", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Created Todo: {content}");
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
        }
        public async Task DeleteToDoListAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet access detected.");
                return;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/todo/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Deleted Todo: {id}");
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
            return;
        }

        public async Task UpdateToDoListAsync(int id, ToDo toDoList)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet access detected.");
                return;
            }
            try
            {
                string jsonTodo = JsonSerializer.Serialize<ToDo>(toDoList, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonTodo, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/todo/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Successfully updated ToDo {id}");
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
            return;

        }



        //Items
        public async Task<List<Item>> GetItemsAsync(ToDo toDoList, int id)
        {
            List<Item> items = new();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet access detected.");
                return items;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/todo/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    items = JsonSerializer.Deserialize<List<Item>>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
            return items;
            //Returning whole ToDo, might not need this. I can access list of items from the GET ToDo above. 
        }

        public async Task<Item> AddItemAsync(Item item, ToDo toDoList, int id)
        {
            //Add item to a specific ToDo list
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet access detected.");
                return item;
            }
            try
            {
                string jsonToDo = JsonSerializer.Serialize<ToDo>(toDoList, _jsonSerializerOptions);
                StringContent content = new(jsonToDo, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync("{_url}/todo/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Added {item} to {content}");
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
            return item;
        }


        public async Task DeleteItemAsync(Item item, ToDo toDoList, int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet access detected.");
                return;
            }
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/todo/{id}/items/{item.Id}");
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Deleted Item: {item}");
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
            return;
        }
        public async Task UpdateItemAsync(Item item, ToDo toDoList, int id)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No internet access detected.");
                return;
            }
            try
            {
                string jsonTodo = JsonSerializer.Serialize<Item>(item, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonTodo, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/todo/{id}/items/{item.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Successfully updated ToDo {id} with {item}");
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
            return;

        }
    }
}





