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
            // 퀘스트 버튼의 크기만큼 실행
            for (int i = 0; i < Quest_Data.Length; i++)
            {
                // 만약 n번째 bool값이 false면 n번째 버튼도 비활성화
                if (Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_IsClear == false)
                {
                    Quest_Data[i].transform.GetChild(3).GetChild(1).GetComponent<Button>().interactable = false;
                    // Quest_Reward_Get_Button[i].interactable = false;
                }
                // 만약 n번째 bool값이 true면 n번째 버튼도 활성화
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
            // TODO ## 들고온 업적 데이터 테이블 퀘스트에 적용
            for (int i = 0; i < Quest_Data.Length; i++)
            {
                // 업적 ID
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_ID =
                    GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Quest_ID;

                // 업적 카테고리
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_Category =
                    GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Quest_Category;
                Quest_Data[i].transform.GetChild(1).GetComponent<Text>().text = Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_Category;

                // 업적 카테고리 타입
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_Type =
                    GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Quest_Type;

                // 업적 설명
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_Desc =
                    GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Quest_Desc;
                Quest_Data[i].transform.GetChild(2).GetComponent<Text>().text = Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_Desc;


                // 업적 보상 카테고리
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Reward_Category =
                    GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Reward_Category;

                // 업적 보상 타입
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Reward_Type =
                    GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Reward_Type;

                // 업적 보상 수량
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Reward_Amount =
                   GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Reward_Amount;

                // 업적 클리어 여부
                Quest_Data[i].GetComponent<Quest>().QuestInfo.Quest_IsClear =
                   GameManager.GMInstance.gameDataManagerRef.quest[i].GetComponent<Quest>().QuestInfo.Quest_IsClear;
            }
        }
        #endregion
    }
}