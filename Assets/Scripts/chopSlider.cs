using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chopSlider : MonoBehaviour
{
    private Slider slider;//Slider ∂‘œÛ
    public GameObject chopBoard;
    AudioSource audioSource;
    private bool play = true;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        audioSource = chopBoard.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        if (slider.value < 1)
        {
            if (play)
            {
                play = false;
                audioSource.Play();
            }
            
            slider.value += Time.deltaTime * 0.35f;
        }
        if (slider.value >= 1)
        {
            play = true;
            audioSource.Stop();
            slider.value = 0;
            chopBoard.GetComponent<chopBoard>().chopped() ;
            gameObject.SetActive(false);
        }
    }
}
