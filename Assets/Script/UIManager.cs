using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    public Text healthText;
    public Text timeText;
    // public Image Cube_Q;
    // public Image Cube_E;
    public GameObject [] Cube_Q;
    public GameObject [] Cube_E;
    public GameObject Cube_block;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    // InvisibleObejct by time
    public void InvisibleObejct(string obj_Cube ,float time)
    {
        Cube_block.SetActive(true);
        if (obj_Cube == "Q")
        {
            StartCoroutine(InvisibleObejct_Coroutine(Cube_block, time));
        }
        else if (obj_Cube == "E")
        {
            StartCoroutine(InvisibleObejct_Coroutine(Cube_block, time));
        }
    }

    public IEnumerator  InvisibleObejct_Coroutine(GameObject Cube_block , float time)
    {
        // obj_Cube.SetActive(false);
        yield return new WaitForSeconds(time);
        // obj_Cube.SetActive(true);
        Cube_block.SetActive(false);
    }
}
