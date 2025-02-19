using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePlayer : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    public float jumpForce = 8f;  // ���� ��
    public float forwardSpeed = 5f;
    public bool isDead = false;
    float deathCooldown = 0f;
    public bool isJump = false;
    private int jumpCount = 0;  // ���� ���� Ƚ��
    private const int maxJumpCount = 1;  // �ִ� 2�� ����
    private bool isGrounded = false;
    GameManager gameManager = null;

    void Start()
    {
        gameManager = GameManager.Instance;
        animator = transform.GetComponentInChildren<Animator>();
        rigid = transform.GetComponent<Rigidbody2D>();
        rigid.gravityScale = 2f;  // �߷� Ȱ��ȭ
    }

    void Update()
    {
        if (isDead) return;

        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame();
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        // �����̽��ٸ� ������ ���� (�ִ� 2������ ����)
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            Jump();
        }

        // ���� ���� �ƴ϶�� isJump�� false�� ����
        if (rigid.velocity.y <= 0)
        {
            animator.SetBool("isJump", false);
            isJump= false;
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        // �ٴڿ� ����� �� forwardSpeed�� ����� ����ǵ��� �ϱ�
        rigid.velocity = new Vector2(forwardSpeed, rigid.velocity.y);

        // �ٴ� ���� (LayerMask "Ground" ���̾�θ� �ٴ��� ����)
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));

        // �ٴڿ� ������ ���� Ƚ�� �ʱ�ȭ
        if (isGrounded)
        {
            jumpCount = 0;
            // �ٴڿ� ����� �� �̵� �ӵ��� ����
            if (rigid.velocity.x == 0)
            {
                rigid.velocity = new Vector2(forwardSpeed, rigid.velocity.y); // ���� �� forwardSpeed�� �̵� �簳
            }
        }
    }

    void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpForce); // y �ุ ���� �� ����
        animator.SetBool("isJump", true);  // ���� �ִϸ��̼� ����
        jumpCount++;  // ���� Ƚ�� ����
        isJump = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // ���� ó��
            isDead = true;
            animator.SetBool("isDie", true);  
            rigid.velocity = Vector2.zero;
            deathCooldown = 1f;
            gameManager.GameOver();
            
        }
    }
}
