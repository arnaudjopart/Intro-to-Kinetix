using System;
using Kinetix;
using Kinetix.UI;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class KinetixEmoteController : NetworkBehaviour
{

    private KinetixCharacterComponent m_kinetixCharacterComponent;
    private byte[] m_previousPose = Array.Empty<byte>();

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            ConnectPlayer();
            NetworkManager.Singleton.NetworkTickSystem.Tick += OnTick;
        }
        else
        {
            KinetixCore.Network.RegisterRemotePeerAnimator(NetworkObjectId.ToString(),GetComponent<Animator>());
            m_kinetixCharacterComponent = KinetixCore.Network.GetRemoteKCC(NetworkObjectId.ToString());
        }
        
    }

    private void OnTick()
    {
        if (m_kinetixCharacterComponent == null) return;
        SendEmoteOnNetwork();
    }

    private void SendEmoteOnNetwork()
    {
        var currentPose = m_kinetixCharacterComponent.GetSerializedPose();
        if (currentPose.Length > 0 || m_previousPose.Length > 0)
        {
            //Send data to server
            SendPoseDataToServerRpc(currentPose, NetworkObjectId);
        }
        m_previousPose = currentPose;
    }

    [ServerRpc]
    private void SendPoseDataToServerRpc(byte[] _currentPose, ulong _networkObjectId)
    {
        SendPoseDataToClientRpc(_currentPose, _networkObjectId);
    }

    [ClientRpc]
    private void SendPoseDataToClientRpc(byte[] _currentPose, ulong _networkObjectId)
    {
        Debug.Log("SendPoseDataToClientRpc from "+_networkObjectId);
        if (IsOwner) return;
        if (_networkObjectId != NetworkObjectId) return;
        var timer = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        m_kinetixCharacterComponent.ApplySerializedPose(_currentPose,timer);
    }

    private void ConnectPlayer()
    {
        Debug.Log("This is the local player");
        KinetixCore.Account.OnConnectedAccount += InitPlayer;
        KinetixCore.Account.ConnectAccount("TestUser");
    }

    private void InitPlayer()
    {
        KinetixCore.Animation.RegisterLocalPlayerAnimator(GetComponent<Animator>());
        m_kinetixCharacterComponent = KinetixCore.Animation.GetLocalKCC();
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
