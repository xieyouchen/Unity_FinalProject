using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myParticle : MonoBehaviour
{
    float scaleX;
    // Start is called before the first frame update
    void Start()
    {
        scaleX = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        // ������С��֮����ʧ������
        if(scaleX >= 0)
        {
            scaleX = gameObject.transform.localScale.x - 0.01f;

            gameObject.transform.localScale = new Vector3(scaleX, scaleX, scaleX);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
