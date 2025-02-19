using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    public float Speed;
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;
    GameObject scanNPC;

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

        if(vDown && v==1)
        {
            dirVec = Vector3.up;
        }
        else if(vDown && v== -1)
        {
            dirVec= Vector3.down;
        }
        else if(hDown&& h==-1)
        {
            dirVec= Vector3.left;
        }
        else if(hDown&&h==1)
        {
            dirVec=Vector3.right;
        }
        
        // Scan NPC
        if(Input.GetButtonDown("Jump")&&scanNPC != null)
        {
            Debug.Log("This is:"+scanNPC.name);
            SceneManager.LoadScene("Game Scene");
        }


    }

    private void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec*Speed;

        Debug.DrawRay(rigid.position,dirVec*0.7f,new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f,LayerMask.GetMask("NPC"));
        if(rayHit.collider != null)
        {
            scanNPC = rayHit.collider.gameObject;
        }
        else
        {
            scanNPC = null;
        }
    }

   


}
