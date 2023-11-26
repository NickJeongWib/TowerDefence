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
        WaveSystem waveSystem;

        public int killCount = 0;

        [Header("----Music----")]
        [SerializeField]
        Slider SFX_Slider;

        [SerializeField]
        Slider BGM_Slider;

        [SerializeField]
        Toggle SFXToggle;

        [SerializeField]
        Toggle BGMToggle;

        private void Start()
        {
            towerSpawner = FindObjectOfType<TowerSpawner>();
            waveSystem = FindObjectOfType<WaveSystem>();
            InvokeRepeating("GainCost", 0f, costGainInterval);

            GameManager.GMInstance.SoundManagerRef.PlayBGM(SoundManager.BGM.InGame_1);

            // ���� ���� �ʱ�ȭ
            for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
            {
                SFX_Slider.value = GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].volume;
            }

            for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
            {
                BGM_Slider.value = GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].volume;
            }

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
            // ��ư�� Ŭ���Ǹ� 4 �ڽ�Ʈ�� �Ҹ��ϰ� ����
            if (currentCost >= 4)
            {
                if (!AreAllTilesOccupied())
                {
                    currentCost -= 4;
                    UpdateCostText();  // �ڽ�Ʈ ������Ʈ
                    towerSpawner.SpawnCharacter();  // ��ư ���� ����
                }
                else
                {
                    Debug.Log("��� Ÿ���� ����á���ϴ�.");
                }

            }
            else
            {
                Debug.Log("�ڽ�Ʈ�� �����մϴ�.");
            }

        }

        bool AreAllTilesOccupied()
        {
            foreach (Tile tile in towerSpawner.allTiles)
            {
                if (!tile.isOccupied)
                {
                    return false; // �ϳ��� ��� �ִ� Ÿ���� �ִٸ� false ��ȯ
                }
            }
            return true; // ��� Ÿ���� �� ������ true ��ȯ
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

        // TODO ## �κ�ȭ�� ȯ�漳�� ���� ���� �Լ�
        #region Sound BGM / SFX
        public void SetSFXVolume(float volume)
        {
            // �迭�� �����ϴ� ����Ʈ ������ ũ�⸦ �����Ѵ�.
            for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
            {
                // ȿ���� ���Ұ� �� ���� ���
                if (SFXToggle.GetComponent<Toggle>().isOn == false)
                {
                    return;
                }

                GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].volume = volume;
            }
        }

        public void SetBGMVolume(float volume)
        {
            // �迭�ȿ� �����ϴ� ������� ũ�⸦ �����Ѵ�.
            for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
            {
                // ����� ���Ұ� �� ���� ���
                if (BGMToggle.GetComponent<Toggle>().isOn == false)
                {
                    return;
                }
                GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].volume = volume;
            }
        }

        public void OnClick_SoundToggle_InGame(Toggle obj)
        {
            // ȿ���� ���� ���
            if (obj.name == "SFXToggle")
            {

                if (obj.isOn == true)
                {
                    // �迭�� �����ϴ� ����Ʈ ������ ũ�⸦ �����Ѵ�.
                    for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
                    {

                        GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].volume = SFX_Slider.value;
                    }

                    SFXToggle.transform.GetChild(0).gameObject.SetActive(false);
                    SFXToggle.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    // �迭�� �����ϴ� ����Ʈ ������ ũ�⸦ �����Ѵ�.
                    for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
                    {

                        GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].volume = 0.0f;
                    }

                    SFXToggle.transform.GetChild(0).gameObject.SetActive(true);
                    SFXToggle.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            else if (obj.name == "BGMToggle")
            {
                if (obj.isOn == true)
                {

                    // �迭�ȿ� �����ϴ� ������� ũ�⸦ �����Ѵ�.
                    for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
                    {
                        GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].volume = BGM_Slider.value;
                    }

                    BGMToggle.transform.GetChild(0).gameObject.SetActive(false);
                    BGMToggle.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    // �迭�ȿ� �����ϴ� ������� ũ�⸦ �����Ѵ�.
                    for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
                    {
                        GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].volume = 0;
                    }

                    BGMToggle.transform.GetChild(0).gameObject.SetActive(true);
                    BGMToggle.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
        #endregion
    }
}