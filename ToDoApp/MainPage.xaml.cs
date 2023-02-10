namespace ToDoApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	//Filler data for lists array
	public List<string> Data { get; set; }
	= new List<string> { "List 1", "List 2", "List 3" };
	




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

