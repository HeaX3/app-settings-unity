using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace AppSettings
{
    public interface ISetting
    {
        string Key { get; }
        void LoadValue([CanBeNull] JToken json);
        JToken Serialize();
        void RevertToDefault();
    }
}