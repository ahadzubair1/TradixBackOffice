using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Services;

public class NewtonsoftService : ISerializerService
{
    public T Deserialize<T>(string text)
    {
        return JsonConvert.DeserializeObject<T>(text);
    }

    public string Serialize<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter() { CamelCaseText = true }
            }
        });
    }

    public string Serialize<T>(T obj, Type type)
    {
        return JsonConvert.SerializeObject(obj, type, new());
    }



    //public TData DeserializeByteArray<TData>(byte[] arr)
    //{
    //    var data = Encoding.UTF8.GetString(arr);
    //    return JsonConvert.DeserializeObject<TData>(data);
    //}

    //public byte[] Serialize<TData>(TData data)
    //{
    //    var json = JsonConvert.SerializeObject(data);
    //    return Encoding.UTF8.GetBytes(json);
    //}
}