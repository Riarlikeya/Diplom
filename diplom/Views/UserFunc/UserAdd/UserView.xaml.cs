using diplom.Viewmodels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;

namespace diplom.Views.UserFunc.UserAdd
{
    public partial class UserView : Window
    {
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        private DbControl _dbContext;
        public UserView()
        {
            InitializeComponent();
            _dbContext = new DbControl();
            DataContext = this;
            LoadUsers();
        }
        private void LoadUsers()
        {
            var users = _dbContext.Users.Include(u => u.Srole).Include(u => u.Job).Distinct().ToList();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            User u = dataGrid.SelectedItem as User;
            if (u != null)
            {
                EditUser edit = new EditUser(u);
                edit.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите элемент для редактирования");
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            User u = dataGrid.SelectedItem as User;
            if (u != null)
            {
                _dbContext.RemoveUser(u);
                Users.Remove(u);
                UserView view = new UserView();
                view.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите элемент для удаления");
            }
        }
    }
}
