using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCatch : MonoBehaviour
{

    float speed = 2f;
    private GameObject IngredientCrate;
    private myIngredientCreate myIngredientCreate;
    private bool catchFlag = false;
    private bool chopFlag = false;
    public GameObject tomato;
    public GameObject onion;
    private Animator chefAnimator;
    public int sthInHands = FREE;

    const int FREE = 0;
    const int TOMATO = 1;
    const int TOMATO_CHOPED = 2;
    const int COOKINGPOT_WITH_FOOD = 3;
    const int COOKINGPOT_WITH_BURNEDFOOD = 4;
    const int PLATE = 5;
    const int PLATE_FOOD = 6;
    const int DIRTY_PLATE = 7;
    const int DOWN = 2;


    float ver = 0;
    float hor = 0;
    public float turnspeed = 10;
    Rigidbody body;
    //public GameObject myParticle;
    // Start is called before the first frame update
    void Start()
    {
        IngredientCrate = GameObject.FindWithTag("IngredientCrate");
        myIngredientCreate = IngredientCrate.GetComponent<myIngredientCreate>();
        chefAnimator = gameObject.GetComponent<Animator>();
        body = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        observeHands();

        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        Vector3 pos = gameObject.transform.localPosition;
        pos.x += hor* speed * Time.deltaTime;
        pos.z += ver * speed * Time.deltaTime;
        //body.MovePosition(pos);
        gameObject.transform.localPosition = pos;

        if (Input.GetKey(KeyCode.M))
        {
            catchFlag = true;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            chopFlag = true;
        }
        
    }


    void Rotating(float hor, float ver)
    {
        //获取方向
        Vector3 dir = new Vector3(hor, 0, ver);
        //将方向转换为四元数
        Quaternion quaDir = Quaternion.LookRotation(dir, Vector3.up);
        //缓慢转动到目标点
        transform.rotation = Quaternion.Lerp(transform.rotation, quaDir, Time.fixedDeltaTime * turnspeed);

    }

    private void FixedUpdate()
    {
        if (hor != 0 || ver != 0)
        {
            //转身
            Rotating(hor, ver);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        GameObject foundGameObject = collision.gameObject;
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (foundGameObject.CompareTag("IngredientCrate"))
            {
                Debug.Log("compareTag_Ingre");
                myIngredientCreate.myOpenTrue();
                GameObject mytomato =  Instantiate(tomato, new Vector3(0f, 0.373f, 0.7f) + gameObject.transform.position, Quaternion.identity);
                mytomato.transform.parent = gameObject.transform;
                chefAnimator.SetBool("hasPickup", true);

            }
            if (foundGameObject.CompareTag("chopBoard"))
            {
                Debug.Log(111);
                GameObject mytomato = GameObject.Find("Chef_Model_withWalkParticle/Tomato(Clone)");
                if (!mytomato)
                {
                    // 找不到 myTomato，那就说明是拾取 chopedTomato
                    catchChoppedTomato();
                    return;

                }
                chefAnimator.SetBool("hasPickup", false);
                mytomato.transform.parent = GameObject.Find("ChoppingBoard_Countetop1").transform;
                mytomato.transform.localPosition = new Vector3(0.1f, 0.444f, 0f);
            }
            if (foundGameObject.CompareTag("chopBoard"))
            {

            }

                
            catchFlag = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("chopFlag");
            if (foundGameObject.CompareTag("chopBoard"))
            {
                Debug.Log("chopBoard");
                showKnife();
                GameObject chopSlider = GameObject.Find("ChoppingBoard_Countetop1/Canvas/chopSlider");
                chopSlider.SetActive(true);
                isChopping();
            }
            chopFlag = false;
        }       
    }

    public void PlayAudioHit()
    {

    }

    public void isChopping()
    {
        chefAnimator.SetBool("isChopping", true);
    }

    public void stopChop()
    {
        chefAnimator.SetBool("isChopping", false);
        hideKnife();
    }
    public void catchChoppedTomato()
    {
        Debug.Log("mytomato is null");
        Debug.Log("chopBoard");
        GameObject chopedTomato = GameObject.Find("ChoppingBoard_Countetop1/Tomato_Chopped(Clone)");
        if (!chopedTomato)
        {
            Debug.Log("chopTomato is null");
            return;
        }
        chopedTomato.transform.parent = gameObject.transform;
        chopedTomato.transform.localPosition = new Vector3(0f, 0.373f, 0.7f);
    }

    private void hideKnife()
    {
        GameObject knife = GameObject.Find("Chef_Model_withWalkParticle/Hand_r/Knife");
        knife.SetActive(false);
    }

    private void showKnife()
    {
        GameObject knife = GameObject.Find("Chef_Model_withWalkParticle/Hand_r/Knife");
        knife.SetActive(true);
    }

    private void observeHands()
    {
        // 其实还有一种方法，直接判断 player 的动画
        if (gameObject.transform.childCount == 6) sthInHands = FREE;
        else
        {
            Transform sth = gameObject.transform.GetChild(6);
            switch (sth.name)
            {
                case "Tomato(Clone)":
                    {
                        sthInHands = TOMATO;
                        break;
                    }
                case "Tomato_Chopped(Clone)":
                    {
                        sthInHands = TOMATO_CHOPED;
                        break;
                    }
                case "CookingPot":
                    {
                        
                        cookingPot cookingPot = sth.GetComponent<cookingPot>();
                        if(cookingPot.tomato > DOWN && !cookingPot.burned)
                        {
                            sthInHands = COOKINGPOT_WITH_FOOD;
                        }
                        else if (cookingPot.burned)
                        {
                            sthInHands = COOKINGPOT_WITH_BURNEDFOOD;
                        }
                        
                        break;
                    }
                case "Plate_2":
                    {
                        Material material = sth.GetChild(0).GetComponent<MeshRenderer>().material;
                        print(material.name);
                        print(sth.GetChild(0).childCount);
                        if(material.name == "PlateCleanMat (Instance)")
                        {
                            sthInHands = PLATE;
                        }
                        else if(material.name == "PlateDirtyMat")
                        {
                            sthInHands = DIRTY_PLATE;
                        }
                        if(sth.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().enabled)
                        {
                            sthInHands = PLATE_FOOD;
                        }
                        break;
                    }
            }
        }
    }

    public void mycatch(GameObject other)
    {
        other.transform.SetParent(gameObject.transform, false);
        other.transform.localPosition = new Vector3(0, 0.373f, 0.7f);
    }
}
