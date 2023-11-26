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
        [SerializeField]
        private float currentHP;

        bool isgrassDamageUp;
        float grassDamageUp;

        Player  player;
        IngameManager ingameManager;

        public void Awake()
        {
            currentHP = maxHP;
        }

        void Start()
        {
            player = FindObjectOfType<Player>();
            waveSystem = FindObjectOfType<WaveSystem>();
            ingameManager = FindObjectOfType<IngameManager>();
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
                ingameManager.EnemyKilled();
            }
        }
        public void TakeDamage(float damage)
        {
            if(isgrassDamageUp == false)
            {
                currentHP -= damage;
            }
            else if(isgrassDamageUp == true)
            {
                currentHP -= damage + (damage * (grassDamageUp / 100));
            }
                

            if (currentHP <= 0)
            {
                Destroy(gameObject);
                ingameManager.EnemyKilled();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Fire_Arrow"))
            {
                Instantiate(collision.GetComponent<Arrow>().hitEffect, gameObject.transform.position, Quaternion.identity);
            }

            if (collision.CompareTag("Dark_Arrow"))
            {
                Instantiate(collision.GetComponent<Arrow>().hitEffect, gameObject.transform.position, Quaternion.identity);

                if (Random.Range(0f, 100f) <= collision.GetComponent<Arrow>().charaterAbility)
                {
                    Destroy(gameObject);
                    ingameManager.EnemyKilled();
                }
            }

            if(collision.CompareTag("Ice_Arrow"))
            {
                enemyMoveControl.moveSpeed = enemyMoveControl.moveSpeed - (enemyMoveControl.moveSpeed * ((collision.GetComponent<Arrow>().charaterAbility / 100)));
                Instantiate(collision.GetComponent<Arrow>().hitEffect, gameObject.transform.position, Quaternion.identity);
            }

            if (collision.CompareTag("Grass_Arrow"))
            {
                isgrassDamageUp = true;

                grassDamageUp = collision.GetComponent<Arrow>().charaterAbility;
                Instantiate(collision.GetComponent<Arrow>().hitEffect, gameObject.transform.position, Quaternion.identity);
            }

            if (collision.CompareTag("Lightning_Arrow"))
            {
                Instantiate(collision.GetComponent<Arrow>().hitEffect, gameObject.transform.position, Quaternion.identity);
            }

        }


    }

}