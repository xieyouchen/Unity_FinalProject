using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hob : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Flame;
    public GameObject HobBase;
    public GameObject cookingPot;
    public GameObject GreenCheck;
    public GameObject WarnIcon;

    private bool keyDown = false;
    private GameObject player;
    private myPlayer playerScript;

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


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Flame.GetComponent<myHighlight>().highLight();
        HobBase.GetComponent<myHighlight>().highLight();
    }

    private void OnTriggerExit(Collider other)
    {
        Flame.GetComponent<myHighlight>().lowLight();
        HobBase.GetComponent<myHighlight>().lowLight();
    }

    private void OnTriggerStay(Collider other)
    {
        player = other.gameObject;
        if (player.name == "Body") return;

        playerScript = player.GetComponent<myPlayer>();

        if (!playerScript.keyDown) return;
        playerScript.keyDown = false;

        switch (playerScript.sthInHands)
        {
            case FREE:
                {
                    cookingPot cookingPotScript = cookingPot.GetComponent<cookingPot>();

                    playerScript.mycatch(cookingPot.transform);
                    cookingPotPause();
                    break;
                }
            case TOMATO_CHOPED:
                {
                    // 销毁手中物体，cooking()
                    cookingPot.GetComponent<cookingPot>().cooking();
                    Destroy(player.transform.GetChild(6).gameObject);
                    break;
                }
            case PLATE:
                {
                    // getIcon() 如果锅还没装好3个icon，不能被取走食物
                    if (!cookingPot.GetComponent<cookingPot>().cooked) return;
                    print("plate get icon");
                    GameObject plusIcon = cookingPot.GetComponent<cookingPot>().PlusIconAll;
                    player.transform.GetChild(6).GetComponent<plate>().cookedFood(plusIcon);
                    cookingPot.GetComponent<cookingPot>().newStart();
                    break;
                }
            case COOKINGPOT_WITH_FOOD:
                {
                    player.transform.GetChild(6).SetParent(gameObject.transform.GetChild(2));
                    break;
                }
            case COOKINGPOT:
                {
                    player.transform.GetChild(6).SetParent(gameObject.transform.GetChild(2));
                    cookingPot.transform.localPosition = new Vector3(0, 0, 0);
                    cookingPot.transform.localEulerAngles = new Vector3(0, 0, 0);
                    cookingPot.GetComponent<cookingPot>().hobSlider.SetActive(true);
                    break;
                }
        }

    }

    public void cookingPotPause()
    {
        GreenCheck.SetActive(false);
        WarnIcon.SetActive(false);
        cookingPot.GetComponent<cookingPot>().hobSlider.SetActive(false);
    }

    public void cookingPotContinue()
    {

        cookingPot.GetComponent<cookingPot>().hobSlider.SetActive(true);
    }
    
}
