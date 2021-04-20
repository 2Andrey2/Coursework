using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ShopBook.Data.FileLogGrup;
using ShopBook.Data.FileOperation.FileProductGrup;
using ShopBook.Data.Interface;
using ShopBook.Entities.Products;
using ShopBook.Entities.Products.Additional_products;
using ShopBook.Services.Interface;

namespace ShopBook.Services
{
    class ProductsManagement: IProductsManagement
    {
        internal event Action<IEnumerable<Product>> SearchComplited;
        internal event Action<IEnumerable<Product>> LoadAllItem;
        FileLog log = new FileLog();
        public void ProductAction(string Action, string[] mass)
        {
            if (Action == "Created")
            {
                AddProduct(mass);
            }
            if (Action == "Search")
            {
                SearchProduct(mass);
            }
            if (Action == "Delete")
            {
                DeleteProduct(mass);
            }
            if (Action == "Load_all")
            {
                Load_all();
            }
            log.Action("Recording", Action, mass);
        }
        void AddProduct(string[] mass)
        {
            Product objectt;
            IFile<Product> file;
            switch (mass[0])
            {
                case "Книги": objectt = new Book(mass); file = new FileProduct("Book"); break;
                case "Журнал": objectt = new Magazine(mass); file = new FileProduct("AddProducts"); break;
                case "Концелярия": objectt = new Сhancellery(mass); file = new FileProduct("AddProducts"); break;
                //case "Концелярия": objectt = new Сhancellery(mass); break; Допилить 
                default: throw new ArgumentException("Недопустимый код операции");
            }
            if (file.Duplicate_search(mass) == true)
            {
                MessageBox.Show("Товар с такими данными уже зарегестрирован");
                return;
            }
            file.NewObject(objectt);
        }
        void SearchProduct(string[] mass)
        {
            IFile<Product> file;
            switch (mass[0])
            {
                case "Книги": file = new FileProduct("Book"); break;
                case "Журнал": file = new FileProduct("AddProducts"); break;
                case "Концелярия": file = new FileProduct("AddProducts"); break;
                //case "Концелярия": objectt = new Сhancellery(mass); break; Допилить 
                default: throw new ArgumentException("Недопустимый код операции");
            }

            SearchComplited?.Invoke(file.Search(mass));
            
        }
        void DeleteProduct(string[] mass)
        {
            IFile<Product> file;
            switch (mass[0])
            {
                case "Книги": file = new FileProduct("Book"); break;
                case "Журнал": file = new FileProduct("AddProducts"); break;
                case "Концелярия": file = new FileProduct("AddProducts"); break;
                //case "Концелярия": objectt = new Сhancellery(mass); break; Допилить 
                default: throw new ArgumentException("Недопустимый код операции");
            }
            file.Deleting_Object(mass);
        }
        void Load_all()
        {
            FileProduct file = new FileProduct();
            if (file.Total_number_records() != 0)
            {
                file = new FileProduct("Book");
                IEnumerable<Product> rez = file.Load_all();
                LoadAllItem?.Invoke(rez);
                file = new FileProduct("AddProducts");
                Product[] mass = file.Load_all();
                if (mass != null)
                {
                    Product[] massMagazine = new Product[mass.Length];
                    Product[] massСhancellery = new Product[mass.Length];
                    for (int i = 0; i < mass.Length; i++)
                    {
                        if (mass[i] is Magazine) { massMagazine[i] = mass[i]; }
                        if (mass[i] is Сhancellery) { massСhancellery[i] = mass[i]; }
                    }
                    massMagazine = massMagazine.Where(x => x != null).ToArray();
                    massСhancellery = massСhancellery.Where(x => x != null).ToArray();
                    LoadAllItem?.Invoke(massMagazine);
                    LoadAllItem?.Invoke(massСhancellery);
                }
            }
        }
    }
}
