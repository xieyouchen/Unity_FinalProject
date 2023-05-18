using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public GameObject order;
    public TMP_Text scoreText;
    public TMP_Text clockText;
    public GameObject overMenu;
    public GameObject menu;
    public bool menuFlag = false;
    public TMP_Text overScore;
    public GameObject overStarPanel;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject hob;
    public GameObject initialMenu;

    private hob hobScript;
    private int scoreNow;
    private int time;
    public int numOrder = 0;
    private int X = 300;
    int countDown = 4;
    public Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "map01") countDown = 3;
        menu.transform.SetSiblingIndex(1000);
        overMenu.transform.SetSiblingIndex(1010);
        initialMenu.transform.SetSiblingIndex(1011);
        hobScript = hob.GetComponent<hob>();
        scoreNow = 0;
        time = 180  ; //s

        if (initialMenu.active == true) return;

        StartCoroutine(updateTime());
        StartCoroutine(autoGenerateOrder());
        generateOrder();
        StartCoroutine(WaitForSecondsRealtime(5.0f, () =>
        {
            //这里写上duration秒后要执行的内容
            generateOrder();
        }));
    }

    public static IEnumerator WaitForSecondsRealtime(float duration, Action action = null)
    {
        yield return new WaitForSecondsRealtime(duration);
        action?.Invoke();
    }

    private IEnumerator autoGenerateOrder()
    {
        while(time > 0)
        {
            float time = UnityEngine.Random.Range(-5f, 5f);
            yield return new WaitForSeconds(20 + time);
            if (numOrder >= 4) break;
            generateOrder();
        }

    }

    private IEnumerator updateTime()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);// 每次 自减1，等待 1 秒
            time--;
            string second = "";
            if (time % 60 < 10)
            {
                second = "0" + time % 60;
            }
            else
            {
                second = "" + time % 60;
            }
            clockText.text = time/60 + ":" + second;
        }
        GameOver();
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            initialMenu.SetActive(false);
            menuFlag = true;
            Start();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //updateScore(10);
            menuFlag = !menuFlag;
        }
        if (menuFlag)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
            hobScript.cookingPotPause();
        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = 1;
            hobScript.cookingPotContinue();
        }

        GameObject[] array = GameObject.FindGameObjectsWithTag("Player");

        bool speedIsZero = true;
        for (int i = 0; i < array.Length; i++)
        {
            //print(array[i].name);
            if(array[i].GetComponent<myPlayer>().speed > 0)
            {
                speedIsZero = false;
                
            }
            //print(speedIsZero);
        }
        if (speedIsZero) GameOver();
    }

    void generateOrder()
    {
        gameObject.GetComponent<AudioSource>().Play();
        GameObject myPanel = Instantiate(order);
        myPanel.transform.SetParent(gameObject.transform, false);
        Vector3 pos = myPanel.transform.localPosition;
        myPanel.transform.localPosition = new Vector3(pos.x + X * numOrder, pos.y, pos.z);
        myPanel.transform.SetSiblingIndex(2+numOrder);
        numOrder++;
    }

    public void updateScore(int score)
    {
        scoreNow += score;
        scoreText.text = "" + scoreNow;
        Destroy(gameObject.transform.GetChild(2).gameObject);
        updatePos();
        numOrder--;
    }

    public void GameOver()
    {
        print("Game Over");
        Time.timeScale = 0;
        overMenu.SetActive(true);
        overScore.text = "Score: " + scoreNow;
        if(scoreNow >= 30 && scoreNow < 50)
        {
            star1.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (scoreNow >= 50 && scoreNow < 70)
        {
            star1.transform.localScale = new Vector3(1, 1, 1);
            star2.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (scoreNow >= 70)
        {
            star1.transform.localScale = new Vector3(1, 1, 1);
            star2.transform.localScale = new Vector3(1, 1, 1);
            star3.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void updatePos()
    {
        for(int i = 2; i< transform.childCount-countDown; i++)
        {
            Transform tmp = transform.GetChild(i);
            Vector3 pos = tmp.localPosition;
            tmp.localPosition = new Vector3(pos.x - X, pos.y, pos.z);
            tmp.SetSiblingIndex(i);
        }
    }
}
