using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BasePanel : MonoBehaviour
{
    [SerializeField] protected Button YesBtn;
    [SerializeField] protected Button NoBtn;
    [SerializeField] protected Button CancelBtn;
    // [SerializeField] protected
    // Start is called before the first frame update

    private void Awake() {
        
        if(CancelBtn != null) CancelBtn.onClick.AddListener(() => Cancel_func() );
    }
    void Start()
    {
        
        // YesBtn.onClick.AddListener(() => ToScene("Game_1") );
        // NoBtn.onClick.AddListener(() => ToScene("Game_1") );
        // CancelBtn.onClick.AddListener(() => Cancel_func() );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    protected void Cancel_func(){
        this.gameObject.SetActive(false);
        
        // Destroy(this.gameObject);
    }
    
    public void OpenPanel_func(){
        this.gameObject.SetActive(true);
    }

    protected void BackToMain(string sceneName){
        // LevelController.Instance.savePoint();
        // StartCoroutine(GlobalManager.Instance.loadSceneAsync(sceneName));
        GlobalManager.Instance.LoadSceneAsync(sceneName);

    }
  
}
