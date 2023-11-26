using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class EffectDamage : MonoBehaviour
    {
        public float attackDamage;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �浹�� Collider2D�� Enemy ������Ʈ�� ������ �ִ��� Ȯ��
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Enemy ������Ʈ�� �ִٸ� TakeDamage ȣ��
                enemy.TakeDamage(attackDamage);
            }
        }
    }
}
