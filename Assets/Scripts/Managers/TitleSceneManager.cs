using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace TowerDefence
{
    public class TitleSceneManager : MonoBehaviour
    {
        [SerializeField]
        Button GoLobby_Btn;

        // Start is called before the first frame update
        void Start()
        {
            GameManager.GMInstance.TitleSceneManagerRef = this;
        }


        public void OnClick_Btn(GameObject obj)
        {
            // ���� ���� ��ư Ŭ�� �� ȭ�� �̵�
            if (obj.gameObject.name == "Go_Lobby_Btn")
            {
                SceneManager.LoadScene("Lobby");
            }
        }
    }
}