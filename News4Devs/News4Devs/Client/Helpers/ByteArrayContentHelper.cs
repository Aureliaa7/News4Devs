using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace News4Devs.Client.Helpers
{
    public static class ByteArrayContentHelper
    {
        public static ByteArrayContent ConvertToByteArrayContent<T>(T objectToBeConverted)
        {
            var serializedData = JsonConvert.SerializeObject(objectToBeConverted);
            var bytes = Encoding.UTF8.GetBytes(serializedData);
            var byteArrayContent = new ByteArrayContent(bytes);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(ClientConstants.ContentType);

            return byteArrayContent;
        }
    }
}
