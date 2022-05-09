using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Quiz.Core;
using Quiz.DataBase;
using Quiz.MVVM.Model;

namespace Quiz.MVVM.ViewModel
{
    internal class SettingsViewModel : ObservableObject
    {
        private readonly DatabaseTools _questionTools;
        private readonly NavigationViewModel _navigationViewModel;
        private string _content = string.Empty;
        private string _answerA = string.Empty;
        private string _answerB = string.Empty;
        private string _answerC = string.Empty;
        private string _answerD = string.Empty;
        private bool _isA = true;
        private bool _isB;
        private bool _isC;
        private bool _isD;
        private bool _isAddButtonActive = true;
        private bool _isRemoveButtonActive = true;
        private bool _isEditButtonActive = true;
        private bool _isSaveQuestionButtonActive = false;
        private bool _isSaveAndQuitButtonActive = true;

        private string CorrectAnswer
        {
            get
            {
                if(IsA)
                    return AnswerA;
                if(IsB)
                    return AnswerB;
                return IsC ? AnswerC : AnswerD;
            }
        }

        private int _editingQuestion = -1;

        public SettingsViewModel(DatabaseTools questionTools, NavigationViewModel navigationViewModel)
        {
            _questionTools = questionTools;
            _navigationViewModel = navigationViewModel;
            AddQuestionCommand = new RelayCommand(AddQuestion);
            RemoveQuestionCommand = new RelayCommand(RemoveQuestion);
            EditQuestionCommand = new RelayCommand(EditQuestion);
            SaveQuestionCommand = new RelayCommand(SaveQuestion);
            SaveAndQuitCommand = new RelayCommand(SaveAndQuit);
        }

        public ICommand RemoveQuestionCommand { get; set; }
        public ICommand AddQuestionCommand { get; set; }
        public ICommand EditQuestionCommand { get; set; }
        public ICommand SaveQuestionCommand { get; set; }
        public ICommand SaveAndQuitCommand { get; set; }

        public List<string> QuestionsForView => _questionTools.Questions.Select(q => q.QuestionContent).ToList();
        public int CurrentIndex { get; set; } = -1;

        public string Content
        {
            get => _content;
            set
            {
                _content= Regex.Replace(value.Trim(), @"\s+", " ");
                OnPropertyChanged(nameof(Content));
            }
        }

        public string AnswerA
        {
            get => _answerA;
            set
            {
                _answerA = Regex.Replace(value.Trim(), @"\s+", " ");
                OnPropertyChanged(nameof(AnswerA));
            }
        }

        public string AnswerB
        {
            get => _answerB;
            set
            {
                _answerB = Regex.Replace(value.Trim(), @"\s+", " ");
                OnPropertyChanged(nameof(AnswerB));
            }
        }

        public string AnswerC
        {
            get => _answerC;
            set
            {
                _answerC = Regex.Replace(value.Trim(), @"\s+", " ");
                OnPropertyChanged(nameof(AnswerC));
            }
        }

        public string AnswerD
        {
            get => _answerD;
            set
            {
                _answerD = Regex.Replace(value.Trim(), @"\s+", " ");
                OnPropertyChanged(nameof(AnswerD));
            }
        }

        public bool IsA
        {
            get => _isA;
            set
            {
                _isA=value;
                OnPropertyChanged(nameof(IsA));
            }
        }
        public bool IsB
        {
            get => _isB;
            set
            {
                _isB = value;
                OnPropertyChanged(nameof(IsB));
            }
        }
        public bool IsC
        {
            get => _isC;
            set
            {
                _isC = value;
                OnPropertyChanged(nameof(IsC));
            }
        }
        public bool IsD
        {
            get => _isD;
            set
            {
                _isD = value;
                OnPropertyChanged(nameof(IsD));
            }
        }

        public bool IsAddButtonActive
        {
            get => _isAddButtonActive;
            set
            {
                _isAddButtonActive = value;
                OnPropertyChanged(nameof(IsAddButtonActive));
            }
        }
        public bool IsRemoveButtonActive
        {
            get => _isRemoveButtonActive;
            set
            {
                _isRemoveButtonActive = value;
                OnPropertyChanged(nameof(IsRemoveButtonActive));
            }
        }
        public bool IsEditButtonActive
        {
            get => _isEditButtonActive;
            set
            {
                _isEditButtonActive = value;
                OnPropertyChanged(nameof(IsEditButtonActive));
            }
        }
        public bool IsSaveQuestionButtonActive
        {
            get => _isSaveQuestionButtonActive;
            set
            {
                _isSaveQuestionButtonActive = value;
                OnPropertyChanged(nameof(IsSaveQuestionButtonActive));
            }
        }
        public bool IsSaveAndQuitButtonActive
        {
            get => _isSaveAndQuitButtonActive;
            set
            {
                _isSaveAndQuitButtonActive = value;
                OnPropertyChanged(nameof(IsSaveAndQuitButtonActive));
            }
        }
        private void AddQuestion(object obj)
        {
            if (!NullCheck()) return;
            var question = new Question(Content, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer);
            _questionTools.Questions.Add(question);
            OnPropertyChanged(nameof(QuestionsForView));
            ClearScreen();
        }

        private bool NullCheck()
        {
            var git = true;
            var empty = new List<string>();
            if (string.IsNullOrWhiteSpace(Content))
            {
                git = false;
                empty.Add("Pytanie");
            }
            if (string.IsNullOrWhiteSpace(AnswerA))
            {
                git = false;
                empty.Add("Odpowiedz A");
            }
            if (string.IsNullOrWhiteSpace(AnswerB))
            {
                git = false;
                empty.Add("Odpowiedz B");
            }
            if (string.IsNullOrWhiteSpace(AnswerC))
            {
                git = false;
                empty.Add("Odpowiedz C");
            }
            if (string.IsNullOrWhiteSpace(AnswerD))
            {
                git = false;
                empty.Add("Odpowiedz D");
            }

            if (git) return git;

            var message = (empty.Count < 2) ? "Pole: " : "Pola: ";
            message = empty.Aggregate(message, (current, s) => current + $"{s}, ");
            message += "jest puste!";
            MessageBox.Show(message);

            return git;
        }

        private void RemoveQuestion(object obj)
        {
            if (CurrentIndex < 0) return;
            _questionTools.Questions.RemoveAt(CurrentIndex);
            CurrentIndex = -1;
            OnPropertyChanged(nameof(QuestionsForView));
        }
        private void EditQuestion(object obj)
        {
            if (CurrentIndex < 0) return;

            IsAddButtonActive = false;
            IsRemoveButtonActive = false;
            IsEditButtonActive = false;
            IsSaveQuestionButtonActive = true;
            IsSaveAndQuitButtonActive = false;
            
            var question = _questionTools.Questions[CurrentIndex];
            Content = question.QuestionContent;
            AnswerA = question.AnswerA;
            AnswerB = question.AnswerB;
            AnswerC = question.AnswerC;
            AnswerD = question.AnswerD;
            if (question.CorrectAnswer == question.AnswerA)
                IsA = true;
            else if (question.CorrectAnswer == question.AnswerB)
                IsB = true;
            else if (question.CorrectAnswer == question.AnswerC)
                IsC = true;
            else
                IsD = true;

            _editingQuestion = CurrentIndex;

        }
        
        private void SaveQuestion(object obj)
        {
            if (!NullCheck()) return;

            var questionRef = _questionTools.Questions[_editingQuestion];
            questionRef.QuestionContent = Content;
            questionRef.AnswerA = AnswerA;
            questionRef.AnswerB = AnswerB;
            questionRef.AnswerC = AnswerC;
            questionRef.AnswerD = AnswerD;
            questionRef.CorrectAnswer = CorrectAnswer;
            _editingQuestion = -1;
            OnPropertyChanged(nameof(QuestionsForView));
            ClearScreen();
        }
        
        private void ClearScreen()
        {
            Content = string.Empty;
            AnswerA = string.Empty;
            AnswerB = string.Empty;
            AnswerC = string.Empty;
            AnswerD = string.Empty;
            IsA = true;

            IsAddButtonActive = true;
            IsRemoveButtonActive = true;
            IsEditButtonActive = true;
            IsSaveQuestionButtonActive = false;
            IsSaveAndQuitButtonActive = true;
        }
        private void SaveAndQuit(object obj)
        {
            _questionTools.Save();
            _navigationViewModel.SelectedViewModel = new MenuViewModel(_navigationViewModel);
        }
    }
}
