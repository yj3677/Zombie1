using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (m_instance==null)
            {//������ GameManager ������Ʈ�� ã�Ƽ� �Ҵ�
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }
    private static GameManager m_instance;

    private int score = 0; //���� ���� ����
    public bool isGameover;

    private void Awake()
    {
        if(instance !=this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        FindObjectOfType<PlayerHealth>().OnDeath += EndGame;
    }

    //������ �߰��ϰ� UI ����
    public void AddScore(int newScore)
    {  //���ӿ����� �ƴ� ���¿����� ���� �߰� ����
        if (!isGameover)
        {
            //���� �߰�
            score += newScore;
            //���� UI �ؽ�Ʈ ����
            UIManager.instance.UpdateScoreText(score);
        }
    }
    public void EndGame()
    {
        //���ӿ��� ���¸� ������ ����
        isGameover = true;
        //���ӿ��� UI Ȱ��ȭ
        UIManager.instance.SetActiveGameoverUI(true);
    }
}
