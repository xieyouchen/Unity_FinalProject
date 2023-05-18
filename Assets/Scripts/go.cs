using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class go : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void goMap1()
    {
        SceneManager.LoadScene("map01", LoadSceneMode.Single);
    }
    public void goMap2()
    {
        SceneManager.LoadScene("map02", LoadSceneMode.Single);
    }
    public void goMap3()
    {
        SceneManager.LoadScene("map03", LoadSceneMode.Single);
    }

    public void reStart()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
