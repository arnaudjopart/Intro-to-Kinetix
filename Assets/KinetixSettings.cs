using System;
using Kinetix;
using UnityEngine;

public class KinetixSettings : MonoBehaviour
{
    private const string API_Key = "206ceb29c16de50a447f4e1771bf2990";

    private void Awake()
    {
        KinetixCore.OnInitialized += OnInit;
    }

    private void OnInit()
    {
        Debug.Log("OnInit");
    }

    // Start is called before the first frame update
    void Start()
    {
        KinetixCore.Initialize(new KinetixCoreConfiguration()
        {
            PlayAutomaticallyAnimationOnAnimators = true,
            EnableAnalytics = true,
            EnableUGC = false,
            VirtualWorldId = API_Key
        });
    }

    private void OnDestroy()
    {
        KinetixCore.OnInitialized -= OnInit;
    }
}
