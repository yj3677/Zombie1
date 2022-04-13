using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamageable
{
    //생명체로 동작할 게임 오브젝트들을 위한 뼈대를 제공
    //체력, 대미지 받아들이기, 사망 기능, 사망 이벤트를 제공
    public float startingHealth = 100;
    public float health { get; protected set; }//현재 체력
    public bool dead { get; protected set; }//사망 상태
    public event Action OnDeath; //사망 시 발동할 이벤트

    protected virtual void OnEnable()
    {
        //사망하지 않은 상태로 시작
        dead = false;
        //체력을 시작 체력으로 초기화
        health = startingHealth;
    }
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        //대미지만큼 체력 감소
        health -= damage;
        //체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health<=0 &&!dead)
        {
            Die();
        }
    }
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
        {
            //이미 사망한 경우 체력을 회복할 수 없음
            return;
        }
        health += newHealth;
    }
    public virtual void Die()
    {
        if (OnDeath!=null)
        {
            OnDeath();
        }
        dead = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
