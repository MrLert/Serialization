using Newtonsoft.Json;

namespace Lab1
{
    class JsonSerializer : ISerializer
    {
        public string Serializer<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string bytes)
        {
            return JsonConvert.DeserializeObject<T>(bytes);
        }
    }
}