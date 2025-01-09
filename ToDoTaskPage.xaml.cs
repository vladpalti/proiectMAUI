using Plugin.LocalNotification;
using proiectMAUI.Models;
using System;

namespace proiectMAUI
{
    public partial class ToDoTaskPage : ContentPage
    {
        private ToDoList todoList;
        private ToDoTask todoTask;

        public ToDoTaskPage(ToDoList tlist)
        {
            InitializeComponent();
            todoList = tlist;
            BindingContext = new ToDoTask();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var todotask = (ToDoTask)BindingContext;
            todotask.Deadline = deadlinePicker.Date.Add(timePicker.Time);

            await App.Database.SaveToDoTaskAsync(todotask);

            var notification1 = new NotificationRequest
            {
                NotificationId = todotask.ID,
                Title = "Task Reminder",
                Description = $"Task '{todotask.Description}' is due tomorrow!",
                ReturningData = $"taskId={todotask.ID}",
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = todotask.Deadline.AddDays(-1)
                }                
            };

            var notification2 = new NotificationRequest
            {
                NotificationId = todotask.ID + 1,
                Title = "Task Reminder",
                Description = $"Task '{todotask.Description}' is due in 4 hours!",
                ReturningData = $"taskId={todotask.ID}",
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = todotask.Deadline.AddHours(-4) 
                }
            };

            LocalNotificationCenter.Current.Show(notification1);
            LocalNotificationCenter.Current.Show(notification2);

            var listToDoTask = new ListToDoTask
            {
                ToDoListID = todoList.ID,
                ToDoTaskID = todotask.ID
            };

            await App.Database.SaveListToDoTaskAsync(listToDoTask);
            listView.ItemsSource = await App.Database.GetListToDoTasksAsync(todoList.ID);
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var todotask = (ToDoTask)BindingContext;
            await App.Database.DeleteToDoTaskAsync(todotask);

            listView.ItemsSource = await App.Database.GetListToDoTasksAsync(todoList.ID);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetListToDoTasksAsync(todoList.ID);
        }

        async void OnAddButtonClicked(object sender, EventArgs e)
        {
            ToDoTask t;
            if (listView.SelectedItem != null)
            {
                t = listView.SelectedItem as ToDoTask;
                var lt = new ListToDoTask
                {
                    ToDoListID = todoList.ID,
                    ToDoTaskID = t.ID
                };
                await App.Database.SaveListToDoTaskAsync(lt);
                t.ListToDoTask = new List<ListToDoTask> { lt };
                await Navigation.PopAsync();
            }
        }
        async void OnDeleteTaskButtonClicked(object sender, EventArgs e)
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
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
