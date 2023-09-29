using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class LobbyManager : MonoBehaviour
    {
        #region Var
        [SerializeField]
        Slider SFX_Slider;
        [SerializeField]
        Slider BGM_Slider;
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

        #region Pop_Up
        public void OnClickOpenPopUp_Btn(GameObject obj)
        {
            obj.gameObject.SetActive(true);
        }

        public void OnClickClosePopUp_Btn(GameObject obj)
        {
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

        void Init()
        {
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
