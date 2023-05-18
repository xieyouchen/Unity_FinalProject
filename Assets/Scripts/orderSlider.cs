using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class orderSlider : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        slider.value = slider.value - 0.0001f;
        if(slider.value <= 0)
        {
            gameObject.GetComponentInParent<GameController>().updateScore(-30);
            gameObject.GetComponentInParent<orderSelf>().myDestroy();
        }
    }
}
