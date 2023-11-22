using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private int maxHealth = 3;  // 최대 체력
        private int currentHealth;  // 현재 체력
        IngameManager ingameManager;

        public Image[] heartImages = null;  // 하트 이미지 배열

        public void Start()
        {
            currentHealth = maxHealth;
            ingameManager = FindObjectOfType<IngameManager>();
        }

        public void TakeDamage()
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                ingameManager.GameOver();
            }
            UpdateHealthUI();
        }

        public void UpdateHealthUI()
        {
            for (int i = 0; i < heartImages.Length; i++)
            {
                if (i < currentHealth)
                {
                    heartImages[i].gameObject.SetActive(true);  // 체력이 있는 경우 이미지 활성화
                }
                else
                {
                    heartImages[i].gameObject.SetActive(false); // 체력이 없는 경우 이미지 비활성화
                }
            }
        }




    }
}
