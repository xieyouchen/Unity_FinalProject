using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mySlider : MonoBehaviour
{
    private Slider slider;//Slider ∂‘œÛ
    public GameObject chopTomato;
    bool chopOne = true;
    private myPlayer myPlayer;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        myPlayer = GameObject.Find("Chef_Model_withWalkParticle").GetComponent<myPlayer>();
        if (!slider || !myPlayer) return;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < 1)
        {
            slider.value += Time.deltaTime*0.1f;
        }
        if(slider.value >= 1 && chopOne)
        {
           
            chop();
            chopOne = false;
            Debug.Log("slider update function");
            GameObject chopSlider = GameObject.Find("ChoppingBoard_Countetop1/Canvas/chopSlider");
            if (!chopSlider) return;
            chopSlider.SetActive(false);
            myPlayer.stopChop();
        }

    }

    void chop()
    {
        Debug.Log("chop function");
        GameObject afterChopTomato = Instantiate(chopTomato);
        afterChopTomato.transform.parent = GameObject.Find("ChoppingBoard_Countetop1").transform;
        afterChopTomato.transform.localPosition = new Vector3(0.1f, 0.444f, 0f);
        Destroy(GameObject.Find("ChoppingBoard_Countetop1/Tomato(Clone)"));
    }

}
