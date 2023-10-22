using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TowerDefence
{
    public class Quest_UI : MonoBehaviour
    {
        [SerializeField]
        bool[] Quest_Success;

        [SerializeField]
        Button[] Quest_Reward_Get_Button;

        void Start()
        {

            // 퀘스트 버튼의 크기만큼 실행
            for (int i = 0; i < Quest_Reward_Get_Button.Length; i++)
            {
                // 만약 n번째 bool값이 false면 n번째 버튼도 비활성화
                if (Quest_Success[i] == false)
                {
                    Quest_Reward_Get_Button[i].interactable = false;
                }
                // 만약 n번째 bool값이 true면 n번째 버튼도 활성화
                else if (Quest_Success[i] == true)
                {
                    Quest_Reward_Get_Button[i].interactable = true;
                }
            }
        }
    }
}