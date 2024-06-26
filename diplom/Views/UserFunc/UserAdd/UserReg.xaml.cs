using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace diplom.Views.UserFunc.UserAdd
{
    /// <summary>
    /// Логика взаимодействия для UserAdd.xaml
    /// </summary>
    public partial class UserReg : Window
    {
        public ObservableCollection<SysRole> Roles { get; set; }
        public ObservableCollection<Access> Accesses { get; set; }
        public ObservableCollection<Job> Jobs { get; set; }
        private RgresDbContext _dbContext;
        public UserReg()
        {
            InitializeComponent();
            DataContext = this;
            LoadRoles();
            LoadAccesses();
            LoadJobs();
            _dbContext = new RgresDbContext();
        }
        private void LoadRoles()
        {
            using (var _dbContext = new RgresDbContext())
            {
                Roles = new ObservableCollection<SysRole>(_dbContext.SysRoles.ToList());
            }
        }
        private void LoadAccesses()
        {
            using (var _dbContext = new RgresDbContext())
            {
                Accesses = new ObservableCollection<Access>(_dbContext.Accesses.ToList());
            }
        }
        private void LoadJobs()
        {
            using (var _dbContext = new RgresDbContext())
            {
                Jobs = new ObservableCollection<Job>(_dbContext.Jobs.ToList());
            }
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fio = Fio.Text;
                string password = Password.Text;
                string phone = pNumber.Text;
                string login = Login.Text;
                string email = Email.Text;
                var srole = (SysRole)RoleComboBox.SelectedItem;
                var access = (Access)AccessComboBox.SelectedItem;
                var job = (Job)JobComboBox.SelectedItem;

                if (!IsStrongPassword(password))
                {
                    MessageBox.Show("Пароль должен содержать хотя-бы один спецсимвол, символ верхнего или нижнего регистра, длинной от 8 до 32 символов.");
                    return;
                }
                else
                {
                    if (!IsValidEmail(email))
                    {
                        MessageBox.Show("Электронная почта введена неверно.\nПример адреса электронной почты example@example.com");
                        return;
                    }
                    if (!IsValidatePhone(phone))
                    {
                        MessageBox.Show("Неправильный формат номера телефона.\nПример номера +1 (123) 456-78-90");
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(fio))
                        {
                            MessageBox.Show("Заполните ФИО.");
                            return;
                        }
                        if (string.IsNullOrEmpty(phone))
                        {
                            MessageBox.Show("Введите номер телефона.");
                            return;
                        }
                        if (string.IsNullOrEmpty(password))
                        {
                            MessageBox.Show("Введите пароль.");
                            return;
                        }
                        if (string.IsNullOrEmpty(login))
                        {
                            MessageBox.Show("Введите логин.");
                            return;
                        }
                        if (string.IsNullOrEmpty(email))
                        {
                            MessageBox.Show("Введите электронную почту.");
                            return;
                        }
                        if (srole == null)
                        {
                            MessageBox.Show("Выберите роль.");
                            return;
                        }
                        if (access == null)
                        {
                            MessageBox.Show("Выберите уровень доступа.");
                            return;
                        }
                        if (job == null)
                        {
                            MessageBox.Show("Выберите отдел.");
                            return;
                        }
                        else
                        {

                            if (_dbContext.Users.Any(u => u.Login == login))
                            {
                                MessageBox.Show("Пользователь с таким логином существует.");
                            }
                            else
                            {
                                if (_dbContext.Users.Any(u => u.Pnumber == phone))
                                {
                                    MessageBox.Show("Данный номер телефона занят.");
                                }
                                else
                                {

                                    _dbContext.Users.Add(new User { Fio = fio, Email = email, Login = login, Password = password, Pnumber = phone, Sroleid = srole.Id, Accessid = access.Id, Jobid = job.Id });
                                    _dbContext.SaveChanges();
                                    MessageBox.Show("Пользователь успешно зарегистрирован!");
                                    this.Close();
                                }
                            }
                        }
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
        private bool IsValidatePhone(string phone)
        {
            string pattern = @"^\+?\d{1,3}[\s-]?\(?\d{2,3}\)?[\s-]?\d{2,3}[\s-]?\d{2,3}$";
            return Regex.IsMatch(phone, pattern);

        }
        private bool IsStrongPassword(string password)
        {
            string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,32}$";
            return Regex.IsMatch(password, pattern);
        }
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);
                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
