using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class body : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public Slider loverSlider;
    private myPlayer p1Script;
    private myPlayer p2Script;
    private bool ctrKeyDown = false;
    private bool hopeHug = false;
  


    const int FREE = 0;
    const int TOMATO = 1;
    const int TOMATO_CHOPED = 2;
    const int COOKINGPOT = 8;
    const int COOKINGPOT_WITH_FOOD = 3;
    const int COOKINGPOT_WITH_BURNEDFOOD = 4;
    const int PLATE = 5;
    const int PLATE_FOOD = 6;
    const int DIRTY_PLATE = 7;
    const int DOWN = 2;
    const int STH_INDEX = 6;

    // Start is called before the first frame update
    void Start()
    {
        p1Script = Player1.GetComponent<myPlayer>();
        p2Script = Player2.GetComponent<myPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (p1Script.hopeHug || p2Script.hopeHug)
            hopeHug = true;
        if (p1Script.ctrKeyDown || p2Script.ctrKeyDown) ctrKeyDown = true;
    }

    private void OnTriggerStay(Collider other)
    {
        print(other.gameObject.name);
        
        if (other.gameObject.CompareTag("Player") && hopeHug && ctrKeyDown && p1Script.sthInHands == FREE && p2Script.sthInHands == FREE)
        {
            hopeHug = false;
            ctrKeyDown = false;
            p1Script.ctrKeyDown = false;
            p2Script.ctrKeyDown = false;
            loverSlider.gameObject.SetActive(true);
        }
    }
}
