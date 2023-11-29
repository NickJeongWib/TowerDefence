using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    [System.Serializable]
    public struct SpawnPoints
    {
        public Tile[] allTiles; // ��� Ÿ�� �迭
    }
    public class TowerSpawner : MonoBehaviour
    {
        public GameObject[] characterPrefab; // ĳ���� ������
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
            // ��� �ִ� Ÿ�� ����� ������ ����Ʈ ����
            //List<Tile> emptyTiles = new List<Tile>();
            //foreach (Tile tile in allTiles)
            //{
            //    // ĭ�� ����ִٸ�
            //    if (!tile.isOccupied)
            //    {
            //        // �� Ÿ�� ����Ʈ�� Ÿ���߰�
            //        emptyTiles.Add(tile);
            //    }
            //}

            while(true)
            {
                int randomIndex = Random.Range(0, spawnPoints[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].allTiles.Length);

                if (spawnPoints[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].allTiles[randomIndex].isOccupied == false)
                {
                    int randomCharacterIndex = Random.Range(0, characterPrefab.Length);

                    // ������ Ÿ���� ��� ���� ���� ĳ���͸� ��ȯ
                    GameObject newCharacter = Instantiate(characterPrefab[randomCharacterIndex], 
                        spawnPoints[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].allTiles[randomIndex].transform.position, Quaternion.identity);
                    spawnPoints[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].allTiles[randomIndex].isOccupied = true;
                    spawnPoints[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].allTiles[randomIndex].spawnchar = newCharacter;
                    newCharacter.transform.parent = spawnPoints[(int)GameManager.GMInstance.gameDataManagerRef.Stage_Lv].allTiles[randomIndex].transform;
                    break;
                }
            }

            // ��� �ִ� Ÿ���� �ִ��� Ȯ��
            //if (emptyTiles.Count > 0)
            //{
            //    // ������ ��� �ִ� Ÿ�� ����
            //    int randomTileIndex = Random.Range(0, emptyTiles.Count);
            //    Tile selectedTile = emptyTiles[randomTileIndex];

            //    int randomCharacterIndex = Random.Range(0, characterPrefab.Length);

            //    // ������ Ÿ���� ��� ���� ���� ĳ���͸� ��ȯ
            //    GameObject newCharacter = Instantiate(characterPrefab[randomCharacterIndex], selectedTile.transform.position, Quaternion.identity);

            //    selectedTile.spawnchar = characterPrefab[randomCharacterIndex];
            //    selectedTile.isOccupied = true;
            //}
        }
    }
}