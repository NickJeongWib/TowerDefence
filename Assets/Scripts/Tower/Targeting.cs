using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerDefence
{
    public class Targeting : MonoBehaviour
    {
        private EnemyMoveControl enemyMoveControl;
        private Transform target;

        public void Setup(Transform target)
        {
            enemyMoveControl = GetComponent<EnemyMoveControl>();
            this.target = target;                                   // Ÿ���� �������� target
        }

        private void Update()
        {
            if(target != null)
            {
                // �߻�ü�� target�� ��ġ�� �̵�
                Vector3 direction = (target.position - transform.position).normalized;
                enemyMoveControl.MoveTo(direction);
            }    
            else           // ���� ������ target�� �������
            {
                // �߻�ü ������Ʈ ����
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Enemy"))
                return; // ���� �ƴ� ���� �ε�����
            if (collision.transform != target) 
                return; // ���� target�� ���� �ƴҶ�

            collision.GetComponent<Enemy>().OnDie(); // �� ��� �Լ� ȣ��
            Destroy(gameObject);    // �߻�ü ������Ʈ ����
        }
    }
}