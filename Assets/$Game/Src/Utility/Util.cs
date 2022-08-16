using System;
using System.IO;
using System.Text;
using UnityEngine;

public class Util
{
    /// <summary>
    /// Class for file operations for saving and loading data to and from files
    /// </summary>
    public static class FileOperations
    {
        public static void Save<T>(T[] data, string fileName="",bool encrypt = false)
        {
            string jsonString = JsonHelper.ToJson(data, true);
            //  Save json file
            string nameOfFile = string.IsNullOrEmpty(fileName) ? typeof(T).Name : fileName;
            try
            {
                if (encrypt)
                {
                    File.WriteAllText(GetFilePath(nameOfFile), Security.Base64.Encode(jsonString));
                }
                else
                {
                    File.WriteAllText(GetFilePath(nameOfFile), jsonString);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static T[] Load<T>(string fileName = "", bool isEncrypted = false)
        {
            T[] data = default(T[]);

            try
            {
                string jsonString = string.Empty;   //  TODO read json data
                if (isEncrypted)
                {
                    jsonString = Security.Base64.Decode(GetFilePath(fileName));
                }
                else
                {
                    jsonString = File.ReadAllText(GetFilePath(fileName));
                }

                data = JsonHelper.FromJson<T>(jsonString);
            }
            catch (Exception e)
            {

                throw;
            }
            return data;
        }

        public static string GetFilePath(string fileToSave)
        {
            return Application.streamingAssetsPath + "/" + fileToSave;
        }
    }

    public static class DataOperations
    {
        public static void Save<T>(T[] data, string key = "")
        {
            StringBuilder sbKey = new StringBuilder();
            sbKey.Append(typeof(T).Name);
            if(!string.IsNullOrEmpty(key))
            {
                sbKey.Append(key);
            }
            string jsonString = JsonHelper.ToJson(data);
            PlayerPrefs.SetString(sbKey.ToString(), jsonString);
        }

        public static T[] Load<T>(string key = "")
        {
            StringBuilder sbSavedKey = new StringBuilder();
            sbSavedKey.Append(typeof(T).Name);
            sbSavedKey.Append(key);
            T[] data = default(T[]);
            string jsonString = string.Empty;   //  TODO read json data

            if(PlayerPrefs.HasKey(sbSavedKey.ToString()))
            {
                jsonString = PlayerPrefs.GetString(sbSavedKey.ToString());
                data = JsonHelper.FromJson<T>(jsonString);
            }
            return data;
        }
    }
}
