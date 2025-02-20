using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePlayer : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    public float jumpForce = 8f;  // 점프 힘
    public float forwardSpeed = 5f; // 이동 속도
    public bool isDead = false; // 플레이어 사망 여부
    float deathCooldown = 0f; // 재시작 대기 시간
    public bool isJump = false; // 점프 여부
    private int jumpCount = 0;  // 현재 점프 횟수
    private const int maxJumpCount = 1;  // 최대 2단 점프
    private bool isGrounded = false; // 플레이어가 바닥에 닿았는지 판단
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
                // 스페이스바 또는 마우스 클릭 시 게임 재시작
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame();
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime; // 사망 대기 시간 감소
            }
        }
        // 스페이스바를 누르면 점프 
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            jumpCount++;
            Jump();
        }

        // 점프가 끝나면 isJump를 false로 설정
        if (rigid.velocity.y <= 0)
        {
            animator.SetBool("isJump", false);
            isJump= false;
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        // X축 속도를 forwardSpeed만큼 일정하게 지정
        rigid.velocity = new Vector2(forwardSpeed, rigid.velocity.y);

        // 바닥 감지 
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));

       
        if (isGrounded)
        {
            jumpCount = 0; // 바닥에 닿으면 점프 횟수 초기화
           
            if (rigid.velocity.x == 0) // 이동 속도가 0일 경우 forwardSpeed로 이동 재개
            {
                rigid.velocity = new Vector2(forwardSpeed, rigid.velocity.y); 
            }
        }
    }

    void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpForce); // y 축만 점프 힘 적용
        animator.SetBool("isJump", true);  // 점프 애니메이션 실행
         // 점프 횟수 증가
        isJump = true;
        isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision) // 장애물과 충돌 시 사망 처리
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // 죽음 처리
            isDead = true;
            animator.SetBool("isDie", true);  
            rigid.velocity = Vector2.zero; // 이동 정지
            deathCooldown = 1f;
            gameManager.GameOver();// 게임 오버 처리

        }
    }
}
