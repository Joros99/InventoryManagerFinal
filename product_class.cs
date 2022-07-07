using System;
using System.Collections.Generic;
using System.Text;

namespace RaktarKezeloDiploma
{
    public class product_class
    {
        int id;
        string product_name;
        int quantity;
        int price;
        public product_class() { }
        public string Name {
            get { return product_name; }
            set { product_name = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public int Price {
            get { return price; }
            set { price = value; }
        }
        
        public product_class(int id0, string product_name0, int quantity0, int price0)
        {
            this.id = id0;
            this.product_name = product_name0;
            this.quantity = quantity0;
            this.price = price0;
        }
        ~product_class() { }
    }
}
