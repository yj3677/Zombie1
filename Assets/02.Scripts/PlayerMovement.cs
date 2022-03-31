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
        //��������� �̵��� �Ÿ� ���
        Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        //������ٵ� �̿��� ���� ������Ʈ ��ġ ����
        playerRigidBody.MovePosition(playerRigidBody.position + moveDistance);
    }
    private void Rotate()
    {
        //��������� ȸ���� ��ġ ���
        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;
        playerRigidBody.rotation = playerRigidBody.rotation * Quaternion.Euler(0, turn, 0);
    }
}
