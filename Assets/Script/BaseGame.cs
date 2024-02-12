using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseGame : MonoBehaviour
{
    protected Image BG;

    protected AudioSource audioSource;
    protected AudioClip myAudioClip;



    // private Dictionary<int, int> baseRounds_num = new Dictionary<int, string>
    // {
    //     { 0, 9999 },
    //     { 1, 1},
    //     { 2, 1},
    // };

    // public string GetLevelDescription(int level)
    // {
    //     if (levelDescriptions.ContainsKey(level))
    //     {
    //         return levelDescriptions[level];
    //     }
    //     return "Unknown"; // Return default if the level is not found
    // }

    protected void StartGame()
    {
        Debug.Log("BaseGame StartGame()");

        // Debug.Log($"difficultyLevel: {difficultyLevel}");
    }

    public virtual void Awake() {
        // if object == null 
        // add button listener 
        // if (endGameBtn_prefab!=null){
        //     endGameBtn_prefab.onClick.AddListener(()=>
        //     {
        //         EndGame();
        //     });  
        // }
        // else{
        //     Debug.Log("Error: endGameBtn_prefab is null");
        // }
    }

    public virtual void Start()
    {
        StartGame();    
    }

    public void Update()
    {
        
    }
    
    public virtual void EndGame()  // Game over
    {
        Debug.Log("Is EndGame");
        // return to main menu;
        
        // All API to "ServerGetCallComplete";
    }
}
