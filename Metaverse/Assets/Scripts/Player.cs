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
    public float Speed; // 플레이어 이동 속도
    float h; // x 입력값
    float v;// y 입력값
    bool isHorizonMove; // x축 이동 여부 판단 변수
    Vector3 dirVec; // 플레이어가 바라보는 방향 벡터
    GameObject scanNPC; 
    public GameObject spaceIcon_Luna; // Luna NPC 상호작용 아이콘
    public GameObject spaceIcon_Ludo; // Ludo NPC 상호작용 아이콘
    public GameObject miniGameZone; // 미니게임으로 갈 수 있게 하는 오브젝트

    int type;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 키 입력 값 받기
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // 키 입력 감지
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonDown("Horizontal");
        bool vUp = Input.GetButtonDown("Vertical");

        // 수평 이동 판단
        if (hDown)
        {
            isHorizonMove = true;
        }
        else if (vDown)
        {
            isHorizonMove = false;
        }

        // 애니메이션 실행을 위한 값
        animator.SetInteger("isHor", (int)h);
        animator.SetInteger("isVer", (int)v);

        // 플레이어가 바라보는 방향
        if (vDown && v == 1)
        {
            dirVec = Vector3.up;
        }
        else if (vDown && v == -1)
        {
            dirVec = Vector3.down;
        }
        else if (hDown && h == -1)
        {
            dirVec = Vector3.left;
        }
        else if (hDown && h == 1)
        {
            dirVec = Vector3.right;
        }

        // NPC를 스캔하여 상호작용 처리
        if (Input.GetButtonDown("Jump") && scanNPC != null)
        {
            if (type == 1) // Luna NPC와 상호작용 시 게임종료
            {
                Debug.Log("This is:" + scanNPC.name);
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit(); // 어플리케이션 종료
                #endif
            }
            else // Ludo NPC와 상호작용 시 미니게임으로 이동
            {
                Debug.Log("This is:" + scanNPC.name);
                SceneManager.LoadScene("Game Scene");
            }

        }


    }

    private void FixedUpdate()
    {
        // 플레이어 이동 처리
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        
        // Raycast를 사용하여 NPC 및 미니게임존 감지
        RaycastHit2D rayHitLuna = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Luna"));
        RaycastHit2D rayHitLudo = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Ludo"));
        RaycastHit2D miniGameZ = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("mgz"));
        
        if (rayHitLuna.collider != null || rayHitLudo.collider != null)
        {
            // Luna 또는 Ludo NPC가 감지되었을 때
            if (rayHitLuna) // Luna NPC가 감지되면
            {
                type = 1;
                scanNPC = rayHitLuna.collider.gameObject;
                spaceIcon_Luna.SetActive(true); // 상호작용 아이콘 활성화
            }
            else // Ludo NPC가 감지되면
            {
                type = 2;
                scanNPC = rayHitLudo.collider.gameObject;
                spaceIcon_Ludo.SetActive(true);// 상호작용 아이콘 활성화
            }

        }
        else 
        {
            // NPC를 감지하지 못하면 상호작용 아이콘 비활성화
            scanNPC = null; 
            spaceIcon_Luna.SetActive(false);
            spaceIcon_Ludo.SetActive(false);
        }

        if (miniGameZ)   // 미니게임존에 들어가면 "Game Scene"으로 이동
        {
            SceneManager.LoadScene("Game Scene");
        }
    }


}
