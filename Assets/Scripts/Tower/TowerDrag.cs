using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefence
{
    public class TowerDrag : MonoBehaviour
    {
        public GameObject[] CharaterPrefabs;
        private GameObject draggedTower; // �巡�� ���� Ÿ�� ������Ʈ
        private Vector3 initialTowerPosition; // Ÿ���� �ʱ� ��ġ
        private Vector3 initialMouseOffset; //�巡�� ���� �� ���콺�� Ÿ�� ��ġ ���� ������
        private bool isDragging = false;
        bool ismix = false;
        bool dontmove;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                // Ŭ���� ��ġ�� Ÿ���� �ִٸ�
                if (hit.collider != null && hit.collider.CompareTag("Tower"))
                {
                    // �巡�� ����
                    draggedTower = hit.collider.gameObject;
                    initialMouseOffset = draggedTower.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    initialTowerPosition = draggedTower.transform.position; // �ʱ� ��ġ ���
                    isDragging = true;
                    ismix = false;
                }
            }

            // �巡�� ���� Ÿ���� ���� ��
            if (isDragging && draggedTower != null)
            {
                // �巡�� ���� Ÿ���� ���콺 ��ġ�� �̵�
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                draggedTower.transform.position = new Vector3(mousePosition.x, mousePosition.y, draggedTower.transform.position.z) + new Vector3(initialMouseOffset.x, initialMouseOffset.y, 0);

                if (Input.GetMouseButtonUp(0) && draggedTower != null && dontmove == false)
                {
                    draggedTower.transform.position = new Vector3(initialTowerPosition.x, initialTowerPosition.y, 0);
                    isDragging = false;
                    ismix = true;
                }
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Tower") && ismix == true)
            {
                
                Destroy(draggedTower);
                Destroy(other.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
           if(other.CompareTag("Tower"))
            {
                dontmove = true;
            }
        }
    }
}
