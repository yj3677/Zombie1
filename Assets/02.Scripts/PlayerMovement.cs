using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5; //�ӵ�
    public float rotateSpeed = 180;  //�¿� ȸ�� �ӵ�

    private PlayerInput playerInput;  //�÷��̾� �Է��� �˷��ִ� ������Ʈ
    private Rigidbody playerRigidBody;  //�÷��̾� ĳ������ ������ٵ�
    private Animator playerAnimator;  //�÷��̾� ĳ������ �ִϸ��̼�

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidBody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }
    //���� ���� �ֱ⸶�� ������, ȸ��, �ִϸ��̼� ó�� ����
    private void FixedUpdate()
    {
        Rotate();
        Move();

        //�Է°��� ���� �ִϸ������� Move �Ķ���Ͱ� ����
        playerAnimator.SetFloat("Move", playerInput.move);
    }
    private void Move()
    {

    }
    private void Rotate()
    {

    }
}
