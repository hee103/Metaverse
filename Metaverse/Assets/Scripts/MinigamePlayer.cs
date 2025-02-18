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

    bool isJump = false;
    private int jumpCount = 0;  // 현재 점프 횟수
    private const int maxJumpCount = 1;  // 최대 2단 점프
    private bool isGrounded = false;

    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        rigid = transform.GetComponent<Rigidbody2D>();
        rigid.gravityScale = 2f;  // 중력 활성화
    }

    void Update()
    {
        if (isDead) return;

        // 스페이스바를 누르면 점프 (최대 2번까지 가능)
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            Jump();
        }

        // 점프 중이 아니라면 isJump를 false로 설정
        if (rigid.velocity.y <= 0)
        {
            animator.SetBool("isJump", false);
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        rigid.velocity = new Vector2(forwardSpeed, rigid.velocity.y);

        // 바닥 감지 (LayerMask "Ground" 레이어로만 바닥을 감지)
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));

        // 바닥에 닿으면 점프 횟수 초기화
        if (isGrounded)
        {
            jumpCount = 0;
        }
    }

    void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        animator.SetBool("isJump", true);  // 점프 애니메이션 실행
        jumpCount++;  // 점프 횟수 증가
    }
}
