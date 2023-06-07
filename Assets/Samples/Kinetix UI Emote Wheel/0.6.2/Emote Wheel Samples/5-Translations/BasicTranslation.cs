// // ----------------------------------------------------------------------------
// // <copyright file="BasicMain.cs" company="Kinetix SAS">
// // Kinetix Unity SDK - Copyright (C) 2022 Kinetix SAS
// // </copyright>
// // ----------------------------------------------------------------------------

using UnityEngine;

namespace Kinetix.UI.EmoteWheel
{
    public class BasicTranslation : MonoBehaviour
    {
        [SerializeField] private Animator               localPlayerAnimator;

        private void Awake()
        {
            KinetixCore.OnInitialized += OnKinetixInitialized;
            KinetixCore.Initialize(new KinetixCoreConfiguration()
            {
                PlayAutomaticallyAnimationOnAnimators = true,
                ShowLogs = true,
                EnableAnalytics = true
            });
        }

        private void OnDestroy()
        {
            KinetixCore.OnInitialized -= OnKinetixInitialized;
        }

        private void OnKinetixInitialized()
        {            
            KinetixUIEmoteWheel.Initialize(new KinetixUIEmoteWheelConfiguration() {
                baseLanguage = SystemLanguage.English
            });

            KinetixCore.Animation.RegisterLocalPlayerAnimator(localPlayerAnimator);
        }
    }
}
