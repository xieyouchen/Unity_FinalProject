using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dishTray : MonoBehaviour
{
    private GameObject player;
    private myPlayer playerScript;
    public GameObject dirtyPlate;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateDirtPlate()
    {
        GameObject myPlate = Instantiate(dirtyPlate);
        myPlate.transform.SetParent(gameObject.transform);
        myPlate.transform.localPosition = new Vector3(0, 0.4f, 0);
        myPlate.name = "Plate";
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.transform.GetChild(0).GetComponent<myHighlight>().highLight();
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.transform.GetChild(0).GetComponent<myHighlight>().lowLight();
    }

    private void OnTriggerStay(Collider other)
    {
        player = other.gameObject;
        if (player.name == "Body") return;

        playerScript = player.GetComponent<myPlayer>();

        if (!playerScript.keyDown) return;
        playerScript.keyDown = false;

        // 如果没东西，返回即可
        if (gameObject.transform.childCount == 2) return;

        playerScript.mycatch(gameObject.transform.GetChild(2));
    }
}
