using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAsset : MonoBehaviour
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
