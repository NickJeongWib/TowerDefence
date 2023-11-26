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
        public float MoveSpeed => moveSpeed;   //moveSpeed 변수의 프로퍼티(Property) (Get 가능)
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

        // TODO ## EnemyMoveControl - 몬스터 충돌 관련
        void OnTriggerEnter2D(Collider2D collision)
        {
            // JGW -- 웨이포인트 도착 시 몬스터 방향 전환
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