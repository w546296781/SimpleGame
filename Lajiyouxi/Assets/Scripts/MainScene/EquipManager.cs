using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour
{

    List<SlotClass> thisPackage = new List<SlotClass>();

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
        
    }

    public void Refresh()
    {

        DBManager dbm = new DBManager();
        Equips = dbm.GetAllEquipment();
        package = dbm.GetPackage(1);

        CreatePositions();

        for (int i = 0; i < 30; i++)
        {
            if(thisPackage[i].item != 0)
            {
                PutItemToSlot(thisPackage[i]);
            }
        }
    }

    public void PutItemToSlot(SlotClass thisSlot)
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
