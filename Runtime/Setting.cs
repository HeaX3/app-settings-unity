using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace AppSettings
{
    public class Setting<T> : ISetting
    {
        public delegate void ValueChangedEvent(T value);

        public event ValueChangedEvent changed = delegate { };

        private SettingType<T> SettingType { get; }
        private SettingValue<T> ValueContainer { get; }

        public string Key => SettingType.Key;
        public T DefaultValue => SettingType.DefaultValue;

        public T Value
        {
            get => ValueContainer.Value;
            set => ValueContainer.Value = value;
        }

        public Setting(SettingType<T> type)
        {
            SettingType = type;
            ValueContainer = (SettingValue<T>)type.CreateValue();
            ValueContainer.ValueChanged += value => changed(value);
        }

        public JToken Serialize() => ValueContainer.Serialize();

        public void RevertToDefault() => ValueContainer.Value = DefaultValue;

        public void LoadValue(JToken json)
        {
            if (json == null || json.Type is JTokenType.Null or JTokenType.None)
            {
                RevertToDefault();
            }
            else
            {
                ValueContainer.LoadValue(json);
            }
        }
    }
}