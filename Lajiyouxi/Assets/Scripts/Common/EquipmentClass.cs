using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentClass : IComparer<EquipmentClass>
{
    public int id, Class, price, quality;
    public string name;
    public string attr1;
    public string attr2;
    public string attr3;
    public string attr4;
    public string attr5;
    public string attr6;

    public int Compare(EquipmentClass x, EquipmentClass y)
    {
        if(x.quality > y.quality)
        {
            return -1;
        }
        else if(x.quality < y.quality)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
