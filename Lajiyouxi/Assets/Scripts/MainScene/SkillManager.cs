using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public List<SkillClass> skillList;

    public Text text_mainSkill_name, text_mainSkill_level, text_secondarySkill1_name, text_secondareySkill1_level, text_secondarySkill2_name, text_secondareySkill2_level;
    public Text text_SDL_level, text_BD_level, text_HQS_level, text_BFX_level, text_HY_level, text_ATKup_level, text_DEFup_level, text_SPDup_level, text_APup_level;
    // Start is called before the first frame update
    void Start()
    {
        DBManager dbm = new DBManager();
        skillList = dbm.GetAllSkill();
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Btn_Quit_Click()
    {
        DestroyImmediate(gameObject);
    }

    public void Refresh()
    {
        foreach (SkillClass i in skillList)
        {
            if (i.active == 1)
            {

            }
            else if (i.active == 2)
            {

            }
            else if (i.active == 3)
            {

            }
        }
    }
}
