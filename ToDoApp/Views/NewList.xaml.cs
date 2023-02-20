using ToDoApp.DataServices;
using ToDoApp.Models;


namespace ToDoApp.Views;

public partial class NewList : ContentPage
{
    private readonly IRestDataService _restDataService;
    public NewList()
    {
        InitializeComponent();
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        var list = new ToDo
        {
           


           
        };
        await _restDataService.AddToDoListAsync(list);
        //await _restDataService.AddItemAsync(list.Items);
        await Navigation.PopAsync();

        

        await Navigation.PopAsync();
    }
}