using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sinkSlider : MonoBehaviour
{
    private Slider slider;//Slider ∂‘œÛ
    public GameObject sink;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        if (slider.value < 1)
        {
            slider.value += Time.deltaTime * 0.35f;
        }
        if(slider.value >= 1)
        {
            slider.value = 0;
            sink.GetComponent<sink>().washed();
            gameObject.SetActive(false);
        }
    }
}
