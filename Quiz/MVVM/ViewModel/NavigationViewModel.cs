using System.ComponentModel;

namespace Quiz.MVVM.ViewModel
{
    class NavigationViewModel : INotifyPropertyChanged
    {
        private object selectedViewModel;

        public object SelectedViewModel
        {
            get => selectedViewModel;
            set
            {
                selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
