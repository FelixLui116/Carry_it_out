using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // [SerializeField] private List<GameObject> clonePoint = new List<GameObject>();
    [SerializeField] private GameObject clonePool_Obj;

    // public GameObject luaggageitemPrefab;

    public GameObject playerDropPoint;   
    public GameObject playerDropPoint_2; 
    
    // private bool isItem_1 = false;  // true = skill item , false = luggage item
    // private bool isItem_2 = false;  // true = skill item , false = luggage item
    
    [SerializeField] private int [] player_Item_QE = new int[2];  // check which skill item 
    [SerializeField] private GameObject[] player_Item_Obj = new GameObject[3];
    private int playerDrop_color_1;   
    private int playerDrop_color_2; 
    private int stageLevel = 1;
    [SerializeField]private int stageLevel_counter = 0;


    [SerializeField] private int Random_Bomb = 30;
    [SerializeField] private int Random_magnet = 20;

    
    private float spawn_luaggage_timerCount = 0f;
    [SerializeField] private float spawn_luaggage_timer = 5f;

    // 定义大圈和小圈的半径
    private float radiusBig = 4f;
    private float radiusSmall = 3.6f;
    
    [SerializeField] private Transform centerPosition; // 中心点位置

    /// <summary>
    /// 
    /// </summary>

    [SerializeField] private string [] luaggage_color = new string[7] {"red", "blue", "green", "yellow", "purple", "cyan", "orange"};
    [SerializeField] private GameObject [] skill_Item = new GameObject[2];

    public GameObject OutBoard_Object;
    
    public int playerScore = 0;
    public int playerHealth = 3; // 3 life
    public UIManager  uiManager;
    private float QE_delayTimer =0;
    private float QE_delayDuration = 1f;



    // 在平面上生成随机点
    private Vector3 GetRandomPointOnPlane(float radius)
    {
        return new Vector3(Random.Range(-radius, radius), 0f, Random.Range(-radius, radius));
    }
    private void RandomRadius_cloneLuggat ()
    {
        // 获取当前物体的位置作为中心点
        Vector3 center = centerPosition.position;
        // 生成对象的数量
        int numberOfObjects = stageLevel;

        // 生成对象
        for (int i = 0; i < numberOfObjects; i++)
        {
            // 在radiusBig范围内生成随机点
            Vector3 randomPoint = GetRandomPointOnPlane(radiusBig);

            // 如果随机点在radiusSmall范围内，则重新生成新的点，直到找到一个在所需范围内的点
            while (randomPoint.magnitude < radiusSmall)
            {
                randomPoint = GetRandomPointOnPlane(radiusBig);
            }
            // 实例化对象
            GameObject clonedObject = Instantiate(player_Item_Obj[0], center + randomPoint, Quaternion.identity);
            // clonedObject.transform.SetParent(clonePool_Obj.transform);
            LuggageItem cloneObject_LuggageItem = clonedObject.GetComponent<LuggageItem>();
            cloneObject_LuggageItem.SetLuggageColor( luaggage_color[GetRandomColor()]);
            cloneObject_LuggageItem.SetIsRandomSize(true);
        }

        
        uiManager.stageLevelText.text = stageLevel.ToString();
        stageLevel_counter += 1;
        if(stageLevel_counter >= 10) {
            stageLevel_counter = 0;
            stageLevel++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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

        
        spawn_luaggage_timerCount = 5f; // start set the timer to 5s.  // 5s to clone the luggage item 
        // start the game with HP = 3
        uiManager.healthText.text = playerHealth.ToString();

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

    private void UpdateUITimer_Count(){
        float ratio = spawn_luaggage_timerCount / spawn_luaggage_timer; // 5秒是总时间
        uiManager.TimeBar.fillAmount = ratio;
    }

    private void FixedUpdate() {
        spawn_luaggage_timerCount -= Time.deltaTime;
        if (spawn_luaggage_timerCount <= 0)
        {
            RandomRadius_cloneLuggat();
            spawn_luaggage_timerCount = spawn_luaggage_timer;
        }
        UpdateUITimer_Count();
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
                // CloneLuggageItem_press(playerDropPoint, playerDrop_color_1, clonePool);
                CloneItem_press(player_Item_QE[0], playerDropPoint , clonePool_Obj);

                uiManager.InvisibleObejct("Q", QE_delayDuration);
                Random_item(true , false );  // left
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                QE_delayTimer = 0.0f; // Reset the delay timer
                // CloneLuggageItem_press(playerDropPoint_2, playerDrop_color_2 , clonePool);
                CloneItem_press(player_Item_QE[1], playerDropPoint_2 , clonePool_Obj);

                uiManager.InvisibleObejct("E", QE_delayDuration);
                Random_item(false , true );  // right
            }
        }
    }

    //clone itme baseOn  player_Item_QE[0]
    public void CloneItem_press (int item_QE, GameObject playerDropPoint , GameObject parent_obj = null){
        
        
        if (item_QE == 0)  // Clone Luggage
        {
            CloneLuggageItem_press(playerDropPoint, playerDrop_color_1, parent_obj);
        }
        else  // Clone skill item
        {
            CloneSkillItem_press( player_Item_Obj[item_QE], playerDropPoint, parent_obj);
        }
    }

    private void CloneSkillItem_press (GameObject skill_item_prefeb, GameObject playerDropPoint , GameObject parent_obj = null){
        GameObject clonedObject = Instantiate(skill_item_prefeb);
        clonedObject.transform.position = playerDropPoint.transform.position;
        clonedObject.transform.SetParent(clonePool_Obj.transform);
    }


    private void CloneLuggageItem_press (GameObject playerDropPoint, int _color , GameObject parent_obj = null){
        // Clone the luggage item
        GameObject clonedObject = Instantiate(player_Item_Obj[0]);
        LuggageItem cloneObject_LuggageItem = clonedObject.GetComponent<LuggageItem>();
        
        clonedObject.transform.position = playerDropPoint.transform.position;
        clonedObject.transform.SetParent(parent_obj.transform);
        cloneObject_LuggageItem.SetLuggageColor( luaggage_color[_color]);
    }

    private void Randon_item_skill(bool pos_1 , int random_item)
    {
        if (pos_1 == true){
            player_Item_QE[0] = random_item;
            Show_Object_ByChild(uiManager.Cube_Q , random_item);
        }
        else{
            player_Item_QE[1] = random_item;
            Show_Object_ByChild(uiManager.Cube_E , random_item);
        }
    }

    private void Random_item(bool pos_1 , bool pos_2 )  // is the skill item or luggage item
    {
        
        int randomValue = Random.Range(0, 100);
        Debug.Log("randomValue: " +randomValue);
        if (randomValue < Random_Bomb)
        {
            // 10% 的概率选择炸弹 // 在这里处理炸弹的生成逻辑
            Randon_item_skill(pos_1 , 2);
        }
        else if (randomValue < Random_Bomb + Random_magnet)
        {
            // 10% 的概率选择磁铁// 在这里处理磁铁的生成逻辑
            Randon_item_skill(pos_1 , 1);
        }
        else
        {
            // 剩余 80% 的概率选择行李// 在这里处理行李的生成逻辑
            int random_item = 0;
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
