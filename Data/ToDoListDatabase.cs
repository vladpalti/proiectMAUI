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
        public Task<int> SaveToDoListAsync(ToDoList tlist)
        {
            if (tlist.ID != 0)
            {
                return _database.UpdateAsync(tlist);
            }
            else
            {
                return _database.InsertAsync(tlist);
            }
        }
        public Task<int> DeleteToDoListAsync(ToDoList tlist)
        {
            return _database.DeleteAsync(tlist);
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
            return _database.QueryAsync<ToDoTask>("SELECT T.ID, T.Description, T.Deadline FROM ToDoTask T " +
                                         "INNER JOIN ListToDoTask LT ON T.ID = LT.ToDoTaskID " +
                                         "WHERE LT.ToDoListID = ? " +
                                         "ORDER BY T.Deadline ASC", todolistid);
        }
        public async Task<DateTime> GetDeadlineForTaskAsync(int taskId)
        {
            var task = await _database.Table<ToDoTask>().Where(t => t.ID == taskId).FirstOrDefaultAsync();
            return task?.Deadline ?? DateTime.MinValue;
        }
        public Task<ListToDoTask> GetListToDoTaskAsync(int toDoListId, int toDoTaskId)
{
            return _database.Table<ListToDoTask>()
                            .Where(ltt => ltt.ToDoListID == toDoListId && ltt.ToDoTaskID == toDoTaskId)
                            .FirstOrDefaultAsync();
        }
        public Task<int> DeleteListToDoTaskAsync(ListToDoTask listToDoTask)
        {
            return _database.DeleteAsync(listToDoTask);
        }

    }
}
