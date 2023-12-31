using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class TowerDrag : MonoBehaviour
    {
        public GameObject[] characterPrefabs;
        private GameObject draggedTower; // 드래그 중인 타워 오브젝트
        private Vector3 initialTowerPosition; // 타워의 초기 위치
        private Vector3 initialMouseOffset; // 드래그 시작 시 마우스와 타워 위치 간의 오프셋
        public bool isDragging = false;
        TowerCharacter towerCharacter;
        public GameObject aura;
        public GameObject auraParticle;
        public GameObject mixEffect;

        private void Start()
        {
            towerCharacter = FindObjectOfType<TowerCharacter>();
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
                // 클릭한 위치에 타워가 있다면
                if (hit.collider != null && hit.collider.CompareTag("Tower"))
                {
                    // 드래그 시작
                    draggedTower = hit.collider.gameObject;
                    initialMouseOffset = draggedTower.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    initialTowerPosition = draggedTower.transform.position; // 초기 위치 기억
                    isDragging = true;
                    hit.collider.gameObject.GetComponent<TowerCharacter>().DragIn = false;
                }
                if (hit.collider != null && hit.collider.CompareTag("Tower2"))
                {
                    // 드래그 시작
                    draggedTower = hit.collider.gameObject;
                    initialMouseOffset = draggedTower.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    initialTowerPosition = draggedTower.transform.position; // 초기 위치 기억
                    isDragging = true;
                    hit.collider.gameObject.GetComponent<TowerCharacter>().DragIn = false;
                }
            }
            // 드래그 중인 타워가 있을 때
            if (isDragging && draggedTower != null)
            {
                // 드래그 중인 타워를 마우스 위치로 이동
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                draggedTower.transform.position = new Vector3(mousePosition.x, mousePosition.y, draggedTower.transform.position.z) + new Vector3(initialMouseOffset.x, initialMouseOffset.y, 0);

                // 왼쪽 버튼을 땟을 때
                if (Input.GetMouseButtonUp(0))
                {
                    // 충돌 체크
                    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(draggedTower.transform.position, 0.1f);
                    foreach (Collider2D hitCollider in hitColliders)
                    {
                        if (hitCollider.CompareTag("Tower") && hitCollider.gameObject != draggedTower && draggedTower.CompareTag("Tower")
                             && draggedTower.GetComponent<TowerCharacter>().characterinfo.Charactertype == hitCollider.GetComponent<TowerCharacter>().characterinfo.Charactertype)
                        {

                            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Fusion_Sound);

                            int randomCharacterIndex = Random.Range(0, characterPrefabs.Length);
                            GameObject newCharacter = Instantiate(characterPrefabs[randomCharacterIndex], hitCollider.gameObject.transform.position, Quaternion.identity);
                            GameObject newAura = Instantiate(aura, hitCollider.gameObject.transform.position, Quaternion.identity);
                            GameObject newMixEffect = Instantiate(mixEffect, hitCollider.gameObject.transform.position, Quaternion.identity);
                            newCharacter.tag = "Tower2";
                            newAura.transform.parent = newCharacter.transform;

                            newCharacter.transform.parent = hitCollider.gameObject.transform.parent;
                            newCharacter.transform.parent.GetComponent<Tile>().spawnchar = newCharacter;
                            newCharacter.transform.parent.GetComponent<Tile>().isOccupied = true;

                            // 충돌한 타워와 드래그 중인 타워를 삭제
                            Destroy(hitCollider.gameObject);
                            Destroy(draggedTower);
                            break;
                        }
                        if(hitCollider.CompareTag("Tower2") && hitCollider.gameObject != draggedTower && draggedTower.CompareTag("Tower2")
                            && draggedTower.GetComponent<TowerCharacter>().characterinfo.Charactertype == hitCollider.GetComponent<TowerCharacter>().characterinfo.Charactertype)
                        {
                            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Fusion_Sound);

                            int randomCharacterIndex = Random.Range(0, characterPrefabs.Length);
                            GameObject newCharacter = Instantiate(characterPrefabs[randomCharacterIndex], hitCollider.gameObject.transform.position, Quaternion.identity);
                            GameObject newAuraParticle = Instantiate(auraParticle, hitCollider.gameObject.transform.position, Quaternion.identity);
                            GameObject newMixEffect = Instantiate(mixEffect, hitCollider.gameObject.transform.position, Quaternion.identity);
                            newCharacter.tag = "Tower3";
                            newAuraParticle.transform.parent = newCharacter.transform;

                            newCharacter.transform.parent = hitCollider.gameObject.transform.parent;
                            newCharacter.transform.parent.GetComponent<Tile>().spawnchar = newCharacter;
                            newCharacter.transform.parent.GetComponent<Tile>().isOccupied = true;

                            // 충돌한 타워와 드래그 중인 타워를 삭제
                            Destroy(hitCollider.gameObject);
                            Destroy(draggedTower);
                            break;
                        }
                    }
                    // 초기 위치로 이동
                    draggedTower.transform.position = new Vector3(initialTowerPosition.x, initialTowerPosition.y, 0);
                    isDragging = false;
                    towerCharacter.DragIn = true;  
                }
            }
        }
    }
}

