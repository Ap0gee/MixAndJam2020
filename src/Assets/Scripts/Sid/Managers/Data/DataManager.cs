using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using GameJam.Managers._Data;

//TODO: Player Prefs?
namespace GameJam.Managers
{
    public class DataManager : MonoBehaviour
    {
        private static DataManager instance;
        
        private GameSave saveData;

        public struct PrefNames
        {
            public static string Resolution
            {
                get { return "Resolution"; }
            }
        }

        private static GameSave SaveData
        {
            get { return instance.saveData; }
            set { instance.saveData = value; }
        }

        public static bool HasSaveData 
        {
            get { return SaveData != null; }
        }

        public static string DataPath
        {
            //%userprofile%\AppData\LocalLow\<CompanyName>\<ProductName>\FileSaveGame
            get { return Path.Combine(Application.persistentDataPath, Config.FileSaveGame); }
        }

        public static void ResetSaveData()
        {
            SaveData = new GameSave
            {
                dateTimeStarted = DateTime.Now
            };
        }

        public static void SaveGame()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileIO = File.Open(DataPath, FileMode.Create);
            bf.Serialize(fileIO, SaveData);
            fileIO.Close();
        }

        public static bool LoadGame()
        {
            if (File.Exists(DataPath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fileIO = File.Open(DataPath, FileMode.Open);
                SaveData = (GameSave)bf.Deserialize(fileIO);
                fileIO.Close();
            }

            return SaveData != null;
        }

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;

            }
            else if (instance != this)
            {
                Destroy(this);
            }
        }

        protected void OnDestroy()
        {
            if (instance != null)
            {
                instance = null;
            }
        }
    }
}