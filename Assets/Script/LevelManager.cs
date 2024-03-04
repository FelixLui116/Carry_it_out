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

    
    [SerializeField] private int playerDrop_color;   
    [SerializeField] private int playerDrop_color_2; 

    [SerializeField] private string [] luaggage_color = new string[7] {"red", "blue", "green", "yellow", "purple", "cyan", "orange"};

    public GameObject clonePool;
    
    public GameObject OutBoard_Object;
    
    public int playerScore = 0;
    public int playerHealth = 3; // 3 life
    public UIManager  uiManager;


    public Color SetLuggageColor( string luggageColor)
    {
        Color color_;
        // Set the luggage color based on the string value
        switch (luggageColor)
        {
            case "red": // Corrected syntax
                color_ = Color.red;
                break;
            case "blue": // Corrected syntax
                color_ = Color.blue;
                break;
            case "green": // Corrected syntax
                color_ = Color.green;
                break;
            case "yellow": // Corrected syntax
                color_ = Color.yellow;
                break;
            case "purple": // Corrected syntax
                color_ = new Color(0.5f, 0f, 0.5f); // Purple
                break;
            case "cyan": // Corrected syntax
                color_ = Color.cyan;
                break;
            case "orange": // Corrected syntax
                color_ = new Color(1f, 0.5f, 0f); // Orange
                break;
            default:
                color_ = Color.white; // Default color
                break;
        }
        return color_;
    }


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
            // cloneObject_LuggageItem.SetIsRandomColor(true , luaggage_color );
            
            cloneObject_LuggageItem.SetLuggageColor( luaggage_color[GetRandomColor()]);
            cloneObject_LuggageItem.SetIsRandomSize(true);
        }

        // start the game with HP = 3
        uiManager.healthText.text = playerHealth.ToString();

        NextColor(true , true , false);
        NextColor(true , false , true);
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
            // cloneObject_LuggageItem.SetIsRandomColor(true, luaggage_color);
            cloneObject_LuggageItem.SetLuggageColor( luaggage_color[playerDrop_color]);
            NextColor(true , true , false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject clonedObject = Instantiate(luaggageitemPrefab);
            LuggageItem cloneObject_LuggageItem = clonedObject.GetComponent<LuggageItem>();

            clonedObject.transform.position = playerDropPoint_2.transform.position;
    
            clonedObject.transform.SetParent(clonePool.transform);
            // cloneObject_LuggageItem.SetIsRandomColor(true , luaggage_color);
            cloneObject_LuggageItem.SetLuggageColor( luaggage_color[playerDrop_color_2]);
            NextColor(true , false , true);
        }
    }
    private int GetRandomColor()
    {
        // Generate a random index within the bounds of the array
        int randomIndex = Random.Range(0, luaggage_color.Length);
        // Return the color at the random index
        return randomIndex;
    }

    void NextColor(bool IsRandonColor , bool pos_1 , bool pos_2)
    {
        if (IsRandonColor == true)
        {
            int randomColor = GetRandomColor();
            if (pos_1 == true)
            {
                playerDrop_color = randomColor;
                uiManager.Cube_Q.color = SetLuggageColor(luaggage_color[playerDrop_color]);
            }
            else if (pos_2 == true)
            {
                playerDrop_color_2 = randomColor;
                uiManager.Cube_E.color = SetLuggageColor(luaggage_color[playerDrop_color_2]);
            }
        }
        else
        {
            
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
