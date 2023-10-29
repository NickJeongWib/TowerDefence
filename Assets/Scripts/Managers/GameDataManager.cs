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

        void Start()
        {
            GameManager.GMInstance.gameDataManagerRef = this;
        }
    }
}