using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using proiectMAUI.Models;
namespace proiectMAUI.Data
{
    public class ToDoListDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public ToDoListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ToDoList>().Wait();
            _database.CreateTableAsync<ToDoTask>().Wait();
            _database.CreateTableAsync<ListToDoTask>().Wait();

        }
        public Task<int> SaveToDoTaskAsync(ToDoTask todotask)
        {
            if (todotask.ID != 0)
            {
                return _database.UpdateAsync(todotask);
            }
            else
            {
                return _database.InsertAsync(todotask);
            }
        }
        public Task<int> DeleteToDoTaskAsync(ToDoTask todotask)
        {
            return _database.DeleteAsync(todotask);
        }
        public Task<List<ToDoTask>> GetToDoTasksAsync()
        {
            return _database.Table<ToDoTask>().ToListAsync();
        }
        public Task<List<ToDoList>> GetToDoListsAsync()
        {
            return _database.Table<ToDoList>().ToListAsync();
        }
        public Task<ToDoList> GetToDoListAsync(int id)
        {
            return _database.Table<ToDoList>()
            .Where(i => i.ID == id)
           .FirstOrDefaultAsync();
        }
        public Task<int> SaveToDoListAsync(ToDoList slist)
        {
            if (slist.ID != 0)
            {
                return _database.UpdateAsync(slist);
            }
            else
            {
                return _database.InsertAsync(slist);
            }
        }
        public Task<int> DeleteToDoListAsync(ToDoList slist)
        {
            return _database.DeleteAsync(slist);
        }
        public Task<int> SaveListToDoTaskAsync(ListToDoTask listt)
        {
            if (listt.ID != 0)
            {
                return _database.UpdateAsync(listt);
            }
            else
            {
                return _database.InsertAsync(listt);
            }
        }
        public Task<List<ToDoTask>> GetListToDoTasksAsync(int todolistid)
        {
            return _database.QueryAsync<ToDoTask>("select T.ID, T.Description from ToDoTask T" + " inner join ListToDoTask LT" + " on T.ID = LT.ToDoTaskID where LT.ToDoListID = ?", todolistid);
        }
    }
}
