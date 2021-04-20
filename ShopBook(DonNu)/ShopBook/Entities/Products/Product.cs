using ShopBook.Entities.Products.Interface;
using System;

namespace ShopBook.Entities.Products
{
    [Serializable]
    class Product: IProduct
    {
        protected int Namber;
        protected string Name;
        public string Storage { get; protected set; }
        protected string Manufacturer;
        protected double Price;
        public string[] Maptemp { get; protected set; }
        public string Type { get; protected set; }
        public void Sale(int Namber, int Quantity) { }
    }
}
