using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ���� ������Ʈ�� �ֱ������� ����
public class ZombieSpawner : MonoBehaviour
{
    public Enemy zombiePrefab; //������ ���� ���� ������

    public ZombieData[] zombieDatas; //������ ���� �¾� ������
    public Transform[] spawnPoints; //���� AI�� ��ȯ�� ��ġ

    private List<Enemy> zombies = new List<Enemy>(); //������ ���� ��� ����Ʈ
    private int wave; //���� ���̺�
 
    void Update()
    {
        //���ӿ��� ������ ���� �������� ����
        if (GameManager.instance!=null &&GameManager.instance.isGameover)
        {
            return;
        }
        //���� ��� ����ģ ��� ���� ���� ����
        if (zombies.Count<=0)
        {
            SpawnWave();
        }
        UpdateUI(); //UI ����   
    }
    //���̺� ������ UI�� ǥ��
    private void UpdateUI()
    {
        //���� ���̺�� ���� ���� �� ǥ��
        UIManager.instance.UpdateWaveText(wave, zombies.Count);
    }
    //���� ���̺꿡 ���� ���� ����
    private void SpawnWave()
    {

    }
    //���� �����ϰ� ���� ������ ��� �Ҵ�
    private void CreateZombie()
    {

    }
}
