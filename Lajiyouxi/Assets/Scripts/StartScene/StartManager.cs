using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public Button btn_continue, btn_start, btn_quit;
    public GameObject createGameNotice_popup;

    private string savedGame = "";
    // Start is called before the first frame update
    void Start()
    {
        DBManager dbm = new DBManager();
        savedGame = dbm.GetGame(1);
        if(savedGame == "")
        {
            btn_continue.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Btn_Start_Onclick()
    {

        if(savedGame == "")
        {
            CreateNewGame();
        }
        else
        {
            GameObject instance = (GameObject)Instantiate(createGameNotice_popup, new Vector2(960,540), transform.rotation);
            instance.transform.SetParent(transform);
            HideUI();
        }
    }

    public void CreateNewGame()
    {
        DBManager dbm = new DBManager();

        string newGame = System.DateTime.Now.ToString();
        dbm.SaveGame(1, newGame);

        EventClass newEvent = new EventClass();
        newEvent.id = 1;
        newEvent.level = 1;
        newEvent.area1 = 0;
        newEvent.area2 = 0;
        newEvent.area3 = 0;
        newEvent.event_left = 30;
        newEvent.battle_position = 0;
        newEvent.battle_finish = 0;
        dbm.SaveEvent(newEvent);

        SceneManager.LoadScene(1);
    }

    public void Btn_Quit_Onclick()
    {
        Application.Quit();
    }

    public void Btn_Continue_Onclick()
    {
        SceneManager.LoadScene(1);
    }

    public void HideUI()
    {
        btn_start.gameObject.SetActive(false);
        btn_quit.gameObject.SetActive(false);
        btn_continue.gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        btn_start.gameObject.SetActive(true);
        btn_quit.gameObject.SetActive(true);
        btn_continue.gameObject.SetActive(true);
    }

}
