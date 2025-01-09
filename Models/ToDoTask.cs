using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace proiectMAUI.Models
{
    public class ToDoTask
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; } 

        [OneToMany]
        public List<ListToDoTask> ListToDoTask { get; set; }
    }
}
