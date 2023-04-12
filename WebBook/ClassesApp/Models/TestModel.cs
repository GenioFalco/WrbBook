using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBook.EntityFramework;

namespace WebBook.ClassesApp.Models
{
    public class TestModel 
    {
        public List<QuestionModel> QuestionModel { get; set; } = new List<QuestionModel>();

        public Test Test { get; set; } = new Test();


        public TestModel(Test test)
        {
            Test = test;
            GetJS();
        }

        public string JsStr() 
        {
            string vizov = JSONConverter.ConvertJSTo(QuestionModel);
            return vizov;
        }

        private void GetJS() 
        {
            if(Test.JsonFile == null) 
            {
                QuestionModel = new List<QuestionModel>();
                return;
            }
            QuestionModel = JSONConverter.ConvertJSOut<List<QuestionModel>>(Test.JsonFile);

        }

        public void SaveJs()
        {
           Test.JsonFile = JsStr();
        }

        public void AddQ(QuestionModel questionModel) 
        { 
            QuestionModel.Add(questionModel);   
        }

        public void DelQ(QuestionModel questionModel)
        {
            QuestionModel.Remove(questionModel);
        }

    }
}
