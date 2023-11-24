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
        public int currentCost;  // 현재 코스트
        public float costGainInterval = 2f;  // 코스트 획득 간격 (초)
        public TextMeshProUGUI costText;  // 연결할 UI Text
        TowerSpawner towerSpawner;
        public GameObject gameOverUi;
        public GameObject gameWinUi;
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
            UpdateCostText();
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
            // 버튼이 클릭되면 4 코스트를 소모하고 실행
            if (currentCost >= 4)
            {
                if (!AreAllTilesOccupied())
                {
                    currentCost -= 4;
                    UpdateCostText();  // 코스트 업데이트
                    towerSpawner.SpawnCharacter();  // 버튼 동작 실행
                }
                else
                {
                    Debug.Log("모든 타일이 가득찼습니다.");
                }

            }
            else
            {
                Debug.Log("코스트가 부족합니다.");
            }

        }

        bool AreAllTilesOccupied()
        {
            foreach (Tile tile in towerSpawner.allTiles)
            {
                if (!tile.isOccupied)
                {
                    return false; // 하나라도 비어 있는 타일이 있다면 false 반환
                }
            }
            return true; // 모든 타일이 차 있으면 true 반환
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