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
    public class ThemeMain : MonoBehaviour
    {
        [SerializeField] private Animator               localPlayerAnimator;
        [SerializeField] private KinetixCustomTheme     kinetixCustomTheme;
        [SerializeField] private ECustomTheme           defaultConfig = ECustomTheme.DARK_MODE;

        [SerializeField] private TMP_Dropdown dropdown;
        private List<string> dropdownTheme;

        private void Awake()
        {
            KinetixCore.OnInitialized += OnKinetixInitialized;
            KinetixCore.Initialize(new KinetixCoreConfiguration()
            {
                PlayAutomaticallyAnimationOnAnimators   = true,
                ShowLogs                                = true,
                EnableAnalytics                         = true
            });
        }

        private void Start()
        {
            dropdownTheme = new List<string>();

            dropdownTheme.Add( ECustomTheme.LIGHT_MODE.ToString() );
            dropdownTheme.Add( ECustomTheme.DARK_MODE.ToString() );
            dropdownTheme.Add( "CUSTOM THEME" );

            dropdown.AddOptions(dropdownTheme);

            dropdown.value = dropdownTheme.IndexOf(defaultConfig.ToString());
        }

        public void OnThemeDropdownChanged()
        {
            if(dropdown.value < System.Enum.GetValues(typeof(ECustomTheme)).Length ) 
            {
                KinetixUIEmoteWheel.UpdateTheme( (ECustomTheme)dropdown.value );
            }            
            else if( dropdown.options[dropdown.value].text == "CUSTOM THEME")
            {
                KinetixUIEmoteWheel.UpdateThemeOverride( kinetixCustomTheme );
            }            
        }

        private void OnDestroy()
        {
            KinetixCore.OnInitialized -= OnKinetixInitialized;
        }

        private void OnKinetixInitialized()
        {            
            KinetixUIEmoteWheel.Initialize( new KinetixUIEmoteWheelConfiguration()
            {
                customThemeOverride     = null,
                customTheme             = defaultConfig,
                baseLanguage            = SystemLanguage.English
            });

            KinetixCore.Animation.RegisterLocalPlayerAnimator(localPlayerAnimator);
        }
    }
}
