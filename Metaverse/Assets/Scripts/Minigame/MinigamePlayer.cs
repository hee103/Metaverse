using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePlayer : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    public float jumpForce = 8f;  // 점프 힘
    public float forwardSpeed = 5f;
    public bool isDead = false;
    float deathCooldown = 0f;
    public bool isJump = false;
    private int jumpCount = 0;  // 현재 점프 횟수
    private const int maxJumpCount = 1;  // 최대 2단 점프
    private bool isGrounded = false;
    GameManager gameManager = null;

    void Start()
    {
        gameManager = GameManager.Instance;
        animator = transform.GetComponentInChildren<Animator>();
        rigid = transform.GetComponent<Rigidbody2D>();
        rigid.gravityScale = 2f;  // 중력 활성화
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
        // 스페이스바를 누르면 점프 (최대 2번까지 가능)
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            Jump();
        }

        // 점프 중이 아니라면 isJump를 false로 설정
        if (rigid.velocity.y <= 0)
        {
            animator.SetBool("isJump", false);
            isJump= false;
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        // 바닥에 닿았을 때 forwardSpeed가 제대로 적용되도록 하기
        rigid.velocity = new Vector2(forwardSpeed, rigid.velocity.y);

        // 바닥 감지 (LayerMask "Ground" 레이어로만 바닥을 감지)
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));

        // 바닥에 닿으면 점프 횟수 초기화
        if (isGrounded)
        {
            jumpCount = 0;
            // 바닥에 닿았을 때 이동 속도를 적용
            if (rigid.velocity.x == 0)
            {
                rigid.velocity = new Vector2(forwardSpeed, rigid.velocity.y); // 착지 시 forwardSpeed로 이동 재개
            }
        }
    }

    void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpForce); // y 축만 점프 힘 적용
        animator.SetBool("isJump", true);  // 점프 애니메이션 실행
        jumpCount++;  // 점프 횟수 증가
        isJump = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // 죽음 처리
            isDead = true;
            animator.SetBool("isDie", true);  
            rigid.velocity = Vector2.zero;
            deathCooldown = 1f;
            gameManager.GameOver();
            
        }
    }
}
