using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private int maxHealth = 3;  // �ִ� ü��
        private int currentHealth;  // ���� ü��
        IngameManager ingameManager;

        public Image[] heartImages = null;  // ��Ʈ �̹��� �迭

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
                    heartImages[i].gameObject.SetActive(true);  // ü���� �ִ� ��� �̹��� Ȱ��ȭ
                }
                else
                {
                    heartImages[i].gameObject.SetActive(false); // ü���� ���� ��� �̹��� ��Ȱ��ȭ
                }
            }
        }




    }
}
