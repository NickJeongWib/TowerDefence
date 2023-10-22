using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class TowerCharacter : MonoBehaviour
    {
        public Transform projectileSpawnPoint; // 발사체 발사 위치
        public GameObject projectilePrefab; // 발사체 프리팹
        public float projectileSpeed = 10.0f; // 발사체 속도
        public float attackCooldown = 1.0f; // 공격 쿨다운 시간
        private float currentCooldown = 0.0f; // 현재 쿨다운 시간
        public float attackRange = 3.0f; // 캐릭터의 공격 범위
        private Enemy closestEnemy;

        private void Update()
        {
            // 쿨다운 감소
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0)
            {
                // 가장 가까운 적을 찾음
                closestEnemy = FindClosestEnemy();

                if (closestEnemy != null)
                {
                    float distance = Vector3.Distance(transform.position, closestEnemy.transform.position);

                    if (distance <= attackRange)
                    {
                        // 발사체 생성
                        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

                        // 방향 설정
                        Vector3 direction = (closestEnemy.transform.position - projectileSpawnPoint.position).normalized;

                        // 회전 설정
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                        newProjectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                        // Rigidbody2D 가져오고 방향과 속도 설정
                        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
                        rb.velocity = direction * projectileSpeed;

                        currentCooldown = attackCooldown; // 공격 쿨다운 설정
                    }
                }
            }
        }

        private Enemy FindClosestEnemy()
        {
            // 모든 활성화된 적(Enemy)을 찾음
            Enemy[] enemies = FindObjectsOfType<Enemy>();

            if (enemies.Length == 0)
            {
                return null; // 적이 없으면 null 반환
            }

            float closestDistance = Mathf.Infinity;
            Enemy closest = null;

            foreach (Enemy enemy in enemies)
            {
                // 현재 캐릭터 위치에서 적까지의 거리 계산
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = enemy;
                }
            }

            return closest;
        }
    }
}