using Quiz.Core;
using Quiz.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Quiz.MVVM.ViewModel
{
    internal class MenuViewModel : ObservableObject
    {
        public ICommand QuestionCommand { get; set; }
        private readonly NavigationViewModel _navigationViewModel;

        public MenuViewModel(NavigationViewModel navigationViewModel)
        {
            _navigationViewModel = navigationViewModel;
            QuestionCommand = new RelayCommand(OpenQuestionView);
        }

        private void OpenQuestionView(object obj)
        {
            _navigationViewModel.SelectedViewModel = new QuestionViewModel();
        }
    }
}
