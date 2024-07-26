using BlazorApp10.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace BlazorApp10.Services
{
    public class WebService : IWebService
    {
        public async Task<ItemValueVo[]> GetItemList()
        {
            HttpClient _httpClient = new HttpClient();
            List<ItemValueVo>? itemList = await _httpClient.GetFromJsonAsync<List<ItemValueVo>>("http://10.101.5.131:8280/SmartConsoleWebService/fa/GetItemValueList");
            if (itemList != null)
            {
                return [.. itemList];
            }
            else
            {
                return [];
            }
        }

        public async Task<bool> AddUpdateItem(ItemValueVo item)
        {
            bool bAddResult = false;
            HttpClient _httpClient = new HttpClient();
            try
            {
                string jsonContent = JsonConvert.SerializeObject(item);
                var formContent = new FormUrlEncodedContent(
                [
                    new KeyValuePair<string,string>("item", jsonContent)
                ]);
                var response = await _httpClient.PostAsync("http://10.101.5.131:8280/SmartConsoleWebService/fa/InsertItemValue", formContent);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                ResultCodeVo? resultCode = JsonConvert.DeserializeObject<ResultCodeVo>(responseBody);
                if (resultCode != null && resultCode.strResultType.ToUpper().Trim() == "SUCCESS")
                {
                    bAddResult = true;
                }
                else
                {
                    bAddResult = false;
                }
                return bAddResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteItem(ItemValueVo item)
        {
            HttpClient _httpClient = new HttpClient();
            bool bDeleteResult = false;
            try
            {
                int index = item.Index;

                var response = await _httpClient.DeleteAsync($"http://10.101.5.131:8280/SmartConsoleWebService/fa/DeleteItemValue/{index}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody != null && responseBody != string.Empty)
                {
                    ResultCodeVo? resultCode = JsonConvert.DeserializeObject<ResultCodeVo>(responseBody);
                    if (resultCode != null)
                    {
                        if (resultCode.strResultType != string.Empty && resultCode.strResultType.ToUpper().Trim() == "SUCCESS")
                        {
                            bDeleteResult = true;
                        }
                    }
                    else
                    {
                        bDeleteResult = false;
                    }
                }
                else
                {
                    bDeleteResult = false;
                }

                return bDeleteResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
