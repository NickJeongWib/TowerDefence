using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using static TowerDefence.Define;

namespace TowerDefence
{

    public class IngameManager : MonoBehaviour
    {
        public int currentCost;  // 현재 코스트
        public float costGainInterval = 2f;  // 코스트 획득 간격 (초)
        public TextMeshProUGUI costText;  // 연결할 UI Text
        public TextMeshProUGUI winGold_Text;
        public TextMeshProUGUI overGold_Text;
        public TextMeshProUGUI currentWaveScoreText;
        public TextMeshProUGUI maxWaveScoreText;
        public TextMeshProUGUI timerText;
        public float currentTime;
        public int addGold;
        public int currentWaveScore;
        TowerSpawner towerSpawner;
        public GameObject gameOverUi;
        public GameObject gameWinUi;
        WaveSystem waveSystem;
        [SerializeField]
        public SpawnPoints[] spawnPoints;

        public int killCount = 0;

        [SerializeField]
        GameObject[] Char_Img;

        [SerializeField]
        public GameObject[] Stage_Lv;

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
            GameManager.GMInstance.SoundManagerRef.PlayBGM(SoundManager.BGM.InGame_1);

            // 사운드 관련 초기화
            for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
            {
                SFX_Slider.value = GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].volume;
            }

            for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
            {
                BGM_Slider.value = GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].volume;
            }


            towerSpawner = FindObjectOfType<TowerSpawner>();
            waveSystem = FindObjectOfType<WaveSystem>();
            InvokeRepeating("GainCost", 0f, costGainInterval);

            MaxWaveScoreText();
            

            if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_1)
            {
                Stage_Lv[0].SetActive(true);
            }
            else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_2)
            {
                Stage_Lv[1].SetActive(true);
            }
            else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_3)
            {
                Stage_Lv[2].SetActive(true);
            }
            else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_4)
            {
                Stage_Lv[3].SetActive(true);
            }
            else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_5)
            {
                Stage_Lv[4].SetActive(true);
            }
            else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_6)
            {
                Stage_Lv[5].SetActive(true);
            }
            else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_7)
            {
                Stage_Lv[6].SetActive(true);
            }
            else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_8)
            {
                Stage_Lv[7].SetActive(true);
            }
            else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_9)
            {
                Stage_Lv[8].SetActive(true);
            }
            else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_10)
            {
                Stage_Lv[9].SetActive(true);
            }

            for (int i = 0; i < Char_Img.Length; i++)
            {
                Char_Img[i].GetComponent<Image>().sprite = GameManager.GMInstance.gameDataManagerRef.Equip_Char[i].GetComponent<SpriteRenderer>().sprite;
            }

        }
        private void Update()
        {
            GameWin();
            currentWaveScore = waveSystem.currentWaveIndex + 1;
            CurrentWaveScoreText();
            TimeUp();
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
            foreach (Tile tile in spawnPoints[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].allTiles)
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
            Stage_Lv[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].SetActive(false);
        }

        public void OnClickRePlay_Btn(GameObject obj)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void GameWin()
        {
            if (killCount == (waveSystem.wave[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].maxEnemyCount 
                * waveSystem.wave[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].enemyPrefabs.Length))
            {
                Time.timeScale = 0.0f;
                gameWinUi.SetActive(true);
                GameWinGoldAdd();
                Win_Gold_Text();

                killCount = 0;
                GameManager.GMInstance.gameDataManagerRef.isClearStage[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv]
                    = true;
            }
        }
        public void GameOver()
        {
            Time.timeScale = 0.0f;
            gameOverUi.SetActive(true);
            GameOverGoldAdd();
            Over_Gold_Text();
        }

            // TODO ## 로비화면 환경설정 사운드 조절 함수
            #region Sound BGM / SFX
            public void SetSFXVolume(float volume)
        {
            // 배열에 존재하는 이펙트 음들의 크기를 조절한다.
            for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
            {
                // 효과음 음소거 시 실행 취소
                if (SFXToggle.GetComponent<Toggle>().isOn == false)
                {
                    return;
                }

                GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].volume = volume;
            }
        }

        public void SetBGMVolume(float volume)
        {
            // 배열안에 존재하는 배경음의 크기를 조절한다.
            for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
            {
                // 배경음 음소거 시 실행 취소
                if (BGMToggle.GetComponent<Toggle>().isOn == false)
                {
                    return;
                }
                GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].volume = volume;
            }
        }

        public void OnClick_SoundToggle_InGame(Toggle obj)
        {
            // 효과음 제거 토글
            if (obj.name == "SFXToggle")
            {

                if (obj.isOn == true)
                {
                    // 배열에 존재하는 이펙트 음들의 크기를 조절한다.
                    for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
                    {

                        GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].volume = SFX_Slider.value;
                    }

                    SFXToggle.transform.GetChild(0).gameObject.SetActive(false);
                    SFXToggle.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    // 배열에 존재하는 이펙트 음들의 크기를 조절한다.
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

                    // 배열안에 존재하는 배경음의 크기를 조절한다.
                    for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
                    {
                        GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].volume = BGM_Slider.value;
                    }

                    BGMToggle.transform.GetChild(0).gameObject.SetActive(false);
                    BGMToggle.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    // 배열안에 존재하는 배경음의 크기를 조절한다.
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

        // TODO ## 게임 승리시 획득 골드 함수
        public void GameWinGoldAdd()
        {
            if(GameManager.GMInstance.gameDataManagerRef.isClearStage[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv] == false)
            {
                if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_1)
                {
                    addGold = 30;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_2)
                {
                    addGold = 60;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_3)
                {
                    addGold = 90;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_4)
                {
                    addGold = 120;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_5)
                {
                    addGold = 150;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_6)
                {
                    addGold = 180;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_7)
                {
                    addGold = 210;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_8)
                {
                    addGold = 240;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_9)
                {
                    addGold = 270;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_10)
                {
                    addGold = 300;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
            }

            if (GameManager.GMInstance.gameDataManagerRef.isClearStage[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv] == true)
            {
                if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_1)
                {
                    addGold = 10;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_2)
                {
                    addGold = 20;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_3)
                {
                    addGold = 30;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_4)
                {
                    addGold = 40;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_5)
                {
                    addGold = 50;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_6)
                {
                    addGold = 60;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_7)
                {
                    addGold = 70;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_8)
                {
                    addGold = 80;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_9)
                {
                    addGold = 90;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (GameManager.GMInstance.gameDataManagerRef.Stage_Lv == Stage_Level.Stage_10)
                {
                    addGold = 100;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
            }
        }

        // TODO ## 게임 오버시 획득 골드 함수
        public void GameOverGoldAdd()
        {
            if (GameManager.GMInstance.gameDataManagerRef.isClearStage[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv] == false)
            {
                if (waveSystem.currentWaveIndex == 0)
                {
                    addGold = 5;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (waveSystem.currentWaveIndex == 1)
                {
                    addGold = 10;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (waveSystem.currentWaveIndex == 2)
                {
                    addGold = 15;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (waveSystem.currentWaveIndex == 3)
                {
                    addGold = 20;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (waveSystem.currentWaveIndex >= 4)
                {
                    addGold = 25;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }

            }

            if (GameManager.GMInstance.gameDataManagerRef.isClearStage[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv] == true)
            {
                if (waveSystem.currentWaveIndex == 0)
                {
                    addGold = 1;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (waveSystem.currentWaveIndex == 1)
                {
                    addGold = 2;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (waveSystem.currentWaveIndex == 2)
                {
                    addGold = 3;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (waveSystem.currentWaveIndex == 3)
                {
                    addGold = 4;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
                else if (waveSystem.currentWaveIndex >= 4)
                {
                    addGold = 5;
                    GameManager.GMInstance.gameDataManagerRef.Gold = GameManager.GMInstance.gameDataManagerRef.Gold + addGold;
                }
            }
        }

        public void Win_Gold_Text()
        {
            winGold_Text.text = addGold.ToString();
        }
        public void Over_Gold_Text()
        {
            overGold_Text.text = addGold.ToString();
        }
        public void CurrentWaveScoreText()
        {
            currentWaveScoreText.text = currentWaveScore.ToString();
        }
        public void MaxWaveScoreText()
        {
            int maxWaveScore = waveSystem.wave[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].enemyPrefabs.Length;
            maxWaveScoreText.text = maxWaveScore.ToString();
        }
        public void TimeUp()
        {
            currentTime += Time.deltaTime;

            string minutes = Mathf.Floor(currentTime / 60).ToString("00");
            string seconds = (currentTime % 60).ToString("00");

            timerText.text = $"{minutes}:{seconds}";
        }
    }
}