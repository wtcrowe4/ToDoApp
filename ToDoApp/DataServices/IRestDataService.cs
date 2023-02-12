using ToDoApp.Models;

namespace ToDoApp.DataServices
{
    public interface IRestDataService
    {
        //Lists Services
        Task<List<ToDo>> GetToDoLists();
        Task<ToDo> GetToDoList(int Id);
        Task<ToDo> AddToDoList(ToDo toDoList);
        Task<ToDo> UpdateToDoList(ToDo toDoList);
        Task DeleteToDoList(int id);

        //Items Services
        Task<List<Item>> GetItems(ToDo toDoList, int Id);
        Task<List<Item>> AddItem(Item item, ToDo toDoList, int id);
        Task<List<Item>> UpdateItem(Item item, ToDo toDoList, int id);
        Task<List<Item>> DeleteItem(Item item, ToDo toDoList, int id);
    }
}
