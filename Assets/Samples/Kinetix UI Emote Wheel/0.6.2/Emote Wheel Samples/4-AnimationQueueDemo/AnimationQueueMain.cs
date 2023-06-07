using System.Linq;
using Kinetix;
using Kinetix.UI;
using UnityEngine;

public class AnimationQueueMain : MonoBehaviour
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

        // EVENTS
        KinetixCore.Animation.OnPlayedAnimationQueueLocalPlayer     += OnPlayedAnimationQueueLocal;
        KinetixCore.Animation.OnAnimationStartOnLocalPlayerAnimator += OnAnimationStartOnLocalPlayerAnimator;
        KinetixCore.Animation.OnAnimationEndOnLocalPlayerAnimator   += OnAnimationEndOnLocalPlayerAnimator;

        KinetixCore.Animation.RegisterLocalPlayerAnimator(localPlayerAnimator);
        
        KinetixCore.Metadata.GetUserAnimationMetadatas(animations =>
        {
            AnimationIds[] animationIDs = animations.Select(metadata => metadata.Ids).Take(2).ToArray();
            KinetixCore.Animation.LoadLocalPlayerAnimations(animationIDs, () =>
            {
                KinetixCore.Animation.PlayAnimationQueueOnLocalPlayer(animationIDs, true);
            });
        });
    }

    private void OnPlayedAnimationQueueLocal(AnimationIds[] _AnimationIdsArray)
    {
        string animationStr = "";
        _AnimationIdsArray.ToList().ForEach(animationIds => animationStr += animationIds.UUID + "\n");
        Debug.Log("[LOCAL] Played Animation queue : \n" + animationStr);
    }

    private void OnAnimationStartOnLocalPlayerAnimator(AnimationIds _AnimationIds)
    {
        Debug.Log("[LOCAL] Animation Started : " + _AnimationIds.UUID);
    }
    
    private void OnAnimationEndOnLocalPlayerAnimator(AnimationIds _AnimationIds)
    {
        Debug.Log("[LOCAL] Animation Ended : " + _AnimationIds.UUID);
    }
}
