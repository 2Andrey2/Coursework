using System.Windows;
using System.Windows.Controls;
using ShopBook.Services.Interface;
using ShopBook.Services;
using ShopBook.Views.Working_form;

namespace ShopBook.Views
{
    /// <summary>
    /// Логика взаимодействия для AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        Working_form_Product worcform;
        TextBox[] massBookObject;
        TextBox[] massMagazineObject;
        TextBox[] massСhancelleryObject;
        IProductsManagement product;
        public AddItem()
        {
            InitializeComponent();
            GrupBook.Visibility = Visibility.Collapsed;
            GrupMagazine.Visibility = Visibility.Collapsed;
            GrupСhancellery.Visibility = Visibility.Collapsed;
            massBookObject = new TextBox[] { AuthorBookT, NameBookT, GenreBookT, ManufacturerBookT, MaterialBookT, StorageBookT, PriceBookT };
            massMagazineObject = new TextBox[] { NameMagazineT, AuthorMagazineT, TopicMagazineT, StorageMagazineT, GenreMagazineT, ManufacturerMagazineT, AudienceMagazineT, PriceMagazineT };
            massСhancelleryObject = new TextBox[] { NameСhancelleryT, StorageСhancelleryT, ManufacturerСhancelleryT, AppointmentСhancelleryT, PriceСhancelleryT };
            worcform = new Working_form_Product();
            product = new ProductsManagement();
        }
        private void AddDataBase(object sender, RoutedEventArgs e)
        {
            product.ProductAction("Created", worcform.data_collection(TypeProduct, massBookObject, massMagazineObject, massСhancelleryObject));
        }

        private void TypeProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FonW.Visibility = Visibility.Hidden;
            ComboBoxItem Items = (ComboBoxItem)e.AddedItems[0];
            if (Items.Content.ToString() == "Книги")
            {
                GrupMagazine.Visibility = Visibility.Collapsed;
                GrupBook.Visibility = Visibility.Visible;
                GrupСhancellery.Visibility = Visibility.Collapsed;
            }
            if (Items.Content.ToString() == "Журнал")
            {
                GrupBook.Visibility = Visibility.Collapsed;
                GrupMagazine.Visibility = Visibility.Visible;
                GrupСhancellery.Visibility = Visibility.Collapsed;
            }
            if (Items.Content.ToString() == "Концелярия")
            {
                GrupBook.Visibility = Visibility.Collapsed;
                GrupMagazine.Visibility = Visibility.Collapsed;
                GrupСhancellery.Visibility = Visibility.Visible;
            }
        }
        private void Delite_Click(object sender, RoutedEventArgs e)
        {
            product.ProductAction("Delete", worcform.data_collection(TypeProduct, massBookObject, massMagazineObject, massСhancelleryObject));
        }

        private void Randome_Click(object sender, RoutedEventArgs e)
        {

            product.ProductAction("Created", worcform.random_data_collection(TypeProduct, massBookObject, massMagazineObject, massСhancelleryObject));
        }
    }
}
