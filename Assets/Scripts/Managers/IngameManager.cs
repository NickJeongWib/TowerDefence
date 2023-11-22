using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace TowerDefence
{
    public class IngameManager : MonoBehaviour
    {
        public int currentCost;  // ���� �ڽ�Ʈ
        public float costGainInterval = 2f;  // �ڽ�Ʈ ȹ�� ���� (��)
        public TextMeshProUGUI costText;  // ������ UI Text
        TowerSpawner towerSpawner;
        public GameObject gameOverUi;
        public GameObject gameWinUi;
        EnemySpawner enemySpawner;
        WaveSystem waveSystem;

        public int killCount = 0;

        private void Start()
        {
            towerSpawner = FindObjectOfType<TowerSpawner>();
            waveSystem = FindObjectOfType<WaveSystem>();
            InvokeRepeating("GainCost", 0f, costGainInterval);
            
        }
        private void Update()
        {
            GameWin();
        }
        public void EnemyKilled()
        {
            killCount++;
        }
        private void GainCost()
        {
            currentCost += 1;
            UpdateCostText();  // UI Text ������Ʈ
                               // ���⿡ �ڽ�Ʈ�� ȹ��� �� ������ �۾��� �߰��� �� �ֽ��ϴ�.
        }

        private void UpdateCostText()
        {
            if (costText != null)
            {
                costText.text = currentCost.ToString();
            }
        }

        public void OnButtonClicked()
        {
            // ��ư�� Ŭ���Ǹ� 4 �ڽ�Ʈ�� �Ҹ��ϰ� ����
            if (currentCost >= 4)
            {
                currentCost -= 4;
                UpdateCostText();  // �ڽ�Ʈ ������Ʈ
                towerSpawner.SpawnCharacter();  // ��ư ���� ����
            }
            else
            {
                Debug.Log("�ڽ�Ʈ�� �����մϴ�.");
            }
        }

        public void OnClickOpenPopUp_Btn(GameObject obj)
        {
            obj.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }

        public void OnClickClosePopUp_Btn(GameObject obj)
        {
            obj.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }

        public void OnClickGoLobby_Btn(GameObject obj)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("Lobby");
        }

        public void OnClickRePlay_Btn(GameObject obj)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void GameWin()
        {
            if (killCount == (waveSystem.wave[0].maxEnemyCount * waveSystem.wave[0].enemyPrefabs.Length))
            {
                Time.timeScale = 0.0f;
                gameWinUi.SetActive(true);
                killCount = 0;
            }
        }
        public void GameOver()
        {
            Time.timeScale = 0.0f;
            gameOverUi.SetActive(true);
        }
    }
}
