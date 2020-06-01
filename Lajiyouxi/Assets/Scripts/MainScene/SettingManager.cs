using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Btn_BackToStart_Click()
    {
        SceneManager.LoadScene(0);
    }

    public void Btn_Continue_Click()
    {
        gameObject.transform.parent.GetComponent<MainManager>().ShowUI();
        DestroyImmediate(gameObject);
    }
}
