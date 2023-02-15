using ToDoApp.Models;

namespace ToDoApp.DataServices
{
    public interface IRestDataService
    {
        //Lists Services
        Task<List<ToDo>> GetToDoListsAsync();
        Task<ToDo> GetToDoListAsync(int Id);
        Task<ToDo> AddToDoListAsync(ToDo toDoList);
        Task UpdateToDoListAsync(int Id, ToDo toDoList);
        Task DeleteToDoListAsync(int id);

        //Items Services
        Task<List<Item>> GetItemsAsync(ToDo toDoList, int Id);
        Task<Item> AddItemAsync(Item item, ToDo toDoList, int id);
        Task UpdateItemAsync(Item item, ToDo toDoList, int id);
        Task DeleteItemAsync(Item item, ToDo toDoList, int id);
    }
}
