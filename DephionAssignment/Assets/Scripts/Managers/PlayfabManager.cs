using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.Json;

public class PlayfabManager : MonoBehaviour
{
    static PlayfabManager instance;
    public static PlayfabManager Instance { get { return instance; } }
    private string m_TitleId = "E469A";
    private string m_PlayFabID = "";
    public DateTime ServerTime
    {
        get; private set;
    }

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayFabSettings.TitleId = m_TitleId;
        Login();
    }

    private void Login()
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
    private void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
    {

        m_PlayFabID = result.PlayFabId;
        Debug.Log("PlayFabID: " + m_PlayFabID);
        GetMyData();
    }

    private void GetMyData() 
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = m_PlayFabID
        }, result =>
        {
            if (result.Data == null) 
            { 
                            
            }

            if (result.Data.ContainsKey("myContacts"))
            {
                List<Contact> myContacts = PlayFabSimpleJson.DeserializeObject<List<Contact>>(result.Data["myContacts"].Value);
                UIManager.Instance.m_ContactsUIController.myContacts = myContacts;
                UIManager.Instance.m_ContactsUIController.Test();
                //GameManager.Instance.InitializeGame();
            }
            else
            {
                //GameManager.Instance.InitializeGame();
            }

        }, OnPlayFabError);
    }

    public void SaveNewContacts(List<Contact> newContacts)
    {
        string serializedList = PlayFabSimpleJson.SerializeObject(newContacts);

        UpdateUserDataRequest request = new UpdateUserDataRequest();
        request.Data = new Dictionary<string, string>() {
            {"myContacts", serializedList} };

        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log("Successfully updated user data"),
            OnPlayFabError);
    }

    public void OnPlayFabError(PlayFabError error)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Debug.Log("ON PLAYFAB ERROR  " + error.ErrorMessage);
#endif
        //ShowConnectionError();
    }
}
