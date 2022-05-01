using Newtonsoft.Json;
using Quiz.MVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.MVVM.ViewModel
{
    internal class QuestionViewModel
    {
        private const string _questionsPath = @"../../../DataBase/questions.json";
        private readonly List<Question> _questions;
        private int _currentQuestionIndex;
        private readonly int _correctAnswer = 0;
        public string CorrectAnswerCounter { get; set; }
        public string QuestionNumber { get; set; }
        public string QuestionBox { get; set; }


        public QuestionViewModel()
        {
            _questions = LoadQuestions().ToList();
            _currentQuestionIndex = 0;

            DisplayCurrentQuestionOnScreen();
        }

        private static IEnumerable<Question> LoadQuestions()
        {
            using (var reader = new StreamReader(_questionsPath, Encoding.Default))
            {
                var json = reader.ReadToEnd();
                var questions = JsonConvert.DeserializeObject<List<Question>>(json);
                return questions;
            }
        }

        private void NextQuestion()
        {
            if (_currentQuestionIndex + 1 < _questions.Count)
            {
                _currentQuestionIndex++;
                DisplayCurrentQuestionOnScreen();
                return;
            }
            //todo Display end screen
        }

        private void DisplayCurrentQuestionOnScreen()
        {

            CorrectAnswerCounter = $"Poprawe odpowiedzi: {_correctAnswer}";
            QuestionNumber = $"Pytanie: {_currentQuestionIndex + 1}/{_questions.Count}";

            var currentQuestion = _questions[_currentQuestionIndex];


            // Set question content

            QuestionBox = currentQuestion.QuestionContent;


            // Set background default background color for all answers buttons

            //AnswerButtonA.ClearValue(BackgroundProperty);
            //AnswerButtonB.ClearValue(BackgroundProperty);
            //AnswerButtonC.ClearValue(BackgroundProperty);
            //AnswerButtonD.ClearValue(BackgroundProperty);


            // Set content for all answers buttons

            //    AnswerButtonA.Content = $"A. {currentQuestion.AnswerA}";
            //    AnswerButtonA.Tag = currentQuestion.AnswerA;

            //    AnswerButtonB.Content = $"B. {currentQuestion.AnswerB}";
            //    AnswerButtonB.Tag = currentQuestion.AnswerB;

            //    AnswerButtonC.Content = $"C. {currentQuestion.AnswerC}";
            //    AnswerButtonC.Tag = currentQuestion.AnswerC;

            //    AnswerButtonD.Content = $"D. {currentQuestion.AnswerD}";
            //    AnswerButtonD.Tag = currentQuestion.AnswerD;
            //}

            //private async void CheckAnswer(object sender, RoutedEventArgs e)
            //{
            //    var correctAnswer = _questions[_currentQuestionIndex].CorrectAnswer;
            //    var button = sender as Button;
            //    var buttonAnswer = button?.Tag.ToString();
            //    if (buttonAnswer == correctAnswer)
            //    {
            //        _correctAnswer++;
            //    }

            //    AnswerButtonA.Background = AnswerButtonA.Tag.ToString() == correctAnswer ? Brushes.Green : Brushes.Red;
            //    AnswerButtonB.Background = AnswerButtonB.Tag.ToString() == correctAnswer ? Brushes.Green : Brushes.Red;
            //    AnswerButtonC.Background = AnswerButtonC.Tag.ToString() == correctAnswer ? Brushes.Green : Brushes.Red;
            //    AnswerButtonD.Background = AnswerButtonD.Tag.ToString() == correctAnswer ? Brushes.Green : Brushes.Red;

            //    await Task.Delay(2000);
            //    NextQuestion();
            //}
        }
    }
}