using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureManager : MonoBehaviour
{
    public GameObject canvas;
    public MainManager mm;
    public Text text_packageIsFull, text_equip;
    private PackageClass thePackage;
    List<EquipmentClass> equipList = new List<EquipmentClass>();
    List<EquipmentClass> dropedList = new List<EquipmentClass>();
    public GameObject item_prefab;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.parent.gameObject;
        mm = canvas.transform.GetComponent<MainManager>();

        DBManager dbm = new DBManager();
        equipList = dbm.GetAllEquipment();
        thePackage = dbm.GetPackage(1);

        DropItem();
        Save();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Button_OK_Click()
    {
        mm.event_finish = true;
        DestroyImmediate(gameObject);
    }

    public void DropItem()
    {
        int dropCount = 0;

        equipList.Sort(new EquipmentClass());

        foreach (EquipmentClass i in equipList)
        {
            int thresold = 0;
            switch (i.quality)
            {
                case 1:
                    thresold = -1;
                    break;
                case 2:
                    thresold = 10;
                    break;
                case 3:
                    thresold = 20;
                    break;
                case 4:
                    thresold = 10;
                    break;
            }

            int ran = Random.Range(0, 100);

            if (ran < thresold)
            {
                dropCount++;
                dropedList.Add(i);
                PutItem(i, dropCount);
            }

            if (dropCount > 3)
            {
                break;
            }
        }

        if (dropCount == 0)
        {
            text_equip.text = "无";
        }
    }

    public void PutItem(EquipmentClass thisEquip, int count)
    {
        int x = 0;
        int y = -40;
        switch (count)
        {
            case 1:
                x = -400;
                break;
            case 2:
                x = -250;
                break;
            case 3:
                x = -100;
                break;
        }

        Vector3 thisPosition = new Vector3(x + 960, y + 560, 0);
        GameObject instance = (GameObject)Instantiate(item_prefab, thisPosition, transform.rotation);
        instance.transform.SetParent(transform);
        instance.GetComponent<ItemPrefabManager>().thisEquip = thisEquip;
        instance.GetComponent<ItemPrefabManager>().isOnDropMenu = true;
    }

    public void Save()
    {
        for (int i = 0; i < thePackage.slots.Count; i++)
        {
            if (dropedList.Count == 0)
            {
                break;
            }

            if (thePackage.slots[i] == 0)
            {
                thePackage.slots[i] = dropedList[0].id;
                dropedList.RemoveAt(0);
            }
        }
        if (dropedList.Count != 0)
        {
            text_packageIsFull.text = "背包已满！";
        }

        DBManager dbm = new DBManager();
        dbm.SavePackage(thePackage);
    }
}
