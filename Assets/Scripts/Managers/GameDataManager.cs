using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class GameDataManager : MonoBehaviour
    {
        // Ŭ���� �� �������� ����
        [SerializeField]
        public bool[] isClearStage;

        void Start()
        {
            GameManager.GMInstance.gameDataManagerRef = this;
        }
    }
}