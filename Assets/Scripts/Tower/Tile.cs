using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class Tile : MonoBehaviour
    {
        public bool isOccupied = false; // �ش� Ÿ���� ��� ������ ����

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Tower"))
            {
                isOccupied = true;
            }
            if (collision.CompareTag("Tower2"))
            {
                isOccupied = true;
            }
            if (collision.CompareTag("Tower3"))
            {
                isOccupied = true;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Tower"))
            {
                isOccupied = true;
            }
            if (collision.CompareTag("Tower2"))
            {
                isOccupied = true;
            }
            if (collision.CompareTag("Tower3"))
            {
                isOccupied = true;
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Tower"))
            {
                isOccupied = false;
            }
            if (collision.CompareTag("Tower2"))
            {
                isOccupied = false;
            }
            if (collision.CompareTag("Tower3"))
            {
                isOccupied = false;
            }
        }
    }
}


