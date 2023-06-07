using System.Collections;
using System.Collections.Generic;
using Kinetix.UI;
using UnityEngine;

public class ShowWheelButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (KinetixUI.IsShown)
        {
            KinetixUI.HideAll();
            return;
        }
        KinetixUI.Show();
    }
}
