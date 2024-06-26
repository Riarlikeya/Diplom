using diplom.Models;
using diplom.Views;
using diplom.Views.UserFunc;
using Npgsql;
using System.Windows;
using System.Windows.Controls;

namespace diplom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Login = LoginBox.Text;
                string Password = PasswordBox.Password;

                if (string.IsNullOrEmpty(Login))
                {
                    MessageBox.Show("Пожалуйста, заполните поле логин.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(Password))
                {
                    MessageBox.Show("Пожалуйста, заполните поле пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var dbContext = new RgresDbContext())
                {
                    var user = dbContext.Users.Where(u => u.Login == Login && u.Password == Password).FirstOrDefault();

                    if (user != null)
                    {
                        int userId = GetUserIdByUsername(Login);
                        UserManager.SetCurrentUser(userId);
                        // Пользователь найден
                        if (user.Sroleid == 1)
                        {
                            SelectWindow selectWindow = new SelectWindow();
                            selectWindow.Show();
                            this.Close();
                        }
                        if (user.Sroleid == 2)
                        {
                            UserMain userMain = new UserMain();
                            userMain.Show();
                            this.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                // Обработка ошибок подключения к базе данных
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Обработка других исключений
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public int GetUserIdByUsername(string Login)
        {
            using (var context = new RgresDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Login == Login);
                return user != null ? user.Id : -1;
            }
        }
    }

}