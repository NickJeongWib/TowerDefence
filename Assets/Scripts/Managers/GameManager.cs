using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{ 
    public class GameManager : MonoBehaviour
    {
        // ManagerRef
        public TitleSceneManager TitleSceneManagerRef;

        int Test_1;

        // �̱���
        public static GameManager GMInstance;


        // Managers Reference
        public SoundManager SoundManagerRef;

        void Awake()
        {
            /** GMInstance�� �� Ŭ������ �ǹ��Ѵ�. */
            GMInstance = this;

            /** ȭ���� �ٲ��� Ŭ���� ���� */
            DontDestroyOnLoad(gameObject);
        }
    }
}