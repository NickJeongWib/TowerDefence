using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerDefence.Define;

namespace TowerDefence
{
    public class Arrow : MonoBehaviour
    {
      
        public Transform target; // 촉이 목표하는 대상 (예: 적)
        private Rigidbody2D rb;
        public TowerCharacter towerCharacter;
        [SerializeField]
        public float attackDamage;
        [SerializeField]
        public float atkSpeed;

        public float charaterAbility;



        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            FindClosestEnemy(); // 가장 가까운 적 찾기
        }

        private void Update()
        {
            if (target != null)
            {
                // 목표 방향 벡터 계산
                Vector3 targetDirection = target.position - transform.position;

                // 화살촉이 몬스터를 향해 회전
                transform.up = targetDirection.normalized;

                // 화살을 이동
                Vector3 moveDirection = targetDirection.normalized;
                
                rb.velocity = moveDirection * atkSpeed;
            }
            else
            {
                // 목표가 없으면 다시 가장 가까운 적을 찾음
                FindClosestEnemy();
            }

        }

        private void FindClosestEnemy()
        {
            // 모든 활성화된 적(Enemy)을 찾음
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            if (enemies.Length == 0)
            {
                Destroy(gameObject);
                return; // 적이 없으면 아무 작업도 하지 않음
            }

            Transform closestEnemy = enemies[0].transform;
            float closestDistance = Vector3.Distance(transform.position, closestEnemy.position);

            foreach (Enemy enemy in enemies)
            {
                // 현재 화살 위치에서 적까지의 거리 계산
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy.transform;
                }
            }

            // 가장 가까운 적을 목표로 설정
            target = closestEnemy;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Enemy")) return;
            if (collision.CompareTag("Enemy"))
            {
                GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Hit);
                Destroy(gameObject);
            }

            collision.GetComponent<Enemy>().TakeDamage(attackDamage);
            Destroy(gameObject);
        }
    }
}