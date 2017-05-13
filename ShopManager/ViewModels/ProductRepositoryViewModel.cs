using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;
using Microsoft.Win32;
using ShopManager.Models;
using ShopManager.ViewModels.Common;

namespace ShopManager.ViewModels
{
    public class ProductRepositoryViewModel
    {
        private ICommand _generateProducts;
        private ICommand _clearProducts;
        private ICommand _saveProducts;
        private ICommand _loadProducts;
        private ICommand _showAll;
        private ICommand _showFiltered;

        public ProductFilterViewModel ProductFilter { get; set; }
        public ObservableCollection<ProductViewModel> Products { get; set; }
        public ObservableCollection<ProductViewModel> FilteredProducts { get; set; }

        public ICommand ShowAll
        {
            get
            {
                if (_showAll == null)
                {
                    _showAll = new RelayCommand(x=>
                    {
                        FilteredProducts.Clear();
                        foreach (var productViewModel in Products)
                        {
                            FilteredProducts.Add(productViewModel);
                        }
                    });
                }

                return _showAll;
            }
        }
        public ICommand ShowFiltered
        {
            get
            {
                if (_showFiltered == null)
                {
                    _showFiltered = new RelayCommand(x =>
                    {
                        int priceFrom = 0;
                        int priceTo = 0;

                        if (ProductFilter.ByPrice)
                        {
                            bool r1 = Int32.TryParse(ProductFilter.PriceFrom, out priceFrom);
                            bool r2 = Int32.TryParse(ProductFilter.PriceTo, out priceTo);

                            if (!(r1 && r2))
                            {
                                MessageBox.Show("Incorrect price values", "Error", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                                return;
                            }
                        }

                        var filteredObj = from obj in Products
                            where (!ProductFilter.ByCategory || obj.Category == ProductFilter.Category)
                                  &&
                                  (!ProductFilter.ByText ||
                                   Regex.IsMatch(obj.Name, $"^{ProductFilter.Text}", RegexOptions.IgnoreCase))
                                  &&
                                  (!ProductFilter.ByPrice ||
                                   (obj.Price > priceFrom && obj.Price < priceTo))
                            select obj;

                        FilteredProducts.Clear();

                        foreach (var productViewModel in filteredObj)
                        {
                            FilteredProducts.Add(productViewModel);
                        }

                    }, x => ProductFilter.ByCategory || ProductFilter.ByPrice || ProductFilter.ByText);
                }
                return _showFiltered;
            }
        }

        public ICommand GenerateProducts
        {
            get
            {
                if (_generateProducts == null)
                {
                    _generateProducts = new RelayCommand(x =>
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Products.Add(new ProductViewModel()
                            {
                                Name = "Apple",
                                Category = Categories.Food,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                                Price = 2.9943,
                            });
                            Products.Add(new ProductViewModel()
                            {
                                Name = "Computer",
                                Category = Categories.Electronics,
                                Description = "Sed do eiusmod tempor incididunt a",
                                Price = 2190.6945,
                            });
                            Products.Add(new ProductViewModel()
                            {
                                Name = "T-shirt",
                                Category = Categories.Clothes,
                                Description = "Ut enim ad minim veniam",
                                Price = 109,
                            });
                        }
                    });
                }

                return _generateProducts;
            }
        }
        public ICommand ClearProducts
        {
            get
            {
                if (_clearProducts == null)
                {
                    _clearProducts = new RelayCommand(x =>
                    {
                        Products.Clear();
                    });
                }

                return _clearProducts;
            }
        }
        public ICommand SaveProducts
        {
            get
            {
                if (_saveProducts == null)
                {
                    _saveProducts = new RelayCommand(x =>
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "XML File|*.xml";
                        saveFileDialog.ShowDialog();
                        if (saveFileDialog.FileName != "")
                        {
                            FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                            var products = from model in Products where (model.Name != null) select model.Model();
                            XmlSerializer xml = new XmlSerializer(typeof(List<Product>));
                            xml.Serialize(fs, products.ToList());
                            fs.Close();
                        }
                    });
                }
                return _saveProducts;
            }
        }
        public ICommand LoadProducts
        {
            get
            {
                if (_loadProducts == null)
                {
                    _loadProducts = new RelayCommand(x =>
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "XML File|*.xml";
                        openFileDialog.ShowDialog();
                        if (openFileDialog.FileName != "")
                        {
                            FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
                            XmlSerializer xml = new XmlSerializer(typeof(List<Product>));
                            var models = (IEnumerable<Product>)xml.Deserialize(fs);
                            fs.Close();
                            foreach (var model in models)
                            {
                                Products.Add(new ProductViewModel(model));
                            }
                        }
                    });
                }
                return _loadProducts;
            }
        }


        public ProductRepositoryViewModel()
        {
            Products = new ObservableCollection<ProductViewModel>();
            Products.CollectionChanged += Products_CollectionChanged;
            FilteredProducts = new ObservableCollection<ProductViewModel>();
            ProductFilter = new ProductFilterViewModel();
        }

        private void Products_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ShowAll.Execute(null);
        }
    }
}
