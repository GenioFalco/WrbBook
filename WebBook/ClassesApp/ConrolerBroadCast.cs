using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBook.ClassesApp.Models;
using WebBook.EntityFramework;
using WebBook.PageWindow;

namespace WebBook.ClassesApp
{
    public class ConrolerBroadCast
    {
        public static Topic topic = new Topic();

        public static EntityFramework.Task task = new EntityFramework.Task();

        public static bool CheckAdd = false;

        public static bool CheckTest = false;

        public static Test test = new Test();

        public static List<QuestionModel> questionModel = new List<QuestionModel>();

        public static List<AnswerModel> answerModels = new List<AnswerModel>();

        public static int Number { get; set; }
    }
}
