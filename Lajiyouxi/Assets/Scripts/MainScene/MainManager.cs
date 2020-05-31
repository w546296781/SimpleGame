﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    public Canvas canvas;

    public Text text_eventleft, text_level;

    private Vector3 position_area1 = new Vector3(410, 560, 0);
    private Vector3 position_area2 = new Vector3(960, 560, 0);
    private Vector3 position_area3 = new Vector3(1510, 560, 0);
    private List<Vector3> positions = new List<Vector3>();

    private List<GameObject> prefabs_random = new List<GameObject>();
    private List<GameObject> prefabs = new List<GameObject>();

    public List<bool> area_finish = new List<bool>();

    public bool event_finish = false;
    private GameObject event_obj;

    public bool battle_finish = true;

    EventClass theEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        //用于随机
        //怪物事件的机率为40%， 奇遇事件为30%， 宝箱事件为20%， 商店事件为10%
        prefabs_random.Add(prefab_monster);
        prefabs_random.Add(prefab_monster);
        prefabs_random.Add(prefab_monster);
        prefabs_random.Add(prefab_monster);
        prefabs_random.Add(prefab_treasure);
        prefabs_random.Add(prefab_treasure);
        prefabs_random.Add(prefab_shop);
        prefabs_random.Add(prefab_adventure);
        prefabs_random.Add(prefab_adventure);
        prefabs_random.Add(prefab_adventure);

        prefabs.Add(prefab_monster);
        prefabs.Add(prefab_treasure);
        prefabs.Add(prefab_shop);
        prefabs.Add(prefab_adventure);

        positions.Add(position_area1);
        positions.Add(position_area2);
        positions.Add(position_area3);

        LoadFromDatabase();
/*
        //在3个位置随机分配3张卡
        RandomSelectEvent(position_area1);
        RandomSelectEvent(position_area2);
        RandomSelectEvent(position_area3);*/

        area_finish.Add(false);
        area_finish.Add(false);
        area_finish.Add(false);
    }

    public void LoadFromDatabase()
    {
        //从数据库读取关数，3个位置的事件，剩余卡牌数，BattleFinish标志位
        DBManager dbm = new DBManager();
        theEvent = dbm.GetEvent(1);

        text_eventleft.text = "剩余事件：" + theEvent.event_left.ToString();
        text_level.text = "第" + theEvent.level.ToString() + "层";

        //对于三个area，0表示空，1表示怪兽事件，2表示宝箱事件，3表示商店事件，4表示奇遇事件
        if(theEvent.area1 == 0)
        {
            RandomSelectEvent(position_area1);
        }
        else
        {
            GeneratePrefab(position_area1, prefabs[theEvent.area1 - 1]);
        }

        if (theEvent.area2 == 0)
        {
            RandomSelectEvent(position_area2);
        }
        else
        {
            GeneratePrefab(position_area2, prefabs[theEvent.area2 - 1]);
        }

        if (theEvent.area3 == 0)
        {
            RandomSelectEvent(position_area3);
        }
        else
        {
            GeneratePrefab(position_area3, prefabs[theEvent.area3 - 1]);
        }

        if(theEvent.battle_finish == 1)
        {
            theEvent.battle_finish = 0;
            var monsters = GameObject.FindGameObjectsWithTag("Monster");
            foreach(GameObject obj in monsters)
            {
                if(obj.transform.position == positions[theEvent.battle_position - 1])
                {
                    event_obj = obj;
                    event_finish = true;
                }
            }
        }

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

        if(battle_finish == true)
        {
            
        }
    }

    public int PositionToInt(Vector3 position)
    {
        if (position == position_area1)
        {
            return 1;
        }
        else if (position == position_area2)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    public void BattleBegin(GameObject obj)
    {
        theEvent.battle_finish = 0;
        theEvent.battle_position = PositionToInt(obj.transform.position);

        Save();
        SceneManager.LoadScene(2);
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
        GeneratePrefab(position, prefabs_random[index]);
    }

    public void GeneratePrefab(Vector3 position, GameObject obj)
    {
        GameObject instance = (GameObject)Instantiate(obj, position, transform.rotation);
        instance.transform.SetParent(transform);

        int event_type = 0;

        //绑定触发函数
        EventTrigger trigger = instance.gameObject.AddComponent<EventTrigger>();
        UnityAction<BaseEventData> action = null;
        if (instance.name.Contains("Treasure"))
        {
            action = new UnityAction<BaseEventData>(delegate {
                TreasureEvent(instance);
            });
            event_type = 2;
        }
        else if (instance.name.Contains("Shop"))
        {
            action = new UnityAction<BaseEventData>(delegate {
                ShopEvent(instance);
            });
            event_type = 3;
        }
        else if (instance.name.Contains("Adventure"))
        {
            action = new UnityAction<BaseEventData>(delegate {
                AdventureEvent(instance);
            });
            event_type = 4;
        }
        else
        {
            action = new UnityAction<BaseEventData>(delegate {
                BattleBegin(instance);
            });
            event_type = 1;
        }
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);

        if(PositionToInt(position) == 1)
        {
            theEvent.area1 = event_type;
        }
        else if (PositionToInt(position) == 2)
        {
            theEvent.area2 = event_type;
        }
        else
        {
            theEvent.area3 = event_type;
        }

        Save();
    }

    public void Save()
    {
        DBManager dbm = new DBManager();
        dbm.SaveEvent(theEvent);
    }
}
