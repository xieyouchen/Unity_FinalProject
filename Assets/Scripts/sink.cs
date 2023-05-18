using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sink : MonoBehaviour
{
    private GameObject player;
    private myPlayer playerScript;
    
    public List<GameObject> arrayCleanPlate = new List<GameObject>();
    public List<GameObject> arrayDirtyPlate = new List<GameObject>();
    public GameObject sinkSlider;
    public GameObject cleanPlate;

    const int FREE = 0;
    const int TOMATO = 1;
    const int TOMATO_CHOPED = 2;
    const int COOKINGPOT = 8;
    const int COOKINGPOT_WITH_FOOD = 3;
    const int COOKINGPOT_WITH_BURNEDFOOD = 4;
    const int PLATE = 5;
    const int PLATE_FOOD = 6;
    const int DIRTY_PLATE = 7;
    const int DOWN = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        observeSink();
    }

    public void getDirtyPlate(GameObject plate)
    {

        plate.transform.SetParent(gameObject.transform);
        plate.transform.localPosition = new Vector3(0.438022f, 0.1953201f, -0.02301079f);
        plate.transform.localEulerAngles = new Vector3(6.521f, 78.403f, 21.175f);
        plate.name = "toCleanPlate";
    }

    public void washed()
    {
        if (player.name == "Body") return;
        player.GetComponent<Animator>().SetBool("isCleaning", false);
        Destroy(arrayDirtyPlate[0]);
        GameObject plate = Instantiate(cleanPlate);
        plate.transform.SetParent(gameObject.transform);
        plate.transform.localPosition = new Vector3(-0.5f, 0.4f, -0.023f);
        plate.name = "Plate";
    }

    public void washing()
    {
        if (player.name == "Body") return;

        player.GetComponent<Animator>().SetBool("isCleaning", true);
    }


    private void OnTriggerStay(Collider other)
    {
        player = other.gameObject;
        if (player.name == "Body") return;

        playerScript = player.GetComponent<myPlayer>();
        // 拿放
        if (playerScript.keyDown)
        {
            playerScript.keyDown = false;
            // 看player手上有没有东西
            switch (playerScript.sthInHands)
            {
                // 没东西
                case FREE:
                    {
                        if (arrayCleanPlate.Count == 0) return;
                        playerScript.mycatch(arrayCleanPlate[0].transform);
                        break;
                    }
                    // 脏盘子
                case DIRTY_PLATE:
                    {
                        getDirtyPlate(player.transform.GetChild(6).gameObject);
                        break;
                    }
            }

        }
        // 洗盘子
        else if (playerScript.ctrKeyDown)
        {
            playerScript.ctrKeyDown = false;
            if (arrayDirtyPlate.Count == 0) return;
            // 如果不是空手，返回
            if (playerScript.sthInHands != FREE) return;
            washing();
            sinkSlider.SetActive(true);
        }
            



    }

    private void observeSink()
    {
        arrayCleanPlate.Clear();
        arrayDirtyPlate.Clear();
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject plate = gameObject.transform.GetChild(i).gameObject;
            if (plate.name == "toCleanPlate") arrayDirtyPlate.Add(plate); 
            else if(plate.name == "Plate")arrayCleanPlate.Add(plate);
        }
    }
}
