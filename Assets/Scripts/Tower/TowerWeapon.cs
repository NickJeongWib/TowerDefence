using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public enum WeaponState { SearchTarget = 0, AttackToTarget }

    public class TowerWeapon : MonoBehaviour
    {
        [SerializeField]
        private GameObject      weaponPrefab;                               // �߻�ü ������
        [SerializeField]
        private Transform       spawnPoint;                                 // �߻�ü ���� ��ġ
        [SerializeField]
        private float           attackRate = 0.5f;                          // ���� �ӵ�
        [SerializeField]
        private float           attackRange = 10.0f;                         // ���� ����
        private WeaponState     weaponState = WeaponState.SearchTarget;     // Ÿ�� ������ ����
        private Transform       attackTarget = null;                        // ���� ���
        private EnemySpawner    enemySpawner;                               // ���ӿ� �����ϴ� �� ���� ȹ���

        // JGW
        Vector3 TargetDir;
        Vector3 TargetPos;


        public void Setup(EnemySpawner enemySpawner)
        {
            this.enemySpawner = enemySpawner;

            // ���� ���¸� WeaponState.SearchTarget���� ����
            ChangeState(WeaponState.SearchTarget);
        }

        public void ChangeState(WeaponState newState)
        {
            // ������ ������̴� ���� ����
            StopCoroutine(weaponState.ToString());
            // ���� ����
            weaponState = newState;
            // ���ο� ���� ���
            StartCoroutine(weaponState.ToString());
        }

        private void Update()
        {
            if(attackTarget != null)
            {
                RotateToTarget();
            }
        }
        private void RotateToTarget()
        {
            #region HMJ Weapon Rotate
            // �������κ����� �Ÿ��� ���������κ����� ������ �̿��� ��ġ�� ���ϴ� �� ��ǥ�� �̿�
            // ���� = arctan(y/x)
            // x,y ������ ���ϱ�
            float dx = attackTarget.position.x - transform.position.x;
            float dy = attackTarget.position.y - transform.position.y;
            // x,y �������� �������� ���� ���ϱ�
            float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, degree);
            #endregion

            #region JGW Weapon Rotate
            //// ���� ��ġ�� �˻��� Ÿ�� ��ġ
            //TargetPos = attackTarget.transform.position;
            //// �÷��̾ ����� �������� ���
            //TargetDir = TargetPos - transform.position;
            //// TargetDir�� ����ȭ ���ش�(0 ~ 1)
            //TargetDir = TargetDir.normalized;

            //weaponPrefab.transform.position = transform.position;
            //weaponPrefab.transform.rotation = Quaternion.FromToRotation(Vector3.down, TargetDir);
            #endregion 
        }

        private IEnumerator SearchTarget()
        {
            while (true)
            {
                // ���� ������ �ִ� ���� ã�� ���� ���� �Ÿ��� �ִ��� ũ�� ����
                float closestDistSqr = Mathf.Infinity;
                // EnemySpawner�� EnemyList�� �ִ� ���� �ʿ� �����ϴ� ��� �� �˻�
                for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
                {
                    float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                    // ���� �˻����� ������ �Ÿ��� ���ݹ��� ���� �ְ�, ������� �˻��� ������ �Ÿ��� ������
                    if(distance <= attackRange && distance <= closestDistSqr)
                    {
                        closestDistSqr = distance;
                        attackTarget = enemySpawner.EnemyList[i].transform;
                    }
                }

                if(attackTarget != null)
                {
                    ChangeState(WeaponState.AttackToTarget);
                }

                yield return null;
            }
        }

        private IEnumerator AttackToTarget()
        {
            while(true)
            {
                // 1. target�� �ִ��� �˻� ( �ٸ� �߻�ü�� ���� ����, Goal �������� �̵��� ���� ��)
                if(attackTarget == null)
                {
                    ChangeState(WeaponState.SearchTarget);
                    break;
                }

                // 2. target�� ���� ���� �ȿ� �ִ��� �˻�(���� ������ ����� ���ο� �� Ž��)
                float distance = Vector3.Distance(attackTarget.position, transform.position);
                if(distance > attackRange)
                {
                    attackTarget = null;
                    ChangeState(WeaponState.SearchTarget);
                    break;
                }

                // 3. attackRate �ð���ŭ ���
                yield return new WaitForSeconds(attackRate);

                // 4. ���� (�߻�ü ����)
                SpawnWeapon();
            }
        }

        private void SpawnWeapon()
        {
            GameObject clone = Instantiate(weaponPrefab, spawnPoint.position, Quaternion.identity);
            // ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����
            clone.GetComponent<Targeting>().Setup(attackTarget);
        }
    }
}