using diplom.Models;
using diplom.Services;
using diplom.Viewmodels;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace diplom.Views.TalonCreating
{
    /// <summary>
    /// Логика взаимодействия для TalonCreate.xaml
    /// </summary>
    public partial class TalonCreate : Window
    {
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Defect> Defects { get; set; }
        private readonly DbControl _dbContext;
        public TalonCreate()
        {
            InitializeComponent();
            TodayDate();
            _dbContext = new DbControl();
            DataContext = this;
            LoadUserData();
            LoadDefects();
            LoadUsers();
        }
        private void LoadDefects()
        {
            using (var _dbContext = new RgresDbContext())
            {
                Defects = new ObservableCollection<Defect>(_dbContext.Defects.ToList());
                defectId.ItemsSource = Defects;
            }
        }
        private void LoadUsers()
        {
            using (var _dbContext = new RgresDbContext())
            {
                Users = new ObservableCollection<User>(_dbContext.Users.ToList());
                fioTAcombobox.ItemsSource = Users;
            }
        }
        private void LoadUserData()
        {
            int currentUserId = UserManager.CurrentUserId;
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Id == currentUserId);

            regUser.Text = currentUser.Fio;
            regUserEmail.Text = currentUser.Email;
        }
        private void TodayDate()
        {
            var todayDate = DateTime.Now;
            var futureDate = DateTime.Now.AddDays(4);
            datePicker.DisplayDateStart = todayDate;
            datePicker.DisplayDateEnd = futureDate;
        }
        private void DatePicker2DateTime_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePicker.SelectedDate.HasValue)
            {
                var selectedDate = datePicker.SelectedDate;
                datePicker2.DisplayDateStart = selectedDate;
            }
        }

        private void email1_Checked(object sender, RoutedEventArgs e)
        {
            email1text.Visibility = Visibility.Visible;
        }

        private void email1_Unchecked(object sender, RoutedEventArgs e)
        {
            email1text.Visibility = Visibility.Collapsed;
        }

        private void email2_Checked(object sender, RoutedEventArgs e)
        {
            email2text.Visibility = Visibility.Visible;
        }

        private void email2_Unchecked(object sender, RoutedEventArgs e)
        {
            email2text.Visibility = Visibility.Collapsed;
        }

        private void email3_Checked(object sender, RoutedEventArgs e)
        {
            email3text.Visibility = Visibility.Visible;
        }

        private void email3_Unchecked(object sender, RoutedEventArgs e)
        {
            email3text.Visibility = Visibility.Collapsed;
        }

        private void email4_Checked(object sender, RoutedEventArgs e)
        {
            email4text.Visibility = Visibility.Visible;
        }

        private void email4_Unchecked(object sender, RoutedEventArgs e)
        {
            email4text.Visibility = Visibility.Collapsed;
        }
        public static bool IsValidTime(string input)
        {
            string pattern = @"^(?<hours>[0-2][0-9]):(?<minutes>[0-5][0-9])$";
            Match match = Regex.Match(input, pattern);

            if (!match.Success)
            {
                return false;
            }

            int hours = int.Parse(match.Groups["hours"].Value);
            int minutes = int.Parse(match.Groups["minutes"].Value);

            if (hours >= 0 && hours < 24 && minutes >= 0 && minutes < 60)
            {
                return true;
            }

            return false;
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(dopTime.Text))
                {
                    if (IsValidTime(dopTime.Text))
                    {
                        if (datePicker.SelectedDate != null)
                        {
                            if (datePicker2.SelectedDate != null)
                            {
                                if (defectId.SelectedIndex != -1)
                                {
                                    string startDateString = datePicker.SelectedDate.ToString();
                                    EmailSmtpService emailSmtp = new EmailSmtpService();
                                    string body = "Здравствуйте, ваш допуск назначен на " + dopTime.Text + " " + TruncateString(startDateString);
                                    string dopEmails = "";
                                    if (!string.IsNullOrEmpty(email1text.Text) && IsValidEmail(email1text.Text))
                                    {
                                        dopEmails += email1text.Text + " ";
                                        emailSmtp.SendMail(email1text.Text, body);
                                    }
                                    if (!string.IsNullOrEmpty(email2text.Text) && IsValidEmail(email2text.Text))
                                    {
                                        dopEmails += email2text.Text + " ";
                                        emailSmtp.SendMail(email2text.Text, body);
                                    }
                                    if (!string.IsNullOrEmpty(email3text.Text) && IsValidEmail(email3text.Text))
                                    {
                                        dopEmails += email3text.Text + " ";
                                        emailSmtp.SendMail(email3text.Text, body);
                                    }
                                    if (!string.IsNullOrEmpty(email4text.Text) && IsValidEmail(email4text.Text))
                                    {
                                        dopEmails += (email4text.Text + " ");
                                        emailSmtp.SendMail(email4text.Text, body);
                                    }
                                    emailSmtp.SendMail(regUserEmail.Text, body);

                                    int regUserName = _dbContext.Users.FirstOrDefault(u => u.Fio == regUser.Text).Id;
                                    var defect = defectId.SelectedItem as Defect;
                                    string regUserMail = regUserEmail.Text;
                                    string dopuskTime = dopTime.Text;
                                    var plannedDate = datePicker2.SelectedDate;
                                    var startDate = datePicker.SelectedDate;
                                    DateOnly plannedDateOnly = DateOnly.FromDateTime((DateTime)plannedDate);
                                    DateOnly startDateOnly = DateOnly.FromDateTime((DateTime)startDate);
                                    var fio = fioTAcombobox.SelectedItem as User;

                                    Talon newTalon = new Talon()
                                    {
                                        Reguser = regUserName,
                                        Readytime = dopuskTime,
                                        Regemail = regUserMail,
                                        DefectId = defect.Id,
                                        Startdate = startDateOnly,
                                        Enddate = plannedDateOnly,
                                        Emaildop = dopEmails,
                                        TstatusId = 1
                                    };
                                    _dbContext.AddTalon(newTalon);

                                    var _editedDefect = _dbContext.Defects.FirstOrDefault(d => d.Id == defect.Id);
                                    _editedDefect.FioTa = fio.Id;
                                    _editedDefect.Statusid = 3;
                                    _dbContext.SaveChanges();

                                    this.Close();
                                }
                                else MessageBox.Show("Не выбран дефект");
                            }
                            else MessageBox.Show("Не выбрана дата окончания работ");
                        }
                        else MessageBox.Show("Не выбрана дата начала работ");
                    }
                    else MessageBox.Show("Время допуска введено неправильно");
                }
                else MessageBox.Show("Введите время допуска");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        public string TruncateString(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            if (input.Length > 10)
            {
                return input.Substring(0, 10);
            }

            return input;
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
