using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// �̱��� ���ٿ� ������Ƽ
    /// </summary>
    public static UIManager instance
    {
        get { 
            if(m_instance==null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
            }
    }
    private static UIManager m_instance;//�̱����� �Ҵ�� ����

    public Text ammoText; //ź�� ǥ�ÿ� �ؽ�Ʈ
    public Text scoreText; //���� ǥ�ÿ� �ؽ�Ʈ
    public Text waveText; //�� ���̺� ǥ�ÿ� �ؽ�Ʈ
    public GameObject gameOverUI; //���ӿ��� �� Ȱ��ȭ�� UI

    /// <summary>
    /// ź�� �ؽ�Ʈ ����
    /// </summary>
    /// <param name="magAmmo"></param>
    /// <param name="remainAmmo"></param>
    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }
    /// <summary>
    /// ���� �ؽ�Ʈ ����
    /// </summary>
    /// <param name="newScore"></param>
    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }
    /// <summary>
    /// �� ���̺� �ؽ�Ʈ ����
    /// </summary>
    /// <param name="waves"></param>
    /// <param name="count"></param>
    public void UpdateWaveText(int waves, int count)
    {
        waveText.text = "Wave : " + waves + "|nEnemy Left : " + count;
    }
    /// <summary>
    /// ���ӿ��� UI Ȱ��ȭ
    /// </summary>
    /// <param name="active"></param>
    public void SetActiveGameoverUI(bool active)
    {
        gameOverUI.SetActive(active);
    }
    /// <summary>
    /// ���� �����
    /// </summary>
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
