using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace IOTLightsUniversal.DataModel
{

    public class AzureDataItem
    {
        public string UniqueID { get; set; }
        public string DeviceName { get; set; }
        public string Location { get; set; }
        public bool IOState { get; set; }
        public double Uptime { get; set; }
        public double Cost { get; set; }

        public AzureDataItem(String uniqueId, String deviceName, String location, bool ioState, double uptime, double cost)
        {
            this.UniqueID = uniqueId;
            this.DeviceName = deviceName;
            this.Location = location;
            this.IOState = ioState;
            this.Uptime = uptime;
            this.Cost = cost;
        }

        public override string ToString()
        {
            return this.DeviceName;
        }
    }

    //Not including classes SampleDataGroup



    public class AzureDataSource
    {
        private static AzureDataSource _azureDataSource = new AzureDataSource();

        private ObservableCollection<AzureDataItem> _dataitems = new ObservableCollection<AzureDataItem>();
        public ObservableCollection<AzureDataItem> DataItems
        {
            get { return this._dataitems; }
        }

        public static async Task<IEnumerable<AzureDataItem>> GetDataItemsAsync()
        {
            await _azureDataSource.GetAzureDataAsync();

            return _azureDataSource.DataItems;
        }

        public static async Task<AzureDataItem> GetDataItemAsync(string uniqueID)
        {
            await _azureDataSource.GetAzureDataAsync();
            var matches = _azureDataSource.DataItems.Where((group) => group.UniqueID.Equals(uniqueID));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public async Task<string> GetjsonStream()
        {
            HttpClient client = new HttpClient();
            string url = "http://joelbraun.azurewebsites.net/IOT/IOTData.html";
            HttpResponseMessage response = await client.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            Debug.WriteLine("Content: " + content);
            return content; 
        }

        public async Task GetAzureDataAsync()
        {
            if (this._dataitems.Count != 0)
            {
                return;
            }

            string jsonText = await GetjsonStream();
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Items"].GetArray();
            
            foreach (JsonValue dataitemValue in jsonArray)
            {
                JsonObject dataitemObject = dataitemValue.GetObject();
                AzureDataItem dataItem = new AzureDataItem(dataitemObject["UniqueID"].GetString(), 
                                                           dataitemObject["DeviceName"].GetString(), 
                                                           dataitemObject["Location"].GetString(), 
                                                           dataitemObject["IOState"].GetBoolean(), 
                                                           dataitemObject["Uptime"].GetNumber(), 
                                                           dataitemObject["Cost"].GetNumber());
                this.DataItems.Add(dataItem);
            }

        }
     
      
    }
}
