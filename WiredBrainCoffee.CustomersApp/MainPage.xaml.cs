using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using Windows.UI.Popups;
using WiredBrainCoffee.CustomersApp.Data_Provider;
using Windows.ApplicationModel;
using WiredBrainCoffee.CustomersApp.Models;
using System.Linq;

namespace WiredBrainCoffee.CustomersApp
{

   public sealed partial class MainPage : Page
    {
      private CustomerDataProvider _customerDataProvider;

      public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
         App.Current.Suspending += App_Suspending;
            _customerDataProvider = new CustomerDataProvider();
        }

      private async void MainPage_Loaded(object sender, RoutedEventArgs e)
      {
         customerListView.Items.Clear();

         var customers = await _customerDataProvider.LoadCustomersAsync();
         foreach (var customer in customers)
         {
            customerListView.Items.Add(customer);
         }
      }

      private async void App_Suspending(object sender, SuspendingEventArgs e)
      {
         await _customerDataProvider.SaveCustomersAsync(
            customerListView.Items.OfType<Customer>());
      }

      private async void ButtonAddCustomer_Click(object sender, RoutedEventArgs e)
      {
         var messageDialog = new MessageDialog("Customer Added!");
         await messageDialog.ShowAsync();
      }

      private void ButtonDeleteCustomer_Click(object sender, RoutedEventArgs e)
      {

      }

      private void ButtonMove_Click(object sender, RoutedEventArgs e)
      {

         //int column = (int)customerListGrid.GetValue(Grid.ColumnProperty);
         int column = Grid.GetColumn(customerListGrid);

         int newColumn = column == 0 ? 2 : 0;

         //customerListGrid.SetValue(Grid.ColumnProperty, newColumn);
         Grid.SetColumn(customerListGrid, newColumn);

         moveSymbolIcon.Symbol = newColumn == 0 ? Symbol.Forward : Symbol.Back;
      }
   }
}
