using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class childSlider : MonoBehaviour
{
    public GameObject childGameObject;
    public bool startAudio = true;


    private Slider slider;
    private bool child;
    private bool withParent;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        withParent = childGameObject.GetComponent<child>().withParent;
        child = childGameObject.GetComponent<child>().cry;

        if (slider.value > 0 && child)
        {
            slider.value -= Time.deltaTime * 0.1f;
        }
        if(slider.value <= 0)
        {
            if (startAudio)
            {
                childGameObject.GetComponent<AudioSource>().Play();
                startAudio = false;
            }
        }
        if(slider.value >= 0.5)
        {
            childGameObject.GetComponent<AudioSource>().Stop();
        }

        if (withParent)
        {
            slider.value += Time.deltaTime * 0.3f;
        }
        if(slider.value >= 1 && withParent)
        {
            gameObject.SetActive(false);
        }
    }
}
