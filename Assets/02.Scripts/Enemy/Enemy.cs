using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    public LayerMask whatIsTarget; //추격 대상 레이어
    private LivingEntity targetEntity; //추적 대상
    private NavMeshAgent pathFinder; //경로 계산 AI 에이전트

    public ParticleSystem hitEffect; //피격 시 재생할 파티클 효과
    public AudioClip deathSound; //사망 시 재생할 소리
    public AudioClip hitSound; //피격 시 재생할 소리

    public Animator enemyAnimator; //애니메이터 컴포넌트
    private AudioSource enemyAudioPlayer; //오디오 소스 컴포넌트
    private Renderer enemyRenderer; //렌더러 컴포넌트

    public float damage = 5; //공격력
    public float timeBetAttck = 10f; //공간 간격
    private float lastAttackTime; //마지막 공격 시점
    bool isAttack; //공격중
    //추적할 대상이 존재하는지 알려주는 프로퍼티
    private bool hasTarget
    {
        get
        {
            if (targetEntity != null &&!targetEntity.dead)
            {
                return true;
            }
            return false;
        } 
    }
    private void Awake()
    {
        pathFinder = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        enemyAudioPlayer = GetComponent<AudioSource>();

        enemyRenderer = GetComponentInChildren<Renderer>();
    }
    //적 AI의 초기 스펙을 결정하는 셋업 메서드
    public void Setup(ZombieData zombieData)
    {   //체력 설정
        startingHealth = zombieData.health;
        health = zombieData.health;
        //공격력 설정
        damage = zombieData.damage;
        //네비매시 에이전트의 이동 속도 설정
        pathFinder.speed = zombieData.speed;
        //렌더러가 사용 중인 머터리얼의 컬러를 변경
        enemyRenderer.material.color = zombieData.skinColor;
        
    }
    void Start()
    {
        //게임 오브젝트 활성화와 동시에 AI의 추적 루틴 시작
        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        enemyAnimator.SetBool("HasTarget", hasTarget);
        
    }
    private IEnumerator UpdatePath()
    {
        while(!dead)
        {
            if (hasTarget)
            {
                //추적 대상 존재 : 경로를 갱신하고 AI 이동을 계속 진행
                pathFinder.isStopped = false;
                pathFinder.SetDestination(targetEntity.transform.position);
            }
            else
            {
                //추적 대상 없음 : AI 이동 중지
                pathFinder.isStopped = true;
                //20유닛의 반지름을 가진 가상의 구를 그렸을 때 구와 겹치는 모든 콜라이더를 가져옴
                //단,WhatIsTarget 레이어를 가진 콜라이더만 가져오도록 필터링
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20, whatIsTarget);
                //모든 콜라이더를 순회하면서 살아 있는 LivingEntity 찾기
                for (int i = 0; i < colliders.Length; i++)
                {
                    //콜라이더로부터 LivingEntity 컴포넌트 가져오기
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();
                    //LivingEntity 컴포넌트가 존재하며, 해당 LivingEntity가 살아 있다면
                    if (livingEntity!=null && !livingEntity.dead)
                    {
                        //추적 대상을 해당 LivingEntity로 설정
                        targetEntity = livingEntity;
                        break;
                    }
                }
            }
            //0.25초 주기로 처리 반복
            yield return new WaitForSeconds(0.25f);
        }
    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!dead)
        {
            //공격받은 지점과 방향으로 파티클 효과 재생
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();

            //피격 효과음 재생
            enemyAudioPlayer.PlayOneShot(hitSound);
        }
        base.OnDamage(damage, hitPoint, hitNormal);
    }
    public override void Die()
    {
        base.Die();
        //다른 AI를 방해하지 않도록 자신의 모든 콜라이더를 비활성화
        Collider[] enemyColliders = GetComponents<Collider>();
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            enemyColliders[i].enabled = false;
        }
        //AI 추적을 중지하고 내비메시 컴포넌트 비활성화
        pathFinder.isStopped = true;
        pathFinder.enabled = false;
        //사망 애니메이션 재생
        enemyAnimator.SetTrigger("Die");
        //사망 효과음 재생
        enemyAudioPlayer.PlayOneShot(deathSound);
    }
    private void OnTriggerStay(Collider other)
    {
        //트리거 충돌한 상대방 게임 오브젝트가 추적 대상이라면 공격 실행
        //자신이 사망하지 않았으며, 최근 공격 시점에서 timeBetAttack 이상 시간이 지났다면 공격 가능
        if (!dead && Time.time>=lastAttackTime+timeBetAttck)
        {
            //상대방의 LivingEntity 타입 가져오기 시도
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();
            //상대방의 LivingEntity가 자신의 추적 대상이라면 공격 실행
            if (attackTarget != null && attackTarget == targetEntity)
            {
                //최근 공격 시간 갱신
                lastAttackTime = Time.time;
                //상대방의 피격 위치와 피격 방향을 근삿값으로 계산
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;
                attackTarget.OnDamage(damage, hitPoint, hitNormal);
                enemyAnimator.SetTrigger("Attack");
                
            }
        }
    }

}
