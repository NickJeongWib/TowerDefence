using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class Equip_Character_Info : MonoBehaviour
    {
        public GameObject Equip_Character;

        void Start()
        {
            this.GetComponent<Image>().sprite = Equip_Character.GetComponent<SpriteRenderer>().sprite;
        }
    }
}