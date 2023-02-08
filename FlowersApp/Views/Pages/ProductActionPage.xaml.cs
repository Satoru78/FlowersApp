using FlowersApp.Context;
using FlowersApp.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ProductActionPage.xaml
    /// </summary>
    public partial class ProductActionPage : Page
    {
        public List<ProductCategory> ProductCategorys { get; set; }
        public Product Product { get; set; }
    
        public ProductActionPage(Product product)
        {
            InitializeComponent();
            Product = product;
            ProductCategorys = Data.db.ProductCategory.ToList();
            this.DataContext = this;
        }
        //Сохранение данных
        private void TxbSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Product.ID == 0)
                {
                    Product.GetImage = "\\products\\" + System.IO.Path.GetFileName(img.FileName);
                    Data.db.Product.Add(Product);
                }
                File.Copy(img.FileName, $"products\\{System.IO.Path.GetFileName(img.FileName).Trim()}", true);
                Product.GetImage = "\\products\\" + System.IO.Path.GetFileName(img.FileName);
                Data.db.SaveChanges();
                MessageBox.Show("Данные сохранены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        //Кнопка возврата назад
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        //Кнопка выбора изображения и фильтр
        private void SelectPictureBtn_Click(object sender, RoutedEventArgs e)
        {
            img.Filter = "Image (*.jpg;*.jpeg;*.png;) |  *.jpg; *.jpeg; *.png";
            if (img.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage(new Uri(img.FileName));
                Img.Source = image;
            }
        }
        OpenFileDialog img = new OpenFileDialog();
    }
}
