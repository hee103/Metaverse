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

    bool isJump = false;
    private int jumpCount = 0;  // ���� ���� Ƚ��
    private const int maxJumpCount = 1;  // �ִ� 2�� ����
    private bool isGrounded = false;

    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        rigid = transform.GetComponent<Rigidbody2D>();
        rigid.gravityScale = 2f;  // �߷� Ȱ��ȭ
    }

    void Update()
    {
        if (isDead) return;

        // �����̽��ٸ� ������ ���� (�ִ� 2������ ����)
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            Jump();
        }

        // ���� ���� �ƴ϶�� isJump�� false�� ����
        if (rigid.velocity.y <= 0)
        {
            animator.SetBool("isJump", false);
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        rigid.velocity = new Vector2(forwardSpeed, rigid.velocity.y);

        // �ٴ� ���� (LayerMask "Ground" ���̾�θ� �ٴ��� ����)
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));

        // �ٴڿ� ������ ���� Ƚ�� �ʱ�ȭ
        if (isGrounded)
        {
            jumpCount = 0;
        }
    }

    void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        animator.SetBool("isJump", true);  // ���� �ִϸ��̼� ����
        jumpCount++;  // ���� Ƚ�� ����
    }
}
