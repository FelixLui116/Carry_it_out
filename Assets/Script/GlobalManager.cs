using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GlobalManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource sfxMusic;

    public static GlobalManager Instance {get; private set;}
    public MusicController musicController;    
    // private Color [] playerColors = new Color[4] {Color.red, Color.blue, Color.green, Color.yellow};

    private void Awake() {
        /////// set it to DontDestroy keep Object to next scene
        if (Instance == null) {
            DontDestroyOnLoad(gameObject); // keep GlobaManager
            
            DontDestroyOnLoad(musicController);
            // DontDestroyOnLoad(backgroundMusic);
            // DontDestroyOnLoad(sfxMusic);
            Instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //// load JSON when the game start
        // LoadJson();
        
        // GameObject clonedObject = Instantiate(Prefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //// set Dictionary to level number to string
    // public Dictionary<int, string> levelDescriptions = new Dictionary<int, string>
    // {
    //     { 0, "Rookie" },
    //     { 1, "Pro" },
    //     { 2, "Expert" },
    //     { 3, "Masters" }
    // };   
    
    // //// set Dictionary level entry Coin need 
    // public Dictionary<int, int> entryCoin = new Dictionary<int, int>
    // {
    //     { 0, 1},
    //     { 1, 5},
    //     { 2, 10},
    //     { 3, 20}
    // };
    // //// set Dictionary level entry Req_Min need 
    // public Dictionary<int, int> entryReq_Min = new Dictionary<int, int>
    // {
    //     { 0, 1 },
    //     { 1, 20},
    //     { 2, 40},
    //     { 3, 50}
    // };
    // //// set Dictionary level entry Req_Max need 
    // public Dictionary<int, int> entryReq_Max = new Dictionary<int, int>
    // {
    //     { 0, 100 },
    //     { 1, 200},
    //     { 2, 400},
    //     { 3, 500}
    // };

    // Load Scene Async
    public void LoadSceneAsync(string sceneName) {
        Debug.Log("Loading Scene: " + sceneName);
        SceneManager.LoadSceneAsync(sceneName , LoadSceneMode.Single); 
        // SceneManager.LoadSceneAsync(sceneName); 
    }
    


    /// <summary>
    /// //////////////////////////////////////////// JSON ////////////////////////////////////////////
    /// </summary>

    [SerializeField] public Data _Data = new Data();
    private string path = "/JSON/PlayerDate.json"; // JSON path file

    private string GetPath()    // custom function to get path
    {   
        string path_ = Path.Combine(Application.dataPath + path);
        Debug.Log("path" +path_);
        return path_;
    }

    public void SaveIntoJson(){
        // Debug.Log( "SaveIntoJson : "+ _Data.playerlist[0].playerName);
        // Debug.Log( "SaveIntoJson : "+ _Data.playerlist[0].playerID);

        string potion = JsonUtility.ToJson(_Data);
        System.IO.File.WriteAllText(Application.dataPath + path, potion);
        // System.IO.File.WriteAllText(Application.dataPath+"/JSON/SaveDate.json", potion);
    }

     public void LoadJson()
    {
        string JSONpath = GetPath();
        Debug.Log("JSONpath: " + JSONpath);

        string json = File.ReadAllText( JSONpath ); // read JSON
        _Data = JsonUtility.FromJson<Data>(json);   // JSON to readable data in unity
        Debug.Log("_Data: " + _Data);
        // Debug.Log( "LoadJson : "+ _Data.playerlist[0].playerName);
        // Debug.Log( "LoadJson : "+ _Data.playerlist[0].playerID);
        // foreach (InfoData player in _Data.playerlist)
        // {
        //     Debug.Log("Player ID: " + player.playerID);
        //     Debug.Log("Player Name: " + player.playerName);
        //     Debug.Log("Player Score: " + player.playerScoure);
        //     Debug.Log("Player Level: " + player.playerLevel);
        //     Debug.Log("Player Coin: " + player.playerCoin);
        // }
    }
    
    // All player list info
    [System.Serializable]
    public class Data{
        public List<InfoData> playerlist = new List<InfoData>();
    }

    [System.Serializable]    // player info data value
    public class InfoData{
        public int playerID;  // unique ID - like UserID
        public string playerName;
        public int playerScoure;
        public int playerLevel;
        public int playerCoin;  
        public List<int> RecordScoure = new List<int>();
    }

    // save data 
    [System.Serializable]
    public class PlayerData{
        public int playerID;
        public int playerScoure;
        public int playerLevel;
        public int playerCoin;

    }

    // setup new data
    /*
    InfoData newPlayer = new InfoData
    {
        playerID = 1,
        playerName = "ABC",
        playerScoure = 100,
        playerLevel = 2,
        playerCoin = 100,
        RecordScoure = new List<int> { 0, 0, 0 }  // Initialize RecordScoure
    };
    */

}
