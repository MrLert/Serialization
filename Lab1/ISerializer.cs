using System.Runtime.InteropServices.ComTypes;

namespace Lab1
{
    interface ISerializer
    {
        string Serializer<T>(T obj);
        T Deserialize<T>(string bytes);
    }
}