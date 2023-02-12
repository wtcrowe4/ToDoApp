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
        public List<Items>? Items { get; set; }
    }

    public class Items
    {
        [Key]
        public int Id { get; set; }
        public string? Item { get; set; }
        public bool? IsChecked { get; set; }
    }
}
