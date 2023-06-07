using System.Linq;
using Kinetix;
using Kinetix.UI;
using UnityEngine;

public class LegacyMain : MonoBehaviour
{
    
    [SerializeField] private GameObject character;
    [SerializeField] private Avatar     avatar;
    [SerializeField] private Transform  rootTransform;
    

    private Animation animationComponent;

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
        
        // EVENTS UI
        KinetixUI.OnPlayedAnimationWithEmoteSelector       += OnLocalPlayedAnimation;

        KinetixCore.Animation.RegisterLocalPlayerCustom(avatar, rootTransform, EExportType.AnimationClipLegacy);
    }

    private void OnLocalPlayedAnimation(AnimationIds _AnimationIds)
    {
        KinetixCore.Animation.GetRetargetedAnimationClipLegacyForLocalPlayer(_AnimationIds, (animationClip) =>
        {
            if (animationComponent == null)
            {                   
                animationComponent                   = character.gameObject.AddComponent<Animation>();
                animationComponent.playAutomatically = false;
            }
                
            animationComponent.AddClip(animationClip, _AnimationIds.UUID);
            animationComponent.Play( _AnimationIds.UUID);
        });
    }
    
}
