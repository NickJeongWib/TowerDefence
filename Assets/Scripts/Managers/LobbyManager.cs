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
            // ��������ȭ���� �ݱ� ��ư Ŭ�� ��
            if (obj.gameObject.name == "DungeonSelect_Close")
            {
                // ���� ����ȭ���� ��ũ�� value�� 0���� �ʱ�ȭ ���ְ� ������Ʈ ��Ȱ��ȭ.
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
