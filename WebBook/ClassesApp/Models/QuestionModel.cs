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
        public string Title { get; set; }
        public List<AsnswerModel> asnswerModels { get; set; } = new List<AsnswerModel>();


        [JsonIgnore]

        public List<AsnswerModel> Vibran { get; set; } = new List<AsnswerModel>();

        public void AddA(AsnswerModel asnswerModel)
        {
            asnswerModels.Add(asnswerModel);
        }

        public void DelA(AsnswerModel asnswerModel)
        {
            asnswerModels.Remove(asnswerModel);
        }
    }
}
