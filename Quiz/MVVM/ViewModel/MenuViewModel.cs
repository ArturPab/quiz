using Quiz.Core;
using System.Windows.Input;
using Quiz.DataBase;

namespace Quiz.MVVM.ViewModel
{
    internal class MenuViewModel : ObservableObject
    {
        public ICommand PlayCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        private readonly NavigationViewModel _navigationViewModel;

        public MenuViewModel(NavigationViewModel navigationViewModel)
        {
            _navigationViewModel = navigationViewModel;
            PlayCommand = new RelayCommand(OpenPlayView);
            SettingsCommand = new RelayCommand(OpenSettingsView);
        }

        private void OpenPlayView(object obj)
        {
            _navigationViewModel.SelectedViewModel = new PlayViewModel(new DatabaseTools(), _navigationViewModel);
        }
        private void OpenSettingsView(object obj)
        {
            _navigationViewModel.SelectedViewModel = new SettingsViewModel(new DatabaseTools());
        }
    }
}
