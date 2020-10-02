﻿using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{

    /// <summary>
    /// For the most part, don't have to change anything here over games, just manages data
    /// Doesn't have to be placed in scenes
    /// </summary>

    public static void SaveData()
    {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.sav";

        //Save to high score later
        PlayerData data = StatsManager.instance.ExportPlayerData();

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static PlayerData LoadData()
    {

        string path = Application.persistentDataPath + "/player.sav";

        if (!File.Exists(path)) { return null; }

        FileStream stream = new FileStream(path, FileMode.Open);
        stream.Position = 0;
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path) && stream.Length > 0)
        {

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            //We create a new save file
            return null;
        }

    }



}