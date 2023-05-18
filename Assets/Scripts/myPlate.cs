using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myPlate : MonoBehaviour
{
    public GameObject soup;
    public Material cleanMaterial;
    public Material dirtyMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            soup.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<MeshRenderer>().material = dirtyMaterial;
        }
    }
}
