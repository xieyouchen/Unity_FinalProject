using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chopBoard : MonoBehaviour
{
    public GameObject choppedTomatoPrefab;
    public GameObject chopedBoard;
    public GameObject chopSlider;
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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player?.name == "Body") return;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<myHighlight>().highLight();
        chopedBoard.GetComponent<myHighlight>().highLight();
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<myHighlight>().lowLight();
        chopedBoard.GetComponent<myHighlight>().lowLight();
    }

    public void getsth(GameObject sth)
    {
        sth.transform.SetParent(gameObject.transform);
        sth.transform.localPosition = new Vector3(0.1f, 0.444f, 0);
    }

    public void chop()
    {
        chopSlider.SetActive(true);
        playerScript.chopping();
    }

    public void chopped()
    {
        playerScript.stopChop();
        Destroy(gameObject.transform.GetChild(4).gameObject);
        GameObject choppedTomato = Instantiate(choppedTomatoPrefab);
        choppedTomato.name = "Tomato_Chopped";
        getsth(choppedTomato);
        
    }

    private void OnTriggerStay(Collider other)
    {
        player = other.gameObject;
        if (player.name == "Body") return;

        playerScript = player.GetComponent<myPlayer>();

        if (playerScript.keyDown)
        {
            playerScript.keyDown = false;
            // 没东西
            if (gameObject.transform.childCount == 4)
            {
                // 如果手上有 tomato，或者 tomato_chopped，可以放下
                if (playerScript.sthInHands == TOMATO || playerScript.sthInHands == TOMATO_CHOPED)
                {
                    getsth(player.transform.GetChild(6).gameObject);
                }
            }
            // 有东西，并且空手，拿起
            else
            {
                if(playerScript.sthInHands == FREE)
                {
                    playerScript.mycatch(gameObject.transform.GetChild(4));
                }
            }
        }
        if (playerScript.ctrKeyDown)
        {
            playerScript.ctrKeyDown = false;
            // 有东西并且是 tomato，并且player空手
            if (gameObject.transform.childCount == 4) return;
            if (gameObject.transform.GetChild(4).gameObject.name == "Tomato" && playerScript.sthInHands == FREE)
            {
                chop();
            }
        }



    }
}
