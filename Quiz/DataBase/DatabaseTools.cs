using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Newtonsoft.Json;
using Quiz.MVVM.Model;

namespace Quiz.DataBase
{
    public class DatabaseTools
    {
        private static readonly string _questionsPath = @"..\..\..\DataBase\questions.json";
        public List<Question> Questions { get; set; } = LoadQuestions();
        private static List<Question> LoadQuestions()
        {
            using var reader = new StreamReader(_questionsPath);
            var json = reader.ReadToEnd();
            var questions = JsonConvert.DeserializeObject<List<Question>>(json);

            return questions;
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(Questions);
            File.WriteAllText(_questionsPath, json);
        }
    }
}
