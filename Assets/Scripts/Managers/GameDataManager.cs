using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerDefence.Define;

namespace TowerDefence
{
    public class GameDataManager : MonoBehaviour
    {
        // 클리어 된 스테이지 여부
        [SerializeField]
        public bool[] isClearStage;

        public Stage_Level Stage_Lv;

        public GameObject[] Equip_Char;

        public GameObject[] character;
        public GameObject[] quest;
        public GameObject[] monster;

        List<Dictionary<string, object>> Character_Data;
        List<Dictionary<string, object>> Quest_Data;
        List<Dictionary<string, object>> Monster_Data;

        public int Gold;
        public int Gem;
        public bool isFirstStarter;
        public bool isFirstEnter;

        void Awake()
        {
            // TODO ## 데이터 테이블 읽어오는 곳
            Character_Data = CSVReader.Read("Character_DT");
            Quest_Data = CSVReader.Read("Quest_DT");
            Monster_Data = CSVReader.Read("Monster_DT");

            #region DB_Character
            for (int i = 0; i < Character_Data.Count; i++)
            {
                character[i].GetComponent<TowerCharacter>().characterinfo.Index = (int)Character_Data[i]["Index"];
                character[i].GetComponent<TowerCharacter>().characterinfo.Character_ID = (int)Character_Data[i]["Char_ID"];
                character[i].GetComponent<TowerCharacter>().characterinfo.Character_Name = Character_Data[i]["Char_Name"].ToString();
                character[i].GetComponent<TowerCharacter>().characterinfo.Level = (int)Character_Data[i]["Char_Lv"];
                character[i].GetComponent<TowerCharacter>().characterinfo.Charactertype = (CharacterType)Character_Data[i]["Char_Type"];

                character[i].GetComponent<TowerCharacter>().characterinfo.Damage = (float)Character_Data[i]["Char_Damage"];
                character[i].GetComponent<TowerCharacter>().characterinfo.Damage_Up_Rate = (float)Character_Data[i]["Char_Up_Damage"];

                character[i].GetComponent<TowerCharacter>().characterinfo.Char_ATKSpeed = (float)Character_Data[i]["Char_ATKSpeed"] / 100.0f;
                character[i].GetComponent<TowerCharacter>().characterinfo.ATK_Speed_Up_Rate = (float)Character_Data[i]["Char_Up_ATKSpeed"] / 100.0f;

                character[i].GetComponent<TowerCharacter>().characterinfo.ATK_Range = (float)Character_Data[i]["Char_ATKRange"] / 100.0f;
                character[i].GetComponent<TowerCharacter>().characterinfo.ATK_Range_Up_Rate = (float)Character_Data[i]["Char_Up_ATKRange"] / 100.0f;

                character[i].GetComponent<TowerCharacter>().characterinfo.Ability_Percent = (float)Character_Data[i]["Char_Ability"];
                character[i].GetComponent<TowerCharacter>().characterinfo.Ability_Percent_Up_Rate = (float)Character_Data[i]["Char_Up_Ability"];

                character[i].GetComponent<TowerCharacter>().characterinfo.Character_Upgrade_Price = (int)Character_Data[i]["Char_Up_Price"];

                character[i].GetComponent<TowerCharacter>().characterinfo.Spawn_Cost = (int)Character_Data[i]["Char_Spawn_Cost"];
                character[i].GetComponent<TowerCharacter>().characterinfo.Price = (int)Character_Data[i]["Char_Price"];

                if ((int)Character_Data[i]["Char_Exist"] == 1)
                {
                    character[i].GetComponent<TowerCharacter>().characterinfo.isExist = true;
                }
                else
                {
                    character[i].GetComponent<TowerCharacter>().characterinfo.isExist = false;
                }

                if ((int)Character_Data[i]["Base_Char"] == 1)
                {
                    character[i].GetComponent<TowerCharacter>().characterinfo.isBase_Char = true;
                }
                else
                {
                    character[i].GetComponent<TowerCharacter>().characterinfo.isBase_Char = false;
                }

                //print("ID : " + Character_Data[i]["Character_ID"] + " " +
                //    "Type : " + Character_Data[i]["Character_Type"] + " " +
                //    "Name : " + Character_Data[i]["Character_Name"] + " " +
                //    "Price : " + Character_Data[i]["Price"] + " " +
                //    "Attack : " + Character_Data[i]["Attack"] + " " +
                //    "ATK_Speed : " + Character_Data[i]["ATK_Speed"] + " " +
                //    "Ability_Percent : " + Character_Data[i]["Ability_Percent"]);
            }
            #endregion

            #region DB_Quest
            // TODO ## 업적 데이터 테이블 읽어오는 곳
            for (int i = 0; i < Quest_Data.Count; i++)
            {
                quest[i].GetComponent<Quest>().QuestInfo.Index = (int)Quest_Data[i]["Index"];
                quest[i].GetComponent<Quest>().QuestInfo.Quest_ID = (int)Quest_Data[i]["Quest_ID"];
                quest[i].GetComponent<Quest>().QuestInfo.Quest_Category = Quest_Data[i]["Category"].ToString();
                quest[i].GetComponent<Quest>().QuestInfo.Quest_Type = (Quest_Category_Type)Quest_Data[i]["Category_Type"];
                quest[i].GetComponent<Quest>().QuestInfo.Quest_Desc = Quest_Data[i]["Description"].ToString();
                quest[i].GetComponent<Quest>().QuestInfo.Reward_Category = Quest_Data[i]["Reward_Category"].ToString();
                quest[i].GetComponent<Quest>().QuestInfo.Reward_Type = (Quest_Reward)Quest_Data[i]["Reward_Type"];
                quest[i].GetComponent<Quest>().QuestInfo.Reward_Amount = (int)Quest_Data[i]["Amount"];

                // 업적 클리어 여부
                int isclear = (int)Quest_Data[i]["IsClear"];

                if (isclear == 1)
                {
                    quest[i].GetComponent<Quest>().QuestInfo.Quest_IsClear = true;
                }
                else
                {
                    quest[i].GetComponent<Quest>().QuestInfo.Quest_IsClear = false;
                }
            }
            #endregion

            #region DB_Monster
            for (int i = 0; i < Monster_Data.Count; i++)
            {
                monster[i].GetComponent<Enemy>().monsterinfo.index = (int)Monster_Data[i]["Index"];
                monster[i].GetComponent<Enemy>().monsterinfo.MonsterID = (int)Monster_Data[i]["Monster_ID"];
                monster[i].GetComponent<Enemy>().monsterinfo.Monster_Name = Monster_Data[i]["Monster_Name"].ToString();
                monster[i].GetComponent<Enemy>().monsterinfo.Spawn_Stage = (Stage_Level)((int)Monster_Data[i]["Spawn_Stage"] - 1);
                monster[i].GetComponent<Enemy>().monsterinfo.Monster_HP = (float)Monster_Data[i]["Monster_Hp"];
                monster[i].GetComponent<Enemy>().monsterinfo.Monster_Speed = (float)Monster_Data[i]["Monster_Speed"] / 100;
            }

            #endregion
        }

        void Start()
        {
            GameManager.GMInstance.gameDataManagerRef = this;

            JsonSerialize.LoadPlayerFromJson(this);
        }
    }
}