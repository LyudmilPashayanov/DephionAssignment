using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabManager : MonoBehaviour
{
    static PlayfabManager instance;
    public static PlayfabManager Instance { get { return instance; } }
    private string m_TitleId = "E469A";
    private string m_PlayFabID = "";
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayFabSettings.TitleId = m_TitleId;
        Login();
    }

    public void Login()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var request = new LoginWithAndroidDeviceIDRequest
        {
            AndroidDevice = SystemInfo.deviceModel,
            AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        Debug.Log(" Logging with android account!!!");
        PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLoginSuccess, OnPlayFabError);
#endif
#if UNITY_EDITOR
        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,

            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnPlayFabError);
#endif
    }
    protected void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
    {

        m_PlayFabID = result.PlayFabId;
        Debug.Log("PlayFabID: " + m_PlayFabID);
        //GetPlayerData();
    }


    public void OnPlayFabError(PlayFabError error)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Debug.Log("ON PLAYFAB ERROR  " + error.ErrorMessage);
#endif
        //ShowConnectionError();
    }
}
