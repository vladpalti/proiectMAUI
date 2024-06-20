using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace proiectMAUI.Models
{
    public class ListToDoTask
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(ToDoList))]
        public int ToDoListID { get; set; }
        public int ToDoTaskID { get; set; }

    }
}
