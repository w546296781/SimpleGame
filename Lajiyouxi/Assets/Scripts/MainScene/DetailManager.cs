using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailManager : MonoBehaviour
{
    public Text showText;
    // Start is called before the first frame update
    void Start()
    {
        SkillClass skill = gameObject.transform.parent.parent.GetComponent<SkillManager>().selectedSkill;
        string detail = gameObject.transform.parent.parent.GetComponent<SkillManager>().selectedDetail;

        showText.text = GettheIntro(skill.id, ConvertDetailtoOrder(detail));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GettheIntro(int id, int order)
    {
        string result = "";

        switch (id)
        {
            case 1:
                switch (order)
                {
                    case 1:
                        result = "这是闪电链技能天赋1";
                        break;
                    case 2:
                        result = "这是闪电链技能天赋2";
                        break;
                    case 3:
                        result = "这是闪电链技能天赋3";
                        break;
                    case 4:
                        result = "这是闪电链技能天赋4";
                        break;
                    case 5:
                        result = "这是闪电链技能天赋5";
                        break;
                    case 6:
                        result = "这是闪电链技能天赋6";
                        break;
                    case 7:
                        result = "这是闪电链技能天赋7";
                        break;
                    case 8:
                        result = "这是闪电链技能天赋8";
                        break;
                    case 9:
                        result = "这是闪电链技能天赋9";
                        break;
                    case 10:
                        result = "这是闪电链技能天赋10";
                        break;
                    case 11:
                        result = "这是闪电链技能天赋11";
                        break;
                    case 12:
                        result = "这是闪电链技能天赋12";
                        break;
                }
                break;
            case 2:
                switch (order)
                {
                    case 1:
                        result = "这是火球术技能天赋1";
                        break;
                    case 2:
                        result = "这是火球术技能天赋2";
                        break;
                    case 3:
                        result = "这是火球术技能天赋3";
                        break;
                    case 4:
                        result = "这是火球术技能天赋4";
                        break;
                    case 5:
                        result = "这是火球术技能天赋5";
                        break;
                    case 6:
                        result = "这是火球术技能天赋6";
                        break;
                    case 7:
                        result = "这是火球术技能天赋7";
                        break;
                    case 8:
                        result = "这是火球术技能天赋8";
                        break;
                    case 9:
                        result = "这是火球术技能天赋9";
                        break;
                    case 10:
                        result = "这是火球术技能天赋10";
                        break;
                    case 11:
                        result = "这是火球术技能天赋11";
                        break;
                    case 12:
                        result = "这是火球术技能天赋12";
                        break;
                }
                break;
            case 3:
                switch (order)
                {
                    case 1:
                        result = "这是暴风雪技能天赋1";
                        break;        
                    case 2:           
                        result = "这是暴风雪技能天赋2";
                        break;        
                    case 3:           
                        result = "这是暴风雪技能天赋3";
                        break;        
                    case 4:           
                        result = "这是暴风雪技能天赋4";
                        break;        
                    case 5:           
                        result = "这是暴风雪技能天赋5";
                        break;        
                    case 6:           
                        result = "这是暴风雪技能天赋6";
                        break;        
                    case 7:           
                        result = "这是暴风雪技能天赋7";
                        break;        
                    case 8:           
                        result = "这是暴风雪技能天赋8";
                        break;        
                    case 9:           
                        result = "这是暴风雪技能天赋9";
                        break;        
                    case 10:          
                        result = "这是暴风雪技能天赋10";
                        break;        
                    case 11:          
                        result = "这是暴风雪技能天赋11";
                        break;        
                    case 12:          
                        result = "这是暴风雪技能天赋12";
                        break;
                }
                break;
            case 4:
                switch (order)
                {
                    case 1:
                        result = "这是冰弹技能天赋1";
                        break;        
                    case 2:           
                        result = "这是冰弹技能天赋2";
                        break;        
                    case 3:           
                        result = "这是冰弹技能天赋3";
                        break;        
                    case 4:           
                        result = "这是冰弹技能天赋4";
                        break;        
                    case 5:           
                        result = "这是冰弹技能天赋5";
                        break;        
                    case 6:           
                        result = "这是冰弹技能天赋6";
                        break;        
                    case 7:           
                        result = "这是冰弹技能天赋7";
                        break;        
                    case 8:           
                        result = "这是冰弹技能天赋8";
                        break;        
                    case 9:           
                        result = "这是冰弹技能天赋9";
                        break;        
                    case 10:          
                        result = "这是冰弹技能天赋10";
                        break;        
                    case 11:          
                        result = "这是冰弹技能天赋11";
                        break;        
                    case 12:          
                        result = "这是冰弹技能天赋12";
                        break;
                }
                break;
            case 5:
                switch (order)
                {
                    case 1:
                        result = "这是火雨技能天赋1";
                        break;        
                    case 2:           
                        result = "这是火雨技能天赋2";
                        break;        
                    case 3:           
                        result = "这是火雨技能天赋3";
                        break;        
                    case 4:           
                        result = "这是火雨技能天赋4";
                        break;        
                    case 5:           
                        result = "这是火雨技能天赋5";
                        break;        
                    case 6:           
                        result = "这是火雨技能天赋6";
                        break;        
                    case 7:           
                        result = "这是火雨技能天赋7";
                        break;       
                    case 8:           
                        result = "这是火雨技能天赋8";
                        break;        
                    case 9:           
                        result = "这是火雨技能天赋9";
                        break;        
                    case 10:          
                        result = "这是火雨技能天赋10";
                        break;        
                    case 11:          
                        result = "这是火雨技能天赋11";
                        break;        
                    case 12:          
                        result = "这是火雨技能天赋12";
                        break;
                }
                break;
            case 6:
                switch (order)
                {
                    case 1:
                        result = "这是闪电链技能天赋1";
                        break;
                    case 2:
                        result = "这是闪电链技能天赋2";
                        break;
                    case 3:
                        result = "这是闪电链技能天赋3";
                        break;
                    case 4:
                        result = "这是闪电链技能天赋4";
                        break;
                    case 5:
                        result = "这是闪电链技能天赋5";
                        break;
                    case 6:
                        result = "这是闪电链技能天赋6";
                        break;
                    case 7:
                        result = "这是闪电链技能天赋7";
                        break;
                    case 8:
                        result = "这是闪电链技能天赋8";
                        break;
                    case 9:
                        result = "这是闪电链技能天赋9";
                        break;
                    case 10:
                        result = "这是闪电链技能天赋10";
                        break;
                    case 11:
                        result = "这是闪电链技能天赋11";
                        break;
                    case 12:
                        result = "这是闪电链技能天赋12";
                        break;
                }
                break;
            case 7:
                switch (order)
                {
                    case 1:
                        result = "这是闪电链技能天赋1";
                        break;
                    case 2:
                        result = "这是闪电链技能天赋2";
                        break;
                    case 3:
                        result = "这是闪电链技能天赋3";
                        break;
                    case 4:
                        result = "这是闪电链技能天赋4";
                        break;
                    case 5:
                        result = "这是闪电链技能天赋5";
                        break;
                    case 6:
                        result = "这是闪电链技能天赋6";
                        break;
                    case 7:
                        result = "这是闪电链技能天赋7";
                        break;
                    case 8:
                        result = "这是闪电链技能天赋8";
                        break;
                    case 9:
                        result = "这是闪电链技能天赋9";
                        break;
                    case 10:
                        result = "这是闪电链技能天赋10";
                        break;
                    case 11:
                        result = "这是闪电链技能天赋11";
                        break;
                    case 12:
                        result = "这是闪电链技能天赋12";
                        break;
                }
                break;
            case 8:
                switch (order)
                {
                    case 1:
                        result = "这是闪电链技能天赋1";
                        break;
                    case 2:
                        result = "这是闪电链技能天赋2";
                        break;
                    case 3:
                        result = "这是闪电链技能天赋3";
                        break;
                    case 4:
                        result = "这是闪电链技能天赋4";
                        break;
                    case 5:
                        result = "这是闪电链技能天赋5";
                        break;
                    case 6:
                        result = "这是闪电链技能天赋6";
                        break;
                    case 7:
                        result = "这是闪电链技能天赋7";
                        break;
                    case 8:
                        result = "这是闪电链技能天赋8";
                        break;
                    case 9:
                        result = "这是闪电链技能天赋9";
                        break;
                    case 10:
                        result = "这是闪电链技能天赋10";
                        break;
                    case 11:
                        result = "这是闪电链技能天赋11";
                        break;
                    case 12:
                        result = "这是闪电链技能天赋12";
                        break;
                }
                break;
            case 9:
                switch (order)
                {
                    case 1:
                        result = "这是闪电链技能天赋1";
                        break;
                    case 2:
                        result = "这是闪电链技能天赋2";
                        break;
                    case 3:
                        result = "这是闪电链技能天赋3";
                        break;
                    case 4:
                        result = "这是闪电链技能天赋4";
                        break;
                    case 5:
                        result = "这是闪电链技能天赋5";
                        break;
                    case 6:
                        result = "这是闪电链技能天赋6";
                        break;
                    case 7:
                        result = "这是闪电链技能天赋7";
                        break;
                    case 8:
                        result = "这是闪电链技能天赋8";
                        break;
                    case 9:
                        result = "这是闪电链技能天赋9";
                        break;
                    case 10:
                        result = "这是闪电链技能天赋10";
                        break;
                    case 11:
                        result = "这是闪电链技能天赋11";
                        break;
                    case 12:
                        result = "这是闪电链技能天赋12";
                        break;
                }
                break;


        }

        return result;
    }

    public int ConvertDetailtoOrder(string detail)
    {
        int result = 0;

        if (detail.Contains("1-1"))
        {
            result = 1;
        }
        else if (detail.Contains("1-2"))
        {
            result = 2;
        }
        else if (detail.Contains("1-3"))
        {
            result = 3;
        }
        else if (detail.Contains("2-1"))
        {
            result = 4;
        }
        else if (detail.Contains("2-2"))
        {
            result = 5;
        }
        else if (detail.Contains("2-3"))
        {
            result = 6;
        }
        else if (detail.Contains("3-1"))
        {
            result = 7;
        }
        else if (detail.Contains("3-2"))
        {
            result = 8;
        }
        else if (detail.Contains("3-3"))
        {
            result = 9;
        }
        else if (detail.Contains("4-1"))
        {
            result = 10;
        }
        else if (detail.Contains("4-2"))
        {
            result = 11;
        }
        else if (detail.Contains("4-3"))
        {
            result = 12;
        }
        else
        {
            result = 0;
        }


        return result;
    }
}
