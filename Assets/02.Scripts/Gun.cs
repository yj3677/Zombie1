using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //���� ���¸� ǥ���ϴ� �� ����� Ÿ���� ����
    public enum State { Ready,Empty,Reloading}

    public State state { get; private set; } //���� ���� ����
    public Transform fireTransform;

    public ParticleSystem muzzleFlashEffect; //�ѱ� ȭ�� ȿ��
    public ParticleSystem shellEjectEffect; //ź�� ���� ȿ��
    private LineRenderer bulletLineRenderer; //Ÿ�� ������ �׸��� ���� ������
    private AudioSource gunAudioPlayer; //�� �Ҹ� �����

    //public AudioClip shotClip; //�߻� �Ҹ�
    //public AudioClip reloadclip; //������ �Ҹ�
    //public float damage = 25; //���ݷ�
    //public int magCapacity = 25; //źâ �뷮

    //���� ���� ������
    public GunData gunData;

    public int magAmmo; //���� źâ�� ���� �ִ� ź��
    public int ammoRemain = 100; //���� ��ü ź��
    private float fireDistance = 50; //�����Ÿ�

    private float lastFireTime; //���� ���������� �߻��� ����
    //public float timeBetFire = 0.12f; //ź�� �߻� ����
    //public float reloadTime = 1.8f; //������ �ҿ� �ð�
    

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }
    private void OnEnable()
    {
        
    }
    public void Fire()
    {

    }
    private void Shot()
    {

    }
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        bulletLineRenderer.enabled = true;
        yield return new WaitForSeconds(0.03f);
        bulletLineRenderer.enabled = false;
    }

    public bool Reload()
    {
        return false;
    }
    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;
        yield return new WaitForSeconds(gunData.reloadTime);
        state = State.Ready;
    }


    void Update()
    {
        
    }
}