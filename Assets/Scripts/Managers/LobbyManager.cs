using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static TowerDefence.Define;
using TMPro;

namespace TowerDefence
{
    public class LobbyManager : MonoBehaviour
    {
        #region Var
        [SerializeField]
        Slider SFX_Slider;

        [SerializeField]
        Slider BGM_Slider;

        [SerializeField]
        GameObject Shop_Focus;

        [SerializeField]
        TextMeshProUGUI[] Shop_Menu_Text;

        [SerializeField]
        GameObject[] Shop_PopUps; // Index 0 : 캐릭터 상점 팝업, Index 1 : 골드 상점 팝업, Index 2 : 젬 상점 팝업

        [SerializeField]
        GameObject Shop_PopUp;

        [SerializeField]
        GameObject Shop_Character_Vertical_Value;

        [SerializeField]
        GameObject Inventory_Vertical_Value;

        [SerializeField]
        GameObject Character_Select_PopUp;

        [SerializeField]
        GameObject None_Touch_Btn;

        public GameObject[] Equip_CharacterList;

        int Shop_Index;
        #endregion

        #region Manager
        [SerializeField]
        ScrollManager ScrollManagerRef;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        // TODO ## 로비화면 팝업 관련 함수
        #region Pop_Up
        /// <summary>
        /// 일반 팝업 오픈
        /// </summary>
        /// <param name="obj"></param>
        public void OnClickOpenPopUp_Btn(GameObject obj)
        {
            //효과음 실행
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Btn_Select);

            if (obj.name == "Character_Info_Btn")
            {
                // 캐릭터 클릭 시 팝업 활성화
                Character_Select_PopUp.gameObject.SetActive(true);
                None_Touch_Btn.gameObject.SetActive(true);
                Character_Select_PopUp.transform.position = new Vector3(obj.transform.parent.position.x, obj.transform.parent.position.y + 100.0f, 0);
            }
            else if (obj.name == "Character_Upgrade")
            {
                Character_Select_PopUp.SetActive(false);
            }

            obj.gameObject.SetActive(true);
        }

        /// <summary>
        /// TODO ## 상점 관련 팝업 오픈
        /// </summary>
        /// <param name="obj"></param>
        public void OpenPopUp_Btn(GameObject obj)
        {
            //효과음 실행
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Btn_Select);

            if (obj.name == "Gold_Add_Btn")
            {
                Shop_PopUp.gameObject.SetActive(true);

                Shop_Index = 2;

                //Debug.Log(1);
                // 해당사항 메뉴 흰색으로 표시

                for (int i = 0; i < Shop_Menu_Text.Length; i++)
                {
                    // 설정한 인덱스가 맞으면 활성화
                    if (i == Shop_Index)
                    {
                        Shop_Menu_Text[i].color = Color.white;
                        Shop_PopUps[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // 인덱스가 아닌것들은 비활성화
                        Shop_Menu_Text[i].color = Color.gray;
                        Shop_PopUps[i].gameObject.SetActive(false);
                    }
                }

                // 상점 재화 창 스크롤 value 값 초기화 - 스크롤 바로 골드 창이 뜰 수 있는 value값
                Shop_PopUp.transform.GetChild(7).GetChild(1).GetComponent<Scrollbar>().value = 0.095f;

                // 포커스 위치 조정
                Shop_Focus.transform.position = new Vector3(Shop_PopUp.transform.GetChild(1).GetChild(4).position.x, Shop_Focus.transform.position.y, 0);
            }
            else if (obj.name == "Gem_Add_Btn")
            {
                Shop_PopUp.gameObject.SetActive(true);

                Shop_Index = 2;

                //Debug.Log(1);
                // 해당사항 메뉴 흰색으로 표시

                for (int i = 0; i < Shop_Menu_Text.Length; i++)
                {
                    // 설정한 인덱스가 맞으면 활성화
                    if (i == Shop_Index)
                    {
                        Shop_Menu_Text[i].color = Color.white;
                        Shop_PopUps[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // 인덱스가 아닌것들은 비활성화
                        Shop_Menu_Text[i].color = Color.gray;
                        Shop_PopUps[i].gameObject.SetActive(false);
                    }
                }
                

                // 상점 재화 창 스크롤 value 값 초기화 - 스크롤 최상단으로 변경
                Shop_PopUp.transform.GetChild(7).GetChild(1).GetComponent<Scrollbar>().value = 1.0f;
                // 포커스 위치 조정
                Shop_Focus.transform.position = new Vector3(Shop_PopUp.transform.GetChild(1).GetChild(4).position.x, Shop_Focus.transform.position.y, 0);
            }
            else if (obj.name == "Button_Shop")
            {
                Shop_PopUp.gameObject.SetActive(true);

                Shop_Index = 0;

                //Debug.Log(1);
                // 해당사항 메뉴 흰색으로 표시

                for (int i = 0; i < Shop_Menu_Text.Length; i++)
                {
                    // 설정한 인덱스가 맞으면 활성화
                    if (i == Shop_Index)
                    {
                        Shop_Menu_Text[i].color = Color.white;
                        Shop_PopUps[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // 인덱스가 아닌것들은 비활성화
                        Shop_Menu_Text[i].color = Color.gray;
                        Shop_PopUps[i].gameObject.SetActive(false);
                    }
                }

                // 포커스 위치 조정
                Shop_Focus.transform.position = new Vector3(Shop_PopUp.transform.GetChild(1).GetChild(1).position.x, Shop_Focus.transform.position.y, 0);
            }
        }

        // TODO ## 팝업 닫기 함수
        public void OnClickClosePopUp_Btn(GameObject obj)
        {
            //효과음 실행
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.PopUp_Close);

            // 던전선택화면의 닫기 버튼 클릭 시
            if (obj.gameObject.name == "DungeonSelect_Close")
            {
                // 던전 선택화면의 스크롤 value를 0으로 초기화 해주고 오브젝트 비활성화.
                obj.transform.parent.GetChild(1).GetComponent<Scrollbar>().value = 0;
                obj.transform.parent.gameObject.SetActive(false);

                ScrollManagerRef.targetPos = 0.0f;
                ScrollManagerRef.curPos = 0.0f;
                return;
            }

            // 상점 관련 닫기 버튼
            if (obj.gameObject.name == "Shop_Tap")
            {
                Shop_Character_Vertical_Value.GetComponent<Scrollbar>().value = 1.0f;

                Shop_PopUp.transform.GetChild(7).GetChild(1).GetComponent<Scrollbar>().value = 1.0f;
            }

            // 인벤토리 관련 닫기 버튼
            if (obj.gameObject.name == "Inventory_BG")
            {
                Inventory_Vertical_Value.GetComponent<Scrollbar>().value = 1.0f;
                OnClick_None_Touch_Btn();
            }



            obj.transform.parent.gameObject.SetActive(false);
        }
        #endregion

        // TODO ## 로비화면 환경설정 사운드 조절 함수
        #region Sound BGM / SFX
        public void SetSFXVolume(float volume)
        {
            // 배열에 존재하는 이펙트 음들의 크기를 조절한다.
            for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
            {

                GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].volume = volume;
            }
        }

        public void SetBGMVolume(float volume)
        {
            // 배열안에 존재하는 배경음의 크기를 조절한다.
            for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
            {
                GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].volume = volume;
            }
        }

        #endregion

        // TODO ## 로비화면 상점 메뉴 변경 팝업
        #region Shop_Menu
        public void OnClick_Shop_Menu_Change(GameObject btn)
        {
            // Debug.Log(btn.name);

            if (btn.name == "Shop_Character_Button")
            {
                Shop_Index = 0;

                //Debug.Log(1);
                // 해당사항 메뉴 흰색으로 표시

                for (int i = 0; i < Shop_Menu_Text.Length; i++)
                {
                    // 설정한 인덱스가 맞으면 활성화
                    if (i == Shop_Index)
                    {
                        Shop_Menu_Text[i].color = Color.white;
                        Shop_PopUps[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // 인덱스가 아닌것들은 비활성화
                        Shop_Menu_Text[i].color = Color.gray;
                        Shop_PopUps[i].gameObject.SetActive(false);
                    }
                }

                //Shop_Menu_Text[0].color = Color.white;
                //Shop_Menu_Text[1].color = Color.gray;
                //Shop_Menu_Text[2].color = Color.gray;

                //Shop_PopUps[0].gameObject.SetActive(true);
                //Shop_PopUps[1].gameObject.SetActive(false);
                //Shop_PopUps[2].gameObject.SetActive(false);
            }
            else if (btn.name == "Shop_Package_Button")
            {
                Shop_Index = 1;

                //Debug.Log(2);
                // 해당사항 메뉴 흰색으로 표시

                for (int i = 0; i < Shop_Menu_Text.Length; i++)
                {
                    // 설정한 인덱스가 맞으면 활성화
                    if (i == Shop_Index)
                    {
                        Shop_Menu_Text[i].color = Color.white;
                        Shop_PopUps[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // 인덱스가 아닌것들은 비활성화
                        Shop_Menu_Text[i].color = Color.gray;
                        Shop_PopUps[i].gameObject.SetActive(false);
                    }
                }

                //Shop_Menu_Text[0].color = Color.gray;
                //Shop_Menu_Text[1].color = Color.white;
                //Shop_Menu_Text[2].color = Color.gray;

                //Shop_PopUps[0].gameObject.SetActive(false);
                //Shop_PopUps[1].gameObject.SetActive(true);
                //Shop_PopUps[2].gameObject.SetActive(false);
            }
            else if (btn.name == "Shop_Goods_Button")
            {
                Shop_Index = 2;

                for (int i = 0; i < Shop_Menu_Text.Length; i++)
                {
                    // 설정한 인덱스가 맞으면 활성화
                    if (i == Shop_Index)
                    {
                        Shop_Menu_Text[i].color = Color.white;
                        Shop_PopUps[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // 인덱스가 아닌것들은 비활성화
                        Shop_Menu_Text[i].color = Color.gray;
                        Shop_PopUps[i].gameObject.SetActive(false);
                    }
                }
                //Debug.Log(3);
                // 해당사항 메뉴 흰색으로 표시
                //Shop_Menu_Text[0].color = Color.gray;
                //Shop_Menu_Text[1].color = Color.gray;
                //Shop_Menu_Text[2].color = Color.white;

                //Shop_PopUps[0].gameObject.SetActive(false);
                //Shop_PopUps[1].gameObject.SetActive(false);
                //Shop_PopUps[2].gameObject.SetActive(true);
            }
            else if (btn.name == "Shop_Costume_Button")
            {
                Shop_Index = 3;

                for (int i = 0; i < Shop_Menu_Text.Length; i++)
                {
                    // 설정한 인덱스가 맞으면 활성화
                    if (i == Shop_Index)
                    {
                        Shop_Menu_Text[i].color = Color.white;
                        Shop_PopUps[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // 인덱스가 아닌것들은 비활성화
                        Shop_Menu_Text[i].color = Color.gray;
                        Shop_PopUps[i].gameObject.SetActive(false);
                    }
                }

                //Debug.Log(3);
                // 해당사항 메뉴 흰색으로 표시
                //Shop_Menu_Text[0].color = Color.gray;
                //Shop_Menu_Text[1].color = Color.gray;
                //Shop_Menu_Text[2].color = Color.white;

                //Shop_PopUps[0].gameObject.SetActive(false);
                //Shop_PopUps[1].gameObject.SetActive(false);
                //Shop_PopUps[2].gameObject.SetActive(true);
            }

            // 포커스 위치 조정
            Shop_Focus.transform.position = new Vector3(btn.transform.position.x, Shop_Focus.transform.position.y, 0);
        }
        #endregion

        #region Init
        void Init()
        {
            GameManager.GMInstance.lobbyManagerRef = this;

            // 사운드 관련 초기화
            for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
            {
                SFX_Slider.value = GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].volume;
            }

            for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
            {
                BGM_Slider.value = GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].volume;
            }
        }
        #endregion

        #region GameStart_Btn
        public void OnClickGameStart(GameObject obj)
        {
            // 버튼 클릭시 선택 단계 저장
            if (obj.name == "Stage1_GameStart_Button")
            {
                GameManager.GMInstance.gameDataManagerRef.Stage_Lv = Stage_Level.Stage_1;
            }
            else if (obj.name == "Stage2_GameStart_Button")
            {
                GameManager.GMInstance.gameDataManagerRef.Stage_Lv = Stage_Level.Stage_2;
            }
            else if (obj.name == "Stage3_GameStart_Button")
            {
                GameManager.GMInstance.gameDataManagerRef.Stage_Lv = Stage_Level.Stage_3;
            }
            else if (obj.name == "Stage4_GameStart_Button")
            {
                GameManager.GMInstance.gameDataManagerRef.Stage_Lv = Stage_Level.Stage_4;
            }
            else if (obj.name == "Stage5_GameStart_Button")
            {
                GameManager.GMInstance.gameDataManagerRef.Stage_Lv = Stage_Level.Stage_5;
            }
            else if (obj.name == "Stage6_GameStart_Button")
            {
                GameManager.GMInstance.gameDataManagerRef.Stage_Lv = Stage_Level.Stage_6;
            }
            else if (obj.name == "Stage7_GameStart_Button")
            {
                GameManager.GMInstance.gameDataManagerRef.Stage_Lv = Stage_Level.Stage_7;
            }
            else if (obj.name == "Stage8_GameStart_Button")
            {
                GameManager.GMInstance.gameDataManagerRef.Stage_Lv = Stage_Level.Stage_8;
            }
            else if (obj.name == "Stage9_GameStart_Button")
            {
                GameManager.GMInstance.gameDataManagerRef.Stage_Lv = Stage_Level.Stage_9;
            }
            else if (obj.name == "Stage10_GameStart_Button")
            {
                GameManager.GMInstance.gameDataManagerRef.Stage_Lv = Stage_Level.Stage_10;
            }

            SceneManager.LoadScene("InGame");
        }

        #endregion

        public void OnClick_None_Touch_Btn()
        {
            Character_Select_PopUp.SetActive(false);
            None_Touch_Btn.SetActive(false);
        }
    }
}
