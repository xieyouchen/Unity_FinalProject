using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trash : MonoBehaviour
{
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.transform.GetChild(0).GetComponent<myHighlight>().highLight();
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.transform.GetChild(0).GetComponent<myHighlight>().lowLight();
    }

    private void OnTriggerStay(Collider other)
    {
        player = other.gameObject;
        playerScript = player.GetComponent<myPlayer>();

        if (!playerScript.keyDown) return;
        playerScript.keyDown = false;

        playerScript.toTrash();
    }
}
