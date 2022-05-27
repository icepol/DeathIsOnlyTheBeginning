namespace pixelook
{
    public class Settings
    {
        const string SFX_ENABLED = "sfx_enabled";
        private const string MUSIC_ENABLED = "music_enabled";

# if UNITY_WEBGL
        static IPrefs _prefs = new CSVPrefs();
# else
        static IPrefs _prefs = new UnityPrefs();
#endif

        public static bool IsSfxEnabled
        {
            get => _prefs.GetInt(SFX_ENABLED, 1) == 1;

            set
            {
                _prefs.SetInt(SFX_ENABLED, value ? 1 : 0);
                _prefs.Save();
            }
        }

        public static bool IsMusicEnabled
        {
            get => _prefs.GetInt(MUSIC_ENABLED, 1) == 1;

            set
            {
                _prefs.SetInt(MUSIC_ENABLED, value ? 1 : 0);
                _prefs.Save();
            }
        }
    }
}