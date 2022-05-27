using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace pixelook
{
    public class CSVPrefs : IPrefs
    {
        private string prefsFileName = Path.Combine(Application.persistentDataPath, "prefs.csv");
        private Dictionary<string, string> _prefs;

        public string GetString(string key, string defaultValue)
        {
            return ReadPrefs().ContainsKey(key) ? ReadPrefs()[key] : defaultValue;
        }

        public int GetInt(string key, int defaultValue)
        {
            return ReadPrefs().ContainsKey(key) ? int.Parse(ReadPrefs()[key]) : defaultValue;
        }

        public void SetString(string key, string value)
        {
            ReadPrefs()[key] = value;
        }

        public void SetInt(string key, int value)
        {
            ReadPrefs()[key] = value.ToString();
        }

        public void Save()
        {
            Debug.Log("CSVPrefs: save called");
            if (_prefs == null)
                // no need to save
                return;

            Debug.Log("CSVPrefs: writing data");
            StreamWriter sw = new StreamWriter(prefsFileName);

            foreach (KeyValuePair<string, string> pair in _prefs)
            {
                sw.WriteLine($"{pair.Key},{pair.Value}");
            }

            sw.Close();

            Debug.Log("CSVPrefs: save finished");
        }

        private Dictionary<string, string> ReadPrefs()
        {
            if (_prefs == null)
            {
                _prefs = new Dictionary<string, string>();

                StreamReader sr;

                try
                {
                    Debug.Log("CSVPrefs: read called");
                    sr = new StreamReader(prefsFileName);
                }
                catch (FileNotFoundException)
                {
                    Debug.Log("CSVPrefs: file not found");
                    return _prefs;
                }

                while (true)
                {
                    string line = sr.ReadLine();

                    if (line == null)
                        break;

                    if (line == "")
                        continue;

                    string[] items = line.Trim().Split(new char[] {','});

                    // add new value
                    _prefs[items[0]] = items[1];
                }

                sr.Close();

                Debug.Log("CSVPrefs: read finished");
            }

            return _prefs;
        }
    }
}