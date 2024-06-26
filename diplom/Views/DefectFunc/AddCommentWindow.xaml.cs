using System.Windows;

namespace diplom.Views.DefectFunc
{
    /// <summary>
    /// Логика взаимодействия для AddCommentWindow.xaml
    /// </summary>
    public partial class AddCommentWindow : Window
    {
        public Defect _editedDefect;
        public AddCommentWindow(Defect editedDefect)
        {
            InitializeComponent();
            _editedDefect = editedDefect;
            DataContext = this;
            LoadDefectData(_editedDefect);
        }
        private void LoadDefectData(Defect defect)
        {
            commentBox.Text = defect.Comment;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var _dbContext = new RgresDbContext())
            {
                var editedDefect = _dbContext.Defects.FirstOrDefault(d => d.Id == _editedDefect.Id);
                if (editedDefect != null)
                {
                    editedDefect.Comment = commentBox.Text;

                    _dbContext.SaveChanges();
                    DefectView defectView = new DefectView();
                    defectView.Show();
                    this.Close();
                }
            }
        }
    }
}
