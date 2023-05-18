using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plate : MonoBehaviour
{
    public GameObject soup;
    public Material cookedSoupMaterial;
    public Material dirtyMaterial;
    public Material cleanMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cookedFood(GameObject plusIconPara)
    {
        GameObject plusIcon = Instantiate(plusIconPara);
        soup.GetComponent<MeshRenderer>().enabled = true;
        plusIcon.transform.SetParent(gameObject.transform);
        plusIcon.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(558f, 227.7f, -4f);
        plusIcon.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 0);
        soup.GetComponent<MeshRenderer>().material = cookedSoupMaterial;
    }

    public void dirty()
    {
        soup.GetComponent<MeshRenderer>().enabled = true;
        soup.GetComponent<MeshRenderer>().material = dirtyMaterial;
    }

    public void clean()
    {
        soup.GetComponent<MeshRenderer>().enabled = false;
        soup.GetComponent<MeshRenderer>().material = cleanMaterial;
    }

    
}
