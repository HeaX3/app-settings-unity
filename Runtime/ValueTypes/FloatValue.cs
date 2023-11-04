using Newtonsoft.Json.Linq;

namespace AppSettings
{
    public class FloatValue : SettingValue<float>
    {
        public override JToken Serialize()
        {
            return Value;
        }

        public override void LoadValue(JToken json)
        {
            Value = (float) json;
        }
    }
}