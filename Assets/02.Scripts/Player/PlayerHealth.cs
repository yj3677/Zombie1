using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�÷��̾� ĳ������ ����ü�μ��� ������ ���
public class PlayerHealth : LivingEntity
{
    public Slider healthSlider; //ü���� ǥ���� UI�����̴�

    public AudioClip deathClip; //��� �Ҹ�
    public AudioClip hitClip; //�ǰ� �Ҹ�
    public AudioClip itemPickupclip; //������ ���� �Ҹ�

    private AudioSource playerAudioPlayer; //�÷��̾� �Ҹ� �����
    private Animator playerAnimator; //�÷��̾��� �ִϸ�����

    private PlayerMovement playerMovement; //�÷��̾� ������ ������Ʈ
    private PlayerShooter playerShooter; //�÷��̾��� ���� ������Ʈ
    Enemy enemy;

    private void Awake()
    {
        playerAudioPlayer = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
        enemy = GetComponentInParent<Enemy>();
    }

    protected override void OnEnable()
    {
        //LivingEntity�� OnEnable() ����(���� �ʱ�ȭ)
        base.OnEnable();

        //ü�� �����̴� Ȱ��ȭ
        healthSlider.gameObject.SetActive(true);
        //ü�½����̴��� �ִ��� �⺻ ü�°����� ����
        healthSlider.maxValue = startingHealth;
        //ü�� �����̴��� ���� ���� ü�°����� ����
        healthSlider.value = health;

        //�÷��̾� ������ �޴� ������Ʈ Ȱ��ȭ
        playerMovement.enabled = true;
        playerShooter.enabled = true;
    }
    //ü�� ȸ��
    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
        //���ŵ� ü������ ü�� �����̴� ����
        healthSlider.value = health;
    }
    //����� ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!dead)
        {
            //������� ���� ��쿡�� ȿ���� ���
            playerAudioPlayer.PlayOneShot(hitClip);
        }
        base.OnDamage(damage, hitPoint, hitNormal);
        healthSlider.value = health;
    }
    public override void Die()
    {
        base.Die();
        //ü�� �����̴� ��Ȱ��ȭ
        healthSlider.gameObject.SetActive(false);
        //����� ���
        playerAudioPlayer.PlayOneShot(deathClip);
        //�ִϸ������� Die Ʈ���Ÿ� �ߵ����� ��� �ִϸ��̼� ���
        playerAnimator.SetTrigger("Die");

        //�÷��̾� ������ �޴� ������Ʈ ��Ȱ��ȭ
        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        //�����۰� �浹�� ��� �ش� �������� ����ϴ� ó��
        if (!dead)
        {
            //�浹�� �������κ��� IItem ������Ʈ �������� �õ�
            IItem item = other.GetComponent<IItem>();
            //�浹�� �������κ��� IItem ������Ʈ�� �������� �� �����ߴٸ�
            if (item!=null)
            {
                //Use �޼��带 �����Ͽ� ������ ���
                item.Use(gameObject);
                //������ ���� �Ҹ� ���
                playerAudioPlayer.PlayOneShot(itemPickupclip);
            }
        }
    }


}
