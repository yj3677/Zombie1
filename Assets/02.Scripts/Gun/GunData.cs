using UnityEngine;
/// <summary>
/// 총의 수치 데이만 따로 관리하기 힘들다.
/// 같은 수치를 가진 같은 타입의 충돌이 데이터를 각자 가지게 된다
/// 게임 도중에 총의 외형은 가만히 두고 데이터만 교체하기 힘들다
/// "Scriptable Object":유니티 프로젝트의 에셋 형태로 데이터를 담을 수 있는 타입
/// 데이터를 분리 보관 관리 할 수 있는 형태 :JSON, 단순 텍스트, XML 텍스트(유니티 에디터의 인스펙터 창에서 바로바로 편집이 불가능)
/// </summary>

[CreateAssetMenu (menuName ="Scriptable/GunData",fileName ="Gun Data")]
public class GunData :ScriptableObject
{
    public AudioClip shotClip; //발사 소리
    public AudioClip reloadClip; //재장전 소리
    public float damage = 25; //공격력
    public int startAmmoRemain = 100; //처음에 주어질 전체 탄알
    public int magCapacity = 25; //탄창 용량
    public float timeBetFire = 0.12f; //탄알 발사 간격
    public float reloadTime = 1.8f; //재장전 소요 시간
}
