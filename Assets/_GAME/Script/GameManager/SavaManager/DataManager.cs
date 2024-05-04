using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    private static string destinationPart = "Assets/userData.json";

    //save user data to json fine
    public static void SaveData(UserData userData)
    {
        string jsonData = JsonConvert.SerializeObject(userData);

        File.WriteAllText(destinationPart, jsonData);
    }

    //load data of user to init when start game
    public static UserData LoadData()
    {
        if (File.Exists(destinationPart))
        {
            string jsonData = File.ReadAllText(destinationPart);

            UserData userData = JsonConvert.DeserializeObject<UserData>(jsonData);

            return userData;
        }
        else
        {
            //if not data, init default data
            Debug.Log("__________load default data__________");
            UserData defaultUserData = InitializedDefaultData();
            SaveData(defaultUserData);
            return defaultUserData;
        }
    }

    // default user data
    public static UserData InitializedDefaultData()
    {
        int defaultCoin = Player.currentCoins;
        List<int> defaultListStatusWp = Player.listWP;
        List<int> defaultListStatusHead = Player.listHead;
        List<int> defaultListStatusPant = Player.listPant;
        List<int> defaultListStatusSkin = Player.listSkin;

        UserData defaultUserData = new UserData(defaultCoin, defaultListStatusWp, defaultListStatusHead, defaultListStatusPant, defaultListStatusSkin);

        return defaultUserData;
    }
}
