using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Btn_Start_Onclick()
    {
        SceneManager.LoadScene(1);
    }

    public void Btn_Quit_Onclick()
    {
        Application.Quit();
    }
}
