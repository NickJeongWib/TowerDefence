using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class Arrow : MonoBehaviour
    {
        public Transform target; // ���� ��ǥ�ϴ� ��� (��: ��)
        public float speed = 10.0f; // ȭ�� �̵� �ӵ�
        private Rigidbody2D rb;

        [SerializeField]
        private int attackDamage = 1;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
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
                rb.velocity = moveDirection * speed;
            }
            else
            {
                // ��ǥ�� ������ �ٽ� ���� ����� ���� ã��
                FindClosestEnemy();
            }
        }

        private void FindClosestEnemy()
        {
            // ��� Ȱ��ȭ�� ��(Enemy)�� ã��
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            if (enemies.Length == 0)
            {
                return; // ���� ������ �ƹ� �۾��� ���� ����
            }

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
            if (!collision.CompareTag("Enemy")) return;
            if (collision.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }

            collision.GetComponent<Enemy>().TakeDamage(attackDamage);
            Destroy(gameObject);
        }
    }
}