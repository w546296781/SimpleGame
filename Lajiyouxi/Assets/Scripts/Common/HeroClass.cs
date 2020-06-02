using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroClass
{
    public int level, exp, str, agi, Int, attrPoint, skillPoint;
    public int id, atk, def, speed, life, ap, critDamage, fireResis, coldResis, lightResis, FirePene, coldPene, lightPene;
    public string name;
    public List<List<int>> skillList;
    public double critChance, dodge; 
}
