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

        public GameObject hitEffect;
        IngameManager ingameManager;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            ingameManager = FindObjectOfType<IngameManager>();
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
                Destroy(gameObject);
            }

            if(target != null && target.GetComponent<Enemy>().currentHP <= 0 )
            {
                Destroy(gameObject);
            }

        }

        private void FindClosestEnemy()
        {
            // 모든 활성화된 적(Enemy)을 찾음
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            
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
            if (collision.CompareTag("Enemy"))
            {
                if(this.CompareTag("Dark_Arrow"))
                {
                    if (Random.Range(0f, 100f) <= charaterAbility && collision.GetComponent<Enemy>().currentHP > attackDamage)
                    {
                        Destroy(gameObject);
                        Destroy(collision.gameObject);
                        ingameManager.EnemyKilled();
                    }
                    else
                    {
                        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Hit);
                        collision.GetComponent<Enemy>().TakeDamage(attackDamage);
                        Destroy(gameObject);
                    }
                }
                else
                {
                    GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Hit);
                    collision.GetComponent<Enemy>().TakeDamage(attackDamage);
                    Destroy(gameObject);
                }
            }
        }
    }
}