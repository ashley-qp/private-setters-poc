using System;
using Newtonsoft.Json;

Run();

static void Run()
{
    var obj = new PrivateSettersClass();
    obj.ChangeValues();

    var serialized = JsonConvert.SerializeObject(obj);
    var deserialized = JsonConvert.DeserializeObject<PrivateSettersClass>(serialized);

    Console.WriteLine($"{deserialized.Prop1} {deserialized.Prop2}");
}

public class PrivateSettersClass
{
    [JsonProperty] public string Prop1 { get; private set; }
    [JsonProperty] public string Prop2 { get; private set; }

    public void ChangeValues()
    {
        Prop1 = "Property";
        Prop2 = "Attribute";
    }
}
