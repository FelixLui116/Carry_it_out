using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // [SerializeField] private List<GameObject> clonePoint = new List<GameObject>();
    [SerializeField] private GameObject clonePool_Obj;

    // public GameObject luaggageitemPrefab;

    public GameObject playerDropPoint;   
    public GameObject playerDropPoint_2; 
    
    // private bool isItem_1 = false;  // true = skill item , false = luggage item
    // private bool isItem_2 = false;  // true = skill item , false = luggage item
    
    private int [] player_Item_QE = new int[2];  // check which skill item 
    [SerializeField] private GameObject[] player_Item_Obj = new GameObject[3];
    private int playerDrop_color_1;   
    private int playerDrop_color_2; 


    [SerializeField] private string [] luaggage_color = new string[7] {"red", "blue", "green", "yellow", "purple", "cyan", "orange"};
    [SerializeField] private GameObject [] skill_Item = new GameObject[2];

    public GameObject clonePool;
    
    public GameObject OutBoard_Object;
    
    public int playerScore = 0;
    public int playerHealth = 3; // 3 life
    public UIManager  uiManager;
    [SerializeField] private float QE_delayTimer =0;
    private float QE_delayDuration = 0.5f;




    // Start is called before the first frame update
    void Start()
    {
        // for loop to clone luggage item on each clone point
        
        // for (int i = 0; i < clonePoint.Count; i++)
        // {
        //     GameObject clonedObject = Instantiate(player_Item_Obj[0]);
        //     LuggageItem cloneObject_LuggageItem = clonedObject.GetComponent<LuggageItem>();

        //       clonedObject.transform.SetParent(clonePoint[i].transform);
        //     clonedObject.transform.position = clonePoint[i].transform.position;
            
        //     cloneObject_LuggageItem.SetIsRandomColor(true);
        // }

        // for loop to clone luggage item on each clonePool_Obj point
        for (int i = 0; i < clonePool_Obj.transform.childCount; i++)
        {
            GameObject clonedObject = Instantiate(player_Item_Obj[0]);
            LuggageItem cloneObject_LuggageItem = clonedObject.GetComponent<LuggageItem>();

            clonedObject.transform.SetParent(clonePool_Obj.transform.GetChild(i));
            clonedObject.transform.position = clonePool_Obj.transform.GetChild(i).position;
            // cloneObject_LuggageItem.SetIsRandomColor(true , luaggage_color );
            
            cloneObject_LuggageItem.SetLuggageColor( luaggage_color[GetRandomColor()]);
            cloneObject_LuggageItem.SetIsRandomSize(true);
        }

        // start the game with HP = 3
        uiManager.healthText.text = playerHealth.ToString();
        //////////////////////////////  
        // NextColor(true , true , false);
        // NextColor(true , false , true);
        //////////////////////////////
        Random_item(true , false );  // left
        Random_item(false , true );  // right
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
        // add delay time for Q E button
        QE_delayTimer += Time.deltaTime;

        if (QE_delayTimer > QE_delayDuration)
        {
            // set the Q E button click to clone the luaggage in the playerDropPoint
            if (Input.GetKeyDown(KeyCode.Q))
            {
                QE_delayTimer = 0.0f; // Reset the delay timer
                CloneLuggageItem_press(playerDropPoint, playerDrop_color_1, clonePool);

                uiManager.InvisibleObejct("Q", QE_delayDuration);
                Random_item(true , false );  // left
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                QE_delayTimer = 0.0f; // Reset the delay timer
                CloneLuggageItem_press(playerDropPoint_2, playerDrop_color_2 , clonePool);

                uiManager.InvisibleObejct("E", QE_delayDuration);
                Random_item(false , true );  // right
            }
        }
    }

    //To Do  clone itme baseOn  player_Item_QE[0]

    private void CloneSkillItem_press (GameObject playerDropPoint , GameObject parent_obj = null){
        GameObject clonedObject = Instantiate(skill_Item[0]);
        clonedObject.transform.position = playerDropPoint.transform.position;
        clonedObject.transform.SetParent(clonePool.transform);
    }


    private void CloneLuggageItem_press (GameObject playerDropPoint, int _color , GameObject parent_obj = null){
        // Clone the luggage item
        GameObject clonedObject = Instantiate(player_Item_Obj[0]);
        LuggageItem cloneObject_LuggageItem = clonedObject.GetComponent<LuggageItem>();
        
        clonedObject.transform.position = playerDropPoint.transform.position;
        clonedObject.transform.SetParent(parent_obj.transform);
        cloneObject_LuggageItem.SetLuggageColor( luaggage_color[_color]);
    }



    private void Random_item(bool pos_1 , bool pos_2 )  // is the skill item or luggage item
    {
        int random_item = Random.Range(0, skill_Item.Length );
        
        // int random_item = 0;
        if (random_item == 0)   // Clone Luggage
        {
            // Debug.Log( "luggage Item");
            if (pos_1 == true){
                player_Item_QE[0] = random_item;
                Show_Object_ByChild(uiManager.Cube_Q , random_item);
                uiManager.Cube_Q[random_item].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = SetLuggageColor(luaggage_color[playerDrop_color_1]);
            }
            else{
                player_Item_QE[1] = random_item;
                Show_Object_ByChild(uiManager.Cube_E , random_item);
                uiManager.Cube_E[random_item].transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = SetLuggageColor(luaggage_color[playerDrop_color_2]);
            }

            NextColor(true , pos_1 , pos_2);
        }
        else  // Clone skill item
        {
            // Debug.Log( "Skill Item");

            if (pos_1 == true){
                player_Item_QE[0] = random_item;
                Show_Object_ByChild(uiManager.Cube_Q , random_item);
            }
            else{
                player_Item_QE[1] = random_item;
                Show_Object_ByChild(uiManager.Cube_E , random_item);
            }
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
                playerDrop_color_1 = randomColor;
            }
            else if (pos_2 == true)
            {
                playerDrop_color_2 = randomColor;
            }
        }
        else{
        }
    }

    public void  Show_Object_ByChild(GameObject[] objArray , int index)
    {
        foreach (GameObject obj in objArray)
        {
            if (obj != null)
            {
                obj.transform.gameObject.SetActive(false);
            }
        }
        objArray[index].gameObject.SetActive(true);
    }

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

}
