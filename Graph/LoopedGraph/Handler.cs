using System;
using Newtonsoft.Json;
using HelpLayer.LoopedGraph.Model;

namespace HelpLayer.LoopedGraph
{
    public static class Handler
    {
        public static A InitA(string name)
        {
            //A a = new A
            //{
            //    name = name,
            //    c1 = new C
            //    {
            //        Cname = "c1"
            //    },
            //    c2 = new C
            //    {
            //        Cname = "c2"
            //    }
            //};
            //a.c1.a = a;
            //a.c2.a = new A { name = "empty A" };

            return new A(name);
        }

        public static A SwopC(A _A)
        {
            A tmpA1 = _A.c1.a;
            A tmpA2 = _A.c2.a;
            _A.c1.a = tmpA2;
            _A.c2.a = tmpA1;

            return _A;
        }

        public static string Serialize(object o)
        {
            return
                JsonConvert.SerializeObject(o, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    Formatting = Formatting.Indented
                });
        }

        public static A AFromJson(string json)
        {
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects, ReferenceLoopHandling = ReferenceLoopHandling.Serialize };

            A a =

                string.IsNullOrEmpty(json) ? null :
                JsonConvert.DeserializeObject<A>(json);

            return a;
        }
    }
}
