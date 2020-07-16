using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameNoticeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Btn_Confirm_Onclick()
    {
        gameObject.transform.parent.GetComponent<StartManager>().CreateNewGame();
        DestroyImmediate(gameObject);
    }

    public void Btn_Cancel_Onclick()
    {
        gameObject.transform.parent.GetComponent<StartManager>().ShowUI();
        DestroyImmediate(gameObject);
    }
}
