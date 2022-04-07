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
    private LineRenderer bulletLineRenderer; //ź�� ������ �׸��� ���� ������
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
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        //����� ���� �� ���� ����
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    void Start()
    {
        
    }
    private void OnEnable()
    {
        //��ü ���� ź�� ���� �ʱ�ȭ
        ammoRemain = gunData.startAmmoRemain;
        //���� źâ�� ���� ä���
        magAmmo = gunData.magCapacity;
        //���� ���� ���¸� ���� �� �غ� �� ���·� ����
        state = State.Ready;
        //���������� ���� �� ������ �ʱ�ȭ
        lastFireTime = 0;
    }
    public void Fire()
    {
        //���� ���°� �߻� ������ ����
        // && ������ �� �߻� �������� timeBetFire �̻��� �ð��� ����
        if (state==State.Ready && Time.time>=lastFireTime+gunData.timeBetFire)
        {
            //������ �� �߻� ���� ����
            lastFireTime = Time.time;
            //���� �߻� ó�� ����
            Shot();
        }
    }
    private void Shot()
    {
        // ����ĳ��Ʈ�� ���� �浹 ������ �����ϴ� �����̳�
        RaycastHit hit;
        //ź���� ���� ���� ������ ����
        Vector3 hitPosition = Vector3.zero;
        //����ĳ��Ʈ(��������, ����, �浹 ���� �����̳�, �����Ÿ�)
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            //���̰� � ��ü�� �浹�� ���
            //�浹�� �������κ��� IDamageable ������Ʈ �������� �õ�
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            //�������κ��� IDamageable ������Ʈ�� �������µ� �����ߴٸ�
            if (target !=null)
            {
                //������ OnDamage �Լ��� ������� ���濡 ������ �ֱ�
                target.OnDamage(gunData.damage, hit.point, hit.normal);
            }
            //���̰� �浹�� ��ġ ����
            hitPosition = hit.point;
           
        }
        else
        {
            //���̰� �ٸ� ��ü�� �浹���� �ʾҴٸ�
            //ź���� �ִ� �����Ÿ����� ���ư��� ���� ��ġ�� �浹 ��ġ�� ���
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }
        //�߻� ����Ʈ ��� ����
        StartCoroutine(ShotEffect(hitPosition));
        //���� ź�� ���� -1
        magAmmo--;
        if (magAmmo<=0)
        {
            //źâ�� ���� ź���� ���ٸ� ���� ���� ���¸� Empty�� ����
            state = State.Empty;
        }
    }
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        muzzleFlashEffect.Play();  //�ѱ� ȭ�� ȿ�� ���
        shellEjectEffect.Play();   //ź�� ���� ȿ�� ���
        gunAudioPlayer.PlayOneShot(gunData.shotClip); //�Ѱ� �Ҹ� ���
        bulletLineRenderer.SetPosition(0, fireTransform.position);  //���� �������� �ѱ��� ��ġ
        bulletLineRenderer.SetPosition(1, hitPosition); //���� ������ �Է����� ���� �浹 ��ġ
        
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
