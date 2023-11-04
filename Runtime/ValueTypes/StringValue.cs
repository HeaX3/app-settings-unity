using Newtonsoft.Json.Linq;

namespace AppSettings
{
    public class StringValue : SettingValue<string>
    {
        public override JToken Serialize()
        {
            return Value;
        }

        public override void LoadValue(JToken json)
        {
            Value = (string) json;
        }
    }
}