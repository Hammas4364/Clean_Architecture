using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace SharedKernel.Helpers;

internal class IgnoreEmptyItemsConverter<T> : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType.IsAssignableFrom(typeof(List<T>));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var list = new List<T>();
        var array = JArray.Load(reader);
        foreach (var obj in array.Children<JObject>())
        {
            if (obj.HasValues)
                list.Add(obj.ToObject<T>(serializer));
        }
        return list;
    }

    public override bool CanWrite
    {
        get { return false; }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
