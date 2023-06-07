// // ----------------------------------------------------------------------------
// // <copyright file="CustomMain.cs" company="Kinetix SAS">
// // Kinetix Unity SDK - Copyright (C) 2022 Kinetix SAS
// // </copyright>
// // ----------------------------------------------------------------------------

using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Kinetix.UI.EmoteWheel
{
    public class CustomInputManagerMain : MonoBehaviour
    {
        [SerializeField] private Animator               localPlayerAnimator;
        [SerializeField] private KinetixInputMapSO      kinetixCustomInputActionMap;    

        private void Awake()
        {
            KinetixCore.OnInitialized += OnKinetixInitialized;
            KinetixCore.Initialize(new KinetixCoreConfiguration()
            {
                PlayAutomaticallyAnimationOnAnimators   = true,
                ShowLogs                                = false,
                EnableAnalytics                         = false
            });
        }


        private void OnDestroy()
        {
            KinetixCore.OnInitialized -= OnKinetixInitialized;
        }

        private void OnKinetixInitialized()
        {            
            KinetixUIEmoteWheel.Initialize( new KinetixUIEmoteWheelConfiguration()
            {
                kinetixInputActionMap   = kinetixCustomInputActionMap
            });

            KinetixCore.Animation.RegisterLocalPlayerAnimator(localPlayerAnimator);
        }
    }
}
