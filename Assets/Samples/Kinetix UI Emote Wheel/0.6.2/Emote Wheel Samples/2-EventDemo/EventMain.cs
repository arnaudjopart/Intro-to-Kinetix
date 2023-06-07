using System.Linq;
using Kinetix;
using Kinetix.UI;
using UnityEngine;

public class EventMain : MonoBehaviour
{
    [SerializeField] private Animator localPlayerAnimator;

    private void Awake()
    {
        KinetixCore.OnInitialized += OnKinetixInitialized;
        KinetixCore.Initialize(new KinetixCoreConfiguration()
        { 
            PlayAutomaticallyAnimationOnAnimators = true,
            ShowLogs                              = true
        });
    }

    private void OnDestroy()
    {
        KinetixCore.OnInitialized -= OnKinetixInitialized;
    }

    private void OnKinetixInitialized()
    {
        KinetixUIEmoteWheel.Initialize();
        
        //Core Events
        KinetixCore.Animation.OnPlayedAnimationLocalPlayer += OnPlayedAnimationOnLocalPlayer;
        KinetixCore.Animation.OnAnimationStartOnLocalPlayerAnimator += OnAnimationStartOnLocalPlayerAnimator;
        KinetixCore.Animation.OnAnimationEndOnLocalPlayerAnimator   += OnAnimationEndOnLocalPlayerAnimator;

        //UI Events
        KinetixUI.OnPlayedAnimationWithEmoteSelector += OnPlayedAnimationWithEmoteSelector;
        
        //local
        KinetixCore.Animation.RegisterLocalPlayerAnimator(localPlayerAnimator);
    }


    private void OnPlayedAnimationOnLocalPlayer(AnimationIds _AnimationIds)
    {
        Debug.Log("###EVENT### Played Animation : " + _AnimationIds.UUID);
    }

    private void OnAnimationStartOnLocalPlayerAnimator(AnimationIds _AnimationIds)
    {
        Debug.Log("###EVENT### Animation Started : " + _AnimationIds.UUID);        
    }
    
    private void OnAnimationEndOnLocalPlayerAnimator(AnimationIds _AnimationIds)
    {
        Debug.Log("###EVENT### Animation Ended : " + _AnimationIds.UUID);
    }

    private void OnPlayedAnimationWithEmoteSelector(AnimationIds _AnimationIds)
    {
        Debug.Log("###EVENT### Animation Played With Emote Selector : " + _AnimationIds.UUID);
    }
}
