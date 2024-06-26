using diplom.Services;
using diplom.Viewmodels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;

namespace diplom.Views.UserFunc.TalonApprove
{
    /// <summary>
    /// Логика взаимодействия для TalonViewAdmin.xaml
    /// </summary>
    public partial class TalonViewAdmin : Window
    {
        public ObservableCollection<Talon> Talons { get; set; } = new ObservableCollection<Talon>();
        private ObservableCollection<Talon> _allTalons;
        private DbControl _dbContext;
        public TalonViewAdmin()
        {
            InitializeComponent();
            _dbContext = new DbControl();
            LoadTalons();
            DataContext = this;
        }
        private void LoadTalons()
        {
            try
            {
                _allTalons = new ObservableCollection<Talon>(_dbContext.Talons.Include(t => t.Tstatus).Include(t => t.Defect).Include(t => t.ReguserNavigation).Distinct().ToList());
                FilterTalons();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки талонов: {ex.Message}");
            }
        }
        private void FilterTalons()
        {
            Talons.Clear();
            var filtredTalons = _allTalons;

            try
            {
                if (WaitRadioButton.IsChecked == true)
                {
                    filtredTalons = new ObservableCollection<Talon>(_allTalons.Where(t => t.TstatusId == 1));
                }
                else if (ApprovedRadioButton.IsChecked == true)
                {
                    filtredTalons = new ObservableCollection<Talon>(_allTalons.Where(t => t.TstatusId != 1));
                }

                foreach (var talon in filtredTalons)
                {
                    Talons.Add(talon);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации талонов: {ex.Message}");
            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FilterTalons();
        }
        public string[] SplitStringBySpace(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            string[] result = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return result;
        }

        private void TalonApprove_Click(object sender, RoutedEventArgs e)
        {
            Talon t = dataGrid.SelectedItem as Talon;
            var messageMail = "Вам одобрили заявку на допуск к ремонтным работам от " + t.Startdate + ". Ознакомьтесь с описанием дефекта" + t.DefectId + " в журнале дефектов.";
            EmailSmtpService emailSmtpService = new EmailSmtpService();
            if (t != null)
            {
                if (t.TstatusId == 1)
                {
                    string[] emails = SplitStringBySpace(t.Emaildop);
                    if (emails != null)
                    {
                        foreach (var email in emails)
                        {
                            emailSmtpService.SendMail(email, messageMail);
                        }

                    }
                    emailSmtpService.SendMail(t.Regemail, messageMail);

                    t.TstatusId = 2;
                    var _editedDefect = _dbContext.Defects.FirstOrDefault(d => d.Id == t.DefectId);
                    _editedDefect.Planneddate = t.Enddate;
                    _editedDefect.Statusid = 2;
                    _dbContext.SaveChanges();
                }
                else MessageBox.Show("Заявка уже обработана");
            }
            else MessageBox.Show("Выберите заявку");
        }

        private void TalonReject_Click(object sender, RoutedEventArgs e)
        {
            Talon t = dataGrid.SelectedItem as Talon;
            EmailSmtpService emailSmtpService = new EmailSmtpService();
            var messageMail = "Ваша заявка на допуск к ремонтным работам от " + t.Startdate + " отклонена. За подробной информацией обратитесь к администратору.";
            if (t != null)
            {
                if (t.TstatusId == 1)
                {
                    string[] emails = SplitStringBySpace(t.Emaildop);
                    if (emails != null)
                    {
                        foreach (var email in emails)
                        {
                            if (!string.IsNullOrEmpty(email))
                            {
                                emailSmtpService.SendMail(email, messageMail);
                            }

                        }

                    }
                    emailSmtpService.SendMail(t.Regemail, messageMail);

                    t.TstatusId = 3;
                    var _editedDefect = _dbContext.Defects.FirstOrDefault(d => d.Id == t.DefectId);
                    _editedDefect.Statusid = 2;
                    _dbContext.SaveChanges();
                }
                else MessageBox.Show("Заявка уже обработана");
            }
            else MessageBox.Show("Выберите заявку");
        }

        private void TalonClose_Click(object sender, RoutedEventArgs e)
        {
            var talon = dataGrid.SelectedItem as Talon;
            if (talon != null)
            {
                var def = _dbContext.Defects.FirstOrDefault(d => d.Id == talon.DefectId);
                DefectClose defectClose = new DefectClose(def);
                defectClose.Show();
                this.Close();
            }
            else MessageBox.Show("Выберите заявку");
        }
    }
}
