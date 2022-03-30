using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5; //속도
    public float rotateSpeed = 180;  //좌우 회전 속도

    private PlayerInput playerInput;  //플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidBody;  //플레이어 캐릭터의 리지드바디
    private Animator playerAnimator;  //플레이어 캐릭터의 애니메이션

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidBody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }
    //물리 갱신 주기마다 움직임, 회전, 애니메이션 처리 실행
    private void FixedUpdate()
    {
        Rotate();
        Move();

        //입력값에 따라 애니메이터의 Move 파라미터값 변경
        playerAnimator.SetFloat("Move", playerInput.move);
    }
    private void Move()
    {

    }
    private void Rotate()
    {

    }
}
