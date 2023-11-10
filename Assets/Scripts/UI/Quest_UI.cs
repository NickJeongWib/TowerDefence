using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TowerDefence
{
    public class Quest_UI : MonoBehaviour
    {
        //[SerializeField]
        //bool[] Quest_Success;

        [SerializeField]
        GameObject[] Quest_Data;

        //[SerializeField]
        //Button[] Quest_Reward_Get_Button;

        void Start()
        {
            // ����Ʈ ��ư�� ũ�⸸ŭ ����
            for (int i = 0; i < Quest_Data.Length; i++)
            {
                // ���� n��° bool���� false�� n��° ��ư�� ��Ȱ��ȭ
                if (Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_IsClear == false)
                {
                    Quest_Data[i].transform.GetChild(3).GetChild(1).GetComponent<Button>().interactable = false;
                    // Quest_Reward_Get_Button[i].interactable = false;
                }
                // ���� n��° bool���� true�� n��° ��ư�� Ȱ��ȭ
                else if (Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_IsClear == true)
                {
                    Quest_Data[i].transform.GetChild(3).GetChild(1).GetComponent<Button>().interactable = true;
                    // Quest_Reward_Get_Button[i].interactable = true;
                }
            }

            Init();
        }

        #region Init
        void Init()
        {
            // TODO ## ���� ���� ������ ���̺� ����Ʈ�� ����
            for (int i = 0; i < Quest_Data.Length; i++)
            {
                // ���� ID
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_ID =
                    GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Quest_ID;

                // ���� ī�װ�
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_Category =
                    GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Quest_Category;
                Quest_Data[i].transform.GetChild(1).GetComponent<Text>().text = Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_Category;

                // ���� ī�װ� Ÿ��
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_Type =
                    GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Quest_Type;

                // ���� ����
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_Desc =
                    GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Quest_Desc;
                Quest_Data[i].transform.GetChild(2).GetComponent<Text>().text = Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_Desc;


                // ���� ���� ī�װ�
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Reward_Category =
                    GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Reward_Category;

                // ���� ���� Ÿ��
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Reward_Type =
                    GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Reward_Type;

                // ���� ���� ����
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Reward_Amount =
                   GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Reward_Amount;

                // ���� Ŭ���� ����
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_IsClear =
                   GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Quest_IsClear;
            }
        }
        #endregion
    }
}