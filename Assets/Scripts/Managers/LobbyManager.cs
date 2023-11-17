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


        [Header("----Music----")]
        [SerializeField]
        Slider SFX_Slider;

        [SerializeField]
        Slider BGM_Slider;


        [Header("----Shop----")]
        [SerializeField]
        GameObject Shop_Focus;

        [SerializeField]
        TextMeshProUGUI[] Shop_Menu_Text;

        [SerializeField]
        GameObject[] Shop_PopUps; // Index 0 : ĳ���� ���� �˾�, Index 1 : ��� ���� �˾�, Index 2 : �� ���� �˾�

        [SerializeField]
        GameObject Shop_PopUp;

        [SerializeField]
        GameObject Shop_Character_Vertical_Value;

        [SerializeField]
        GameObject[] Shop_CharacterList;

        [Header("----UpGradePanel----")]
        [SerializeField]
        TextMeshProUGUI[] Upgrade_Panel_Text;
        [SerializeField]
        Image Upgrade_Panel_Image;

        [Header("----Inventory----")]
        [SerializeField]
        GameObject Select_Char;

        [SerializeField]
        GameObject Inventory_Vertical_Value;

        [SerializeField]
        GameObject Character_Select_PopUp;

        [SerializeField]
        GameObject Character_Upgrade_Panel;

        public GameObject[] Equip_CharacterList;

        [SerializeField]
        GameObject None_Touch_Btn;

        [SerializeField]
        GameObject[] Own_Char_List_UI;

        [SerializeField]
        GameObject[] Own_Char_List_Info;

        [SerializeField]
        bool[] is_EmptyEquip_Slots = new bool[5];


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

        // TODO ## �κ�ȭ�� �˾� ���� �Լ�
        #region Pop_Up
        /// <summary>
        /// �Ϲ� �˾� ����
        /// </summary>
        /// <param name="obj"></param>
        public void OnClickOpenPopUp_Btn(GameObject obj)
        {
            //ȿ���� ����
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Btn_Select);

            if (obj.name == "Character_Info_Btn")
            {
                // ĳ���� Ŭ�� �� �˾� Ȱ��ȭ
                Character_Select_PopUp.gameObject.SetActive(true);
                None_Touch_Btn.gameObject.SetActive(true);
                Character_Select_PopUp.transform.position = new Vector3(obj.transform.parent.position.x, obj.transform.parent.position.y + 100.0f, 0);

                Select_Char = obj.transform.parent.GetComponent<Own_Char>().OwnChar;
            }
            else if (obj.name == "Character_Upgrade")
            {
                Upgrade_PopUP_Refresh();

                OnClick_None_Touch_Btn();
            }

            obj.gameObject.SetActive(true);
        }

        /// <summary>
        /// TODO ## ���� ���� �˾� ����
        /// </summary>
        /// <param name="obj"></param>
        public void OpenPopUp_Btn(GameObject obj)
        {
            //ȿ���� ����
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Btn_Select);

            if (obj.name == "Gold_Add_Btn")
            {
                Shop_PopUp.gameObject.SetActive(true);

                Shop_Index = 2;

                //Debug.Log(1);
                // �ش���� �޴� ������� ǥ��

                for (int i = 0; i < Shop_Menu_Text.Length; i++)
                {
                    // ������ �ε����� ������ Ȱ��ȭ
                    if (i == Shop_Index)
                    {
                        Shop_Menu_Text[i].color = Color.white;
                        Shop_PopUps[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // �ε����� �ƴѰ͵��� ��Ȱ��ȭ
                        Shop_Menu_Text[i].color = Color.gray;
                        Shop_PopUps[i].gameObject.SetActive(false);
                    }
                }

                // ���� ��ȭ â ��ũ�� value �� �ʱ�ȭ - ��ũ�� �ٷ� ��� â�� �� �� �ִ� value��
                Shop_PopUp.transform.GetChild(7).GetChild(1).GetComponent<Scrollbar>().value = 0.095f;

                // ��Ŀ�� ��ġ ����
                Shop_Focus.transform.position = new Vector3(Shop_PopUp.transform.GetChild(1).GetChild(4).position.x, Shop_Focus.transform.position.y, 0);
            }
            else if (obj.name == "Gem_Add_Btn")
            {
                Shop_PopUp.gameObject.SetActive(true);

                Shop_Index = 2;

                //Debug.Log(1);
                // �ش���� �޴� ������� ǥ��

                for (int i = 0; i < Shop_Menu_Text.Length; i++)
                {
                    // ������ �ε����� ������ Ȱ��ȭ
                    if (i == Shop_Index)
                    {
                        Shop_Menu_Text[i].color = Color.white;
                        Shop_PopUps[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // �ε����� �ƴѰ͵��� ��Ȱ��ȭ
                        Shop_Menu_Text[i].color = Color.gray;
                        Shop_PopUps[i].gameObject.SetActive(false);
                    }
                }


                // ���� ��ȭ â ��ũ�� value �� �ʱ�ȭ - ��ũ�� �ֻ������ ����
                Shop_PopUp.transform.GetChild(7).GetChild(1).GetComponent<Scrollbar>().value = 1.0f;
                // ��Ŀ�� ��ġ ����
                Shop_Focus.transform.position = new Vector3(Shop_PopUp.transform.GetChild(1).GetChild(4).position.x, Shop_Focus.transform.position.y, 0);
            }
            else if (obj.name == "Button_Shop")
            {
                Shop_PopUp.gameObject.SetActive(true);

                Shop_Index = 0;

                //Debug.Log(1);
                // �ش���� �޴� ������� ǥ��

                for (int i = 0; i < Shop_Menu_Text.Length; i++)
                {
                    // ������ �ε����� ������ Ȱ��ȭ
                    if (i == Shop_Index)
                    {
                        Shop_Menu_Text[i].color = Color.white;
                        Shop_PopUps[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // �ε����� �ƴѰ͵��� ��Ȱ��ȭ
                        Shop_Menu_Text[i].color = Color.gray;
                        Shop_PopUps[i].gameObject.SetActive(false);
                    }
                }

                // ��Ŀ�� ��ġ ����
                Shop_Focus.transform.position = new Vector3(Shop_PopUp.transform.GetChild(1).GetChild(1).position.x, Shop_Focus.transform.position.y, 0);
            }
        }

        // TODO ## �˾� �ݱ� �Լ�
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

            // ���� ���� �ݱ� ��ư
            if (obj.gameObject.name == "Shop_Tap")
            {
                Shop_Character_Vertical_Value.GetComponent<Scrollbar>().value = 1.0f;

                Shop_PopUp.transform.GetChild(7).GetChild(1).GetComponent<Scrollbar>().value = 1.0f;
            }

            // �κ��丮 ���� �ݱ� ��ư
            if (obj.gameObject.name == "Inventory_BG")
            {
                Inventory_Vertical_Value.GetComponent<Scrollbar>().value = 1.0f;
                OnClick_None_Touch_Btn();
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
                Shop_Index = 0;

                //Debug.Log(1);
                // �ش���� �޴� ������� ǥ��

                for (int i = 0; i < Shop_Menu_Text.Length; i++)
                {
                    // ������ �ε����� ������ Ȱ��ȭ
                    if (i == Shop_Index)
                    {
                        Shop_Menu_Text[i].color = Color.white;
                        Shop_PopUps[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // �ε����� �ƴѰ͵��� ��Ȱ��ȭ
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
                // �ش���� �޴� ������� ǥ��

                for (int i = 0; i < Shop_Menu_Text.Length; i++)
                {
                    // ������ �ε����� ������ Ȱ��ȭ
                    if (i == Shop_Index)
                    {
                        Shop_Menu_Text[i].color = Color.white;
                        Shop_PopUps[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // �ε����� �ƴѰ͵��� ��Ȱ��ȭ
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
                    // ������ �ε����� ������ Ȱ��ȭ
                    if (i == Shop_Index)
                    {
                        Shop_Menu_Text[i].color = Color.white;
                        Shop_PopUps[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // �ε����� �ƴѰ͵��� ��Ȱ��ȭ
                        Shop_Menu_Text[i].color = Color.gray;
                        Shop_PopUps[i].gameObject.SetActive(false);
                    }
                }
                //Debug.Log(3);
                // �ش���� �޴� ������� ǥ��
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
                    // ������ �ε����� ������ Ȱ��ȭ
                    if (i == Shop_Index)
                    {
                        Shop_Menu_Text[i].color = Color.white;
                        Shop_PopUps[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // �ε����� �ƴѰ͵��� ��Ȱ��ȭ
                        Shop_Menu_Text[i].color = Color.gray;
                        Shop_PopUps[i].gameObject.SetActive(false);
                    }
                }

                //Debug.Log(3);
                // �ش���� �޴� ������� ǥ��
                //Shop_Menu_Text[0].color = Color.gray;
                //Shop_Menu_Text[1].color = Color.gray;
                //Shop_Menu_Text[2].color = Color.white;

                //Shop_PopUps[0].gameObject.SetActive(false);
                //Shop_PopUps[1].gameObject.SetActive(false);
                //Shop_PopUps[2].gameObject.SetActive(true);
            }

            // ��Ŀ�� ��ġ ����
            Shop_Focus.transform.position = new Vector3(btn.transform.position.x, Shop_Focus.transform.position.y, 0);
        }
        #endregion

        #region Init
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

            // TODO ## �κ� ���� �� ���� �ʱ�ȭ
            // ������ �⺻ ���� ���� �� �� �� ĳ���͵� �ֱ�
            for (int i = 0; i < Shop_CharacterList.Length; i++)
            {
                Shop_CharacterList[i].GetComponent<Shop_Character_List>().Shop_Char_Info = GameManager.GMInstance.gameDataManagerRef.character[i + 5];

                Shop_CharacterList[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                    Shop_CharacterList[i].GetComponent<Shop_Character_List>().Shop_Char_Info.GetComponent<SpriteRenderer>().sprite;
            }
           
            // ���� ���������̺��� ĳ���� ����ŭ ����
            for (int i = 0; i < GameManager.GMInstance.gameDataManagerRef.character.Length; i++)
            {
                // ���� ĳ���Ͱ� �����ϴ� ���� �ִٸ�
                if (GameManager.GMInstance.gameDataManagerRef.character[i].GetComponent<TowerCharacter>().characterinfo.isExist == true)
                {
                    // ����ĳ���Ϳ� �߰�
                    Own_Char_List_Info[i] = GameManager.GMInstance.gameDataManagerRef.character[i];
                }
            }

            for (int i = 0; i < Equip_CharacterList.Length; i++)
            {
                GameManager.GMInstance.gameDataManagerRef.Equip_Char[i] = Equip_CharacterList[i].GetComponent<Equip_Character_Info>().Equip_Character;
            }

            Own_Character_Refresh();
            Empty_Slot_Check();
        }
        #endregion

        #region Btn_Fun

        #region GameStart_Btn
        public void OnClickGameStart(GameObject obj)
        {
            // ��ư Ŭ���� ���� �ܰ� ����
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

        #endregion

        #region Inventory
        // TODO ## ĳ���� ����
        public void OnClick_Char_Equip()
        {
            if (is_EmptyEquip_Slots[0] && is_EmptyEquip_Slots[1] && is_EmptyEquip_Slots[2] && is_EmptyEquip_Slots[3] && is_EmptyEquip_Slots[4])
            {
                Debug.Log("Ǯĭ");
                return;
            }

            // ���� ���� �˻�
            for (int i = 0; i < Equip_CharacterList.Length; i++)
            {
                // ���� ���� ĭ�� ���ٸ� ����
                if (Equip_CharacterList[i].GetComponent<Equip_Character_Info>().Equip_Character == null)
                {
                    Equip_CharacterList[i].GetComponent<Equip_Character_Info>().Equip_Character = Select_Char;
                    Equip_CharacterList[i].GetComponent<Image>().sprite = Select_Char.GetComponent<SpriteRenderer>().sprite;
                    Equip_CharacterList[i].transform.localScale = Vector3.one;
                    Debug.Log("����");
                    break;
                }
            }

            for (int i = 0; i < Equip_CharacterList.Length; i++)
            {
                GameManager.GMInstance.gameDataManagerRef.Equip_Char[i] = Equip_CharacterList[i].GetComponent<Equip_Character_Info>().Equip_Character;
            }

            Empty_Slot_Check();
        }

        // TODO ## ĳ���� ����
        public void OnClick_Char_UnEquip()
        {
            // �ߺ� ĳ���� �˻�
            for (int i = 0; i < Equip_CharacterList.Length; i++)
            {
                if (Equip_CharacterList[i].GetComponent<Equip_Character_Info>().Equip_Character == null)
                {
                    continue;
                }
                // ���� ������ ĳ���Ϳ� ������ ĳ���Ͱ� �������ִٸ�
                if (Select_Char.GetComponent<TowerCharacter>().characterinfo.Character_ID == Equip_CharacterList[i].GetComponent<Equip_Character_Info>().Equip_Character.GetComponent<TowerCharacter>().characterinfo.Character_ID)
                {
                    Equip_CharacterList[i].transform.localScale = Vector3.zero;

                    Equip_CharacterList[i].GetComponent<Equip_Character_Info>().Equip_Character = null;
                }
            }

            for (int i = 0; i < Equip_CharacterList.Length; i++)
            {
                GameManager.GMInstance.gameDataManagerRef.Equip_Char[i] = Equip_CharacterList[i].GetComponent<Equip_Character_Info>().Equip_Character;
            }

            Empty_Slot_Check();
        }
        #endregion


        #region Fun

        void Upgrade_PopUP_Refresh()
        {
            if (Select_Char.GetComponent<TowerCharacter>().characterinfo.Charactertype == CharacterType.Fire)
            {
                // ĳ���� Ÿ��
                Upgrade_Panel_Text[1].text = "��";
            }
            else if (Select_Char.GetComponent<TowerCharacter>().characterinfo.Charactertype == CharacterType.Ice)
            {
                // ĳ���� Ÿ��
                Upgrade_Panel_Text[1].text = "����";
            }
            else if (Select_Char.GetComponent<TowerCharacter>().characterinfo.Charactertype == CharacterType.Grass)
            {
                // ĳ���� Ÿ��
                Upgrade_Panel_Text[1].text = "����";
            }
            else if (Select_Char.GetComponent<TowerCharacter>().characterinfo.Charactertype == CharacterType.Lightning)
            {
                // ĳ���� Ÿ��
                Upgrade_Panel_Text[1].text = "����";
            }
            else if (Select_Char.GetComponent<TowerCharacter>().characterinfo.Charactertype == CharacterType.Dark)
            {
                // ĳ���� Ÿ��
                Upgrade_Panel_Text[1].text = "���";
            }

            // ĳ���� �̹���
            Upgrade_Panel_Image.sprite = Select_Char.GetComponent<SpriteRenderer>().sprite;
            // ĳ���� �̸�
            Upgrade_Panel_Text[0].text = Select_Char.GetComponent<TowerCharacter>().characterinfo.Character_Name;
            // ĳ���� ���ݷ�
            Upgrade_Panel_Text[2].text = Select_Char.GetComponent<TowerCharacter>().characterinfo.Damage.ToString();
            // ĳ���� ���� �ӵ�
            Upgrade_Panel_Text[3].text = Select_Char.GetComponent<TowerCharacter>().characterinfo.Char_ATKSpeed.ToString();
            // ĳ���� ���� ����
            Upgrade_Panel_Text[4].text = Select_Char.GetComponent<TowerCharacter>().characterinfo.ATK_Range.ToString();
        }

        void Empty_Slot_Check()
        {
            for (int i = 0; i < Equip_CharacterList.Length; i++)
            {
                if (Equip_CharacterList[i].GetComponent<Equip_Character_Info>().Equip_Character != null)
                {
                    is_EmptyEquip_Slots[i] = true;
                }
                else
                {
                    is_EmptyEquip_Slots[i] = false;
                }
            }
        }

        void Equip_List_Refresh()
        {

        }

        public void Own_Character_Refresh()
        {
            for (int i = 0; i < Own_Char_List_Info.Length; i++)
            {
                // ���� ĳ���� ������ ������ �����ʴٸ� break�� for�� Ż��
                if (Own_Char_List_Info[i] == null)
                {
                    break;
                } // ĳ���͸� �����ϰ� �ִٸ� ĳ���� ���� ����
                else if (Own_Char_List_Info[i] != null)
                {
                    Own_Char_List_UI[i].GetComponent<Own_Char>().OwnChar = Own_Char_List_Info[i];

                    // �̹��� ���
                    Own_Char_List_UI[i].transform.GetChild(0).GetComponent<Image>().sprite =
                        Own_Char_List_UI[i].GetComponent<Own_Char>().OwnChar.GetComponent<SpriteRenderer>().sprite;

                    // ũ�� 1�� �����
                    Own_Char_List_UI[i].transform.GetChild(0).transform.localScale = Vector3.one;
                }
            }

            for (int i = 0; i < Own_Char_List_UI.Length; i++)
            {
                // ĳ���Ͱ� �� �����̶�� 
                if (Own_Char_List_UI[i].GetComponent<Own_Char>().OwnChar == null)
                {
                    Own_Char_List_UI[i].transform.GetChild(1).GetComponent<Button>().interactable = false;
                }
                else // ĳ���Ͱ� �����ϸ�
                {
                    Own_Char_List_UI[i].transform.GetChild(1).GetComponent<Button>().interactable = true;
                }
            }
        }
        #endregion
    }
}