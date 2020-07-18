using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroClass
{
    public int level, exp, str, agi, Int, attrPoint, skillPoint, gold;
    public int id, atk, def, speed, life, ap, critDamage, fireResis, coldResis, lightResis, FirePene, coldPene, lightPene;
    public string name;
    public List<List<int>> skillList;
    public double critChance, dodge; 

    public void HeroGetExp(double exp)
    {
        
        this.exp = this.exp - System.Convert.ToInt32(exp);

        if (this.exp <= 0)
        {
            this.level++;

            double thisLevelExp = 3000;
            for (int i = 0; i < this.level - 1; i++)
            {
                thisLevelExp = thisLevelExp * 1.5;
            }

            this.exp = System.Convert.ToInt32(thisLevelExp) + this.exp;
            this.attrPoint += 5;
            this.skillPoint += 5;
        }
    }
}
