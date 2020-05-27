using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public GameObject setting_popup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BattleBegin()
    {
        SceneManager.LoadScene(2);
    }

    public void Btn_Setting_Click()
    {
        GameObject instance = (GameObject)Instantiate(setting_popup, transform.position, transform.rotation);
        instance.transform.parent = transform;
    }
}
