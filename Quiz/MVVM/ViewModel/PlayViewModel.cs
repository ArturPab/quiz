using Newtonsoft.Json;
using Quiz.Core;
using Quiz.DataBase;
using Quiz.MVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Quiz.MVVM.ViewModel
{
    internal class PlayViewModel : ObservableObject
    {
        private const string _questionsPath = @"../../../DataBase/questions.json";
        private readonly DatabaseTools _questions;
        private int _correctAnswer = 0;
        private string _correctAnswerCounter;
        private string _questionNumber;
        private string _questionBox;
        private string _content = string.Empty;
        private string _answerA = string.Empty;
        private string _answerB = string.Empty;
        private string _answerC = string.Empty;
        private string _answerD = string.Empty;
        private string _IsCorrectAnswerA = "0";
        private string _IsCorrectAnswerB = "0";
        private string _IsCorrectAnswerC = "0";
        private string _IsCorrectAnswerD = "0";
        private readonly NavigationViewModel _navigationViewModel;
        private readonly DispatcherTimer _timer;

        public string CorrectAnswerCounter
        {
            get { return _correctAnswerCounter; }
            set
            {
                _correctAnswerCounter = value;
                OnPropertyChanged(nameof(CorrectAnswerCounter));
            }
        }
        public string QuestionNumber
        {
            get { return _questionNumber; }
            set
            {
                _questionNumber = value;
                OnPropertyChanged(nameof(QuestionNumber));
            }
        }
        public string QuestionBox
        {
            get { return _questionBox; }
            set
            {
                _questionBox = value;
                OnPropertyChanged(nameof(QuestionBox));
            }
        }

        public int CorrectAnswer
        {
            get { return _correctAnswer; }
            set
            {
                _correctAnswer = value;
                OnPropertyChanged(nameof(CorrectAnswer));
            }
        }

        public string IsCorrectAnswerA
        {
            get
            {
                return _IsCorrectAnswerA;
            }
            set
            {
                _IsCorrectAnswerA = value;
                OnPropertyChanged(nameof(IsCorrectAnswerA));
            }
        }

        public string IsCorrectAnswerB
        {
            get
            {
                return _IsCorrectAnswerB;
            }
            set
            {
                _IsCorrectAnswerB = value;
                OnPropertyChanged(nameof(IsCorrectAnswerB));
            }
        }
        public string IsCorrectAnswerC
        {
            get
            {
                return _IsCorrectAnswerC;
            }
            set
            {
                _IsCorrectAnswerC = value;
                OnPropertyChanged(nameof(IsCorrectAnswerC));
            }
        }
        public string IsCorrectAnswerD
        {
            get
            {
                return _IsCorrectAnswerD;
            }
            set
            {
                _IsCorrectAnswerD = value;
                OnPropertyChanged(nameof(IsCorrectAnswerD));
            }
        }

        public ICommand ItemChangedCommand { get; set; }
        public ICommand CheckAnswerCommand { get; set; }

        public PlayViewModel(DatabaseTools questions, NavigationViewModel navigationViewModel)
        {
            _questions = questions;
            CheckAnswerCommand = new RelayCommand(CheckAnswer);
            _navigationViewModel = navigationViewModel;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            StartedTime = DateTime.Now;
            _timer.Tick += (o, e) => OnPropertyChanged("CurrentTimer");
            _timer.Start();

            DisplayCurrentQuestionOnScreen();
        }

        public DateTime StartedTime { get; set; }

         public string CurrentTimer
        {
            get { return DateTime.Now.Subtract(StartedTime).ToString(@"hh\:mm\:ss"); }
        }

        private void OpenEndScreenView()
        {
            _navigationViewModel.SelectedViewModel = new EndScreenViewModel(_navigationViewModel, CorrectAnswer, CurrentTimer);
        }

        public List<string> QuestionsForView => _questions.Questions.Select(q => q.QuestionContent).ToList();
        public int CurrentIndex { get; set; } = 0;

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        public string AnswerA
        {
            get => _answerA;
            set
            {
                _answerA = value;
                OnPropertyChanged(nameof(AnswerA));
            }
        }

        public string AnswerB
        {
            get => _answerB;
            set
            {
                _answerB = value;
                OnPropertyChanged(nameof(AnswerB));
            }
        }

        public string AnswerC
        {
            get => _answerC;
            set
            {
                _answerC = value;
                OnPropertyChanged(nameof(AnswerC));
            }
        }

        public string AnswerD
        {
            get => _answerD;
            set
            {
                _answerD = value;
                OnPropertyChanged(nameof(AnswerD));
            }
        }

        private void DisplayCurrentQuestionOnScreen()
        {
            var question = _questions.Questions[CurrentIndex];
            Content = question.QuestionContent;
            AnswerA = question.AnswerA;
            AnswerB = question.AnswerB;
            AnswerC = question.AnswerC;
            AnswerD = question.AnswerD;
            CorrectAnswerCounter = $"Poprawe odpowiedzi: {CorrectAnswer}";
            QuestionNumber = $"Pytanie: {CurrentIndex + 1}/{_questions.Questions.Count}";

            var currentQuestion = _questions.Questions[CurrentIndex];


            // Set question content

            QuestionBox = currentQuestion.QuestionContent;
        }

        private async void CheckAnswer(object param)
        {
            string? answer = param as string;
            var correctAnswer = _questions.Questions[CurrentIndex].CorrectAnswer;
            int questionListLength = _questions.Questions.Count;
            if (answer == correctAnswer)
            {
                CorrectAnswer += 1;
                CorrectAnswerCounter = $"Poprawe odpowiedzi: {CorrectAnswer}";
            }

            ChangeColorValue(answer, AnswerA, correctAnswer);
            ChangeColorValue(answer, AnswerB, correctAnswer);
            ChangeColorValue(answer, AnswerC, correctAnswer);
            ChangeColorValue(answer, AnswerD, correctAnswer);

            await Task.Delay(2000);

            if (CurrentIndex+1 == questionListLength)
            {
                _timer.Stop();
                OpenEndScreenView();
            }
            else
            {
                nextQuestion();
            }
        }

        private void nextQuestion()
        {
            CurrentIndex += 1;
            IsCorrectAnswerA = "0";
            IsCorrectAnswerB = "0";
            IsCorrectAnswerC = "0";
            IsCorrectAnswerD = "0";
            DisplayCurrentQuestionOnScreen();
        }

        private void ChangeColorValue(string? selectedAnswer, string answer, string correctAnswer)
        {
            if (answer == selectedAnswer)
            {
                if (AnswerA == answer)
                {
                    IsCorrectAnswerA = correctAnswer == AnswerA ? "1" : "2";
                }
                else if (AnswerB == answer)
                {
                    IsCorrectAnswerB = correctAnswer == AnswerB ? "1" : "2";
                }
                else if (AnswerC == answer)
                {
                    IsCorrectAnswerC = correctAnswer == AnswerC ? "1" : "2";
                }
                else
                {
                    IsCorrectAnswerD = correctAnswer == AnswerD ? "1" : "2";
                }
            }
        }
    }
}