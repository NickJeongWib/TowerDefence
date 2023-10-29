using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerDefence.Define;
namespace TowerDefence
{
    public class GameDataManager : MonoBehaviour
    {
        // Ŭ���� �� �������� ����
        [SerializeField]
        public bool[] isClearStage;
        
        public Stage_Level Stage_Lv;

        void Start()
        {
            GameManager.GMInstance.gameDataManagerRef = this;
        }
    }
}