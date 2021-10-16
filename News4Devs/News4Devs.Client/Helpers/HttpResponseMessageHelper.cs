using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace News4Devs.Client.Helpers
{
    public static class HttpResponseMessageHelper
    {
        public async static Task<T> DeserializeContentAsync<T>(HttpResponseMessage response)
        {
            string responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseJson);
        }
    }
}
