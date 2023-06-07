using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kinetix.UI;

public class ButtonShowEmoteWheel : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (KinetixUI.IsShown)
            KinetixUI.HideAll();
        else
            KinetixUI.Show();
    }
}
