using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cookingPot : MonoBehaviour
{
    public GameObject hobSlider;
    public GameObject GreenCheck;
    public GameObject PlusIconAll;
    public GameObject PlusIcon_LeftUP;
    public GameObject PlusIcon_Down;
    public GameObject PlusIcon_Right;
    public Sprite PlusIcon;
    public Sprite BurnIcon;
    public Sprite IconTomato;
    public GameObject Steam;
    public GameObject BurnedSmoke;
    public GameObject WarnIcon;
    public int tomato = LEFTUP;
    public bool burned = false;
    public bool addTomato = false;
    public bool cooked = false;
    private bool tomatoIsInHands = true;
    

    const int LEFTUP = 0;
    const int RIGHT = 1;
    const int DOWN = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cooking()
    {
        addTomato = true;
        if (burned) return;
        if (addTomato && tomatoIsInHands)
        {
            addTomato = false;
            if (tomato > DOWN) return;
            hobSlider.GetComponent<hobSlider>().sliderRunning();
            Steam.SetActive(true);
            WarnIcon.SetActive(false);
            WarnIcon.GetComponent<WarnIcon>().warnIconNew();
            switch (tomato)
            {
                case LEFTUP:
                    {
                        PlusIcon_LeftUP.GetComponent<SpriteRenderer>().sprite = IconTomato;
                        tomato++;
                        break;
                    }
                case RIGHT:
                    {
                        PlusIcon_Right.GetComponent<SpriteRenderer>().sprite = IconTomato;
                        tomato++;
                        break;
                    }
                case DOWN:
                    {
                        PlusIcon_Down.GetComponent<SpriteRenderer>().sprite = IconTomato;
                        tomato++;
                        break;
                    }
            }
        }
    }

    public void blinkTimerArrive()
    {
        PlusIcon_LeftUP.SetActive(false);
        PlusIcon_Right.SetActive(false);
        PlusIcon_Down.GetComponent<SpriteRenderer>().sprite = BurnIcon;
        BurnedSmoke.SetActive(true);
        Steam.SetActive(false);
        burned = true;
        WarnIcon.GetComponent<AudioSource>().Stop();
    }

    public void newStart()
    {
        print("cooking pot newStart()");
        PlusIcon_LeftUP.SetActive(true);
        PlusIcon_Right.SetActive(true);
        PlusIcon_LeftUP.GetComponent<SpriteRenderer>().sprite = PlusIcon;
        PlusIcon_Right.GetComponent<SpriteRenderer>().sprite = PlusIcon;
        PlusIcon_Down.GetComponent<SpriteRenderer>().sprite = PlusIcon;
        BurnedSmoke.SetActive(false);
        tomato = LEFTUP;
        burned = false;
        Steam.SetActive(false);
        hobSlider.GetComponent<hobSlider>().newStart();
        GreenCheck.SetActive(false);
        WarnIcon.SetActive(false);
        WarnIcon.GetComponent<WarnIcon>().warnIconNew();
        cooked = false;
    }


    public void burnedFlag()
    {
        burned = true;
    }
}
