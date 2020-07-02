using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailManager : MonoBehaviour
{
    public EquipmentClass thisEquip;

    public bool isSell = true;

    public Text text_name, text_class, text_quality, text_attr1, text_attr2, text_attr3, text_attr4, text_attr5, text_attr6, text_sellorbuy, text_price;
    // Start is called before the first frame update
    void Start()
    {
        if(isSell == true)
        {
            text_sellorbuy.text = "出售";
        }
        else
        {
            text_sellorbuy.text = "购买";
        }
        text_price.text = thisEquip.price.ToString() + "G";

        text_name.text = thisEquip.name;
        text_class.text = getClass();
        ChangeQuality();

        text_attr1.text = getAttr(thisEquip.attr1);
        text_attr2.text = getAttr(thisEquip.attr2);
        text_attr3.text = getAttr(thisEquip.attr3);
        text_attr4.text = getAttr(thisEquip.attr4);
        text_attr5.text = getAttr(thisEquip.attr5);
        text_attr6.text = getAttr(thisEquip.attr6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getClass()
    {
        string result = null;

        switch (thisEquip.Class)
        {
            case 1:
                result = "武器";
                break;
            case 2:
                result = "护甲";
                break;
            case 3:
                result = "头盔";
                break;
            case 4:
                result = "鞋子";
                break;
            case 5:
                result = "戒指";
                break;
            case 6:
                result = "护身符";
                break;
        }

        return result;
    }

    public void ChangeQuality()
    {
        switch (thisEquip.quality)
        {
            case 1:
                text_quality.text = "普通";
                text_quality.color = Color.white;
                break;
            case 2:
                text_quality.text = "魔法";
                text_quality.color = Color.green;
                break;
            case 3:
                text_quality.text = "稀有";
                text_quality.color = Color.yellow;
                break;
            case 4:
                text_quality.text = "传说";
                Color orange = new Color(1, 0.6475f, 0.1367f);
                text_quality.color = orange;
                break;
        }
    }

    public string getAttr(string str)
    {
        string result = null;

        return result;
    }
}
