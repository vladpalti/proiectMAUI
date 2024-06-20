using System;
using proiectMAUI.Data;
using System.IO;

namespace proiectMAUI;

public partial class App : Application
{
    static ToDoListDatabase database;
    public static ToDoListDatabase Database
    {
        get
        {
            if (database == null)
            {
                database = new
               ToDoListDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
               LocalApplicationData), "ToDoList.db3"));
            }
            return database;
        }
    }
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
