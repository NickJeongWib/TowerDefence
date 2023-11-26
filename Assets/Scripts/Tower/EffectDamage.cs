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
            collision.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
}
