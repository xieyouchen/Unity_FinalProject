using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class myHighlight : MonoBehaviour
{
    
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        lowLight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        highLight();
    }

    private void OnTriggerExit(Collider other)
    {
        lowLight();
    }


    public void highLight()
    {
        //print("high");
        meshRenderer.material.SetFloat("Highlight_", 1);
    }

    public void lowLight()
    {
        meshRenderer.material.SetFloat("Highlight_", 0);
    }
}
