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
    public float Speed; // �÷��̾� �̵� �ӵ�
    float h; // x �Է°�
    float v;// y �Է°�
    bool isHorizonMove; // x�� �̵� ���� �Ǵ� ����
    Vector3 dirVec; // �÷��̾ �ٶ󺸴� ���� ����
    GameObject scanNPC; 
    public GameObject spaceIcon_Luna; // Luna NPC ��ȣ�ۿ� ������
    public GameObject spaceIcon_Ludo; // Ludo NPC ��ȣ�ۿ� ������
    public GameObject miniGameZone; // �̴ϰ������� �� �� �ְ� �ϴ� ������Ʈ

    int type;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ű �Է� �� �ޱ�
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // Ű �Է� ����
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonDown("Horizontal");
        bool vUp = Input.GetButtonDown("Vertical");

        // ���� �̵� �Ǵ�
        if (hDown)
        {
            isHorizonMove = true;
        }
        else if (vDown)
        {
            isHorizonMove = false;
        }

        // �ִϸ��̼� ������ ���� ��
        animator.SetInteger("isHor", (int)h);
        animator.SetInteger("isVer", (int)v);

        // �÷��̾ �ٶ󺸴� ����
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

        // NPC�� ��ĵ�Ͽ� ��ȣ�ۿ� ó��
        if (Input.GetButtonDown("Jump") && scanNPC != null)
        {
            if (type == 1) // Luna NPC�� ��ȣ�ۿ� �� ��������
            {
                Debug.Log("This is:" + scanNPC.name);
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit(); // ���ø����̼� ����
                #endif
            }
            else // Ludo NPC�� ��ȣ�ۿ� �� �̴ϰ������� �̵�
            {
                Debug.Log("This is:" + scanNPC.name);
                SceneManager.LoadScene("Game Scene");
            }

        }


    }

    private void FixedUpdate()
    {
        // �÷��̾� �̵� ó��
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        
        // Raycast�� ����Ͽ� NPC �� �̴ϰ����� ����
        RaycastHit2D rayHitLuna = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Luna"));
        RaycastHit2D rayHitLudo = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Ludo"));
        RaycastHit2D miniGameZ = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("mgz"));
        
        if (rayHitLuna.collider != null || rayHitLudo.collider != null)
        {
            // Luna �Ǵ� Ludo NPC�� �����Ǿ��� ��
            if (rayHitLuna) // Luna NPC�� �����Ǹ�
            {
                type = 1;
                scanNPC = rayHitLuna.collider.gameObject;
                spaceIcon_Luna.SetActive(true); // ��ȣ�ۿ� ������ Ȱ��ȭ
            }
            else // Ludo NPC�� �����Ǹ�
            {
                type = 2;
                scanNPC = rayHitLudo.collider.gameObject;
                spaceIcon_Ludo.SetActive(true);// ��ȣ�ۿ� ������ Ȱ��ȭ
            }

        }
        else 
        {
            // NPC�� �������� ���ϸ� ��ȣ�ۿ� ������ ��Ȱ��ȭ
            scanNPC = null; 
            spaceIcon_Luna.SetActive(false);
            spaceIcon_Ludo.SetActive(false);
        }

        if (miniGameZ)   // �̴ϰ������� ���� "Game Scene"���� �̵�
        {
            SceneManager.LoadScene("Game Scene");
        }
    }


}
