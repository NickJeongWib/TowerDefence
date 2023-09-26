using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class TowerSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject towerPrefab;

        public void SpawnTower(Transform tileTransform)
        {
            // ������ Ÿ���� ��ġ�� Ÿ�� �Ǽ�
            Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);
        }
    }
}
