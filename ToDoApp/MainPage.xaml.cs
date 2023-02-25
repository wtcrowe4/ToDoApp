using Microsoft.Maui.Controls;
using System.Diagnostics;
using ToDoApp.DataServices;
using ToDoApp.Views;
using ToDoApp.Models;
using System.Net.Security;

namespace ToDoApp;

public partial class MainPage : ContentPage
{
	private readonly IRestDataService _dataService;

	public MainPage(IRestDataService dataService)
	{

		InitializeComponent();
	
		_dataService = dataService;
	}

	protected override async void OnAppearing()
	{
        base.OnAppearing();
        collectionView.ItemsSource = await _dataService.GetToDoListsAsync();
	}

	async void OnAddListButtonClicked(object sender, EventArgs e)
	{
		Debug.WriteLine("Add List button clicked");
		var navigationParameter = new Dictionary<string, object>
		{
			{ nameof(ToDo), new ToDo() }
		};
		await Shell.Current.GoToAsync(nameof(NewList), navigationParameter);
    }

	async void OnListSelectionClicked(object sender, SelectionChangedEventArgs e)
	{
		Debug.WriteLine("List selected");
		var navigationParameter = new Dictionary<string, object>
		{
			{ nameof(ToDo), e.CurrentSelection.FirstOrDefault() as ToDo }
		};
		await Shell.Current.GoToAsync(nameof(ViewList), navigationParameter);
		
	}

	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
        Debug.WriteLine("Delete button clicked");
        var toDo = (sender as MenuItem).CommandParameter as ToDo;
        await _dataService.DeleteToDoListAsync(toDo.Id);
    }




	//Get lists from database
	//public async Task GetLists()
	//{
	//       var lists = await App.Database.GetListsAsync();
	//       Lists.ItemsSource = lists;
	//   }

	////Command to add a new list
	//public Command AddListCommand => new(async () =>
	//{
	//       await Navigation.PushAsync(new AddListPage());
	//   });


}

