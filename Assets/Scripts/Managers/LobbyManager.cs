using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        public void OnClickOpenPopUp_Btn(GameObject obj)
        {
            //효과음 실행
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Btn_Select);

            obj.gameObject.SetActive(true);
        }

        public void OpenPopUp_Btn(GameObject obj)
        {
            //효과음 실행
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Btn_Select);

            if (obj.name == "Gold_Add_Btn")
            {
                Shop_PopUp.gameObject.SetActive(true);

                // 해당사항 메뉴 흰색으로 표시
                Shop_Menu_Text[0].color = Color.gray;
                Shop_Menu_Text[1].color = Color.white;
                Shop_Menu_Text[2].color = Color.gray;

                Shop_PopUps[0].gameObject.SetActive(false);
                Shop_PopUps[1].gameObject.SetActive(true);
                Shop_PopUps[2].gameObject.SetActive(false);

                // 포커스 위치 조정
                Shop_Focus.transform.position = new Vector3(Shop_PopUp.transform.GetChild(1).GetChild(2).position.x, Shop_Focus.transform.position.y, 0);
            }
            else if (obj.name == "Gem_Add_Btn")
            {
                Shop_PopUp.gameObject.SetActive(true);

                Shop_Menu_Text[0].color = Color.gray;
                Shop_Menu_Text[1].color = Color.gray;
                Shop_Menu_Text[2].color = Color.white;

                Shop_PopUps[0].gameObject.SetActive(false);
                Shop_PopUps[1].gameObject.SetActive(false);
                Shop_PopUps[2].gameObject.SetActive(true);

                // 포커스 위치 조정
                Shop_Focus.transform.position = new Vector3(Shop_PopUp.transform.GetChild(1).GetChild(3).position.x, Shop_Focus.transform.position.y, 0);
            }
            else if (obj.name == "Button_Shop")
            {
                Shop_PopUp.gameObject.SetActive(true);

                // 해당사항 메뉴 흰색으로 표시
                Shop_Menu_Text[0].color = Color.white;
                Shop_Menu_Text[1].color = Color.gray;
                Shop_Menu_Text[2].color = Color.gray;

                Shop_PopUps[0].gameObject.SetActive(true);
                Shop_PopUps[1].gameObject.SetActive(false);
                Shop_PopUps[2].gameObject.SetActive(false);

                // 포커스 위치 조정
                Shop_Focus.transform.position = new Vector3(Shop_PopUp.transform.GetChild(1).GetChild(1).position.x, Shop_Focus.transform.position.y, 0);
            }
        }

        //public void OnClickOpenPopUp_Btn(GameObject obj, GameObject popUp)
        //{
        //    //효과음 실행
        //    GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Btn_Select);

        //    popUp.gameObject.SetActive(true);

        //    if (obj.name == "Gold_Add_Btn")
        //    {
        //        // 해당사항 메뉴 흰색으로 표시
        //        Shop_Menu_Text[0].color = Color.gray;
        //        Shop_Menu_Text[1].color = Color.white;
        //        Shop_Menu_Text[2].color = Color.gray;

        //        Shop_PopUp[0].gameObject.SetActive(false);
        //        Shop_PopUp[1].gameObject.SetActive(true);
        //        Shop_PopUp[2].gameObject.SetActive(false);

        //        // 포커스 위치 조정
        //        Shop_Focus.transform.position = new Vector3(0, Shop_Focus.transform.position.y, 0);
        //    }
        //    else if (obj.name == "Gem_Add_Btn")
        //    {
        //        Shop_Menu_Text[0].color = Color.gray;
        //        Shop_Menu_Text[1].color = Color.gray;
        //        Shop_Menu_Text[2].color = Color.white;

        //        Shop_PopUp[0].gameObject.SetActive(false);
        //        Shop_PopUp[1].gameObject.SetActive(false);
        //        Shop_PopUp[2].gameObject.SetActive(true);

        //        // 포커스 위치 조정
        //        Shop_Focus.transform.position = new Vector3(400, Shop_Focus.transform.position.y, 0);
        //    }
        //}

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
                //Debug.Log(1);
                // 해당사항 메뉴 흰색으로 표시
                Shop_Menu_Text[0].color = Color.white;
                Shop_Menu_Text[1].color = Color.gray;
                Shop_Menu_Text[2].color = Color.gray;

                Shop_PopUps[0].gameObject.SetActive(true);
                Shop_PopUps[1].gameObject.SetActive(false);
                Shop_PopUps[2].gameObject.SetActive(false);
            }
            else if (btn.name == "Shop_Gold_Button")
            {
                //Debug.Log(2);
                // 해당사항 메뉴 흰색으로 표시
                Shop_Menu_Text[0].color = Color.gray;
                Shop_Menu_Text[1].color = Color.white;
                Shop_Menu_Text[2].color = Color.gray;

                Shop_PopUps[0].gameObject.SetActive(false);
                Shop_PopUps[1].gameObject.SetActive(true);
                Shop_PopUps[2].gameObject.SetActive(false);
            }
            else if (btn.name == "Shop_Gem_Button")
            {
                //Debug.Log(3);
                // 해당사항 메뉴 흰색으로 표시
                Shop_Menu_Text[0].color = Color.gray;
                Shop_Menu_Text[1].color = Color.gray;
                Shop_Menu_Text[2].color = Color.white;

                Shop_PopUps[0].gameObject.SetActive(false);
                Shop_PopUps[1].gameObject.SetActive(false);
                Shop_PopUps[2].gameObject.SetActive(true);
            }
          
            // 포커스 위치 조정
            Shop_Focus.transform.position = new Vector3(btn.transform.position.x, Shop_Focus.transform.position.y, 0);
        }
        #endregion

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
    }
}
