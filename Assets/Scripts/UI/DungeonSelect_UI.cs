using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TowerDefence
{
    public class DungeonSelect_UI : MonoBehaviour
    {
        [SerializeField]
        Button[] GameStart_BTN;

        void Start()
        {
            // 게임 시작 시 2~10스테이지 게임 시작 버튼 비활성화
            for (int i = 1; i < GameStart_BTN.Length; i++)
            {
                // 만약 스테이지가 클리어 됬다면 다음 스테이지 게임 시작 버튼 활성화
                if (GameManager.GMInstance.gameDataManagerRef.isClearStage[i - 1] == true)
                {
                    GameStart_BTN[i].interactable = true;
                }
                else
                {
                    GameStart_BTN[i].interactable = false;
                }
            }
        }
    }
}