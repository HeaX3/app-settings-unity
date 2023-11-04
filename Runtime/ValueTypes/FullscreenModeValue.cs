using Newtonsoft.Json.Linq;
using UnityEngine;

namespace AppSettings
{
    public class FullscreenModeValue : SettingValue<FullScreenMode>
    {
        public override JToken Serialize()
        {
            return (Value) switch
            {
                FullScreenMode.Windowed => "window",
                FullScreenMode.MaximizedWindow => "maximized",
                FullScreenMode.FullScreenWindow => "fullscreen_window",
                _ => "fullscreen"
            };
        }

        public override void LoadValue(JToken json)
        {
            Value = (string)json switch
            {
                "window" => FullScreenMode.Windowed,
                "maximized" => FullScreenMode.MaximizedWindow,
                "fullscreen_window" => FullScreenMode.FullScreenWindow,
                _ => FullScreenMode.ExclusiveFullScreen
            };
        }
    }
}