using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Butik
{
    public class Product
    {
        public string ProductName { get; set; }
        public Double Price { get; set; }
        // tracks the quantity of the items added in the cart
        public int Quantity { get; set; }


        public double CalcultateTotalPrice()
        {
            return Price * Quantity;
        }
    }
}
