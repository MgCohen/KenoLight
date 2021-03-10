using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Animation")]
public class AnimationAsset : ScriptableObject
{
    public string animationName;
    public List<Sprite> frames;
    public AnimationAsset sequence;
    public animSequence sequenceType;
    public bool waitForEnd;
}

public enum animSequence
{
    Loop,
    Stop,
    Reset,
    Sequence,
}
