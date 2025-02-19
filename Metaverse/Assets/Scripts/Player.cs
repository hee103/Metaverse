using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public GameObject spaceIcon_Luna; 
    public GameObject spaceIcon_Ludo;

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
        RaycastHit2D rayHitLuna = Physics2D.Raycast(rigid.position, dirVec, 0.7f,LayerMask.GetMask("Luna"));
        RaycastHit2D rayHitLudo = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Ludo"));
        if (rayHitLuna.collider != null||rayHitLudo.collider != null)
        {
            if(rayHitLuna)
            {
                scanNPC = rayHitLuna.collider.gameObject;
                spaceIcon_Luna.SetActive(true);
            }
            else
            {
                scanNPC = rayHitLudo.collider.gameObject;
                spaceIcon_Ludo.SetActive(true);
            }
            
        }
        else
        {
            scanNPC = null;
            spaceIcon_Luna.SetActive(false);
            spaceIcon_Ludo.SetActive(false);
        }
    }

   


}
