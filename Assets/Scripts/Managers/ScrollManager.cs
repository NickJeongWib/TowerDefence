using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TowerDefence
{
    public class ScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Scrollbar scrollbar;
        public Transform contentTr;

        
        const int SIZE = 10;
        float[] pos = new float[SIZE];

        public float distance, curPos, targetPos;
        bool isDrag;
        [SerializeField]
        int targetIndex;

        void Start()
        {
            // �Ÿ��� ���� 0~1�� pos����
            distance = 1f / (SIZE - 1);
            for (int i = 0; i < SIZE; i++) 
                pos[i] = distance * i;
        }

        float SetPos()
        {
            // ���ݰŸ��� �������� ����� ��ġ�� ��ȯ
            for (int i = 0; i < SIZE; i++)
                if (scrollbar.value < pos[i] + distance * 0.5f && scrollbar.value > pos[i] - distance * 0.5f)
                {
                    targetIndex = i;
                    return pos[i];
                }
            return 0;
        }
        public void OnBeginDrag(PointerEventData eventData) => curPos = SetPos();

        public void OnDrag(PointerEventData eventData) => isDrag = true;

        public void OnEndDrag(PointerEventData eventData)
        {
            //ȿ���� ����
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Slide);

            isDrag = false;
            targetPos = SetPos();

            // ���ݰŸ��� ���� �ʾƵ� ���콺�� ������ �̵��ϸ�
            if (curPos == targetPos)
            {
                // �� ���� ������ ��ǥ�� �ϳ� ����
                if (eventData.delta.x > 18 && curPos - distance >= 0)
                {
                    --targetIndex;
                    targetPos = curPos - distance;
                }

                // �� ���� ������ ��ǥ�� �ϳ� ����
                else if (eventData.delta.x < -18 && curPos + distance <= 1.01f)
                {
                    ++targetIndex;
                    targetPos = curPos + distance;
                }
            }
        }

        void Update()
        {
            if (!isDrag)
            {
                scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);
            }


            if (Time.time < 0.1f) return;
        }
    }
}
