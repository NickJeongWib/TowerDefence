using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace TowerDefence
{
    public class Enemy : MonoBehaviour
    {
        private int                 wayPointCount;          // �̵� ��� ����
        private Transform[]         wayPoints;              // �̵� ��� ����
        private int                 currentIndex = 0;       // ���� ��ǥ���� �ε���
        private EnemyMoveControl    enemyMoveControl;       // ������Ʈ �̵� ����
        private EnemySpawner        enemySpawner;           // ���� ������ ������ ���� �ʰ� EnemySpawner�� �˷��� ����

        public void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
        {
            enemyMoveControl = GetComponent<EnemyMoveControl>();
            this.enemySpawner = enemySpawner;

            // �� �̵� ��� WayPoints ���� ����
            wayPointCount   = wayPoints.Length;
            this.wayPoints  = new Transform[wayPointCount];
            this.wayPoints  = wayPoints;

            // ���� ��ġ�� ù��° wayPoint ��ġ�� ����
            transform.position = wayPoints[currentIndex].position;

            // �� �̵�/��ǥ���� ���� �ڷ�ƾ �Լ� ����
            StartCoroutine("OnMove");
        }

        private IEnumerator OnMove()
        {
            // ���� �̵� ���� ����
            NextMoveTo();

            while(true)
            {
                // ���� ������ġ�� ��ǥ��ġ�� �Ÿ��� 0.02* monsterMoveControl.MoveSpeed���� ���� �� if ���ǹ� ����
                // Tip. monsterMoveControl.MoveSpeed�� �����ִ� ������ �ӵ��� ������ �� �����ӿ� 0.02���� ũ�� �����̱� ������
                // if ���ǹ��� �ɸ��� �ʰ� ��θ� Ż���ϴ� ������Ʈ�� �߻��� �� �ִ�.
                if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * enemyMoveControl.MoveSpeed)
                {

                    // ���� �̵� ���� ����
                    NextMoveTo();
                }
                yield return null;
            }
        }
        private void NextMoveTo()
        {
            //���� �̵��� wayPoints�� �����ִٸ�
            if (currentIndex < wayPointCount -1)
            {
                //���� ��ġ�� ��Ȯ�ϰ� ��ǥ ��ġ�� ����
                transform.position = wayPoints[currentIndex].position;
                // �̵� ���� ���� => ���� ��ǥ����(wayPoints)
                currentIndex++;
                Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
                enemyMoveControl.MoveTo(direction);
            }
            // ���� ��ġ�� ������ wayPoints�̸�
            else
            {
                // �� ������Ʈ ����
                //Destroy(gameObject);
                OnDie();
            }
        }

        public void OnDie()
        {
            // EnemySpawner���� ����Ʈ�� �� ������ �����ϱ� ������ Destory()�� �������� �ʰ�
            // EnemySpawner���� ������ ������ �� �ʿ��� ó���� �ϵ��� DestoryEnemy() �Լ� ȣ��
            enemySpawner.DestroyEnemy(this);
        }
    }

}