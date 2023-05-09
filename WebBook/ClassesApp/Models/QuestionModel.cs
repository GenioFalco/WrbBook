using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebBook.ClassesApp.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int IdtTest { get; set; }    
    }
}
