using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarnIcon : MonoBehaviour
{
    public Sprite warnIcon;
    public float timer = TIMER;
    public bool blink = false;
    public float scaleTime = 2;
    private float X = 0.1f;
    private bool imageFlag = true;
    public GameObject cookingPot;
    private bool startAudio = true;

    public float blinkTimer = BLINKTIMER;

    const float BLINKTIMER = 0.5f;
    const float TIMER = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame  
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime * 0.1f;
        }
        if (timer <= 0 && !blink)
        {
            gameObject.AddComponent<Image>();
            gameObject.GetComponent<Image>().sprite = warnIcon;
            blink = true;
        }
        if (blink && blinkTimer>=0)
        {
            if (startAudio)
            {
                gameObject.GetComponent<AudioSource>().Play();
                startAudio = false;
            }
            if (scaleTime <= 2)
            {
                X = 0.1f;
            }
            else if(scaleTime >= 2.35)
            {
                X = -0.1f;
            }
            scaleTime += Time.deltaTime*X;
            gameObject.transform.localScale = new Vector3(scaleTime, scaleTime, 2);
            imageFlag = !imageFlag;
            gameObject.GetComponent<Image>().enabled = imageFlag;
            blinkTimer -= Time.deltaTime * 0.1f;
        }
        if(blinkTimer <= 0)
        {
            cookingPot.GetComponent<cookingPot>().blinkTimerArrive();
            gameObject.GetComponent<Image>().enabled = false;
        }
        if (Input.GetKey(KeyCode.B))
        {
            newStart();
        }
    }

    public void newStart()
    {
        cookingPot.GetComponent<cookingPot>().newStart();
        warnIconNew();
    }

    public void warnIconNew()
    {
        timer = TIMER;
        blink = false;
        blinkTimer = BLINKTIMER;
        gameObject.SetActive(false);
        startAudio = true;
    }
}
