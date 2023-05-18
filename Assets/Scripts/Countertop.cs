using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countertop : MonoBehaviour
{
    const int FREE_COUNTER = 0;
    const int POT_FOOD_COUNTER = 1;// ��ʳ���¯��
    const int POT_COUNTER = 3;
    const int CLEAN_PLATE_COUNTER = 2;
    const int TOMATO_COUNTER = 4;
    const int TOMATO_CHOPPED_COUNTER = 5;
    public int sthOnCounter = FREE_COUNTER;
    public bool keyDown = false;

    const int FREE = 0;
    const int TOMATO = 1;
    const int TOMATO_CHOPED = 2;
    const int COOKINGPOT_WITH_FOOD = 3;
    const int COOKINGPOT_WITH_BURNEDFOOD = 4;
    const int PLATE = 5;
    const int PLATE_FOOD = 6;
    const int DIRTY_PLATE = 7;
    const int DOWN = 2;
    private GameObject player;
    private myPlayer playerScript;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        observeCounter();
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.name == "childCountertop")
        {
            child childScript = GameObject.FindWithTag("child").GetComponent<child>();
            childScript.leave();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        player = other.gameObject;
        if (player.name == "Body") return;
        playerScript = player.GetComponent<myPlayer>();

        // �����С������
        if(gameObject.name == "childCountertop")
        {
            if (playerScript.ctrKeyDown)
            {
                playerScript.ctrKeyDown = false;
                child childScript = GameObject.FindWithTag("child").GetComponent<child>();
                childScript.accompany();
            }
        }
        // ������û����
        else if (sthOnCounter == FREE_COUNTER)
        {
            if (!playerScript.keyDown) return;
            playerScript.keyDown = false;
            // �����ж���
            if (playerScript.sthInHands != FREE)
            {

                catchSth(player.transform.GetChild(6));
            }
        }
        else
        // �������ж���
        {
            if (!playerScript.keyDown) return;
            playerScript.keyDown = false;
            // ����û����
            if (playerScript.sthInHands == FREE)
            {
                playerScript.mycatch(gameObject.transform.GetChild(1));
            }
            else
            // ˫�����ж���
            {
                switch (sthOnCounter)
                {
                    // �ɾ�������
                    case CLEAN_PLATE_COUNTER:
                        {
                            // ����װ�����Ĺ����Ϳ��Է�����
                            if (playerScript.sthInHands != COOKINGPOT_WITH_FOOD) return;
                            GameObject plusIcon = player.transform.GetChild(6).GetComponent<cookingPot>().PlusIconAll;
                            gameObject.transform.GetChild(1).GetComponent<plate>().cookedFood(plusIcon);
                            player.transform.GetChild(6).GetComponent<cookingPot>().newStart();
                            break;
                        }
                    // װ�����Ĺ�
                    case POT_FOOD_COUNTER:
                        {
                            // ������Ǹɾ����ӣ����Է�����
                            if (playerScript.sthInHands != PLATE) return;
                            GameObject plusIcon = gameObject.transform.GetChild(1).GetComponent<cookingPot>().PlusIconAll;
                            player.transform.GetChild(6).GetComponent<plate>().cookedFood(plusIcon);
                            gameObject.transform.GetChild(1).GetComponent<cookingPot>().newStart();
                            break;
                        }
                    case TOMATO_CHOPPED_COUNTER:
                        {
                            playerScript.mycatch(gameObject.transform.GetChild(1));
                            break;
                        }
                    case TOMATO_COUNTER:
                        {
                            playerScript.mycatch(gameObject.transform.GetChild(1));
                            break;
                        }
                }
            }
        }
    }

    private void observeCounter()
    {
        if (gameObject.transform.childCount == 1) sthOnCounter = FREE_COUNTER;
        else
        {
            Transform sth = gameObject.transform.GetChild(1);
            switch (sth.name)
            {
                case "Plate":
                    {
                        sthOnCounter = CLEAN_PLATE_COUNTER;
                        break;
                    }
                case "CookingPot":
                    {
                        sthOnCounter = POT_COUNTER;
                        if (sth.GetComponent<cookingPot>().tomato > DOWN && sth.GetComponent<cookingPot>().burned == false)
                            sthOnCounter = POT_FOOD_COUNTER;
                        break;
                    }
                case "Tomato":
                    {
                        sthOnCounter = TOMATO_COUNTER;
                        break;
                    }
                case "Tomato_Chopped":
                    {
                        sthOnCounter = TOMATO_CHOPPED_COUNTER;
                        break;
                    }
            }
        }
    }

    public void catchSth(Transform other)
    {
        other.SetParent(gameObject.transform);
        other.localPosition = new Vector3(0, 0.4f, 0);
    }

}
