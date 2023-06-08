using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ConnectButton : MonoBehaviour
{
    [SerializeField] private Transform m_connectionPanel;
    [SerializeField] private Transform m_emoteWheelButton;
    [SerializeField] private TMP_InputField m_userNameInputField;
    public enum STATUS
    {
        HOST,
        SERVER,
        CLIENT
    }

    public STATUS m_status;

    public void OnClick()
    {
        ConnectionInfo.UserName = m_userNameInputField.text;
        
        switch (m_status)
        {
            case STATUS.HOST:
                NetworkManager.Singleton.StartHost();
                break;
            case STATUS.SERVER:
                NetworkManager.Singleton.StartServer();
                break;
            case STATUS.CLIENT:
                NetworkManager.Singleton.StartClient();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        m_connectionPanel.gameObject.SetActive(false);
        m_emoteWheelButton.gameObject.SetActive(true);
        
    }
}

public static class ConnectionInfo
{
    public static string UserName;
}
