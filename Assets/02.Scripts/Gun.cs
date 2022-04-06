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
    private LineRenderer bulletLineRenderer; //타알 궤적을 그리기 위한 렌더러
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

    }
    private void Shot()
    {

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
