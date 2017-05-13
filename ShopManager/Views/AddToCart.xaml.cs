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
using ShopManager.Models;

namespace ShopManager.Views
{
    /// <summary>
    /// Interaction logic for AddToCart.xaml
    /// </summary>
    public partial class AddToCart : Window
    { 
        bool confirmed;

        public AddToCart()
        {
            InitializeComponent();
        }


        public static int Show(Product product)
        {
            AddToCart windows = new AddToCart();
            windows.quantity.Text = 1.ToString();
            windows.productName.Text = product.Name;
            windows.ShowDialog();

            if (!windows.confirmed)
                return -1;

            int result;
            if (!Int32.TryParse(windows.quantity.Text, out result) || result<=0)
            {
                MessageBox.Show("Wrong input");
                return -1;
            }

            return result;
        }

        private void CancelClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddClicked(object sender, RoutedEventArgs e)
        {
            confirmed = true;
            Close();
        }
    }
}
