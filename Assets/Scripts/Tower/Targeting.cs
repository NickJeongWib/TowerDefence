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
            this.target = target;                                   // 타워가 설정해준 target
        }

        private void Update()
        {
            if(target != null)
            {
                // 발사체를 target의 위치로 이동
                Vector3 direction = (target.position - transform.position).normalized;
                enemyMoveControl.MoveTo(direction);
            }    
            else           // 여러 이유로 target이 사라지면
            {
                // 발사체 오브젝트 삭제
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Enemy")) return; // 적이 아닌 대상과 부딪히면
            if (collision.transform != target) return; // 현재 target인 적이 아닐때

            collision.GetComponent<Enemy>().OnDie(); // 적 사망 함수 호출
            Destroy(gameObject);    // 발사체 오브젝트 삭제
        }
    }
}