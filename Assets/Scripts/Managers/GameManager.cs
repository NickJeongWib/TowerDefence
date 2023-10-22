using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{ 
    public class GameManager : MonoBehaviour
    {
        // ManagerRef
        [SerializeField]
        TitleSceneManager TitleSceneManagerRef;
        public TitleSceneManager titleManagerRef { get { return TitleSceneManagerRef; } set { TitleSceneManagerRef = value; } }

        [SerializeField]
        LobbyManager LobbyManagerRef;
        public LobbyManager lobbyManagerRef { get { return LobbyManagerRef; } set { LobbyManagerRef = value; } }


        [SerializeField]
        GameDataManager GameDataManagerRef;
        public GameDataManager gameDataManagerRef { get { return GameDataManagerRef; } set { GameDataManagerRef = value; } }


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