using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class hobSlider : MonoBehaviour
{
    public GameObject cookingPot;
    public bool Running = false;
    private Slider slider;
    private float valueStart = 0;
    public GameObject GreenCheck;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < 1 && Running)
        {
            slider.value += Time.deltaTime * 0.1f;
        }
        if(slider.value >= 1 && Running)
        {
            Running = false;
            valueStart += 1 / 3.0f;
            slider.value = valueStart;
            gameObject.SetActive(false);
            GreenCheck.SetActive(true);
            if (cookingPot.GetComponent<cookingPot>().tomato > 2)
                cookingPot.GetComponent<cookingPot>().cooked = true;
        }
    }

    public void sliderRunning()
    {
        gameObject.SetActive(true);
        Running = true;
        slider.value = slider.value / 2;
    }

    public void newStart()
    {
        valueStart = 0;
        slider.value = 0;
        Running = false;
    }
}
