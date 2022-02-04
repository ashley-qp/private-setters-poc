// See https://aka.ms/new-console-template for more information

using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Formatting = Formatting.Indented,
    ContractResolver = new CustomPrivateSetterResolver()
};

Run();


static void Run()
{
    var obj = new PrivateSettersClass();
    obj.ChangeValues();

    var serialized = JsonConvert.SerializeObject(obj);
    var deserialized = JsonConvert.DeserializeObject<PrivateSettersClass>(serialized);

    Console.WriteLine($"{deserialized.Prop1} {deserialized.Prop2}");
}

class CustomPrivateSetterResolver : CamelCasePropertyNamesContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);
        if (!prop.Writable)
        {
            var property = member as PropertyInfo;
            var hasPrivateSetter = property?.GetSetMethod(true) != null;
            prop.Writable = hasPrivateSetter;
        }

        return prop;
    }
}

public class PrivateSettersClass
{
    public string Prop1 { get; private set; }
    public string Prop2 { get; private set; }

    public void ChangeValues()
    {
        Prop1 = "Custom";
        Prop2 = "Resolver";
    }
}
