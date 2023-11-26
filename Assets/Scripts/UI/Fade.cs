using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TowerDefence
{
    public class Fade : MonoBehaviour
    {
        Image Black_screen;
        public float Fade_Time = 2f;
        public float Fade_Max = 1f;
        float _time;
        public bool FadeIn_ing = true;
        public bool FadeOut_ing;

        void Start()
        {
            Black_screen = GetComponent<Image>();
        }

        void Update()
        {
            /** ���̵� �� ���̶�� */
            if (FadeIn_ing)
            {
                /** _time�� ƽ�� �ö� */
                _time += Time.deltaTime;
                /** Black_screen.color�� ���� ȭ�鿡�� ����� ȭ������ Fade_Time�ð� ��ŭ ��ȯ */
                Black_screen.color = Color.Lerp(new Color(0, 0, 0, Fade_Max), new Color(0, 0, 0, 0), _time / Fade_Time);
            }

            /** ���̵� �ƿ� ���̶�� */
            if (FadeOut_ing)
            {
                /** _time�� ƽ�� �ö� */
                _time += Time.deltaTime;
                /** Black_screen.color�� ����� ȭ�鿡�� ���� ȭ�� Fade_Time�ð� ��ŭ ��ȯ */
                Black_screen.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, Fade_Max), _time / Fade_Time);
            }

            /** ���� _time �ð��� Fade_Time���� ũ�ٸ� ȭ�� ��ȯ�� �����ٸ� */
            if (_time >= Fade_Time)
            {
                /** _time = 0���� �ʱ�ȭ */
                _time = 0;

                /** ȭ����ȯ�� ������ ������ FadeOut_ing false */
                FadeOut_ing = false;

                /** FadeIn_ing�� true�� */
                if (FadeIn_ing == true)
                {
                    /** �� Ŭ���� ������ �ִ� ������Ʈ ���� */
                    Destroy(this.gameObject);
                }
            }
        }

        public void FadeIn()
        {
            FadeIn_ing = true;
        }

        public void FadeOut()
        {
            FadeOut_ing = true;
        }
    }
}