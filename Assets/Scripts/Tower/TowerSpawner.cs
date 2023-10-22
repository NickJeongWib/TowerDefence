using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class TowerSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject towerPrefab;
        [SerializeField]
        private EnemySpawner enemySpawner; // ���� �ʿ� �����ϴ� �� ����Ʈ ������ ��� ����

        public void SpawnTower(Transform tileTransform)
        {
            Tile tile = tileTransform.GetComponent<Tile>();
            
            // Ÿ�� �Ǽ� ���� ���� Ȯ��
            // ������ Ÿ���� ��ġ�� Ÿ�� �Ǽ�
            if(tile.IsBuildTower == true)
            {
                return;
            }

            // Ÿ���� �Ǽ��Ǿ� �������� ����
            tile.IsBuildTower = true;
            // ������ Ÿ���� ��ġ�� Ÿ�� �Ǽ�
            GameObject clone = Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);
            // Ÿ�� ���⿡ enemySpawner ���� ����
            clone.GetComponent<TowerWeapon>().Setup(enemySpawner);
        }
    }
}