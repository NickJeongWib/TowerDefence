using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerDefence.Define;

namespace TowerDefence
{
    [System.Serializable]
    public struct CharacterInfo
    {
        public int Character_ID;
        public CharacterType Charactertype;
        public string Character_Name;
        public int Level;
        public float Damage;
        public float Damage_Up_Rate;
        public float ATK_Speed;
        public float ATK_Speed_Up_Rate;
        public float ATK_Range;
        public float ATK_Range_Up_Rate;
        public float Ability_Percent;
        public float Ability_Percent_Up_Rate;
        public int Spawn_Cost;
        public int Character_Upgrade_Price;
        public int Price;

        public bool isExist;
    }

    public class TowerCharacter : MonoBehaviour
    {
        public CharacterInfo characterinfo;

        public Transform projectileSpawnPoint; // �߻�ü �߻� ��ġ
        public GameObject projectilePrefab; // �߻�ü ������
        public float attackCooldown = 1.0f; // ���� ��ٿ� �ð�
        private float currentCooldown = 0.0f; // ���� ��ٿ� �ð�
        public float attackRange = 3.0f; // ĳ������ ���� ����
        private Enemy closestEnemy;

        private void Update()
        {
            // ��ٿ� ����
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0)
            {
                // ���� ����� ���� ã��
                closestEnemy = FindClosestEnemy();

                if (closestEnemy != null)
                {
                    float distance = Vector3.Distance(transform.position, closestEnemy.transform.position);

                    if (distance <= attackRange)
                    {
                        // �߻�ü ����
                        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

                        newProjectile.transform.parent = this.transform;
                        // ���� ����
                        Vector3 direction = (closestEnemy.transform.position - projectileSpawnPoint.position).normalized;

                        // ȸ�� ����
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                        newProjectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                        // Rigidbody2D �������� ����� �ӵ� ����
                        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
                        rb.velocity = direction;

                        currentCooldown = attackCooldown; // ���� ��ٿ� ����
                    }
                }
            }
        }

        private Enemy FindClosestEnemy()
        {
            // ��� Ȱ��ȭ�� ��(Enemy)�� ã��
            Enemy[] enemies = FindObjectsOfType<Enemy>();

            if (enemies.Length == 0)
            {
                return null; // ���� ������ null ��ȯ
            }

            float closestDistance = Mathf.Infinity;
            Enemy closest = null;

            foreach (Enemy enemy in enemies)
            {
                // ���� ĳ���� ��ġ���� �������� �Ÿ� ���
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