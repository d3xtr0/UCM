using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using LitJson;

namespace UltimateCheatmenu
{
    public class Localization
    {
        private Dictionary<string, string> lndict = new Dictionary<string, string>();
        
        public Localization()
        {
            SetLocale("en");
        }
        
        public string GetText(string id)
        {
            string val = id;

            if (lndict.ContainsKey(id))
            {
                val = lndict[id];
            }
            return val;
        }

        public void SetLocale(string ln)
        {
            string json = LoadLanguage(ln);

            if (json == "")
            {
                ModAPI.Log.Write("L18n: There was an error loading this language ("+ln+"). The file is missing or empty. Are you sure you have an internet connection?");
                return;
            }

            JsonReader reader = new JsonReader(json);

            string key = "";
            string val = "";

            while (reader.Read())
            {
                if (reader.Token == LitJson.JsonToken.PropertyName)
                {
                    key = reader.Value.ToString();
                }
                if (reader.Token == LitJson.JsonToken.String)
                {
                    val = reader.Value.ToString();
                }
                if (key != "" && val != "")
                {
                    val = val.Replace("\\n", "\n");

                    if (!lndict.ContainsKey(key))
                    {
                        lndict.Add(key, val);
                    }
                    else
                    {
                        lndict[key] = val;
                    }
                    key = "";
                    val = "";
                }
            }
        }

        private string LoadLanguage(string ln)
        {
            string json = "";
            string path = "Mods/UCM.l18n." + ln + ".json";

            if (!File.Exists(path))
            {
                return "";
            }
            json = File.ReadAllText(path);
            if (string.IsNullOrEmpty(json))
            {
                return "";
            }

            return json;
        }

        public void DownloadLanguage(string ln)
        {
            byte[] jsonData = null;
            using (WebClient wc = new WebClient())
            {
                ModAPI.Log.Write("Downloading language "+ ln);
                if (ln != "en")
                {
                    try
                    {
                        jsonData = wc.DownloadData("http://modapi.survivetheforest.net/app/configs/mods/UCM/strings." +
                                                   ln + ".json");
                    }
                    catch (WebException e)
                    {
                        if (((HttpWebResponse)e.Response).StatusCode == HttpStatusCode.NotFound)
                        {
                            jsonData = null;
                            ModAPI.Log.Write("L18n: Language " + ln + " not found on the server.");
                        }
                    }
                }
                if (jsonData == null || ln == "en")
                {
                    try
                    {
                        jsonData = wc.DownloadData("http://modapi.survivetheforest.net/app/configs/mods/UCM/strings.json");
                    }
                    catch (WebException e)
                    {
                        if (((HttpWebResponse)e.Response).StatusCode == HttpStatusCode.NotFound)
                        {
                            jsonData = null;
                            ModAPI.Log.Write("L18n: Language en not found on the server.");
                        }
                    }
                }
                if (jsonData != null)
                {
                    string json = Encoding.UTF8.GetString(jsonData);
                    File.WriteAllText("Mods/UCM.l18n." + ln + ".json", json);
                }
            }
        }
    }
}
