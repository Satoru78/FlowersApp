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
using System.Windows.Shapes;
using FlowersApp.Model;
using FlowersApp.Views.Pages;

namespace FlowersApp.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    /// 
    public partial class AdminWindow : Window
    {
        public User User;
        public AdminWindow(User user)
        {
            InitializeComponent();
            this.User = user;
            AdminFrame.Navigate(new MainPageAdmin());
            tblNameUser.Text = $"Пользователь: {user.FirstName} {user.LastName}";
        }
    }
}
