using ShopManager.ViewModels.Common;

namespace ShopManager.ViewModels
{
    public class ProductInCartViewModel : NotifyPropertyChanged
    {
        private int _quantity;
        public ProductViewModel Product { get; set; }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value; 
                OnPropertyChanged("Quantity");
            }
        }
    }
}
