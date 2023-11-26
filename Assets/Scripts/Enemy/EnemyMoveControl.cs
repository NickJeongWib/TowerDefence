using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace TowerDefence
{
    public class EnemyMoveControl : MonoBehaviour
    {
        [SerializeField]
        public float moveSpeed = 0.0f;
        [SerializeField]
        private Vector3 moveDirection = Vector3.zero;
        public float MoveSpeed => moveSpeed;   //moveSpeed ������ ������Ƽ(Property) (Get ����)
        [SerializeField]
        SkeletonAnimation skeletonAnimation;
        
        void Start()
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
           
        }

        private void Update()
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        public void MoveTo(Vector3 direction)
        {
            moveDirection = direction;
        }

        // TODO ## EnemyMoveControl - ���� �浹 ����
        void OnTriggerEnter2D(Collider2D collision)
        {
            // JGW -- ��������Ʈ ���� �� ���� ���� ��ȯ
            if (collision.CompareTag("Go_Right"))
            {
                skeletonAnimation.initialSkinName = "Side";
                skeletonAnimation.AnimationName = "Side_Walk";
                skeletonAnimation.Initialize(true);
            }

            if (collision.CompareTag("Go_Up"))
            {
                skeletonAnimation.initialSkinName = "Back";
                skeletonAnimation.AnimationName = "Back_Walk";
                skeletonAnimation.Initialize(true);
            }

            if (collision.CompareTag("Go_Front"))
            {
                skeletonAnimation.initialSkinName = "Front";
                skeletonAnimation.AnimationName = "Front_Walk";
                skeletonAnimation.Initialize(true);
            }
        }
    }
}