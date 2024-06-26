using diplom.Viewmodels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;


namespace diplom.Views.TalonCreating
{
    /// <summary>
    /// Логика взаимодействия для TalonView.xaml
    /// </summary>
    public partial class TalonView : Window
    {
        public ObservableCollection<Talon> Talons { get; set; } = new ObservableCollection<Talon>();
        private DbControl _dbContext;
        public TalonView()
        {
            InitializeComponent();
            _dbContext = new DbControl();
            LoadTalons();
            DataContext = this;
        }
        private void LoadTalons()
        {
            var talons = _dbContext.Talons.Include(t => t.Tstatus).Include(t => t.Defect).Include(t => t.ReguserNavigation).Distinct().ToList();
            foreach (var talon in talons)
            {
                Talons.Add(talon);
            }

        }
    }
}
