using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerDefence.Define;

namespace TowerDefence
{
    [System.Serializable]
    public struct Wave
    {
        public float spawnTime;      // 현재 웨이브 적 생성 주기
        public int maxEnemyCount;  //현재 웨이브 적 등장 숫자
        public GameObject[] enemyPrefabs;   // 현재 웨이브 적 등장 종류
        public Transform[] wayPoints;
    }

    public class WaveSystem : MonoBehaviour
    {
        [SerializeField]
        public Wave[] wave;
        [SerializeField]
        private EnemySpawner enemySpawner;
        public int currentWaveIndex;

        public void Start()
        {

            if (currentWaveIndex < wave.Length)
            {
                enemySpawner.StartWave(wave[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv]);
            }
        }
    }
}
