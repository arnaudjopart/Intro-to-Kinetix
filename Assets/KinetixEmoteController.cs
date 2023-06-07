using System;
using System.Collections;
using System.Collections.Generic;
using Kinetix;
using Kinetix.UI;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.InputSystem;

public class KinetixEmoteController : MonoBehaviour
{
    private void Awake()
    {
        KinetixCore.OnInitialized += ConnectPlayer;
    }

    private void ConnectPlayer()
    {
        KinetixCore.Account.OnConnectedAccount += InitPlayer;
        KinetixCore.Account.ConnectAccount("TestUser");
    }

    private void InitPlayer()
    {
        KinetixCore.Animation.RegisterLocalPlayerAnimator(GetComponent<Animator>());
        KinetixUIEmoteWheel.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.f1Key.wasPressedThisFrame)
        {
            KinetixCore.Account.AssociateEmotesToUser(new AnimationIds("3a35852d-2bfa-4bc3-a06d-015165facd74"),OnAddEmoteSuccess);
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            KinetixCore.Metadata.GetUserAnimationMetadatas(OnSuccess,OnFailure);
        }
    }

    private void OnFailure()
    {
        Debug.Log("Error");
    }

    private void OnSuccess(AnimationMetadata[] _obj)
    {
        Debug.Log("Playing Emote");
        KinetixCore.Animation.PlayAnimationOnLocalPlayer(_obj[0].Ids);
    }

    private void OnAddEmoteSuccess(bool _status)
    {
        Debug.Log(_status ? "Emote added with success" : "Error while associating emote with user");
    }

    private void OnDestroy()
    {
        KinetixCore.OnInitialized -= ConnectPlayer;
    }
}
