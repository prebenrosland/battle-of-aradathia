using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public int questId;
    [TextArea(3, 10)]
    public List<string> sentences;
}
