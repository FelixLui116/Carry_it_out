using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // [SerializeField] private List<GameObject> clonePoint = new List<GameObject>();
    [SerializeField] private GameObject clonePool_Obj;

    public GameObject luaggageitemPrefab;

    public GameObject playerDropPoint;
    public GameObject playerDropPoint_2;
    public GameObject clonePool;
    
    public GameObject OutBoard_Object;
    
    public int playerScore = 0;
    public int playerHealth = 3; // 3 life
    public UIManager  uiManager;



    // Start is called before the first frame update
    void Start()
    {
        // for loop to clone luggage item on each clone point
        
        // for (int i = 0; i < clonePoint.Count; i++)
        // {
        //     GameObject clonedObject = Instantiate(luaggageitemPrefab);
        //     LuggageItem cloneObject_LuggageItem = clonedObject.GetComponent<LuggageItem>();

        //       clonedObject.transform.SetParent(clonePoint[i].transform);
        //     clonedObject.transform.position = clonePoint[i].transform.position;
            
        //     cloneObject_LuggageItem.SetIsRandomColor(true);
        // }

        // for loop to clone luggage item on each clonePool_Obj point
        for (int i = 0; i < clonePool_Obj.transform.childCount; i++)
        {
            GameObject clonedObject = Instantiate(luaggageitemPrefab);
            LuggageItem cloneObject_LuggageItem = clonedObject.GetComponent<LuggageItem>();

            clonedObject.transform.SetParent(clonePool_Obj.transform.GetChild(i));
            clonedObject.transform.position = clonePool_Obj.transform.GetChild(i).position;
            cloneObject_LuggageItem.SetIsRandomColor(true);
            cloneObject_LuggageItem.SetIsRandomSize(true);
        }
    }

    public void UpdateScore(int score)
    {
        playerScore += score;
        uiManager.scoreText.text = playerScore.ToString();
    }

    // check Box collider OutBoard_Object for luggage item
    public void UpdateOutBoard(int health)
    {
        playerHealth -= health;
        uiManager.healthText.text = playerHealth.ToString();

        // if playerHealth is 0, then game over
        if (playerHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }




    // Update is called once per frame
    void Update()
    {
        // set the Q E button click to clone the luaggage in the playerDropPoint
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject clonedObject = Instantiate(luaggageitemPrefab);
            LuggageItem cloneObject_LuggageItem = clonedObject.GetComponent<LuggageItem>();

            clonedObject.transform.position = playerDropPoint.transform.position;

            clonedObject.transform.SetParent(clonePool.transform);
            cloneObject_LuggageItem.SetIsRandomColor(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject clonedObject = Instantiate(luaggageitemPrefab);
            LuggageItem cloneObject_LuggageItem = clonedObject.GetComponent<LuggageItem>();

            clonedObject.transform.position = playerDropPoint_2.transform.position;
    
            clonedObject.transform.SetParent(clonePool.transform);
            cloneObject_LuggageItem.SetIsRandomColor(true);
        }
    }

    // void CloneLuggage()
    // {
    //     GameObject clonedObject = Instantiate(luaggageitemPrefab);
    //     LuggageItem cloneObject_LuggageItem = clonedObject.GetComponent<LuggageItem>();

    //     clonedObject.transform.position = playerDropPoint.transform.position;
    //     cloneObject_LuggageItem.SetIsRandomColor(true);
    // }
}
