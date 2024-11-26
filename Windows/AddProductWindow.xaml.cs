using System.Windows;

namespace AuctionManagerApp
{
    public partial class AddProductWindow : Window
    {
        private readonly MainWindow _mainWindow;

        // Constructor accepting a reference to the MainWindow
        public AddProductWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            // Handle Product Name Watermark
            ProductNameTextBox.GotFocus += (s, e) =>
            {
                ProductNameWatermark.Visibility = Visibility.Collapsed;
            };
            ProductNameTextBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrEmpty(ProductNameTextBox.Text))
                {
                    ProductNameWatermark.Visibility = Visibility.Visible;
                }
            };

            // Handle Product Description Watermark
            ProductDescriptionTextBox.GotFocus += (s, e) =>
            {
                ProductDescriptionWatermark.Visibility = Visibility.Collapsed;
            };
            ProductDescriptionTextBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrEmpty(ProductDescriptionTextBox.Text))
                {
                    ProductDescriptionWatermark.Visibility = Visibility.Visible;
                }
            };
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ProductNameTextBox.Text) || string.IsNullOrEmpty(ProductDescriptionTextBox.Text))
            {
                MessageBox.Show("Please enter both product name and description.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string productName = ProductNameTextBox.Text;
            string productDescription = ProductDescriptionTextBox.Text;

            // Simulate adding a product (replace with actual logic)
            MessageBox.Show($"Product '{productName}' added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Close the window after adding the product
            this.Close();
            _mainWindow.Show(); // Show the MainWindow after closing this window
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the current window
            _mainWindow.Show(); // Show the MainWindow
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close without adding product
            _mainWindow.Show(); // Show the MainWindow
        }
    }
}
