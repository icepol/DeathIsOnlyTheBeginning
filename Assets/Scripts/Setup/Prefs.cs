namespace pixelook
{
    interface IPrefs
    {
        string GetString(string key, string defaultValue);
        int GetInt(string key, int defaultValue);

        void SetString(string key, string value);
        void SetInt(string key, int value);

        void Save();
    }
}