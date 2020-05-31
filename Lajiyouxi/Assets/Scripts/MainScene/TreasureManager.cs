using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    public GameObject canvas;
    public MainManager mm;
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.parent.gameObject;
        mm = canvas.transform.GetComponent<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Button_OK_Click()
    {
        mm.event_finish = true;
        DestroyImmediate(gameObject);
    }
}
