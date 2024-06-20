namespace proiectMAUI;
using proiectMAUI.Models;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ToDoList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveToDoListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ToDoList)BindingContext;
        await App.Database.DeleteToDoListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ToDoTaskPage((ToDoList)
       this.BindingContext)
        {
            BindingContext = new ToDoTask()
        });
    }
        protected override async void OnAppearing()
    {
        base.OnAppearing();
        var todol = (ToDoList)BindingContext;

        listView.ItemsSource = await App.Database.GetListToDoTasksAsync(todol.ID);
    }

}
