using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class GameDataManager : MonoBehaviour
    {
        // 클리어 된 스테이지 여부
        [SerializeField]
        public bool[] isClearStage;

        void Start()
        {
            GameManager.GMInstance.gameDataManagerRef = this;
        }
    }
}