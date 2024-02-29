using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageItem : MonoBehaviour
{
    // Start is called before the first frame update

    // public enum ColorType
    // {
    //     red,
    //     blue,
    //     green,
    //     yellow,
    //     purple,
    //     cyan,
    //     orange,
    // }   
    public string luggageColor;

    public int collisionCount = 0;

     [SerializeField] private int damage = 1;

    // [SerializeField] private GameObject[] sameluggage = new GameObject[4];  
    [SerializeField] private List<GameObject> sameLuggage = new List<GameObject>();
    
    [SerializeField] private bool IsRandonColor = true;

    public bool SetIsRandomColor(bool value, string [] color_list )
    {
        if (value == true){
            luggageColor = GetRandomColor(color_list);
        }
        else{    
            luggageColor = color_list[0];
        }

        IsRandonColor = value;
        return IsRandonColor;
    }


    private string GetRandomColor(string[] color_list)
    {
        int randomIndex = Random.Range(0, color_list.Length);
        return color_list[randomIndex];
    }


    void Start()
    {
        sameLuggage.Add(gameObject);
        
        SetLuggageColor();
        
    }

    void SetLuggageColor()
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
        
        GetComponent<Renderer>().material.color = color_;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Luggage"))
        {
            // Check if the collided object is another LuggageItem
            LuggageItem collidedLuggage = collision.gameObject.GetComponent<LuggageItem>();
            
            
            if (collidedLuggage != null)
            {
                // Check if the collided LuggageItem has the same color
                if (collidedLuggage.luggageColor == luggageColor)
                {
                    collisionCount++;

                    //add to array
                    sameLuggage.Add(collidedLuggage.gameObject);

                    if (collisionCount >= 2)
                    {
                        Debug.Log($"Three or more {luggageColor} items collided!");
                        
                        ////////
                        // for sameLuggage destroy
                        foreach (GameObject item in sameLuggage)
                        {
                            // Debug.Log($"Destroy {item.name}");
                            Destroy(item);
                        }
                        ///////////////////////////////
                        
                        // get Score
                        LevelManager levelManager = FindObjectOfType<LevelManager>();
                        if (levelManager != null)
                        {
                            // Increase the score
                            levelManager.UpdateScore(2);
                        }


                    }
                    // Destroy(collidedLuggage.gameObject);
                    // Destroy(gameObject);
                }
            }
        }
        if (collision.gameObject.CompareTag("OutBoard"))
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                Debug.Log("Out of board");
                // Decrease the health
                levelManager.UpdateOutBoard(damage);
            }
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Luggage"))
        {
            // Check if the collided object is another LuggageItem
            LuggageItem collidedLuggage = other.gameObject.GetComponent<LuggageItem>();

            if (collidedLuggage != null)
            {
                // Check if the collided LuggageItem has the same color
                if (collidedLuggage.luggageColor == luggageColor)
                {
                    collisionCount--;
                    sameLuggage.Remove(collidedLuggage.gameObject);
                    
                }
            }
        }
    }

    // ColorType GetRandomColor()
    // {
    //     // Get a random color from the enum
    //     return (ColorType)Random.Range(0, System.Enum.GetValues(typeof(ColorType)).Length);
    // }

    public bool SetIsRandomSize(bool value)
    {
        if (value == true)
        {
            // base is scale is 0.5
            float randomSizeX = Random.Range(0.3f, 0.7f);
            float randomSizeY = Random.Range(0.3f, 0.7f);
            float randomSizeZ = Random.Range(0.3f, 0.7f);
            transform.localScale = new Vector3(randomSizeX, randomSizeY, randomSizeZ);
        }
        return value;
    }
}
