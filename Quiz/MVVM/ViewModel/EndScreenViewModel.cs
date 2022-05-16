using Quiz.Core;
using Quiz.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.MVVM.ViewModel
{
    internal class EndScreenViewModel : ObservableObject
    {
        public string ResultText { get; set; }
        public string ResultTime { get; set; }

        private readonly NavigationViewModel _navigationViewModel;

        public EndScreenViewModel(NavigationViewModel navigationViewModel, int points, string currentTimer)
        {
            _navigationViewModel = navigationViewModel;
            ResultText = $"Liczba punktów: {points}";
            ResultTime = $"Czas: {currentTimer}";
            OpenMenuView();
        }

        private async void OpenMenuView()
        {
            await Task.Delay(2000);
            _navigationViewModel.SelectedViewModel = new MenuViewModel(_navigationViewModel);
        }
    }
}
