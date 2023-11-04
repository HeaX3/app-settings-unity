using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace AppSettings
{
    public sealed class ResolutionSetting : SettingType<string>
    {
        public static readonly ResolutionSetting instance = new("resolution");

        public override string DefaultValue => GetResolutionString(DefaultResolution);

        private Resolution DefaultResolution =>
            Screen.resolutions.OrderByDescending(r => r.width * r.height * r.refreshRate).FirstOrDefault();

        private string GetResolutionString(Resolution resolution)
        {
            return resolution.width + "x" + resolution.height + "@" + resolution.refreshRate;
        }

        private ResolutionSetting(string key) : base(key)
        {
        }

        protected override SettingValue<string> CreateValueInstance()
        {
            return new StringValue();
        }

        public override void Apply(string value)
        {
            base.Apply(value);
            #if !UNITY_WEBGL || UNITY_EDITOR
            var match = Regex.Match(value, "^([0-9]+) ?x ?([0-9]+)(?:@([0-9]+))?$");
            try
            {
                var width = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                var height = int.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
                var refreshRate = match.Groups[3].Success
                    ? int.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture)
                    : 0;
                if (Screen.resolutions.OrderByDescending(r => r.refreshRate).All(r =>
                        r.width != width || r.height != height || (refreshRate > 0 && r.refreshRate != refreshRate)))
                {
                    Debug.LogWarning("This screen doesn't support the resolution '" + value +
                                     "'. Using default resolution instead.");
                    var resolution = DefaultResolution;
                    width = resolution.width;
                    height = resolution.height;
                    refreshRate = resolution.refreshRate;
                }

                var mode = Screen.fullScreenMode;
                if (!Application.isEditor)
                {
                    Screen.SetResolution(width, height, mode, refreshRate);
                }
            }
            catch
            {
                Debug.LogError("Couldn't apply resolution '" + value + "'!");
            }
            #endif
        }
    }
}