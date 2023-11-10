using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class Define
    {
        // 스테이지 선택
        public enum Stage_Level
        {
            Stage_1,
            Stage_2,
            Stage_3,
            Stage_4,
            Stage_5,
            Stage_6,
            Stage_7,
            Stage_8,
            Stage_9,
            Stage_10,
        }

        // 퀘스트 카테고리
        public enum Quest_Category_Type
        {
            Character_Mix,
            Stage_Clear,
            Monster_Kill_Count,
        }

        public enum Quest_Reward
        {
            Gem,
            Gold,
        }

        // 캐릭터 타입
        public enum CharacterType
        {
            Fire, // 0
            Ice, // 1
            Grass, // 2
            Lightning,
            Dark,
        }
    }
}