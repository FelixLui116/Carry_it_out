using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameController : MonoBehaviour
{
    public Button startBtn;
    public Button optionBtn;
    public Button exitBtn;

    [SerializeField] private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        // set btn listener
        if (startBtn!=null){
            startBtn.onClick.AddListener(()=>
            {
                Debug.Log("StartGameController: startBtn.onClick");

                // SceneManager.LoadScene("GameScene");
                GlobalManager.Instance.LoadSceneAsync(sceneName);
            });  
        }
        if (optionBtn!=null){
            optionBtn.onClick.AddListener(()=>
            {
                Debug.Log("StartGameController: optionBtn.onClick");
            });  
        }

        if (exitBtn!=null){
            exitBtn.onClick.AddListener(()=>
            {
                Debug.Log("StartGameController: exitBtn.onClick");
                Application.Quit();
            });  
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
