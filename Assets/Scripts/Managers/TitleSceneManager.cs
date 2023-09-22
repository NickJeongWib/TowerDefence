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
            // 게임 시작 버튼 클릭 시 화면 이동
            if (obj.gameObject.name == "Go_Lobby_Btn")
            {
                SceneManager.LoadScene("Lobby");
            }
        }
    }
}