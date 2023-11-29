using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    [System.Serializable]
    public struct SpawnPoints
    {
        public Tile[] allTiles; // 모든 타일 배열
    }
    public class TowerSpawner : MonoBehaviour
    {
        public GameObject[] characterPrefab; // 캐릭터 프리팹
        [SerializeField] 
        public SpawnPoints[] spawnPoints;

        private void Start()
        {
            for (int i = 0; i < GameManager.GMInstance.gameDataManagerRef.Equip_Char.Length; i++)
            {
                characterPrefab[i] = GameManager.GMInstance.gameDataManagerRef.Equip_Char[i];
            }
        }

        public void SpawnCharacter()
        {
            // 비어 있는 타일 목록을 저장할 리스트 생성
            //List<Tile> emptyTiles = new List<Tile>();
            //foreach (Tile tile in allTiles)
            //{
            //    // 칸이 비어있다면
            //    if (!tile.isOccupied)
            //    {
            //        // 빈 타일 리스트에 타일추가
            //        emptyTiles.Add(tile);
            //    }
            //}

            while(true)
            {
                int randomIndex = Random.Range(0, spawnPoints[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].allTiles.Length);

                if (spawnPoints[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].allTiles[randomIndex].isOccupied == false)
                {
                    int randomCharacterIndex = Random.Range(0, characterPrefab.Length);

                    // 선택한 타일이 비어 있을 때만 캐릭터를 소환
                    GameObject newCharacter = Instantiate(characterPrefab[randomCharacterIndex], 
                        spawnPoints[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].allTiles[randomIndex].transform.position, Quaternion.identity);
                    spawnPoints[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].allTiles[randomIndex].isOccupied = true;
                    spawnPoints[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].allTiles[randomIndex].spawnchar = newCharacter;
                    newCharacter.transform.parent = spawnPoints[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].allTiles[randomIndex].transform;
                    break;
                }
            }

            // 비어 있는 타일이 있는지 확인
            //if (emptyTiles.Count > 0)
            //{
            //    // 랜덤한 비어 있는 타일 선택
            //    int randomTileIndex = Random.Range(0, emptyTiles.Count);
            //    Tile selectedTile = emptyTiles[randomTileIndex];

            //    int randomCharacterIndex = Random.Range(0, characterPrefab.Length);

            //    // 선택한 타일이 비어 있을 때만 캐릭터를 소환
            //    GameObject newCharacter = Instantiate(characterPrefab[randomCharacterIndex], selectedTile.transform.position, Quaternion.identity);

            //    selectedTile.spawnchar = characterPrefab[randomCharacterIndex];
            //    selectedTile.isOccupied = true;
            //}
        }
    }
}