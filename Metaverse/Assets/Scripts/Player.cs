using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    public float Speed;
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonDown("Horizontal");
        bool vUp = Input.GetButtonDown("Vertical");

        if(hDown)
        {
            isHorizonMove = true;
        }
        else if (vDown)
        {
            isHorizonMove = false;
        }

        animator.SetInteger("isHor",(int)h);
        animator.SetInteger("isVer", (int)v);
    }

    private void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec*Speed;

        //Debug.DrawRay();
    }
}
