using diplom.Models;
using diplom.Viewmodels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;

namespace diplom.Views.DefectFunc
{
    /// <summary>
    /// Логика взаимодействия для defectAdd.xaml
    /// </summary>
    public partial class DefectAdd : Window
    {
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Unit> Units { get; set; }
        public ObservableCollection<Gild> Gilds { get; set; }
        public ObservableCollection<Priority> Priorities { get; set; }
        private readonly DbControl _dbContext;
        public DefectAdd()
        {
            InitializeComponent();
            _dbContext = new DbControl();
            LoadUserData();
            DataContext = this;
            LoadUsers();
            LoadGilds();
            LoadUnits();
            LoadPriorities();
            jobAbbrev.Text = GetCurrentUserJob();
        }

        private void LoadUserData()
        {
            string currentUserName = GetCurrentUserName();
            string currentGildAbbrev = GetCurrentUserJob();

            jobAbbrev.Text = currentGildAbbrev;
            regAuthor.Text = currentUserName;
            mesAuthor.Text = currentUserName;
        }
        private void LoadUsers()
        {
            using (var _dbContext = new RgresDbContext())
            {
                Users = new ObservableCollection<User>(_dbContext.Users.ToList());
                mesAuthor.ItemsSource = Users;
            }
        }
        private void LoadGilds()
        {
            using (var _dbContext = new RgresDbContext())
            {
                Gilds = new ObservableCollection<Gild>(_dbContext.Gilds.ToList());
                gildsBox.ItemsSource = Gilds;
            }
        }
        private void LoadUnits()
        {
            using (var _dbContext = new RgresDbContext())
            {
                Units = new ObservableCollection<Unit>(_dbContext.Units.ToList());
                unitsBox.ItemsSource = Units;
            }
        }
        private void LoadPriorities()
        {
            using (var _dbContext = new RgresDbContext())
            {
                Priorities = new ObservableCollection<Priority>(_dbContext.Priorities.ToList());
                priorityBox.ItemsSource = Priorities;
            }
        }
        private string GetCurrentUserName()
        {
            int currentUserId = UserManager.CurrentUserId;
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Id == currentUserId);
            DataContext = currentUser;

            return $"{currentUser?.Fio}";
        }

        private string GetCurrentUserJob()
        {
            int currentUserId = UserManager.CurrentUserId;
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Id == currentUserId);
            var currentUserJob = _dbContext.Jobs.FirstOrDefault(u => u.Id == currentUser.Jobid);
            DataContext = currentUserJob;
            return $"{currentUserJob?.Jabbreviation}";
        }
        private int GetCurrentUserJobId()
        {
            int userId = UserManager.CurrentUserId;
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            var currentUserJob = _dbContext.Jobs.FirstOrDefault(u => u.Id == currentUser.Jobid);
            return currentUserJob.Id;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string regUser = regAuthor.Text;
                int regUserJob = GetCurrentUserJobId();
                var mesUser = mesAuthor.SelectedItem as User;
                string defUnit = unitsBox.Text;
                string defGild = gildsBox.Text;
                string defPrior = priorityBox.Text;
                string defShortDesc = shortDesc.Text;
                string defFullDesc = fullDesc.Text;
                int defStatus = 1;

                if (mesUser != null)
                {
                    if (!string.IsNullOrEmpty(defUnit))
                    {
                        if (!string.IsNullOrEmpty(defGild))
                        {
                            if (!string.IsNullOrEmpty(defPrior))
                            {
                                if (!string.IsNullOrEmpty(defShortDesc))
                                {
                                    Defect newDefect = new Defect()
                                    {
                                        AuthorReg = _dbContext.Users.FirstOrDefault(u => u.Fio == regUser).Id,
                                        AuthorMes = mesUser.Id,
                                        Date = DateTime.Now,
                                        Urid = _dbContext.Jobs.FirstOrDefault(u => u.Id == regUserJob).Id,
                                        Gildid = _dbContext.Gilds.FirstOrDefault(u => u.Gabbreviaton == defGild).Id,
                                        Unitid = _dbContext.Units.FirstOrDefault(u => u.Uabbreviation == defUnit).Id,
                                        Prioretyid = _dbContext.Priorities.FirstOrDefault(u => u.Pname == defPrior).Id,
                                        ShortDesc = defShortDesc,
                                        FullDesc = defFullDesc,
                                        Statusid = _dbContext.DefStatuses.FirstOrDefault(u => u.Id == defStatus).Id

                                    };
                                    _dbContext.AddDefect(newDefect);
                                    MessageBox.Show("Сообщение создано!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                    this.Close();
                                }
                                else MessageBox.Show("Укажите краткое описание", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else MessageBox.Show("Выберите приоритет", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else MessageBox.Show("Выберите цех", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else MessageBox.Show("Выберите блок", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("Поле автор сообщения не заполнено", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
                return;
            }
        }
    }
}
