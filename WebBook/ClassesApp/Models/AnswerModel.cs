using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBook.ClassesApp.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsTrue { get; set; } = false;

        public int IdQuestion { get; set; } 
    }
}
