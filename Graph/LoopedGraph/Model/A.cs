using Newtonsoft.Json;

namespace HelpLayer.LoopedGraph.Model
{
    [JsonObject(IsReference = true)]
    public class A
    {
        public string name { get; set; }


        public C c1 { get; set; }
        public C c2 { get; set; }

        //[JsonConstructor]
        public A() { name = "my new A"; }

        public A(string name)
        {
            this.name = name;
            this.c1 = new C("C1", this);
            this.c2 = new C("C2", new A { name = "Empty A" });
        }
    }
}
