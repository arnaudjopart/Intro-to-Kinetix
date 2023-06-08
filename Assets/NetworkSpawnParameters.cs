using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkSpawnParameters : NetworkBehaviour
{
    [SerializeField] private Material m_ownerMaterial;
    [SerializeField] private SkinnedMeshRenderer m_skinnedMeshRenderer;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner) m_skinnedMeshRenderer.material = m_ownerMaterial;
        if(IsHost && IsOwner) transform.position = Vector3.right*1.5f;
        if(IsHost && !IsOwner) transform.position = Vector3.right*-1.5f;
        if(!IsHost && IsOwner) transform.position = Vector3.right*-1.5f;
        if(!IsHost && !IsOwner) transform.position = Vector3.right*1.5f;
    }

    
}
