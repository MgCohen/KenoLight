using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public class CustomAnim : MonoBehaviour
{
    public Image image;
    AnimationAsset pendingAnim;
    AnimationAsset currentAnimation;

    public bool playOnAwake = false;
    public bool playing = false;
    public float frameDelay;

    float timer = 0;
    int currentFrame;

    UnityAction callBack;

    public AnimationAsset defaultAnim;
    public List<AnimationAsset> anims = new List<AnimationAsset>();
    
    private void OnEnable()
    {
        if(!defaultAnim) defaultAnim = anims[0];
        if (playOnAwake) Play();
    }

    void Play()
    {
        playing = true;
        currentAnimation = defaultAnim;
    }

    public void Play(string animationName, UnityAction OnComplete = null)
    {
        //Get Anim by nane
        var anim = anims.Find(x => x.animationName == animationName);
        if (anim)
        {
            //trigger animation
            playing = true;
            if (currentAnimation.waitForEnd) pendingAnim = anim;
            else
            {
                currentAnimation = anim;
                currentFrame = 0;
                timer = 0;
            }
            callBack = OnComplete;
        }
    }

    public void Resume()
    {
        playing = true;
    }

    public void Stop()
    {
        playing = false;
    }

    private void Update()
    {
        if (playing)
        {
            timer += Time.deltaTime;
            if (timer >= frameDelay)
            {
                timer = 0;
                currentFrame++;

                //verify animationEnd
                if (currentFrame >= currentAnimation.frames.Count)
                {
                    //invoke OnComplete Callback
                    if (callBack != null)
                    {
                        callBack.Invoke();
                        callBack = null;
                    }

                    //check for pending animation
                    if (pendingAnim)
                    {
                        currentFrame = 0;
                        currentAnimation = pendingAnim;
                        pendingAnim = null;
                        image.sprite = currentAnimation.frames[currentFrame];
                        return;
                    }

                    //check for animation sequence
                    if (currentAnimation.sequenceType == animSequence.Loop) currentFrame = 0;
                    else if (currentAnimation.sequenceType == animSequence.Stop)
                    {
                        playing = false;
                        return;
                    }
                    else if (currentAnimation.sequenceType == animSequence.Reset)
                    {
                        currentFrame = 0;
                        currentAnimation = defaultAnim;
                    }
                    else if (currentAnimation.sequenceType == animSequence.Sequence)
                    {
                        currentFrame = 0;
                        currentAnimation = currentAnimation.sequence ? currentAnimation.sequence : defaultAnim;
                    }
                }

                //set next image on loop
                image.sprite = currentAnimation.frames[currentFrame];

            }
        }
    }
}



