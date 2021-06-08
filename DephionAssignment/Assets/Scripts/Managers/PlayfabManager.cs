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

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayFabSettings.TitleId = m_TitleId;
        Login();
    }
    /// <summary>
    /// Connects to the Playfab service
    /// </summary>
    private void Login()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var request = new LoginWithAndroidDeviceIDRequest
        {
            AndroidDevice = SystemInfo.deviceModel,
            AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
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
        GetTitleData();
    }
    /// <summary>
    /// Gets the title data stored on the server and sets it for use in the game/app
    /// </summary>
    private void GetTitleData() 
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
        result =>
        {
            List<string> availablePictures;
            if (result.Data != null) 
            {
                if (result.Data.ContainsKey("pictures"))
                {
                    availablePictures = PlayFabSimpleJson.DeserializeObject<List<string>>(result.Data["pictures"]);
                    ContactsCatalogManager.Instance.SetAvailablePictures(availablePictures);
                }
                GetPlayerData();
            }
        }, 
        OnPlayFabError);
    }
    /// <summary>
    /// Gets the data of the currently logged player/user and sets it for use in the game
    /// </summary>
    private void GetPlayerData() 
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
            }
            if (result.Data.ContainsKey("myProfile"))
            {
                myProfile = PlayFabSimpleJson.DeserializeObject<Contact>(result.Data["myProfile"].Value);
            }
            ContactsCatalogManager.Instance.Init(contacts, myProfile);
            DoneLoading();
        }, OnPlayFabError);
    }
    /// <summary>
    /// Marks the game/app as loaded and starts it
    /// </summary>
    public void DoneLoading()
    {
        UIManager.Instance.SetLoadingScreen(false);
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_AllContactsController);
    }
    /// <summary>
    /// Saves my new profile data to the server
    /// </summary>
    public void SaveMyProfile() 
    {
        string mySerializedProfile = PlayFabSimpleJson.SerializeObject(ContactsCatalogManager.Instance.m_MyProfile);

        UpdateUserDataRequest request = new UpdateUserDataRequest();
        request.Data = new Dictionary<string, string>() {
            {"myProfile", mySerializedProfile} };

        PlayFabClientAPI.UpdateUserData(request,
            result => { },
            OnPlayFabError);
    }
    /// <summary>
    /// Saves the new contacts of mine to the server

    /// </summary>
    public void SaveNewContacts()
    {
        string serializedList = PlayFabSimpleJson.SerializeObject(ContactsCatalogManager.Instance.m_MyContacts);

        UpdateUserDataRequest request = new UpdateUserDataRequest();
        request.Data = new Dictionary<string, string>() {
            {"myContacts", serializedList} };

        PlayFabClientAPI.UpdateUserData(request,
            result => { },
            OnPlayFabError);
    }

    /// <summary>
    /// Failed call or to the server, stops the app and prompts a restart. Far better handling could be done here (retrying, saving locally, etc.).
    /// </summary>
    /// <param name="error"></param>
    public void OnPlayFabError(PlayFabError error)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Debug.Log("ON PLAYFAB ERROR  " + error.ErrorMessage);
#endif
        UIManager.Instance.SetNoConnectionError();
    }
}
