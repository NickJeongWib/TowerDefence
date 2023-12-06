using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerDefence.Define;

namespace TowerDefence
{
    public class Arrow : MonoBehaviour
    {
      
        public Transform target; // ���� ��ǥ�ϴ� ��� (��: ��)
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
            FindClosestEnemy(); // ���� ����� �� ã��
        }

        private void Update()
        {
            if (target != null)
            {
                // ��ǥ ���� ���� ���
                Vector3 targetDirection = target.position - transform.position;

                // ȭ������ ���͸� ���� ȸ��
                transform.up = targetDirection.normalized;

                // ȭ���� �̵�
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
            // ��� Ȱ��ȭ�� ��(Enemy)�� ã��
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            
            Transform closestEnemy = enemies[0].transform;
            float closestDistance = Vector3.Distance(transform.position, closestEnemy.position);
            
            foreach (Enemy enemy in enemies)
            {
                // ���� ȭ�� ��ġ���� �������� �Ÿ� ���
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy.transform; 
                }
            }

            // ���� ����� ���� ��ǥ�� ����
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