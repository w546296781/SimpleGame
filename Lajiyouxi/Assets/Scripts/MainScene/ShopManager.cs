using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject canvas;
    public MainManager mm;

    private List<EquipmentClass> equipList;
    private HeroClass theHero;
    private PackageClass thePackage;

    private List<SlotClass> slotList = new List<SlotClass>();

    public Text text_packageIsFull, text_gold;
    public GameObject item_prefab;


    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.parent.gameObject;
        mm = canvas.transform.GetComponent<MainManager>();

        DBManager dbm = new DBManager();
        equipList = dbm.GetAllEquipment();
        theHero = dbm.GetHero(1);
        thePackage = dbm.GetPackage(1);

        Init_SlotList();

        foreach(EquipmentClass i in equipList)
        {
            i.price = i.price * 2;
        }

        for(int i = 0; i < 6; i++)
        {
            EquipmentClass thisEquip = equipList[Random.Range(0, equipList.Count)];
            PutItemToSlot(thisEquip, slotList[i]);
            equipList.Remove(thisEquip);
        }

        Refresh();
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

    public void Init_SlotList()
    {
        SlotClass slot1 = new SlotClass();
        slot1.x = -375;
        slot1.y = -40;
        slotList.Add(slot1);
        SlotClass slot2 = new SlotClass();
        slot2.x = -225;
        slot2.y = -40;
        slotList.Add(slot2);
        SlotClass slot3 = new SlotClass();
        slot3.x = -75;
        slot3.y = -40;
        slotList.Add(slot3);
        SlotClass slot4 = new SlotClass();
        slot4.x = 75;
        slot4.y = -40;
        slotList.Add(slot4);
        SlotClass slot5 = new SlotClass();
        slot5.x = 225;
        slot5.y = -40;
        slotList.Add(slot5);
        SlotClass slot6 = new SlotClass();
        slot6.x = 375;
        slot6.y = -40;
        slotList.Add(slot6);
    }

    public void PutItemToSlot(EquipmentClass thisEquip, SlotClass thisSlot)
    {
        Vector3 thisPosition = new Vector3(thisSlot.x + 960, thisSlot.y + 560, 0);
        GameObject instance = (GameObject)Instantiate(item_prefab, thisPosition, transform.rotation);
        instance.transform.SetParent(transform);
        instance.GetComponent<ItemPrefabManager>().thisEquip = thisEquip;
        instance.GetComponent<ItemPrefabManager>().isOnShop = true;
    }

    public void BuyThisItem(GameObject obj)
    {
        EquipmentClass thisEquip = obj.transform.GetComponent<ItemPrefabManager>().thisEquip;

        if(theHero.gold > thisEquip.price)
        {
            int emptySlot = GetEmptySlotInPackage();

            if(emptySlot != -1)
            {
                theHero.gold -= thisEquip.price;
                thePackage.slots[emptySlot] = thisEquip.id;

                DBManager dbm = new DBManager();
                dbm.SaveHero(theHero);
                dbm.SavePackage(thePackage);

                DestroyImmediate(obj);
            }

            Refresh();
        }
    }

    public void Refresh()
    {
        if(GetEmptySlotInPackage() != -1)
        {
            text_packageIsFull.gameObject.SetActive(false);
        }
        else
        {
            text_packageIsFull.gameObject.SetActive(true);
        }

        text_gold.text = theHero.gold.ToString();
    }

    public int GetEmptySlotInPackage()
    {
        int emptySlot = -1;
        for (int i = 0; i < thePackage.slots.Count; i++)
        {
            if (thePackage.slots[i] == 0)
            {
                emptySlot = i;
                break;
            }
        }

        return emptySlot;
    }
}
