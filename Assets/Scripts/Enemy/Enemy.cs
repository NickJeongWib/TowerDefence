using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using static TowerDefence.Define;

namespace TowerDefence
{
    [System.Serializable]
    public struct MonsterInfo
    {
        public int index;
        public int MonsterID;
        public string Monster_Name;
        public float Monster_HP;
        public float Monster_Speed;
        public Stage_Level Spawn_Stage;

    }

    public class Enemy : MonoBehaviour
    {
        public MonsterInfo monsterinfo;

        public int wayPointCount;          // �̵� ��� ����
        public Transform[] wayPoints;              // �̵� ��� ����
        private int currentIndex = 0;       // ���� ��ǥ���� �ε���
        private EnemyMoveControl enemyMoveControl;       // ������Ʈ �̵� ����

        WaveSystem waveSystem;
        [SerializeField]
        private float maxHP;
        private float currentHP;

        Player  player;

        public void Awake()
        {
            currentHP = maxHP;
        }

        void Start()
        {
            player = FindObjectOfType<Player>();
            waveSystem = FindObjectOfType<WaveSystem>();
        }

        public void Setup(Transform[] wayPoints)
        {
            enemyMoveControl = GetComponent<EnemyMoveControl>();

            // �� �̵� ��� WayPoints ���� ����
            wayPointCount = wayPoints.Length;
            this.wayPoints = new Transform[wayPointCount];
            this.wayPoints = wayPoints;

            // ���� ��ġ�� ù��° wayPoint ��ġ�� ����
            transform.position = wayPoints[currentIndex].position;

            // �� �̵�/��ǥ���� ���� �ڷ�ƾ �Լ� ����
            StartCoroutine("OnMove");
        }

        private IEnumerator OnMove()
        {
            // ���� �̵� ���� ����
            NextMoveTo();

            while (true)
            {
                // ���� ������ġ�� ��ǥ��ġ�� �Ÿ��� 0.02* monsterMoveControl.MoveSpeed���� ���� �� if ���ǹ� ����
                // Tip. monsterMoveControl.MoveSpeed�� �����ִ� ������ �ӵ��� ������ �� �����ӿ� 0.02���� ũ�� �����̱� ������
                // if ���ǹ��� �ɸ��� �ʰ� ��θ� Ż���ϴ� ������Ʈ�� �߻��� �� �ִ�.
                if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * enemyMoveControl.MoveSpeed)
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
            if (currentIndex < wayPointCount - 1)
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
                Destroy(gameObject);
                player.TakeDamage();
            }
        }
        public void TakeDamage(float damage)
        {
            currentHP -= damage;

            if (currentHP <= 0)
            {
                Destroy(gameObject);
                waveSystem.KillCount++;
            }
        }
    }

}