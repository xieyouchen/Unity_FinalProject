using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GreenCheck : MonoBehaviour
{
    public GameObject WarnIcon;

    // Start is called before the first frame update
    public static IEnumerator WaitForSecondsRealtime(float duration, Action action = null)
    {
        yield return new WaitForSecondsRealtime(duration);
        action?.Invoke();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitForSecondsRealtime(1.0f, () =>
        {
            //这里写上duration秒后要执行的内容
            gameObject.SetActive(false);
            WarnIcon.SetActive(true);
        }));
     
    }


}
