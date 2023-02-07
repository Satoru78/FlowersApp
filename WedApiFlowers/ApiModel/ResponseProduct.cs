using FlowersApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WedApiFlowers.ApiModel
{
    class ResponseProduct
    {
        public ResponseProduct(Product product)
        {
            this.ID = product.ID;
            this.Articul = product.Articul;
            this.Title = product.Title;
            this.Unit = product.Unit;
            this.Cost = product.Cost;
            this.Discount = product.Discount;
            this.Manufacturer = product.Manufacturer;
            this.Supplier = product.Supplier;
            this.IDProductCategory = product.IDProductCategory;
            this.QuantitiInStock = product.QuInStock;
            this.Description = product.Description;
            this.Image = product.Image;

        }
        public ResponseProduct() { }
        public int ID { get; set; }
        public string Articul { get; set; }
        public string Title { get; set; }
        public string Unit { get; set; }
        public decimal Cost { get; set; }
        public int Discount { get; set; }
        public string Manufacturer { get; set; }
        public string Supplier { get; set; }
        public int IDProductCategory { get; set; }
        public int QuantitiInStock { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
