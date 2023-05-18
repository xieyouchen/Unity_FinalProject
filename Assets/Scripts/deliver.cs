using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class deliver : MonoBehaviour
{
    private GameObject player;
    private myPlayer playerScript;
    private bool start = true;
    AudioSource sucessAudio;

    public GameController gameController;
    public GameObject dishTray;
    public ParticleSystem starParticle;


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

        sucessAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.transform.GetChild(0).GetComponent<myHighlight>().highLight();
        gameObject.transform.GetChild(1).GetComponent<myHighlight>().highLight();
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.transform.GetChild(0).GetComponent<myHighlight>().lowLight();
        gameObject.transform.GetChild(1).GetComponent<myHighlight>().lowLight();
    }

    public static IEnumerator WaitForSecondsRealtime(float duration, Action action = null)
    {
        yield return new WaitForSecondsRealtime(duration);
        action?.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        player = other.gameObject;
        if (player.name == "Body") return;

        playerScript = player.GetComponent<myPlayer>();

        if (!playerScript.keyDown) return;
        playerScript.keyDown = false;

        if(playerScript.sthInHands == PLATE_FOOD)
        {
            if (gameController.numOrder == 0) return;
            gameController.updateScore(42);
            Destroy(player.transform.GetChild(6).gameObject);
            if (start)
            {
                start = false;
                starParticle.Play();
                sucessAudio.Play();
            }
            StartCoroutine(WaitForSecondsRealtime(2.0f, () =>
            {
                //生成一个脏盘子
                dishTray.GetComponent<dishTray>().generateDirtPlate();
                start = true;
            }));
        }

    }
}
