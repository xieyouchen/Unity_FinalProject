using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loverSlider : MonoBehaviour
{
    private Slider slider;
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
            slider.value += Time.deltaTime * 0.3f;
        }
        if(slider.value >= 1)
        {
            GameObject[] array = GameObject.FindGameObjectsWithTag("Player");
            for(int i = 0; i < array.Length; i++)
            {
                array[i].GetComponent<myPlayer>().hopeHug = false;
                array[i].GetComponent<myPlayer>().speed = 3.5f;
                slider.value = 0;
                gameObject.SetActive(false);
            }

        }
    }
}
