using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour
{

    List<SlotClass> thisPackage = new List<SlotClass>();
    SlotClass weaponSlot = new SlotClass();
    SlotClass armorSlot = new SlotClass();
    SlotClass halmetSlot = new SlotClass();
    SlotClass bootSlot = new SlotClass();
    SlotClass ringSlot = new SlotClass();
    SlotClass amuletSlot = new SlotClass();

    public Button btn_sell;

    private bool onSell = false;

    List<EquipmentClass> Equips = new List<EquipmentClass>();
    PackageClass package;

    public GameObject item_prefab;

    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Button Function
    public void Btn_Quit_Click()
    {
        gameObject.transform.parent.GetComponent<MainManager>().ShowUI();
        DestroyImmediate(gameObject);
    }

    public void Btn_Sell_Click()
    {
        if(onSell == false)
        {
            onSell = true;
            ColorBlock cb = new ColorBlock();
            cb = btn_sell.colors;
            cb.selectedColor = Color.green;
            btn_sell.colors = cb;
        }
        else
        {
            onSell = false;
            ColorBlock cb = new ColorBlock();
            cb = btn_sell.colors;
            cb.selectedColor = Color.white;
            btn_sell.colors = cb;
        }
    }

    #endregion

    public void CreatePositions()
    {
        thisPackage.Clear();
        for (int i = 0; i < 30; i++)
        {
            SlotClass newSlot = new SlotClass();
            newSlot.item = package.slots[i];
            newSlot.x = 80 + (i % 6) * 100;
            newSlot.y = 200 - (i / 6) * 100;
            thisPackage.Add(newSlot);
        }

        weaponSlot.item = package.weapon;
        weaponSlot.x = -530;
        weaponSlot.y = 0;

        armorSlot.item = package.armor;
        armorSlot.x = -130;
        armorSlot.y = 200;

        halmetSlot.item = package.halmet;
        halmetSlot.x = -530;
        halmetSlot.y = 200;

        bootSlot.item = package.boot;
        bootSlot.x = -130;
        bootSlot.y = 0;

        ringSlot.item = package.ring;
        ringSlot.x = -530;
        ringSlot.y = -200;

        amuletSlot.item = package.amulet;
        amuletSlot.x = -130;
        amuletSlot.y = -200;

    }

    public void Refresh()
    {

        DBManager dbm = new DBManager();
        Equips = dbm.GetAllEquipment();
        package = dbm.GetPackage(1);

        CreatePositions();

        for (int i = 0; i < 30; i++)
        {
            PutItemToSlot(thisPackage[i]);
        }

        PutItemToSlot(weaponSlot);
        PutItemToSlot(armorSlot);
        PutItemToSlot(halmetSlot);
        PutItemToSlot(bootSlot);
        PutItemToSlot(ringSlot);
        PutItemToSlot(amuletSlot);
    }

    public void PutItemToSlot(SlotClass thisSlot)
    {
        if (thisSlot.item != 0)
        {
            Vector3 thisPosition = new Vector3(thisSlot.x + 960, thisSlot.y + 560, 0);
            Debug.Log(thisPosition);
            GameObject instance = (GameObject)Instantiate(item_prefab, thisPosition, transform.rotation);
            instance.transform.SetParent(transform);

            EquipmentClass thisEquip = new EquipmentClass();
            foreach (EquipmentClass i in Equips)
            {
                if (i.id == thisSlot.item)
                {
                    thisEquip = i;
                }
            }
            instance.GetComponent<ItemPrefabManager>().thisEquip = thisEquip;

        }
    }

}
