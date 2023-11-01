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

        public GameObject[] character;

        List<Dictionary<string, object>> Character_Data;

       void Awake()
        {
            // TODO ## 데이터 테이블 읽어오는 곳
            Character_Data = CSVReader.Read("CSV");

            for (int i = 0; i < Character_Data.Count; i++)
            {
                character[i].GetComponent<TowerCharacter>().characterinfo.Character_ID = (int)Character_Data[i]["Character_ID"];
                character[i].GetComponent<TowerCharacter>().characterinfo.Charactertype = (CharacterType)Character_Data[i]["Character_Type"];
                character[i].GetComponent<TowerCharacter>().characterinfo.Character_Name = Character_Data[i]["Character_Name"].ToString();
                character[i].GetComponent<TowerCharacter>().characterinfo.Price = (int)Character_Data[i]["Price"];
                character[i].GetComponent<TowerCharacter>().characterinfo.Attack = (float)Character_Data[i]["Attack"];
                character[i].GetComponent<TowerCharacter>().characterinfo.ATK_Speed = (float)Character_Data[i]["ATK_Speed"];
                character[i].GetComponent<TowerCharacter>().characterinfo.Ability_Percent = (float)Character_Data[i]["Ability_Percent"];


                print("ID : " + Character_Data[i]["Character_ID"] + " " +
                    "Type : " + Character_Data[i]["Character_Type"] + " " +
                    "Name : " + Character_Data[i]["Character_Name"] + " " +
                    "Price : " + Character_Data[i]["Price"] + " " +
                    "Attack : " + Character_Data[i]["Attack"] + " " +
                    "ATK_Speed : " + Character_Data[i]["ATK_Speed"] + " " +
                    "Ability_Percent : " + Character_Data[i]["Ability_Percent"]);
            }
        }

        void Start()
        {
            GameManager.GMInstance.gameDataManagerRef = this;
        }
    }
}