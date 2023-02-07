using FlowersApp.Context;
using FlowersApp.Model;
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
    /// Логика взаимодействия для ProductData.xaml
    /// </summary>
    public partial class ProductData : Page
    {
        public ProductCategory ProductCategory { get; set; }
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
        public ProductData(Product product)
        {
            InitializeComponent();
            Product = product;
            this.DataContext = this;
        }

        private void BtnCsvSave_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream stream = new FileStream(Environment.CurrentDirectory + @"Product_export", FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    var product = Data.db.Product.ToList();
                    writer.WriteLine("Артикул;Название;Еденица измерения;Цена;Скидка;Производитель;Поставщик;Категория продукта;Количество на складе;Описание;Изображение;");
                    foreach (var item in product)
                    {
                        writer.WriteLine($"{item.Articul};{item.Title};{item.Unit};{item.Cost};{item.Discount};{item.Manufacturer};{item.Supplier};{item.IDProductCategory};{item.QuInStock};{item.Description};{item.Image};");
                    }
                }
            }
            MessageBox.Show($"Сохранение прошло успешно, проверьте файл здесь: {Environment.CurrentDirectory}", "Сохранено", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void txbSearchProduct_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ProductDataListView.ItemsSource = Data.db.Product.Where(item => item.Title.Contains(txbSearchProduct.Text) || item.Articul.ToString().Contains(txbSearchProduct.Text)
            || item.Manufacturer.Contains(txbSearchProduct.Text)).ToList();
        }

        private void cmbSearchCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category((cmbSearchCategory.SelectedItem as ComboBoxItem).Content.ToString(), cmbSearchCategory.Text);
        }

        private void Category(string type = "", string search = "")
        {
            var products = Data.db.Product.ToList();
            if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(type))
            {
                if (type == "Горшки")
                {
                    products = products.Where(item => item.ProductCategory.Title == "Серьги").ToList();
                }
                if (type == "Букеты")
                {
                    products = products.Where(item => item.ProductCategory.Title == "Подвеска").ToList();
                }
                if (type == "В горшке")
                {
                    products = products.Where(item => item.ProductCategory.Title == "Ожерелье").ToList();
                }            
                if (type == "Все")
                {
                    products = products.ToList();
                }
            }
            ProductDataListView.ItemsSource = products;
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductActionPage(new Model.Product()));

        }

        private void EditProductBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Product)ProductDataListView.SelectedItem;
            if (selectedItem != null)
            {
                NavigationService.Navigate(new ProductActionPage(selectedItem));
            }
        }

        private void DeleteProductBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Product)ProductDataListView.SelectedItem;
            if (selectedItem != null)
            {
                Data.db.Product.Remove(selectedItem);
                Data.db.SaveChanges();
                Page_Loaded(null, null);
            }
            MessageBox.Show("Данные удалены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Products = Data.db.Product.ToList();
            ProductDataListView.ItemsSource = Products;
        }
    }
}
