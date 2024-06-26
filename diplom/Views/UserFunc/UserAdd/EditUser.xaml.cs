using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace diplom.Views.UserFunc.UserAdd
{
    /// <summary>
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        public ObservableCollection<SysRole> Roles { get; set; }
        public ObservableCollection<Access> Accesses { get; set; }
        public ObservableCollection<Job> Jobs { get; set; }
        private User _editedUser;
        public EditUser(User editedUser)
        {
            InitializeComponent();
            _editedUser = editedUser;
            DataContext = this;
            LoadUserData(_editedUser);
            LoadRoles();
            LoadAccesses();
            LoadJobs();
        }
        private void LoadUserData(User user)
        {
            userEditing.Text = "Редактирование пользователя " + user.Login;
            fio.Text = user.Fio;
            pNumber.Text = user.Pnumber;
            password.Text = user.Password;
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
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveUserData();
            this.Close();
        }
        private void SaveUserData()
        {
            try
            {
                using (var _dbContext = new RgresDbContext())
                {
                    // Получите редактируемого пользователя из базы данных
                    var editedUser = _dbContext.Users.FirstOrDefault(u => u.Id == _editedUser.Id);

                    if (editedUser != null)
                    {
                        if (string.IsNullOrEmpty(pNumber.Text))
                        {
                            MessageBox.Show("Введите номер телефона.");
                            return;
                        }
                        if (string.IsNullOrEmpty(fio.Text))
                        {
                            MessageBox.Show("Введите ФИО.");
                            return;
                        }
                        if (RoleComboBox.SelectedItem == null)
                        {
                            MessageBox.Show("Выберите роль.");
                            return;
                        }
                        if (AccessComboBox.SelectedItem == null)
                        {
                            MessageBox.Show("Выберите уровень доступа.");
                            return;
                        }
                        if (jobComboBox.SelectedItem == null)
                        {
                            MessageBox.Show("Выберите отдел.");
                            return;
                        }
                        if (!IsValidatePhone(pNumber.Text))
                        {
                            MessageBox.Show("Введите корректный номер телефона.");
                            return;
                        }
                        if (!string.IsNullOrEmpty(password.Text))
                        {
                            if (IsStrongPassword(password.Text))
                            {
                                editedUser.Fio = fio.Text;
                                editedUser.Pnumber = pNumber.Text;
                                editedUser.Password = password.Text;
                                editedUser.Srole = (SysRole)RoleComboBox.SelectedItem;
                                editedUser.Access = (Access)AccessComboBox.SelectedItem;
                                editedUser.Job = (Job)jobComboBox.SelectedItem;

                                _dbContext.SaveChanges();
                                MessageBox.Show("Данные сохранены успешно.");
                                UserView userView = new UserView();
                                userView.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Пароль должен содержать хотя-бы один спецсимвол, символ верхнего или нижнего регистра, длинной от 8 до 32 символов.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Поле пароль не должно быть пустым.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при сохранении данных пользователя.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}
