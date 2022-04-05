using UnityEngine;
/// <summary>
/// ���� ��ġ ���̸� ���� �����ϱ� �����.
/// ���� ��ġ�� ���� ���� Ÿ���� �浹�� �����͸� ���� ������ �ȴ�
/// ���� ���߿� ���� ������ ������ �ΰ� �����͸� ��ü�ϱ� �����
/// "Scriptable Object":����Ƽ ������Ʈ�� ���� ���·� �����͸� ���� �� �ִ� Ÿ��
/// �����͸� �и� ���� ���� �� �� �ִ� ���� :JSON, �ܼ� �ؽ�Ʈ, XML �ؽ�Ʈ(����Ƽ �������� �ν����� â���� �ٷιٷ� ������ �Ұ���)
/// </summary>

[CreateAssetMenu (menuName ="Scriptable/GunData",fileName ="Gun Data")]
public class GunData :ScriptableObject
{
    public AudioClip shotClip; //�߻� �Ҹ�
    public AudioClip reloadClip; //������ �Ҹ�
    public float damage = 25; //���ݷ�
    public int startAmmoRemain = 100; //ó���� �־��� ��ü ź��
    public int magCapacity = 25; //źâ �뷮
    public float timeBetFire = 0.12f; //ź�� �߻� ����
    public float reloadTime = 1.8f; //������ �ҿ� �ð�
}
