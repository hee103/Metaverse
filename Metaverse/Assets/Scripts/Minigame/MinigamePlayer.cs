using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePlayer : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    public float jumpForce = 8f;  // ���� ��
    public float forwardSpeed = 5f; // �̵� �ӵ�
    public bool isDead = false; // �÷��̾� ��� ����
    float deathCooldown = 0f; // ����� ��� �ð�
    public bool isJump = false; // ���� ����
    private int jumpCount = 0;  // ���� ���� Ƚ��
    private const int maxJumpCount = 1;  // �ִ� 2�� ����
    private bool isGrounded = false; // �÷��̾ �ٴڿ� ��Ҵ��� �Ǵ�
    GameManager gameManager = null;

    void Start()
    {
        gameManager = GameManager.Instance;
        animator = transform.GetComponentInChildren<Animator>();
        rigid = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead) return;

        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                // �����̽��� �Ǵ� ���콺 Ŭ�� �� ���� �����
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame();
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime; // ��� ��� �ð� ����
            }
        }
        // �����̽��ٸ� ������ ���� 
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            jumpCount++;
            Jump();
        }

        // ������ ������ isJump�� false�� ����
        if (rigid.velocity.y <= 0)
        {
            animator.SetBool("isJump", false);
            isJump= false;
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        // X�� �ӵ��� forwardSpeed��ŭ �����ϰ� ����
        rigid.velocity = new Vector2(forwardSpeed, rigid.velocity.y);

        // �ٴ� ���� 
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));

       
        if (isGrounded)
        {
            jumpCount = 0; // �ٴڿ� ������ ���� Ƚ�� �ʱ�ȭ
           
            if (rigid.velocity.x == 0) // �̵� �ӵ��� 0�� ��� forwardSpeed�� �̵� �簳
            {
                rigid.velocity = new Vector2(forwardSpeed, rigid.velocity.y); 
            }
        }
    }

    void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpForce); // y �ุ ���� �� ����
        animator.SetBool("isJump", true);  // ���� �ִϸ��̼� ����
         // ���� Ƚ�� ����
        isJump = true;
        isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision) // ��ֹ��� �浹 �� ��� ó��
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // ���� ó��
            isDead = true;
            animator.SetBool("isDie", true);  
            rigid.velocity = Vector2.zero; // �̵� ����
            deathCooldown = 1f;
            gameManager.GameOver();// ���� ���� ó��

        }
    }
}
