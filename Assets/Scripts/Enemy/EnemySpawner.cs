using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class EnemySpawner : MonoBehaviour
    {
        //[SerializeField]
        //private GameObject[] enemyPrefab;    // 적 프리팹

        //[SerializeField]
        //private float spawnTime;      // 적 생성 주기

        [SerializeField]
        private Wave currentWave;    // 현재 웨이브 정보
        [SerializeField]
        private WaveSystem wavesystem;

        [SerializeField]
        // 현재 웨이브에서 생성한 적 숫자
        private int spawnEnemyCount = 0;

        public bool gameEND_Count = false;
        private void Start()
        {
            wavesystem = GetComponent<WaveSystem>();
        }

        public void StartWave(Wave wave)
        {
            // 매개변수로 받아온 웨이브 정보 지점
            currentWave = wave;
            // 현재 웨이브 시작
            StartCoroutine("SpawnEnemy");

        }

        private IEnumerator SpawnEnemy()
        {
            while (spawnEnemyCount < currentWave.maxEnemyCount)
            {
                GameObject clone = Instantiate(currentWave.enemyPrefabs[wavesystem.currentWaveIndex], currentWave.wayPoints[0]);        // 적 오브젝트 생성               
                Enemy enemy = clone.GetComponent<Enemy>();  // 방금 생성된 적의 Enemy 컴포넌트
                spawnEnemyCount++;
                enemy.Setup(currentWave.wayPoints);
                if (spawnEnemyCount == currentWave.maxEnemyCount)
                {
                    wavesystem.currentWaveIndex++;
                    spawnEnemyCount = 0;
                    
                    if (wavesystem.currentWaveIndex == currentWave.enemyPrefabs.Length)
                    {
                        currentWave.maxEnemyCount = 0;
                        wavesystem.currentWaveIndex = 4;
                        gameEND_Count = true;
                        break;
                    }
                }
                                           // wayPoint 정보를 매개변수로 Setup() 호출
                yield return new WaitForSeconds(currentWave.spawnTime);         // spawnTime 시간 동안 대기
            }
        }
    }

}
