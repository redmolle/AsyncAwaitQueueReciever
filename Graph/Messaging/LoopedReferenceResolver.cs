//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
//using HelpLayer.LoopedGraph.Model;

//public class LoopedReferenceResolver : IReferenceResolver
//{
//    private readonly IDictionary<string, A> _objects =
//        new Dictionary<string, A>();

//    public object ResolveReference(object context, string reference)
//    {
//        A p;
//        if (_objects.TryGetValue(reference, out p))
//        {
//            //This is the "clever" bit. Instead of returning the found object
//            //we just return a copy of it.
//            //May be better to clone your class here...
//            return new A
//            {
                
//            };
//        }

//        return null;
//    }

//    public string GetReference(object context, object value)
//    {
//        Person p = (Person)value;
//        _objects[p.Name] = p;

//        return p.Name;
//    }

//    public bool IsReferenced(object context, object value)
//    {
//        Person p = (Person)value;

//        return _objects.ContainsKey(p.Name);
//    }

//    public void AddReference(object context, string reference, object value)
//    {
//        _objects[reference] = (Person)value;
//    }
//}