using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Quiz.Core;
using Quiz.DataBase;

namespace Quiz.MVVM.ViewModel
{
    internal class SettingsViewModel : ObservableObject
    {
        private readonly DatabaseTools _questionTools;
        private string _content = string.Empty;
        private string _answerA = string.Empty;
        private string _answerB = string.Empty;
        private string _answerC = string.Empty;
        private string _answerD = string.Empty;
        public ICommand ItemChangedCommand { get; set; }

        public SettingsViewModel(DatabaseTools questionTools)
        {
            _questionTools = questionTools;
            ItemChangedCommand = new RelayCommand(ShowSelectedQuestion);
        }

        public List<string> QuestionsForView => _questionTools.Questions.Select(q => q.QuestionContent).ToList();
        public int CurrentIndex { get; set; } = -1;

        public string Content
        {
            get => _content;
            set
            {
                _content=value;
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

        private void ShowSelectedQuestion(object obj)
        {
            var question = _questionTools.Questions[CurrentIndex];
            Content = question.QuestionContent;
            AnswerA = question.AnswerA;
            AnswerB = question.AnswerB;
            AnswerC = question.AnswerC;
            AnswerD = question.AnswerD;
        }
    }
}
