using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class Dialogue_Parser : MonoBehaviour
    {
        public Story_Dialogue[] Parse(string _CSVFileName, LobbyManager lobbymanager)
        {
            List<Story_Dialogue> dialogueList = new List<Story_Dialogue>(); // 대사 리스트 생성

            TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

            string[] data = csvData.text.Split(new char[] { '\n' });

            for (int i = 1; i < data.Length; i++)
            {
                string[] row = data[i].Split(new char[] { ',' });

                if (row[0].ToString() == "")
                {
                    break;
                }

                lobbymanager.prologue_Text[i - 1] = row[1];

                Story_Dialogue dialogue = new Story_Dialogue();
                List<string> contextList = new List<string>();

                contextList.Add(row[1]);

                dialogue.contexts = contextList.ToArray();
                dialogueList.Add(dialogue);
            }

            return dialogueList.ToArray();
        }
    }
}
