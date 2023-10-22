using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class TowerSpawner : MonoBehaviour
    {
        public GameObject characterPrefab; // ĳ���� ������
        public Tile[] allTiles; // ��� Ÿ�� �迭

        public void SpawnCharacter()
        {
            // ��� �ִ� Ÿ�� ����� ������ ����Ʈ ����
            List<Tile> emptyTiles = new List<Tile>();
            foreach (Tile tile in allTiles)
            {
                if (!tile.isOccupied)
                {
                    emptyTiles.Add(tile);
                }
            }

            // ��� �ִ� Ÿ���� �ִ��� Ȯ��
            if (emptyTiles.Count > 0)
            {
                // ������ ��� �ִ� Ÿ�� ����
                int randomIndex = Random.Range(0, emptyTiles.Count);
                Tile selectedTile = emptyTiles[randomIndex];

                // ������ Ÿ���� ��� ���� ���� ĳ���͸� ��ȯ
                GameObject newCharacter = Instantiate(characterPrefab, selectedTile.transform.position, Quaternion.identity);
                selectedTile.isOccupied = true;
            }
        }
    }
}