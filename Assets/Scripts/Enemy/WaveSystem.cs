using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [System.Serializable]
    public struct Wave
    {
        public float spawnTime;      // ���� ���̺� �� ���� �ֱ�
        public int maxEnemyCount;  //���� ���̺� �� ���� ����
        public GameObject[] enemyPrefabs;   // ���� ���̺� �� ���� ����
    }

    public class WaveSystem : MonoBehaviour
    {
        [SerializeField]
        private Wave[] wave;
        [SerializeField]
        private EnemySpawner enemySpawner;
        public int currentWaveIndex;
        public int KillCount;

        public void Start()
        {
            if (currentWaveIndex < wave.Length)
            {
                enemySpawner.StartWave(wave[currentWaveIndex]);
            }
        }


    }
}

