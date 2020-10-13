using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WiredBrainCoffee.CustomersApp.Models;

namespace WiredBrainCoffee.CustomersApp.Data_Provider
{
   public class CustomerDataProvider
   {
      private static readonly string _customesFileName = "customers.json";
      private static readonly StorageFolder _localFolder = ApplicationData.Current.LocalFolder;

      public async Task<IEnumerable<Customer>> LoadCustomersAsync()
      {
         var storageFile = await _localFolder.TryGetItemAsync(_customersFileName) as StorageFile;
         List<Customer> customerList = null;

         if (storageFile == null)
         {
            customerList = new List<Customer>
            {
               new Customer{FirstName="Thomas", LastName="Huber", IsDeveloper=true},
               new Customer{FirstName="Anna", LastName="Rockstar", IsDeveloper=true},
               new Customer{FirstName="Julia", LastName="Master"},
               new Customer{FirstName="Urs", LastName="Meier", IsDeveloper=true},
               new Customer{FirstName="Sarah", LastName="Ramone"},
               new Customer{FirstName="Elda", LastName="Queen"},
               new Customer{FirstName="Alex", LastName="Baier", IsDeveloper=true}
            };
         }
         else
         {
            using (var stream = await storageFile.OpenAsync(FileAccessMode.Read))
            {
               await dataReader.LoadAsync((unit)stream.Size);
               var json = dataReader.ReadString((unit)stream.Size);
               customerList = JsonConvert.DeserializeObject<List<Customer>>(json);
            }
         }
      }
      return customerList;
   }

   public async Task SaveCustomersAsync(IEnumerable<Customer> customers)
   {
      var storageFile = await _localFolder.CreateFileAsync(_customersFileName, CreateCollisionOption.ReplaceExisting)

      using (var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
      {
         using (var dataWriter = new DataWriter(stream))
         {
            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);
            dataWriter.WriteString(json);
            await dataWriter.StoreAsync();
         }
      }
      
   }
}
