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
        GameObject[] Shop_PopUps; // Index 0 : ĳ���� ���� �˾�, Index 1 : ��� ���� �˾�, Index 2 : �� ���� �˾�

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

        // TODO ## �κ�ȭ�� �˾� ���� �Լ�
        #region Pop_Up
        public void OnClickOpenPopUp_Btn(GameObject obj)
        {
            //ȿ���� ����
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Btn_Select);

            obj.gameObject.SetActive(true);
        }

        public void OpenPopUp_Btn(GameObject obj)
        {
            //ȿ���� ����
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Btn_Select);

            if (obj.name == "Gold_Add_Btn")
            {
                Shop_PopUp.gameObject.SetActive(true);

                // �ش���� �޴� ������� ǥ��
                Shop_Menu_Text[0].color = Color.gray;
                Shop_Menu_Text[1].color = Color.white;
                Shop_Menu_Text[2].color = Color.gray;

                Shop_PopUps[0].gameObject.SetActive(false);
                Shop_PopUps[1].gameObject.SetActive(true);
                Shop_PopUps[2].gameObject.SetActive(false);

                // ��Ŀ�� ��ġ ����
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

                // ��Ŀ�� ��ġ ����
                Shop_Focus.transform.position = new Vector3(Shop_PopUp.transform.GetChild(1).GetChild(3).position.x, Shop_Focus.transform.position.y, 0);
            }
            else if (obj.name == "Button_Shop")
            {
                Shop_PopUp.gameObject.SetActive(true);

                // �ش���� �޴� ������� ǥ��
                Shop_Menu_Text[0].color = Color.white;
                Shop_Menu_Text[1].color = Color.gray;
                Shop_Menu_Text[2].color = Color.gray;

                Shop_PopUps[0].gameObject.SetActive(true);
                Shop_PopUps[1].gameObject.SetActive(false);
                Shop_PopUps[2].gameObject.SetActive(false);

                // ��Ŀ�� ��ġ ����
                Shop_Focus.transform.position = new Vector3(Shop_PopUp.transform.GetChild(1).GetChild(1).position.x, Shop_Focus.transform.position.y, 0);
            }
        }

        //public void OnClickOpenPopUp_Btn(GameObject obj, GameObject popUp)
        //{
        //    //ȿ���� ����
        //    GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Btn_Select);

        //    popUp.gameObject.SetActive(true);

        //    if (obj.name == "Gold_Add_Btn")
        //    {
        //        // �ش���� �޴� ������� ǥ��
        //        Shop_Menu_Text[0].color = Color.gray;
        //        Shop_Menu_Text[1].color = Color.white;
        //        Shop_Menu_Text[2].color = Color.gray;

        //        Shop_PopUp[0].gameObject.SetActive(false);
        //        Shop_PopUp[1].gameObject.SetActive(true);
        //        Shop_PopUp[2].gameObject.SetActive(false);

        //        // ��Ŀ�� ��ġ ����
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

        //        // ��Ŀ�� ��ġ ����
        //        Shop_Focus.transform.position = new Vector3(400, Shop_Focus.transform.position.y, 0);
        //    }
        //}

        public void OnClickClosePopUp_Btn(GameObject obj)
        {
            //ȿ���� ����
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.PopUp_Close);

            // ��������ȭ���� �ݱ� ��ư Ŭ�� ��
            if (obj.gameObject.name == "DungeonSelect_Close")
            {
                // ���� ����ȭ���� ��ũ�� value�� 0���� �ʱ�ȭ ���ְ� ������Ʈ ��Ȱ��ȭ.
                obj.transform.parent.GetChild(1).GetComponent<Scrollbar>().value = 0;
                obj.transform.parent.gameObject.SetActive(false);

                ScrollManagerRef.targetPos = 0.0f;
                ScrollManagerRef.curPos = 0.0f;
                return;
            }

            obj.transform.parent.gameObject.SetActive(false);
        }
        #endregion

        // TODO ## �κ�ȭ�� ȯ�漳�� ���� ���� �Լ�
        #region Sound BGM / SFX
        public void SetSFXVolume(float volume)
        {
            // �迭�� �����ϴ� ����Ʈ ������ ũ�⸦ �����Ѵ�.
            for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
            {

                GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].volume = volume;
            }
        }

        public void SetBGMVolume(float volume)
        {
            // �迭�ȿ� �����ϴ� ������� ũ�⸦ �����Ѵ�.
            for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
            {
                GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].volume = volume;
            }
        }

        #endregion

        // TODO ## �κ�ȭ�� ���� �޴� ���� �˾�
        #region Shop_Menu
        public void OnClick_Shop_Menu_Change(GameObject btn)
        {
            // Debug.Log(btn.name);

            if (btn.name == "Shop_Character_Button")
            {
                //Debug.Log(1);
                // �ش���� �޴� ������� ǥ��
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
                // �ش���� �޴� ������� ǥ��
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
                // �ش���� �޴� ������� ǥ��
                Shop_Menu_Text[0].color = Color.gray;
                Shop_Menu_Text[1].color = Color.gray;
                Shop_Menu_Text[2].color = Color.white;

                Shop_PopUps[0].gameObject.SetActive(false);
                Shop_PopUps[1].gameObject.SetActive(false);
                Shop_PopUps[2].gameObject.SetActive(true);
            }
          
            // ��Ŀ�� ��ġ ����
            Shop_Focus.transform.position = new Vector3(btn.transform.position.x, Shop_Focus.transform.position.y, 0);
        }
        #endregion

        void Init()
        {
            GameManager.GMInstance.lobbyManagerRef = this;

            // ���� ���� �ʱ�ȭ
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
