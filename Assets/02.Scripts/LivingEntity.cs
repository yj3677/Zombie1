using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamageable
{
    //����ü�� ������ ���� ������Ʈ���� ���� ���븦 ����
    //ü��, ����� �޾Ƶ��̱�, ��� ���, ��� �̺�Ʈ�� ����
    public float startingHealth = 100;
    public float health { get; protected set; }//���� ü��
    public bool dead { get; protected set; }//��� ����
    public event Action OnDeath; //��� �� �ߵ��� �̺�Ʈ

    protected virtual void OnEnable()
    {
        //������� ���� ���·� ����
        dead = false;
        //ü���� ���� ü������ �ʱ�ȭ
        health = startingHealth;
    }
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        //�������ŭ ü�� ����
        health -= damage;
        //ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
        if (health<=0 &&!dead)
        {
            Die();
        }
    }
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
        {
            //�̹� ����� ��� ü���� ȸ���� �� ����
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
