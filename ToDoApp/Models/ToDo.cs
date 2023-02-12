using System.ComponentModel;

namespace ToDoApp.Models
{
    public class ToDo : INotifyPropertyChanged
    {
        int _id;
        public int Id { get => _id; set { if (_id == value) return; _id = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id))); } }

        string _title;
        public string Title { get => _title; set { if (_title == value) return; _title = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title))); } }

        string _description;
        public string Description { get => _description; set { if (_description == value) return; _description = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description))); } }

        string _icon;
        public string Icon { get => _icon; set { if (_icon == value) return; _icon = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Icon))); } }

        public List<Item> Items { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Item : INotifyPropertyChanged
    {
        int _id;
        public int Id { get => _id; set { if (_id == value) return; _id = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id))); } }

        string _name;
        public string Name { get => _name; set { if (_name == value) return; _name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name))); } }

        bool _checked;
        public bool IsChecked { get => _checked; set { if (_checked == value) return; _checked = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsChecked))); } }




        public event PropertyChangedEventHandler PropertyChanged;
    }
}
