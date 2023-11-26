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
            // 충돌한 Collider2D가 Enemy 컴포넌트를 가지고 있는지 확인
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Enemy 컴포넌트가 있다면 TakeDamage 호출
                enemy.TakeDamage(attackDamage);
            }
        }
    }
}
