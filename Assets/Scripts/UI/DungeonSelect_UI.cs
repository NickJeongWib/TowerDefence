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
            // ���� ���� �� 2~10�������� ���� ���� ��ư ��Ȱ��ȭ
            for (int i = 1; i < GameStart_BTN.Length; i++)
            {
                // ���� ���������� Ŭ���� ��ٸ� ���� �������� ���� ���� ��ư Ȱ��ȭ
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