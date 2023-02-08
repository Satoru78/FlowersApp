using FlowersApp.Model;
using FlowersApp.Views.Pages;
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

namespace FlowersApp.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User User;
        public MainWindow(User user)
        {
            InitializeComponent();
            ///Проверка введенного пользователя и отображение его данных в окне
            this.User = user;
            MainFrame.Navigate(new MainPageAdmin());
            tblNameUser.Text = $"Пользователь: {user.FirstName} {user.LastName}";
        }
    }
}
