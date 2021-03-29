using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using MassiveStudio.Test.ClassObject;
using System.IO;
using System.Globalization;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonSerializer serializer = JsonSerializer.CreateDefault();
            serializer.TypeNameHandling = TypeNameHandling.All;


            Base[] data = new Base[]
            {
                new TestObj(),
                new BIbv(),
                new BIbv(),
                new Base(),
                new BIbv(),
                new BIbv(),
                new TestObj(),
            };


            /* using (MemoryStream fs = new MemoryStream())
             {
                 using (StreamWriter writer = new StreamWriter(fs))
                     serializer.Serialize(writer, t);
             }*/
            //string v = JsonConvert.SerializeObject(data);

            Type[] cachedNames = new Type[]
            {
                typeof(Base),
                typeof(TestObj),
                typeof(BIbv),
            };


            StringBuilder sb = new StringBuilder(256);
            StringWriter sw = new StringWriter(sb, CultureInfo.InvariantCulture);
            using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
            {
                serializer.Serialize(jsonWriter, data, cachedNames);
            }
            string v = sb.ToString();

            StringReader sr = new StringReader(v);
            object obj;
            using (JsonTextReader jsonReader = new JsonTextReader(sr))
            {
                obj = serializer.Deserialize(jsonReader);
            }

            Console.WriteLine(v);
        }
    }
}

namespace MassiveStudio.Test.ClassObject
{

    public class TestObj : Base
    {
        public bool a;
        public float f;
    }

    public class BIbv: Base
    {
        public string s;
    }

    public class Base
    {
        public int a;
    }
}