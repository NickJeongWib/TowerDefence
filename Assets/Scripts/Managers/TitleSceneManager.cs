using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace TowerDefence
{
    public class TitleSceneManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GameManager.GMInstance.titleManagerRef = this;
        }

        public void OnClick_Btn(GameObject obj)
        {
            //ȿ���� ����
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Btn_Select);

            // ���� ���� ��ư Ŭ�� �� ȭ�� �̵�
            if (obj.gameObject.name == "Go_Lobby_Btn")
            {
                SceneManager.LoadScene("Lobby");
            }
        }
    }
}