using Newtonsoft.Json.Linq;

namespace AppSettings
{
    public class IntValue : SettingValue<int>
    {
        public override JToken Serialize()
        {
            return Value;
        }

        public override void LoadValue(JToken json)
        {
            Value = (int) json;
        }
    }
}