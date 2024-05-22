using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField]
    private SkeletonAnimation skeletonAnimation;
    [SerializeField]
    private AnimationReferenceAsset move, stop, point,wonder; 
    public static string currentState;
    [SerializeField]
    private AudioClip move_sfx, stop_sfx, point_sfx,win_sfx;
    private void Start()
    {

    }

    public void PlayAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
       skeletonAnimation.state.SetAnimation(1,animation,loop).TimeScale = timeScale;
    }

    private void Update()
    {
    }

    public void SetCharacterState(string state)
    {
        switch (state)
        {
            case "Move":
                {
                    AudioManager.instance.playSFX(move_sfx);
                    PlayAnimation(move, true, 1);
                    break;
                }
            case "Point":
                {
                    AudioManager.instance.playSFX(point_sfx);
                    PlayAnimation(point, true, 1);
                    break;
                }
            case "Stop":
                {
                    PlayAnimation(stop, true, 1);
                    break;
                }
            case "Wonder":
                {
                    AudioManager.instance.playSFX(win_sfx);
                    PlayAnimation(wonder, false, 1);
                    break;
                }
        }
    }

}
