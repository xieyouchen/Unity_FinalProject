using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class child : MonoBehaviour
{
    public Slider childSlider;
    private childSlider childSliderScript;

    public bool cry = false;
    public bool withParent = false;
    // Start is called before the first frame update
    void Start()
    {
        childSliderScript = childSlider.GetComponent<childSlider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(1, 6000) == 6 && !withParent)
        {
            cry = true;
            childSlider.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            cry = true;
            childSlider.gameObject.SetActive(true);
        }

    }

    public void accompany()
    {
        cry = false;
        withParent = true;
    }

    public void leave()
    {
        withParent = false;
        childSliderScript.startAudio = true;
    }
}
