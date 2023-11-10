using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefence
{
    public class TowerDrag : MonoBehaviour
    {
        public GameObject[] CharaterPrefabs;
        private GameObject draggedTower; // 드래그 중인 타워 오브젝트
        private Vector3 initialTowerPosition; // 타워의 초기 위치
        private Vector3 initialMouseOffset; //드래그 시작 시 마우스와 타워 위치 간의 오프셋
        private bool isDragging = false;
        bool ismix = false;
        bool dontmove;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                // 클릭한 위치에 타워가 있다면
                if (hit.collider != null && hit.collider.CompareTag("Tower"))
                {
                    // 드래그 시작
                    draggedTower = hit.collider.gameObject;
                    initialMouseOffset = draggedTower.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    initialTowerPosition = draggedTower.transform.position; // 초기 위치 기억
                    isDragging = true;
                    ismix = false;
                }
            }

            // 드래그 중인 타워가 있을 때
            if (isDragging && draggedTower != null)
            {
                // 드래그 중인 타워를 마우스 위치로 이동
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
