﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPrefabManager : MonoBehaviour, IPointerClickHandler
{
    public EquipmentClass thisEquip;
    public HeroClass theHero;

    public GameObject item_popup;

    public UnityEvent leftClick;
    public UnityEvent rightClick;
    public int slotID;

    public bool isOnDropMenu = false;
    public bool isOnShop = false;

    // Start is called before the first frame update
    void Start()
    {
        leftClick.AddListener(new UnityAction(ButtonLeftClick));
        rightClick.AddListener(new UnityAction(ButtonRightClick));

        string imageAddress = ChangePrefab();
        if (imageAddress != null)
        {
            Image image = this.GetComponent<Image>();
            image.sprite = Resources.Load(imageAddress, typeof(Sprite)) as Sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Detail_Popup
    public void Detail_Info_Trigger()
    {
        Vector3 createPosition = new Vector3(transform.position.x + 135, transform.position.y + 100 , 10);
        GameObject instance = (GameObject)Instantiate(item_popup, createPosition, transform.rotation);
        instance.transform.SetParent(transform.parent);

        instance.GetComponent<ItemDetailManager>().thisEquip = thisEquip;
        instance.GetComponent<ItemDetailManager>().isSell = true;
    }

    public void Detail_Info_Delete()
    {
        var popup = GameObject.FindGameObjectsWithTag("item_popup");
        foreach (GameObject i in popup)
        {
            DestroyImmediate(i);
        }
    }

    #endregion

    public string ChangePrefab()
    {
        string result = null;

        switch (thisEquip.id)
        {
            case 1:
                result = "ItemsIcons/GreatBowIcon";
                break;
            case 2:
                result = "ItemsIcons/FireySwordIcon";
                break;
            case 3:
                result = "ItemsIcons/SteelArmorIcon";
                break;
            case 4:
                result = "ItemsIcons/Hard Chest Icon";
                break;
            case 5:
                result = "ItemsIcons/IronHelmetIcon";
                break;
            case 6:
                result = "ItemsIcons/slice165_@";
                break;
        }

        return result;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke();
    }

    private void ButtonLeftClick()
    {
        if (isOnDropMenu == false)
        {
            if(isOnShop == true)
            {

            }
            else if (transform.parent.GetComponent<EquipManager>().onSell == true && slotID <= 30)
            {
                transform.parent.GetComponent<EquipManager>().SellItem(gameObject, slotID);
                Detail_Info_Delete();
            }
        }
    }

    private void ButtonRightClick()
    {
        if (isOnDropMenu == false)
        {
            if (isOnShop == true)
            {
                transform.parent.GetComponent<ShopManager>().BuyThisItem(gameObject);
                Detail_Info_Delete();
            }
            else if (transform.parent.GetComponent<EquipManager>().onSell == false)
            {
                transform.parent.GetComponent<EquipManager>().EquipItem(gameObject, slotID);
                Detail_Info_Delete();
            }
        }

    }
}
