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
            List<Contact> contacts = new List<Contact>();
            Contact myProfile = new Contact();
            if (result.Data.ContainsKey("myContacts"))
            {
                contacts = PlayFabSimpleJson.DeserializeObject<List<Contact>>(result.Data["myContacts"].Value);
                Debug.Log("Getting my contacts data");
            }
            if (result.Data.ContainsKey("myProfile"))
            {
                myProfile = PlayFabSimpleJson.DeserializeObject<Contact>(result.Data["myProfile"].Value);
                Debug.Log("Getting my profile data");
            }
            UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
            ContactsCatalogManager.Instance.Init(contacts, myProfile);
        }, OnPlayFabError);
    }

    public void SaveMyProfile() 
    {
        string mySerializedProfile = PlayFabSimpleJson.SerializeObject(ContactsCatalogManager.Instance.m_MyProfile);

        UpdateUserDataRequest request = new UpdateUserDataRequest();
        request.Data = new Dictionary<string, string>() {
            {"myProfile", mySerializedProfile} };

        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log("Successfully updated my profile"),
            OnPlayFabError);
    }
    public void SaveNewContacts()
    {
        string serializedList = PlayFabSimpleJson.SerializeObject(ContactsCatalogManager.Instance.m_MyContacts);

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
