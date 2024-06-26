using diplom.Viewmodels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;

namespace diplom.Views.DefectFunc
{
    /// <summary>
    /// Логика взаимодействия для defectview.xaml
    /// </summary>
    public partial class DefectView : Window
    {
        public ObservableCollection<Defect> FilteredDefects { get; set; } = new ObservableCollection<Defect>();
        private ObservableCollection<Defect> _allDefects;
        private DbControl _dbContext;
        public DefectView()
        {
            InitializeComponent();
            _dbContext = new DbControl();
            LoadDefects();
            DataContext = this;
        }
        private void LoadDefects()
        {
            try
            {
                var defects = _dbContext.Defects
                    .Include(d => d.Priorety)
                    .Include(d => d.Ur).Include(d => d.Status)
                    .Include(d => d.Unit).Include(d => d.Gild)
                    .Include(d => d.AuthorMesNavigation).Include(d => d.FioTaNavigation)
                    .Include(d => d.AuthorRegNavigation).ToList();
                _allDefects = new ObservableCollection<Defect>(defects);
                FilterDefects();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки журнала дефектов");
            }
        }
        private void FilterDefects()
        {
            FilteredDefects.Clear();
            var filteredDefects = _allDefects.AsEnumerable();

            var selectedGilds = new[]
            {
                gild1.IsChecked == true ? "Электрический цех" : null,
                gild2.IsChecked == true ? "Химический цех" : null,
                gild3.IsChecked == true ? "Котлотурбинный цех блоков 300 МВт" : null,
                gild4.IsChecked == true ? "Котлотурбинный цех блоков 500 МВт" : null,
                gild5.IsChecked == true ? "Топливно-транспортный цех" : null,
                gild6.IsChecked == true ? "Цех золошлакоудаления" : null,
            }.Where(x => x != null).ToArray();

            var selectedUnits = new[]
            {
                unit1.IsChecked == true ? "1" : null,
                unit2.IsChecked == true ? "2" : null,
                unit3.IsChecked == true ? "3" : null,
                unit4.IsChecked == true ? "4" : null,
                unit5.IsChecked == true ? "5" : null,
                unit6.IsChecked == true ? "6" : null,
                unit7.IsChecked == true ? "7" : null,
                unit8.IsChecked == true ? "8" : null,
                unit9.IsChecked == true ? "9" : null,
                unit10.IsChecked == true ? "10" : null,
                unit11.IsChecked == true ? "ТП" : null,
                unit12.IsChecked == true ? "СЗШУ" : null,
                unit13.IsChecked == true ? "ОРУ" : null,
                unit14.IsChecked == true ? "БН-2" : null,
                unit15.IsChecked == true ? "ХВО" : null,
                unit16.IsChecked == true ? "П/О" : null,
                unit17.IsChecked == true ? "СХР" : null,
                unit18.IsChecked == true ? "ЭЛ-300" : null,
                unit19.IsChecked == true ? "ЭЛ-500" : null,
                unit20.IsChecked == true ? "У/Р-300" : null,
                unit21.IsChecked == true ? "У/Р-500" : null,
                unit22.IsChecked == true ? "УКУТ" : null,
                unit23.IsChecked == true ? "ЖДХ" : null,
                unit24.IsChecked == true ? "ПОЖ/ТУШ" : null,
                unit25.IsChecked == true ? "КОНДИЦ" : null,
            }.Where(x => x != null).ToArray();

            var selectedUrs = new[]
            {
                ur1.IsChecked == true ? "СРКО" : null,
                ur2.IsChecked == true ? "СРТО" : null,
                ur3.IsChecked == true ? "ОРВО" : null,
                ur4.IsChecked == true ? "УР ТАИ" : null,
                ur5.IsChecked == true ? "ССТП" : null,
                ur6.IsChecked == true ? "СРЭО 300 500" : null,
                ur7.IsChecked == true ? "СРЭО ТПВС" : null,
                ur8.IsChecked == true ? "СРО ТП" : null,
                ur9.IsChecked == true ? "СРО СЗШУ" : null,
                ur10.IsChecked == true ? "РЗА" : null,
            }.Where(x => x != null).ToArray();

            if (selectedGilds.Any())
            {
                filteredDefects = filteredDefects.Where(d => selectedGilds.Contains(d.Gild.Gname));
            }

            if (selectedUnits.Any())
            {
                filteredDefects = filteredDefects.Where(d => selectedUnits.Contains(d.Unit.Uabbreviation));
            }

            if (selectedUrs.Any())
            {
                filteredDefects = filteredDefects.Where(d => selectedUrs.Contains(d.Ur.Jabbreviation));
            }

            foreach (var defect in filteredDefects)
            {
                FilteredDefects.Add(defect);
            }
        }
        private void FilterChanged(object sender, RoutedEventArgs e)
        {
            FilterDefects();
        }
        private void AddComment_Click(object sender, RoutedEventArgs e)
        {
            Defect d = dataGrid.SelectedItem as Defect;
            if (d != null)
            {
                AddCommentWindow addCommentWindow = new AddCommentWindow(d);
                addCommentWindow.Show();
                this.Close();
            }
            else MessageBox.Show("Выберите элемент для редактирования");
        }
    }
}
