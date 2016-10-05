using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main()
        {
            string serializationtype = Console.ReadLine();
            string obj = Console.ReadLine();

            //string serializationtype = "Json";
            //string obj = "{\"K\":10,\"Sums\":[1.01,2.02],\"Muls\":[1,4] }";

            //string serializationtype = "Xml";
            //string obj = "<Input><K>10</K><Sums><decimal>1.01</decimal><decimal>2.02</decimal></Sums><Muls><int>1</int><int>4</int></Muls></Input>";

            ISerializer serializer = null;
            if (Equals(serializationtype, "Json"))
            {
                serializer = new JsonSerializer();
            }
            else if (Equals(serializationtype, "Xml"))
            {
                serializer = new XmlSerializer();
            }
            var input = serializer.Deserialize<Input>(obj);

            var output = new Output();
            foreach (var i in input.Sums)
            {
                output.SumResult += i;
            }
            output.SumResult *= input.K;

            output.MulResult = 1;
            foreach (var i in input.Muls)
            {
                output.MulResult *= i;
            }

            var mas = input.Sums.ToList();
            foreach (var i in input.Muls)
            {
                mas.Add(i);
            }
            output.SortedInputs = mas.OrderBy(x => x).ToArray();
            Console.WriteLine(serializer.Serializer(output).Replace(Environment.NewLine, "").Replace(" ", "").Replace("\t", ""));
            //Console.ReadLine();
        }
    }
}
