using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManager.Models;

namespace ShopManager.ViewModels
{
    public class ProductViewModel
    {
        private Product product;

        public string Name
        {
            get { return product.Name; }
            set { product.Name = value; }
        }

        public string Description
        {
            get { return product.Description; }
            set { product.Description = value; }
        }

        public double Price
        {
            get { return product.Price; }
            set { product.Price = value; }
        }

        public Categories Category
        {
            get { return product.Category; }
            set { product.Category = value; }
        }

        public Product Model()
        {
            return product;
        }

        public ProductViewModel()
        {
            product = new Product();
        }

        public ProductViewModel(Product p)
        {
            product = p;
        }
    }
}
