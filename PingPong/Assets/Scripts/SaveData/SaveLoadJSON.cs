using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

public class SaveLoadJSON : MonoBehaviour {

    private static SaveLoadJSON _instance;
    public static SaveLoadJSON Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null)
            return;
        _instance = this;
    }

    public bool SaveLeaderboard(LeaderBoardData _data)
    {
        string _path = GetLeaderboardPath();
        string strJson = JsonConvert.SerializeObject(_data);        
        {
            Debug.Log("file name : " + _path);
            Debug.Log("SaveCharacterLocation JSON data : " + strJson);
        }        
        File.WriteAllText(_path, strJson);

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
        return true;
    }

    public LeaderBoardData LoadLeaderboard()
    {
        string _path = GetLeaderboardPath();
        LeaderBoardData leaderboard = null;
        if (File.Exists(_path))
        {            
            string dataAsJson = File.ReadAllText(_path);            
            {
                Debug.Log("file name : " + _path);
                Debug.Log("LeaderBoardData JSON data : " + dataAsJson);
            }
            leaderboard = JsonConvert.DeserializeObject<LeaderBoardData>(dataAsJson);
        }

        return leaderboard;
    }

    private string GetLeaderboardPath()
    {
        string _path = string.Empty;
#if UNITY_EDITOR
        _path = "Assets/Resources/Data/LeaderboardJSON.json";
#elif UNITY_STANDALONE
        _path = Application.dataPath + "/StreamingAssets/LeaderboardJSON.json";
#elif UNITY_IOS || UNITY_ANDROID || UNITY_IPHONE
        _path = Application.persistentDataPath + "PingPong_Data/Resources/Data/LeaderboardJSON.json";
#endif  
        return _path;
    }
}
