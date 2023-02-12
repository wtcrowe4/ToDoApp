using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Icon { get => _icon; set { if (_icon== value) return; _icon= value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Icon))); } }

        public List<Items> Items { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Items : INotifyPropertyChanged
    {
        int _id;
        public int Id { get => _id; set { if (_id == value) return; _id = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id))); } }
        
        string _item;
        public string Item { get => _item; set { if (_item == value) return; _item= value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Item))); } }

        bool _checked;
        public bool IsChecked { get => _checked; set { if (_checked == value) return; _checked = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsChecked))); } }
        public Items(string item, bool isChecked)
        {
            Item = item;
            IsChecked = isChecked;
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
