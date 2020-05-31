using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MainManager : MonoBehaviour
{
    public GameObject setting_popup;

    public GameObject prefab_monster;
    public GameObject prefab_treasure;
    public GameObject prefab_shop;
    public GameObject prefab_adventure;

    public GameObject treasure_popup;
    public GameObject shop_popup;
    public GameObject adventure_popup;

    private Vector3 position_area1 = new Vector3(410, 560, 0);
    private Vector3 position_area2 = new Vector3(960, 560, 0);
    private Vector3 position_area3 = new Vector3(1510, 560, 0);

    private List<GameObject> prefabs = new List<GameObject>();

    public List<bool> area_finish = new List<bool>();

    public bool event_finish = false;
    private GameObject event_obj;

    // Start is called before the first frame update
    void Start()
    {
        //怪物事件的机率为40%， 奇遇事件为30%， 宝箱事件为20%， 商店事件为10%
        prefabs.Add(prefab_monster);
        prefabs.Add(prefab_monster);
        prefabs.Add(prefab_monster);
        prefabs.Add(prefab_monster);
        prefabs.Add(prefab_treasure);
        prefabs.Add(prefab_treasure);
        prefabs.Add(prefab_shop);
        prefabs.Add(prefab_adventure);
        prefabs.Add(prefab_adventure);
        prefabs.Add(prefab_adventure);

        //在3个位置随机分配3张卡
        RandomSelectEvent(position_area1);
        RandomSelectEvent(position_area2);
        RandomSelectEvent(position_area3);

        area_finish.Add(false);
        area_finish.Add(false);
        area_finish.Add(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(area_finish[0] == true)
        {
            RandomSelectEvent(position_area1);
            area_finish[0] = false;
        }

        if (area_finish[1] == true)
        {
            RandomSelectEvent(position_area2);
            area_finish[1] = false;
        }

        if (area_finish[2] == true)
        {
            RandomSelectEvent(position_area3);
            area_finish[2] = false;
        }

        if(event_finish == true)
        {
            Delete(event_obj);
            event_finish = false;
        }
    }

    public void BattleBegin(GameObject obj)
    {
        SceneManager.LoadScene(2);
        event_obj = obj;
    }

    public void Btn_Setting_Click()
    {
        GameObject instance = (GameObject)Instantiate(setting_popup, position_area2, transform.rotation);
        instance.transform.SetParent(transform);
    }

    public void TreasureEvent(GameObject obj)
    {
        GameObject instance = (GameObject)Instantiate(treasure_popup, position_area2, transform.rotation);
        instance.transform.SetParent(transform);
        event_obj = obj;
    }

    public void ShopEvent(GameObject obj)
    {
        GameObject instance = (GameObject)Instantiate(shop_popup, position_area2, transform.rotation);
        instance.transform.SetParent(transform);
        event_obj = obj;
    }

    public void AdventureEvent(GameObject obj)
    {
        GameObject instance = (GameObject)Instantiate(adventure_popup, position_area2, transform.rotation);
        instance.transform.SetParent(transform);
        event_obj = obj;
    }

    public void Delete(GameObject obj)
    {
        if(obj.transform.position == position_area1)
        {
            area_finish[0] = true;
        }
        else if(obj.transform.position == position_area2)
        {
            area_finish[1] = true;
        }
        else
        {
            area_finish[2] = true;
        }

        DestroyImmediate(obj);
    }

    public void RandomSelectEvent(Vector3 position)
    {
        //随机选择事件种类并生成
        int index = Random.Range(0, 9);
        GameObject instance = (GameObject)Instantiate(prefabs[index], position, transform.rotation);
        instance.transform.SetParent(transform);

        //为事件绑定触发函数
        EventTrigger trigger = instance.gameObject.AddComponent<EventTrigger>();
        UnityAction<BaseEventData> action = null;
        if (instance.name.Contains("Treasure"))
        {
            action = new UnityAction<BaseEventData>(delegate {
                TreasureEvent(instance);
            });
        }
        else if (instance.name.Contains("Shop"))
        {
            action = new UnityAction<BaseEventData>(delegate {
                ShopEvent(instance);
            });
        }
        else if (instance.name.Contains("Adventure"))
        {
            action = new UnityAction<BaseEventData>(delegate {
                AdventureEvent(instance);
            });
        }
        else
        {
            action = new UnityAction<BaseEventData>(delegate {
                BattleBegin(instance);
            });
        }
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }


}
