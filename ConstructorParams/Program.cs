// This example breaks if you have an empty constructor, which is problematic

using System;
using Newtonsoft.Json;

Run();


static void Run()
{
    var obj = new PrivateSettersClass(string.Empty, string.Empty);
    obj.ChangeValues();

    var serialized = JsonConvert.SerializeObject(obj);
    var deserialized = JsonConvert.DeserializeObject<PrivateSettersClass>(serialized);

    Console.WriteLine($"{deserialized.Prop1} {deserialized.Prop2}");
}

public class PrivateSettersClass
{
    public PrivateSettersClass(string prop1, string prop2)
    {
        Prop1 = prop1;
        Prop2 = prop2;
    }

    public string Prop1 { get; private set; }
    public string Prop2 { get; private set; }

    public void ChangeValues()
    {
        Prop1 = "Constructor";
        Prop2 = "Param";
    }
}
