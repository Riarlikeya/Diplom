using System.Windows;

namespace diplom.Views.UserFunc.TalonApprove
{
    /// <summary>
    /// Логика взаимодействия для DefectClose.xaml
    /// </summary>
    public partial class DefectClose : Window
    {
        public Defect _editedDefect;
        public DefectClose(Defect editedDefect)
        {
            InitializeComponent();
            _editedDefect = editedDefect;
            DataContext = this;
            LoadDefectData(_editedDefect);
        }
        private void LoadDefectData(Defect defect)
        {
            endComment.Text = defect.Enddcomment;
        }
        private void button1_Checked(object sender, RoutedEventArgs e)
        {
            buttonStack.Visibility = Visibility.Visible;
        }

        private void button1_Unchecked(object sender, RoutedEventArgs e)
        {
            buttonStack.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var _dbContext = new RgresDbContext())
                {
                    var editedDefect = _dbContext.Defects.FirstOrDefault(d => d.Id == _editedDefect.Id);
                    if (editedDefect != null)
                    {
                        if (buttonUR.IsChecked == true)
                        {
                            editedDefect.Statusid = 6;
                        }
                        if (buttonUE.IsChecked == true)
                        {
                            editedDefect.Statusid = 7;
                        }
                        if (closeButton.IsChecked == true)
                        {
                            editedDefect.Statusid = 4;
                        }
                        if (closeButton.IsChecked == false && buttonUR.IsChecked == false && buttonUE.IsChecked == false)
                        {
                            editedDefect.Statusid = 4;
                        }
                        if (closeButton.IsChecked == false && button1.IsChecked == false)
                        {
                            editedDefect.Statusid = 4;
                        }
                        editedDefect.Enddcomment = endComment.Text;

                        editedDefect.Factdate = DateOnly.FromDateTime(DateTime.Now);
                        _dbContext.SaveChanges();
                        AdminDefectView adminDefectView = new AdminDefectView();
                        adminDefectView.Show();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
