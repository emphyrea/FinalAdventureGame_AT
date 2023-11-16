using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "DialogueSegment")]
public class DialogueSegment : ScriptableObject
{
    public List<string> dialogue = new List<string>();
    public Subject speaker;
}
