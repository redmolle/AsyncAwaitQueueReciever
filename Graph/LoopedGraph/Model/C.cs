using Newtonsoft.Json;

namespace HelpLayer.LoopedGraph.Model
{
    [JsonObject(IsReference = true)]
    public class C
    {
        public string Cname { get; set; }
        public A a { get; set; }

        //[JsonConstructor]
        private C() { }

        public C(string name, A _A)
        {
            this.Cname = name;
            this.a = _A;
        }
    }
}
