using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My
{
    public class GameManager : MonoBehaviour
    {
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