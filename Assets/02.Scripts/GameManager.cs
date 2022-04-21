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
            {//씬에서 GameManager 오브젝트를 찾아서 할당
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }
    private static GameManager m_instance;

    private int score = 0; //현재 게임 점수
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

    //점수를 추가하고 UI 갱신
    public void AddScore(int newScore)
    {  //게임오버가 아닌 상태에서만 점수 추가 가능
        if (!isGameover)
        {
            //점수 추가
            score += newScore;
            //점수 UI 텍스트 갱신
            UIManager.instance.UpdateScoreText(score);
        }
    }
    public void EndGame()
    {
        //게임오버 상태를 참으로 변경
        isGameover = true;
        //게임오버 UI 활성화
        UIManager.instance.SetActiveGameoverUI(true);
    }
}
