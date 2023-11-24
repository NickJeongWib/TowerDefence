using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class TowerDrag : MonoBehaviour
    {
        public GameObject[] characterPrefabs;
        private GameObject draggedTower; // �巡�� ���� Ÿ�� ������Ʈ
        private Vector3 initialTowerPosition; // Ÿ���� �ʱ� ��ġ
        private Vector3 initialMouseOffset; // �巡�� ���� �� ���콺�� Ÿ�� ��ġ ���� ������
        public bool isDragging = false;
        TowerCharacter towerCharacter;
        TowerSpawner towerSpawner;
        public GameObject aura;
        public GameObject auraParticle;

        private void Start()
        {
            towerCharacter = FindObjectOfType<TowerCharacter>();
            towerSpawner = FindObjectOfType<TowerSpawner>();
            for (int i = 0; i < GameManager.GMInstance.gameDataManagerRef.Equip_Char.Length; i++)
            {
                characterPrefabs[i] = GameManager.GMInstance.gameDataManagerRef.Equip_Char[i];
            }
        }
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
                    towerCharacter.DragIn = false;
                }
                if (hit.collider != null && hit.collider.CompareTag("Tower2"))
                {
                    // �巡�� ����
                    draggedTower = hit.collider.gameObject;
                    initialMouseOffset = draggedTower.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    initialTowerPosition = draggedTower.transform.position; // �ʱ� ��ġ ���
                    isDragging = true;
                    towerCharacter.DragIn = false;
                }
            }
            // �巡�� ���� Ÿ���� ���� ��
            if (isDragging && draggedTower != null)
            {
                // �巡�� ���� Ÿ���� ���콺 ��ġ�� �̵�
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                draggedTower.transform.position = new Vector3(mousePosition.x, mousePosition.y, draggedTower.transform.position.z) + new Vector3(initialMouseOffset.x, initialMouseOffset.y, 0);

                // ���� ��ư�� ���� ��
                if (Input.GetMouseButtonUp(0))
                {
                    // �浹 üũ
                    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(draggedTower.transform.position, 0.1f);
                    foreach (Collider2D hitCollider in hitColliders)
                    {
                        if (hitCollider.CompareTag("Tower") && hitCollider.gameObject != draggedTower && draggedTower.CompareTag("Tower"))
                        {
                            // �浹�� Ÿ���� �巡�� ���� Ÿ���� ����
                            Destroy(hitCollider.gameObject);
                            Destroy(draggedTower);
                            int randomCharacterIndex = Random.Range(0, characterPrefabs.Length);
                            GameObject newCharacter = Instantiate(characterPrefabs[randomCharacterIndex], hitCollider.gameObject.transform.position, Quaternion.identity);
                            GameObject newAura = Instantiate(aura, hitCollider.gameObject.transform.position, Quaternion.identity);
                            newCharacter.tag = "Tower2";
                            newAura.transform.parent = newCharacter.transform;
                            break;
                        }
                        if(hitCollider.CompareTag("Tower2") && hitCollider.gameObject != draggedTower && draggedTower.CompareTag("Tower2"))
                        {
                            // �浹�� Ÿ���� �巡�� ���� Ÿ���� ����
                            Destroy(hitCollider.gameObject);
                            Destroy(draggedTower);
                            int randomCharacterIndex = Random.Range(0, characterPrefabs.Length);
                            GameObject newCharacter = Instantiate(characterPrefabs[randomCharacterIndex], hitCollider.gameObject.transform.position, Quaternion.identity);
                            GameObject newAuraParticle = Instantiate(auraParticle, hitCollider.gameObject.transform.position, Quaternion.identity);
                            newCharacter.tag = "Tower3";
                            newAuraParticle.transform.parent = newCharacter.transform;
                            break;
                        }
                    }
                    // �ʱ� ��ġ�� �̵�
                    draggedTower.transform.position = new Vector3(initialTowerPosition.x, initialTowerPosition.y, 0);
                    isDragging = false;
                    towerCharacter.DragIn = true;  
                }
            }
        }
    }
}

