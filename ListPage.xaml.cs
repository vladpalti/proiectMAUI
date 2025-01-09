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
        var tlist = (ToDoList)BindingContext;
        await App.Database.SaveToDoListAsync(tlist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var tlist = (ToDoList)BindingContext;
        await App.Database.DeleteToDoListAsync(tlist);
        await Navigation.PopAsync();
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ToDoTaskPage((ToDoList)this.BindingContext)
        {
            BindingContext = new ToDoTask() 
        });
    }
    public async void OnDeleteTaskButtonClicked(object sender, EventArgs e)
    {
        var selectedTask = listView.SelectedItem as ToDoTask;
        if (selectedTask != null)
        {
            var tlist = (ToDoList)BindingContext;
            var listToDoTask = await App.Database.GetListToDoTaskAsync(tlist.ID, selectedTask.ID);
            if (listToDoTask != null)
            {
                await App.Database.DeleteListToDoTaskAsync(listToDoTask);
                listView.ItemsSource = await App.Database.GetListToDoTasksAsync(tlist.ID);
            }
        }
        else
        {
            await DisplayAlert("Error", "Please select a task to delete.", "OK");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var todol = (ToDoList)BindingContext;
        var tasks = await App.Database.GetListToDoTasksAsync(todol.ID);

        foreach (var task in tasks)
        {
            task.Deadline = await App.Database.GetDeadlineForTaskAsync(task.ID);
        }

        tasks.Sort((t1, t2) => t1.Deadline.CompareTo(t2.Deadline));

        listView.ItemsSource = tasks;
    }
}
