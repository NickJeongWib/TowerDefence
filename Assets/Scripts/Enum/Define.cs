using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class Define
    {
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


        public enum CharacterType
        { 
            Fire, // 0
            Water, // 1
            Grass, // 2
            Lighting,
            Dark,
        }
    }
}