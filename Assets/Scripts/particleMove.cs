using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleMove : MonoBehaviour
{
    float ver = 0;
    float hor = 0;
    Rigidbody body;
    public float speed = 2f;
    private GameObject player;
    private bool ParticlePlayer1 = true;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "WalkParticlePlayer2") ParticlePlayer1 = false;
        if (ParticlePlayer1) player = GameObject.Find("Player1");
        else player = GameObject.Find("Player2");

        body = gameObject.GetComponent<Rigidbody>();
        gameObject.GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (ParticlePlayer1)
        {
            hor = Input.GetAxis("HorizontalPlayer1");
            ver = Input.GetAxis("VerticalPlayer1");
        }
        else
        {
            hor = Input.GetAxis("HorizontalPlayer2");
            ver = Input.GetAxis("VerticalPlayer2");
        }

    }

    private void FixedUpdate()
    {
        Vector3 pos = gameObject.transform.localPosition;
        pos.x += hor * speed * Time.deltaTime;
        pos.z += ver * speed * Time.deltaTime;
        //body.MovePosition(pos);
        gameObject.transform.localPosition = player.transform.localPosition;
    }
}
