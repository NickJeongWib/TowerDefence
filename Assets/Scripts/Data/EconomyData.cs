using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [System.Serializable]
    public class EconomyData
    {
        public int Gold;
        public int Gem;
        public bool isFirstStarter;

        public EconomyData(GameDataManager gamedatamanager)
        {
            Gold = gamedatamanager.Gold;
            Gem = gamedatamanager.Gem;
            isFirstStarter = gamedatamanager.isFirstStarter;
        }
    }
}