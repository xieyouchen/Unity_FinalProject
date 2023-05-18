using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class myPlayer : MonoBehaviour
{

    public float speed = 2f;
    private GameObject IngredientCrate;
    private myIngredientCreate myIngredientCreate;
    private bool catchFlag = false;
    private bool chopFlag = false;
    public bool keyDown = false;
    public bool ctrKeyDown = false;
    public GameObject tomato;
    public GameObject onion;
    private Animator chefAnimator;
    public int sthInHands = FREE;
    public bool hopeHug = false;
    public Slider loverSlider;

    const int FREE = 0;
    const int TOMATO = 1;
    const int TOMATO_CHOPED = 2;
    const int COOKINGPOT = 8;
    const int COOKINGPOT_WITH_FOOD = 3;
    const int COOKINGPOT_WITH_BURNEDFOOD = 4;
    const int PLATE = 5;
    const int PLATE_FOOD = 6;
    const int DIRTY_PLATE = 7;
    const int DOWN = 2;
    const int STH_INDEX = 6;


    float ver = 0;
    float hor = 0;
    public float turnspeed = 10;
    public GameObject knife;
    private bool Player1 = true;
    private float speedDown = 0.2f;
    private GameController gameController;
    Scene scene;

    Rigidbody body;

    //public GameObject myParticle;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        IngredientCrate = GameObject.FindWithTag("IngredientCrate");
        myIngredientCreate = IngredientCrate.GetComponent<myIngredientCreate>();
        chefAnimator = gameObject.GetComponent<Animator>();
        body = gameObject.GetComponent<Rigidbody>();
        gameObject.transform.GetChild(5).gameObject.GetComponent<ParticleSystem>().Play();
        if (gameObject.name == "Player2") Player1 = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        scene = gameController.scene;
        //print(scene.name);
        observeHands();
        

        if (Player1)
        {
            if (Random.Range(1, 5400) == 6 && scene.name != "map01")
            {
                hopeHug = true;
                
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                hopeHug = true;
            }

            hor = Input.GetAxisRaw("HorizontalPlayer1");
            ver = Input.GetAxisRaw("VerticalPlayer1");
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                keyDown = true;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                chopFlag = true;
                ctrKeyDown = true;
            }

            if (hopeHug && speed>=0)
            {
                speed -= Time.deltaTime * speedDown;
            }
        }
        else
        {
            if (Random.Range(1, 5400) == 6 && scene.name != "map01")
            {
                hopeHug = true;
            }
            hor = Input.GetAxisRaw("HorizontalPlayer2");
            ver = Input.GetAxisRaw("VerticalPlayer2");
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                keyDown = true;
            }
            if (Input.GetKey(KeyCode.RightControl))
            {
                chopFlag = true;
                ctrKeyDown = true;
            }
            if (hopeHug && speed >= 0)
            {
                speed -= Time.deltaTime * speedDown;
            }
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
        //避免收到力的作用反弹后一直飘动
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); 
        if (hor != 0 || ver != 0)
        {
            //转身
            Rotating(hor, ver);
        }
        Vector3 pos = gameObject.transform.localPosition;
        //Vector3 pos = body.transform.localPosition;
        pos.x += hor * speed * Time.deltaTime;
        pos.z += ver * speed * Time.deltaTime;
        //body.MovePosition(pos);
        //body.MovePosition(pos);
        gameObject.transform.localPosition = pos;
    }

    private void OnTriggerStay(Collider collision)
    {

    }

    public void PlayAudioHit()
    {

    }

    public void chopping()
    {
        chefAnimator.SetBool("isChopping", true);
        showKnife();
    }

    public void stopChop()
    {
        chefAnimator.SetBool("isChopping", false);
        hideKnife();
    }

    private void hideKnife()
    {
        knife.SetActive(false);
    }

    private void showKnife()
    {
        knife.SetActive(true);
    }

    private void observeHands()
    {
        // 其实还有一种方法，直接判断 player 的动画
        //print(gameObject.transform.childCount);
        if (gameObject.transform.childCount == STH_INDEX) sthInHands = FREE;
        else
        {
            Transform sth = gameObject.transform.GetChild(STH_INDEX);
            switch (sth.name)
            {
                case "Tomato":
                    {
                        sthInHands = TOMATO;
                        break;
                    }
                case "Tomato_Chopped":
                    {
                        sthInHands = TOMATO_CHOPED;
                        break;
                    }
                case "CookingPot":
                    {
                        sthInHands = COOKINGPOT;
                        cookingPot cookingPot = sth.GetComponent<cookingPot>();
                        if (cookingPot.tomato > DOWN && !cookingPot.burned)
                        {
                            sthInHands = COOKINGPOT_WITH_FOOD;
                        }
                        else if (cookingPot.burned)
                        {
                            sthInHands = COOKINGPOT_WITH_BURNEDFOOD;
                        }

                        break;
                    }
                case "Plate":
                    {
                        Material material = sth.GetChild(0).GetComponent<MeshRenderer>().material;
                        print(material.name);
                        print(sth.GetChild(0).childCount);
                        if (material.name == "PlateCleanMat (Instance)" || material.name == "PlateCleanMat")
                        {
                            sthInHands = PLATE;
                        }
                        else if (material.name == "PlateDirtyMat (Instance)" || material.name == "PlateDirtyMat")
                        {
                            sthInHands = DIRTY_PLATE;
                        }

                        MeshRenderer soupMaterial = sth.GetChild(0).GetChild(1).GetComponent<MeshRenderer>();
                        if (soupMaterial.enabled)
                        {
                            sthInHands = PLATE_FOOD;
                        }
                        break;
                    }
            }
        }
    }

    public void mycatch(Transform other)
    {
        other.SetParent(gameObject.transform, false);
        other.localPosition = new Vector3(0, 0.373f, 0.7f);
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void toTrash()
    {
        if(sthInHands==TOMATO || sthInHands == TOMATO_CHOPED)
        {
            Destroy(gameObject.transform.GetChild(STH_INDEX).gameObject);
        }
        else if (sthInHands == COOKINGPOT_WITH_BURNEDFOOD)
        {
            gameObject.transform.GetChild(STH_INDEX).GetComponent<cookingPot>().newStart();
        }
    }

    
}
