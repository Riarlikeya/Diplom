using diplom.Views.DefectFunc;
using diplom.Views.TalonCreating;
using System.Windows;

namespace diplom.Views
{
    /// <summary>
    /// Логика взаимодействия для SelectWindow.xaml
    /// </summary>
    public partial class SelectWindow : Window
    {
        public SelectWindow()
        {
            InitializeComponent();
        }
        private void DefectView_Click(object sender, RoutedEventArgs e)
        {
            DefectView defectView = new DefectView();
            defectView.Show();

        }
        private void DefectAdd_Click(object sender, RoutedEventArgs e)
        {
            DefectAdd defectAdd = new DefectAdd();
            defectAdd.Show();
        }

        private void TalonCreate_Click(object sender, RoutedEventArgs e)
        {
            TalonCreate talonCreate = new TalonCreate();
            talonCreate.Show();
        }
        private void TalonView_Click(object sender, RoutedEventArgs e)
        {
            TalonView talonView = new TalonView();
            talonView.Show();
        }
    }
}
