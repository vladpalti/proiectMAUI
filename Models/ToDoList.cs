using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace proiectMAUI.Models
{
    public class ToDoList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(250), Unique]
        public string Description { get; set; }
    }

}