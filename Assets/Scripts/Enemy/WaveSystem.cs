using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerDefence.Define;

namespace TowerDefence
{
    [System.Serializable]
    public struct Wave
    {
        public float spawnTime;      // ���� ���̺� �� ���� �ֱ�
        public int maxEnemyCount;  //���� ���̺� �� ���� ����
        public GameObject[] enemyPrefabs;   // ���� ���̺� �� ���� ����
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
