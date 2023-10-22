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
            //효과음 실행
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Btn_Select);

            // 게임 시작 버튼 클릭 시 화면 이동
            if (obj.gameObject.name == "Go_Lobby_Btn")
            {
                SceneManager.LoadScene("Lobby");
            }
        }
    }
}