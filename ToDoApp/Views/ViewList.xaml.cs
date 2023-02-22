using Microsoft.Maui.Controls;
using System.Diagnostics;
using ToDoApp.DataServices;
using ToDoApp.Models;

namespace ToDoApp.Views;

public partial class ViewList : ContentPage
{
    private readonly IRestDataService _dataService;
    //get id of todo list
    
    

    public ViewList(IRestDataService dataService)
    {
        _dataService = dataService;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //get list from database using id
        var list = await _dataService.GetToDoListAsync();
    }

    async void OnAddItemButtonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("Add Item button clicked");
        //add item to database
        await _dataService.AddItemAsync();


    }

    async void OnItemSelectionClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("Item selected");
        //toggle item checked status
        await _dataService.UpdateItemAsync();

        
    }

    //Get items from database
    //public async Task GetItems()
    //{
    //       var items = await App.Database.GetItemsAsync();
    //       Items.ItemsSource = items;
    //    
    // 
    //}    

}