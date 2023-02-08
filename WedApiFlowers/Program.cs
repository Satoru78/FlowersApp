using FlowersApp.Context;
using FlowersApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using WedApiFlowers.ApiModel;

namespace WedApiFlowers
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpListener server = new HttpListener();
            server.Prefixes.Add("http://localhost:12332/");

            JsonSerializerOptions options = new JsonSerializerOptions() { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) };
            server.Start();
            while (true)
            {
                HttpListenerContext context = server.GetContext();
                /// API метод Get
                if (context.Request.HttpMethod == "GET")
                {
                    try
                    {
                        if (context.Request.RawUrl == "/api/products/")
                        {
                            var productList = Data.db.Product.ToList();
                            string response = JsonSerializer.Serialize(Data.db.Product.ToList().ConvertAll(c => new ResponseProduct(c)), options);
                            byte[] data = Encoding.UTF8.GetBytes(response);
                            context.Response.ContentType = "application/json;charset=utf-8";
                            using (Stream stream = context.Response.OutputStream)
                            {
                                context.Response.StatusCode = 200;
                                stream.Write(data, 0, data.Length);
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        context.Response.StatusCode = 400;
                        context.Response.Close();
                    }
                }
                /// API метод DELETE
                else if (context.Request.HttpMethod == "DELETE")
                {
                    try
                    {
                        if (context.Request.QueryString.Count == 1)
                        {
                            if (context.Request.QueryString.Keys[0] == "id")
                            {
                                int id = Convert.ToInt32(context.Request.QueryString.Get(0));
                                var currentProduct = Data.db.Product.FirstOrDefault(b => b.ID == id);
                                if (currentProduct != null)
                                {
                                    Data.db.Product.Remove(currentProduct);
                                    Data.db.SaveChanges();
                                    context.Response.StatusCode = 200;
                                    context.Response.Close();
                                }
                            }
                        }
                    }
                    catch
                    {
                        context.Response.StatusCode = 400;
                        context.Response.Close();
                    }
                }
                /// API метод POST
                else if (context.Request.HttpMethod == "POST")
                {
                    try
                    {
                        if (context.Request.RawUrl == "/api/products/")
                        {
                            if (context.Request.ContentType == "application/json")
                            {
                                string request = "";
                                byte[] data = new byte[context.Request.ContentLength64];
                                using (Stream stream = context.Request.InputStream)
                                {
                                    stream.Read(data, 0, data.Length);
                                }
                                request = UTF8Encoding.UTF8.GetString(data);
                                var productList = JsonSerializer.Deserialize<List<ResponseProduct>>(request);
                                foreach (var item in productList)
                                {
                                    Product objects = new Product();
                                    objects.Articul = item.Articul;
                                    objects.Title = item.Title;
                                    objects.Unit = item.Unit;
                                    objects.Cost = item.Cost;
                                    objects.Discount = item.Discount;
                                    objects.Manufacturer = item.Manufacturer;
                                    objects.Supplier = item.Supplier;
                                    objects.IDProductCategory = item.IDProductCategory;
                                    objects.QuInStock = item.QuantitiInStock;
                                    objects.Description = item.Description;
                                    objects.Image = item.Image;
                                    Data.db.Product.Add(objects);
                                }
                                Data.db.SaveChanges();
                                context.Response.StatusCode = 200;
                                context.Response.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        context.Response.StatusCode = 400;
                        context.Response.Close();

                    }
                }
            }
        }
    }
}
