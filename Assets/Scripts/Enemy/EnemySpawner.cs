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
        private Wave        currentWave;    // ���� ���̺� ����

        WaveSystem wavesystem;

        [SerializeField]
        // ���� ���̺꿡�� ������ �� ����
        int spawnEnemyCount = 0;
        [SerializeField]
        int enemycount = 0;

        private void Start()
        {
            wavesystem = GetComponent<WaveSystem>();
        }

        private void Awake()
        {
            // �� ���� �ڷ�ƾ �Լ� ȣ��
            //StartCoroutine("SpawnEnemy");
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
                GameObject clone = Instantiate(currentWave.enemyPrefabs[spawnEnemyCount]);        // �� ������Ʈ ����               
                Enemy enemy = clone.GetComponent<Enemy>();     // ��� ������ ���� Enemy ������Ʈ

                enemycount++;
                if (enemycount == currentWave.maxEnemyCount)
                {
                    wavesystem.currentWaveIndex++;
                    spawnEnemyCount++;
                    enemycount = 0;
                }
                enemy.Setup(wayPoints);                             // wayPoint ������ �Ű������� Setup() ȣ��
                yield return new WaitForSeconds(currentWave.spawnTime);         // spawnTime �ð� ���� ���
            }
        }
    }

}
