using UnityEngine;
//좀비 생성시 사용할 셋업 데이터(컨테이너)
[CreateAssetMenu(menuName = "Scriptable/ZombieData", fileName ="Zobie Data")]
public class ZombieData :ScriptableObject
{
    public float health = 100; //체력
    public float damage = 5; //공격력
    public float speed = 0.5f; //이동 속도
    public Color skinColor = Color.white; //피부색

}
