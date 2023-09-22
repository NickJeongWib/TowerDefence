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
            obj.transform.parent.gameObject.SetActive(false);
        }
    }
}
