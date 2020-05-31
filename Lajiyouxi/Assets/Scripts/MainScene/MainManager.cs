using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public GameObject setting_popup;

    public GameObject prefab_monster;
    public GameObject prefab_treasure;
    public GameObject prefab_shop;
    public GameObject prefab_adventure;

    private Vector3 position_area1 = new Vector3(410, 560, 0);
    private Vector3 position_area2 = new Vector3(960, 560, 0);
    private Vector3 position_area3 = new Vector3(1510, 560, 0);

    private List<GameObject> prefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //怪物事件的机率为40%， 其余事件各为20%
        prefabs.Add(prefab_monster);
        prefabs.Add(prefab_monster);
        prefabs.Add(prefab_monster);
        prefabs.Add(prefab_monster);
        prefabs.Add(prefab_treasure);
        prefabs.Add(prefab_treasure);
        prefabs.Add(prefab_shop);
        prefabs.Add(prefab_shop);
        prefabs.Add(prefab_adventure);
        prefabs.Add(prefab_adventure);
        //在3个位置随机分配3张卡

        RandomSelectEvent(position_area1);
        RandomSelectEvent(position_area2);
        RandomSelectEvent(position_area3);
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
        instance.transform.SetParent(transform);
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

    public void RandomSelectEvent(Vector3 position)
    {
        int index = Random.Range(0, 9);
        GameObject instance = (GameObject)Instantiate(prefabs[index], position, transform.rotation);
        instance.transform.SetParent(transform);
        instance.transform.position = position;
        Debug.Log(position);
        Debug.Log(instance.transform.position);
    }
}
