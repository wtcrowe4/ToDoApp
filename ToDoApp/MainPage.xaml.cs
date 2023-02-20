using System.Diagnostics;
using ToDoApp.DataServices;
using ToDoApp.Views;

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
        await Navigation.PushAsync(new NewList());
    }

	async void OnListSelectionClicked(object sender, EventArgs e)
	{
		Debug.WriteLine("List selected");
        await Navigation.PushAsync(new ViewList());
		
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

