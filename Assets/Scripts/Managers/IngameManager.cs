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

        private void Start()
        {
            towerSpawner = FindObjectOfType<TowerSpawner>();
            InvokeRepeating("GainCost", 0f, costGainInterval);
        }

        private void GainCost()
        {
            currentCost += 1;
            UpdateCostText();  // UI Text 업데이트
                               // 여기에 코스트가 획득될 때 수행할 작업을 추가할 수 있습니다.
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
                currentCost -= 4;
                UpdateCostText();  // 코스트 업데이트
                towerSpawner.SpawnCharacter();  // 버튼 동작 실행
            }
            else
            {
                Debug.Log("코스트가 부족합니다.");
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
    }
}
