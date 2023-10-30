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

        void Awake()
        {
            // TODO ## 데이터 테이블 읽어오는 곳
            List<Dictionary<string, object>> data = CSVReader.Read("CSV");

            for (int i = 0; i < data.Count; i++)
            {
                print("ID : " + data[i]["Character_ID"] + " " +
                    "Type : " + data[i]["Character_Type"] + " " +
                    "Name : " + data[i]["Character_Name"] + " " +
                    "Price : " + data[i]["Price"] + " " +
                    "Attack : " + data[i]["Attack"] + " " +
                    "ATK_Speed : " + data[i]["ATK_Speed"] + " " +
                    "Ability_Percent : " + data[i]["Ability_Percent"]);
            }
        }

        void Start()
        {
            GameManager.GMInstance.gameDataManagerRef = this;
        }
    }
}