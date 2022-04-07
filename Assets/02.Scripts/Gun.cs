using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //총의 상태를 표현하는 데 사용할 타입을 선언
    public enum State { Ready,Empty,Reloading}

    public State state { get; private set; } //현재 총의 상태
    public Transform fireTransform;

    public ParticleSystem muzzleFlashEffect; //총구 화염 효과
    public ParticleSystem shellEjectEffect; //탄피 배출 효과
    private LineRenderer bulletLineRenderer; //탄알 궤적을 그리기 위한 렌더러
    private AudioSource gunAudioPlayer; //총 소리 재생기

    //public AudioClip shotClip; //발사 소리
    //public AudioClip reloadclip; //재장전 소리
    //public float damage = 25; //공격력
    //public int magCapacity = 25; //탄창 용량

    //총의 현재 데이터
    public GunData gunData;

    public int magAmmo; //현재 탄창에 남아 있는 탄알
    public int ammoRemain = 100; //남은 전체 탄알
    private float fireDistance = 50; //사정거리

    private float lastFireTime; //총을 마지막으로 발사한 시점
    //public float timeBetFire = 0.12f; //탄알 발사 간격
    //public float reloadTime = 1.8f; //재장전 소요 시간
    

    private void Awake()
    {
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        //사용할 점을 두 개로 변경
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    void Start()
    {
        
    }
    private void OnEnable()
    {
        //전체 예비 탄알 양을 초기화
        ammoRemain = gunData.startAmmoRemain;
        //현재 탄창을 가득 채우기
        magAmmo = gunData.magCapacity;
        //총의 현재 상태를 총을 쏠 준비가 된 상태로 변경
        state = State.Ready;
        //마지막으로 총을 쏜 시점을 초기화
        lastFireTime = 0;
    }
    public void Fire()
    {
        //현재 상태가 발사 가능한 상태
        // && 마지막 총 발사 시점에서 timeBetFire 이상의 시간이 지남
        if (state==State.Ready && Time.time>=lastFireTime+gunData.timeBetFire)
        {
            //마지막 총 발사 시점 갱신
            lastFireTime = Time.time;
            //실제 발사 처리 실행
            Shot();
        }
    }
    private void Shot()
    {
        // 레이캐스트에 의한 충돌 정보를 저장하는 컨테이너
        RaycastHit hit;
        //탄알이 맞은 곳을 저장할 변수
        Vector3 hitPosition = Vector3.zero;
        //레이캐스트(시작지점, 방향, 충돌 정보 컨테이너, 사정거리)
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            //레이가 어떤 물체와 충돌한 경우
            //충돌한 상대방으로부터 IDamageable 오브젝트 가져오기 시도
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            //상대방으로부터 IDamageable 오브젝트를 가져오는데 성공했다면
            if (target !=null)
            {
                //상대방의 OnDamage 함수를 실행시켜 상대방에 데미지 주기
                target.OnDamage(gunData.damage, hit.point, hit.normal);
            }
            //레이가 충돌한 위치 저장
            hitPosition = hit.point;
           
        }
        else
        {
            //레이가 다른 물체와 충돌하지 않았다면
            //탄알이 최대 사정거리까지 날아갔을 때의 위치를 충돌 위치로 사용
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }
        //발사 이펙트 재생 시작
        StartCoroutine(ShotEffect(hitPosition));
        //남은 탄알 수를 -1
        magAmmo--;
        if (magAmmo<=0)
        {
            //탄창에 남은 탄알이 없다면 총의 현재 상태를 Empty로 갱신
            state = State.Empty;
        }
    }
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        muzzleFlashEffect.Play();  //총구 화염 효과 재생
        shellEjectEffect.Play();   //탄피 배출 효과 재생
        gunAudioPlayer.PlayOneShot(gunData.shotClip); //총격 소리 재생
        bulletLineRenderer.SetPosition(0, fireTransform.position);  //선의 시작점은 총구의 위치
        bulletLineRenderer.SetPosition(1, hitPosition); //선의 끝점은 입력으로 들어온 충돌 위치
        
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
