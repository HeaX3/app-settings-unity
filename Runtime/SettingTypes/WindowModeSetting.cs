using UnityEngine;

namespace AppSettings
{
    public class WindowModeSetting : SettingType<FullScreenMode>
    {
        public WindowModeSetting(string key) : base(key)
        {
        }

        public override FullScreenMode DefaultValue => FullScreenMode.ExclusiveFullScreen;

        protected override SettingValue<FullScreenMode> CreateValueInstance()
        {
            return new FullscreenModeValue();
        }

        public override void Apply(FullScreenMode value)
        {
            base.Apply(value);
#if !UNITY_WEBGL || UNITY_EDITOR
            Screen.fullScreenMode = value;
#endif
        }
    }
}