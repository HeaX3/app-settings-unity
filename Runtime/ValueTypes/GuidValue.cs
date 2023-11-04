using System;
using Newtonsoft.Json.Linq;

namespace AppSettings
{
    public class GuidValue : SettingValue<Guid>
    {
        public override JToken Serialize()
        {
            return Value.ToString();
        }

        public override void LoadValue(JToken json)
        {
            Value = Guid.TryParse((string) json, out var guid) ? guid : default;
        }
    }
}