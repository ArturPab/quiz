using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Quiz.Core;
using Quiz.DataBase;

namespace Quiz.MVVM.ViewModel
{
    public class SettingsViewModel : ObservableObject
    {
        public ICommand ItemChangedCommand;
        private readonly DatabaseTools _questionTools;
        public SettingsViewModel(DatabaseTools questionTools)
        {
            _questionTools = questionTools;
            ItemChangedCommand = new RelayCommand(ShowSelectedQuestion);
        }

        public List<string> QuestionsForView => _questionTools.Questions.Select(q => q.QuestionContent).ToList();
        public int CurrentIndex { get; set; } = -1;
        public string Content { get; set; } = string.Empty;
        public string AnswerA { get; set; } = string.Empty;
        public string AnswerB { get; set; } = string.Empty;
        public string AnswerC { get; set; } = string.Empty;
        public string AnswerD { get; set; } = string.Empty;

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
