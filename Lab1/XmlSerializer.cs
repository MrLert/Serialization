using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Lab1
{
    class XmlSerializer : ISerializer
    {
        private readonly XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
        {
            OmitXmlDeclaration = true,
            Encoding = new UTF8Encoding(false),
            Indent = true,
        };
        private readonly ConcurrentDictionary<Type, System.Xml.Serialization.XmlSerializer> serializers =
            new ConcurrentDictionary<Type, System.Xml.Serialization.XmlSerializer>();
        private readonly XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();

        public XmlSerializer()
        {
            xmlSerializerNamespaces.Add("", "");
        }

        public System.Xml.Serialization.XmlSerializer GetSerializer<T>()
        {
            return serializers.GetOrAdd(typeof(T),
                type => new System.Xml.Serialization.XmlSerializer(type, new XmlAttributeOverrides()));
        }
        public string Serializer<T>(T obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                GetSerializer<T>().Serialize(XmlWriter.Create(memoryStream, xmlWriterSettings),
                                                obj, xmlSerializerNamespaces);

                var bytes = memoryStream.ToArray();
                return Encoding.UTF8.GetString(bytes);
            };
        }

        public T Deserialize<T>(string bytes)
        {
            var xml = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var byteArray = Encoding.UTF8.GetBytes(bytes);

            using (var memoryStream = new MemoryStream(byteArray))
            {
                return (T)xml.Deserialize(memoryStream);
            }
        }
    }
}