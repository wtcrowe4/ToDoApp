using System.Diagnostics;
using ToDoApp.DataServices;
using ToDoApp.Models;

namespace ToDoApp.Views;

[QueryProperty(nameof(ToDo), nameof(ToDo))]
public partial class NewList : ContentPage
{
    private readonly IRestDataService _dataService;
    ToDo _toDo;
    bool _isNewItem;

    public ToDo ToDo
    {
        get => _toDo;
        set
        {
            _isNewItem = IsNewItem(value);
            _toDo= value;
            OnPropertyChanged();
        }
        
    }

    public NewList(IRestDataService dataService)
    {
        InitializeComponent();
        _dataService = dataService;
        BindingContext = this;
    }

    private async void OnCreateButtonClicked(object sender, EventArgs e)
    {
        if (_isNewItem)
        {
            Debug.WriteLine("Create new Todo clicked");
            await _dataService.AddToDoListAsync(ToDo);

        }
        else
        {
            Debug.WriteLine("Todo already exists");
            var navigationParameter = new Dictionary<string, object>
            {
                { nameof(ToDo), ToDo }
            };
            await Shell.Current.GoToAsync(nameof(ViewList), navigationParameter);
        }
    }

    async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    bool IsNewItem(ToDo toDo)
    {
        if (toDo.Id == 0) return true; return false;
    }
}   