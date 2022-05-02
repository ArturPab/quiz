using Quiz.MVVM.ViewModel;
using System.Windows;

namespace Quiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new NavigationViewModel();
            viewModel.SelectedViewModel = new MenuViewModel(viewModel);
            this.DataContext = viewModel;
        }
    }
}
