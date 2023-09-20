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

        // Update is called once per frame
        void Update()
        {

        }

        public void OnClickLobby()
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}