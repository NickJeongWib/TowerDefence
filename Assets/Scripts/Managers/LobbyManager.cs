using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class LobbyManager : MonoBehaviour
    {
        #region Var

        #endregion
        [SerializeField]
        ScrollManager ScrollManagerRef;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void OnClickOpenPopUp_Btn(GameObject obj)
        {
            obj.gameObject.SetActive(true);
        }

        public void OnClickClosePopUp_Btn(GameObject obj)
        {
            // 던전선택화면의 닫기 버튼 클릭 시
            if (obj.gameObject.name == "DungeonSelect_Close")
            {
                // 던전 선택화면의 스크롤 value를 0으로 초기화 해주고 오브젝트 비활성화.
                obj.transform.parent.GetChild(1).GetComponent<Scrollbar>().value = 0;
                obj.transform.parent.gameObject.SetActive(false);

                ScrollManagerRef.targetPos = 0.0f;
                ScrollManagerRef.curPos = 0.0f;
                return;
            }

            obj.transform.parent.gameObject.SetActive(false);
        }
    }
}
