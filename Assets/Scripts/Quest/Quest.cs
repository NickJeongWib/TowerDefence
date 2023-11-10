using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerDefence.Define;

namespace TowerDefence
{
    [System.Serializable]
    public struct Quest_Info
    {
        public int Quest_ID;
        public string Quest_Category;
        public Quest_Category_Type Quest_Type;
        public string Quest_Desc;
        public string Reward_Category;
        public Quest_Reward Reward_Type;
        public int Reward_Amount;
        public bool Quest_IsClear;

    }
    public class Quest : MonoBehaviour
    {
        public Quest_Info QuestInfo;
    }
}