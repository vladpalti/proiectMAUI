using proiectMAUI.Models;

namespace proiectMAUI;

public partial class ToDoTaskPage : ContentPage
{
    ToDoList tl;
    public ToDoTaskPage(ToDoList tlist)
    {
        InitializeComponent();
        tl = tlist;

    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var todotask = (ToDoTask)BindingContext;
        await App.Database.SaveToDoTaskAsync(todotask);
        listView.ItemsSource = await App.Database.GetToDoTasksAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var todotask = (ToDoTask)BindingContext;
        await App.Database.DeleteToDoTaskAsync(todotask);
        listView.ItemsSource = await App.Database.GetToDoTasksAsync();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetToDoTasksAsync();
    }
    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        ToDoTask t;
        if (listView.SelectedItem != null)
        {
            t = listView.SelectedItem as ToDoTask;
            var lt = new ListToDoTask()
            {
                ToDoListID = tl.ID,
                ToDoTaskID = t.ID
            };
            await App.Database.SaveListToDoTaskAsync(lt);
            t.ListToDoTask = new List<ListToDoTask> { lt };
            await Navigation.PopAsync();
        }
    }
}