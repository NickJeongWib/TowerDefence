using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace TowerDefence
{
    public class Enemy : MonoBehaviour
    {
        public int wayPointCount;          // 이동 경로 개수
        public Transform[] wayPoints;              // 이동 경로 정보
        private int currentIndex = 0;       // 현재 목표지점 인덱스
        private EnemyMoveControl enemyMoveControl;       // 오브젝트 이동 제어

        WaveSystem waveSystem;
        [SerializeField]
        private float maxHP;
        private float currentHP;

        Player  player;

        public void Awake()
        {
            currentHP = maxHP;
        }

        void Start()
        {
            player = FindObjectOfType<Player>();
            waveSystem = FindObjectOfType<WaveSystem>();
        }

        public void Setup(Transform[] wayPoints)
        {
            enemyMoveControl = GetComponent<EnemyMoveControl>();

            // 적 이동 경로 WayPoints 정보 설정
            wayPointCount = wayPoints.Length;
            this.wayPoints = new Transform[wayPointCount];
            this.wayPoints = wayPoints;

            // 적의 위치를 첫번째 wayPoint 위치로 설정
            transform.position = wayPoints[currentIndex].position;

            // 적 이동/목표지점 설정 코루틴 함수 시작
            StartCoroutine("OnMove");
        }

        private IEnumerator OnMove()
        {
            // 다음 이동 방향 설정
            NextMoveTo();

            while (true)
            {
                // 적의 현재위치와 목표위치의 거리가 0.02* monsterMoveControl.MoveSpeed보다 작을 때 if 조건문 실행
                // Tip. monsterMoveControl.MoveSpeed를 곱해주는 이유는 속도가 빠르면 한 프레임에 0.02보다 크게 움직이기 때문에
                // if 조건문에 걸리지 않고 경로를 탈주하는 오브젝트가 발생할 수 있다.
                if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * enemyMoveControl.MoveSpeed)
                {

                    // 다음 이동 방향 설정
                    NextMoveTo();
                }
                yield return null;
            }
        }
        private void NextMoveTo()
        {
            //아직 이동할 wayPoints가 남아있다면
            if (currentIndex < wayPointCount - 1)
            {
                //적의 위치를 정확하게 목표 위치로 설정
                transform.position = wayPoints[currentIndex].position;
                // 이동 방향 설정 => 다음 목표지점(wayPoints)
                currentIndex++;
                Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
                enemyMoveControl.MoveTo(direction);
            }
            // 현재 위치가 마지막 wayPoints이면
            else
            {
                // 적 오브젝트 삭제
                Destroy(gameObject);
                player.TakeDamage();
            }
        }
        public void TakeDamage(float damage)
        {
            currentHP -= damage;

            if (currentHP <= 0)
            {
                Destroy(gameObject);
                waveSystem.KillCount++;
            }
        }
    }

}