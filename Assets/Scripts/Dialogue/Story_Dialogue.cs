using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [System.Serializable]
    public class Story_Dialogue
    {
        [Tooltip("Index")]
        public int Index;

        [Tooltip("����")]
        public string[] contexts;
    }

    [System.Serializable]
    public class Story_DialogueEvent
    {
        public string name;

        public Vector2 Line;
        public Story_Dialogue[] dialogues;
    }
}