using FlowersApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlowersApp.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPageAdmin.xaml
    /// </summary>
    public partial class MainPageAdmin : Page
    {
        public User User { get; set; }
        public MainPageAdmin()
        {
            InitializeComponent();
            if (User == null)
            {
                btnUserList.Visibility = Visibility.Hidden;
            }
        }

        private void ProductList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductData(new Model.Product()));
        }

        private void btnUserList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserListPage(new Model.LoginHistory()));
        }
    }
}
