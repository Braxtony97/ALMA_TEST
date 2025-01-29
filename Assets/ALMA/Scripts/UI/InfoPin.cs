using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PopupData
{
    public string Id;
    public string Title;
    public string Description;
    public Vector2 Position;
}

public class InfoPin : MonoBehaviour
{
    public string Id;
}
