using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public GameObject setting_popup;

    private Vector3 position_area1 = new Vector3(-550, 20, 0);
    private Vector3 position_area2 = new Vector3(-0, 20, 0);
    private Vector3 position_area3 = new Vector3(550, 20, 0);

    // Start is called before the first frame update
    void Start()
    {
        //在3个位置随机分配3张卡
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

    public void TreasureEvent()
    {

    }

    public void ShopEvent()
    {

    }

    public void AdventureEvent()
    {

    }
}
