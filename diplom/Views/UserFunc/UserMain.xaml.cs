using diplom.Views.UserFunc.TalonApprove;
using diplom.Views.UserFunc.UserAdd;
using System.Windows;

namespace diplom.Views.UserFunc
{
    /// <summary>
    /// Логика взаимодействия для usermain.xaml
    /// </summary>
    public partial class UserMain : Window
    {
        public UserMain()
        {
            InitializeComponent();
        }

        private void UserView_Click(object sender, RoutedEventArgs e)
        {
            UserView userView = new UserView();
            userView.Show();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            UserReg userReg = new UserReg();
            userReg.Show();
        }

        private void TalonsView_Click(object sender, RoutedEventArgs e)
        {
            TalonViewAdmin talonViewAdmin = new TalonViewAdmin();
            talonViewAdmin.Show();
        }

        private void AdminDefect_Click(object sender, RoutedEventArgs e)
        {
            AdminDefectView adminDefectView = new AdminDefectView();
            adminDefectView.Show();
        }
    }
}
