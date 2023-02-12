using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class ToDoList
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public List<Item>? Items { get; set; }

        //Add a method to add items to the list
        //public AddItems(Items)
        //{
        //    Items = new List<Items>();
        //    Items.AddRange(Items);
        //    return ;
        //}
    }

    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsChecked { get; set; }
    }
}
