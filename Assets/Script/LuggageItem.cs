using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageItem : MonoBehaviour
{
    // Start is called before the first frame update

    public enum ColorType
    {
        red,
        blue,
        green,
        yellow,
        purple,
        cyan,
        orange,


    }   
    public ColorType luggageColor;

    public int collisionCount = 0;

     [SerializeField] private int damage = 1;

    // [SerializeField] private GameObject[] sameluggage = new GameObject[4];  
    [SerializeField] private List<GameObject> sameLuggage = new List<GameObject>();
    
    [SerializeField] private bool IsRandonColor = true;

    public bool SetIsRandomColor(bool value)
    {
        IsRandonColor = value;
        return IsRandonColor;
    }

    public void SetIsRandomColor(string colortype )
    {
        
    }



    void Start()
    {
        sameLuggage.Add(gameObject);

        // Set the luggage color
        if (IsRandonColor)
        {
            ColorType previousColor = GetRandomColor(); // Store the previous color
            do
            {
                luggageColor = GetRandomColor(); // Generate a new random color
            }
            while (luggageColor == previousColor); // Repeat until a different color is generated
        }
        
        SetLuggageColor();
        
        // if (IsRandonColor == true)
        // {
        //     luggageColor = GetRandomColor();  // redon color
        // }
        // SetLuggageColor();
    }

    void SetLuggageColor()
    {
        // Set the luggage color based on the enum value
        switch (luggageColor)
        {
            case ColorType.red:
                GetComponent<Renderer>().material.color = Color.red;
                break;
            case ColorType.blue:
                GetComponent<Renderer>().material.color = Color.blue;
                break;
            case ColorType.green:
                GetComponent<Renderer>().material.color = Color.green;
                break;
            case ColorType.yellow:
                GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case ColorType.purple:
                GetComponent<Renderer>().material.color = new Color(0.5f, 0, 0.5f); // Purple
                break;
            case ColorType.cyan:
                GetComponent<Renderer>().material.color = Color.cyan;
                break;
            case ColorType.orange:
                GetComponent<Renderer>().material.color = new Color(1, 0.5f, 0); // Orange
                break;
        }
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

    ColorType GetRandomColor()
    {
        // Get a random color from the enum
        return (ColorType)Random.Range(0, System.Enum.GetValues(typeof(ColorType)).Length);
    }

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
