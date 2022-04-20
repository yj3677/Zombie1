using UnityEngine;
//���� ������ ����� �¾� ������(�����̳�)
[CreateAssetMenu(menuName = "Scriptable/ZombieData", fileName ="Zobie Data")]
public class ZombieData :ScriptableObject
{
    public float health = 100; //ü��
    public float damage = 5; //���ݷ�
    public float speed = 0.5f; //�̵� �ӵ�
    public Color skinColor = Color.white; //�Ǻλ�

}
