using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class EnemySpawner : MonoBehaviour
    {
        //[SerializeField]
        //private GameObject[] enemyPrefab;    // �� ������

        //[SerializeField]
        //private float spawnTime;      // �� ���� �ֱ�

        [SerializeField]
        private Transform[] wayPoints;      // ���� �������� �̵� ���
        [SerializeField]
        private Wave currentWave;    // ���� ���̺� ����

        private WaveSystem wavesystem;

        [SerializeField]
        // ���� ���̺꿡�� ������ �� ����
        private int spawnEnemyCount = 0;



        private void Start()
        {
            wavesystem = GetComponent<WaveSystem>();
        }

        public void StartWave(Wave wave)
        {
            // �Ű������� �޾ƿ� ���̺� ���� ����
            currentWave = wave;
            // ���� ���̺� ����
            StartCoroutine("SpawnEnemy");

        }

        private IEnumerator SpawnEnemy()
        {
            while (spawnEnemyCount < currentWave.maxEnemyCount)
            {
                GameObject clone = Instantiate(currentWave.enemyPrefabs[wavesystem.currentWaveIndex], wayPoints[0]);        // �� ������Ʈ ����               
                Enemy enemy = clone.GetComponent<Enemy>();  // ��� ������ ���� Enemy ������Ʈ
                spawnEnemyCount++;
                if (spawnEnemyCount == currentWave.maxEnemyCount)
                {
                    wavesystem.currentWaveIndex++;
                    spawnEnemyCount = 0;

                    if (wavesystem.currentWaveIndex == currentWave.enemyPrefabs.Length)
                    {
                        currentWave.maxEnemyCount = 0;
                    }
                }
                enemy.Setup(wayPoints);                             // wayPoint ������ �Ű������� Setup() ȣ��
                yield return new WaitForSeconds(currentWave.spawnTime);         // spawnTime �ð� ���� ���
            }
        }
    }

}
