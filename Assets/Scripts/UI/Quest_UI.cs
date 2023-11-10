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

            // ����Ʈ ��ư�� ũ�⸸ŭ ����
            for (int i = 0; i < Quest_Reward_Get_Button.Length; i++)
            {
                // ���� n��° bool���� false�� n��° ��ư�� ��Ȱ��ȭ
                if (Quest_Success[i] == false)
                {
                    Quest_Reward_Get_Button[i].interactable = false;
                }
                // ���� n��° bool���� true�� n��° ��ư�� Ȱ��ȭ
                else if (Quest_Success[i] == true)
                {
                    Quest_Reward_Get_Button[i].interactable = true;
                }
            }
        }
    }
}