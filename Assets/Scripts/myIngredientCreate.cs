using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myIngredientCreate : MonoBehaviour
{
    public GameObject myBase;
    public GameObject myLid;
    public GameObject tomatoPrefab;

    private Animator animator;
    private GameObject player;
    private myPlayer playerScript;

    const int FREE = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void myOpenTrue()
    {
        animator.SetBool("Open", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        myLid.GetComponent<myHighlight>().highLight();
        myBase.GetComponent<myHighlight>().highLight();
    }

    private void OnTriggerExit(Collider other)
    {
        myLid.GetComponent<myHighlight>().lowLight();
        myBase.GetComponent<myHighlight>().lowLight();
    }

    private void OnTriggerStay(Collider other)
    {
        player = other.gameObject;
        if (player.name == "Body") return;

        playerScript = player.GetComponent<myPlayer>();

        if (!playerScript.keyDown) return;
        playerScript.keyDown = false;

        if(playerScript.sthInHands == FREE)
        {
            GameObject tomato = Instantiate(tomatoPrefab);
            tomato.name = "Tomato";
            playerScript.mycatch(tomato.transform);
            myOpenTrue();
        }
    }
}
