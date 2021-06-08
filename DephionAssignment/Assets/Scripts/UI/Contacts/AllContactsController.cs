using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AllContactsController : IUIPage
{
    [SerializeField] private AllContactsView m_view;

    private List<Contact> ShowedContacts = new List<Contact>();
    public RectTransform m_ContactFieldPrefab;
    public PoolController m_ContactsScrollView;

    private void Start() // Sets the functionality of all buttons and subscribed to listen for contact changes. (prepares the UI page for use)
    {
        m_view.RemoveAllListeners();
        m_view.AddListeners(FilterContacts,SortContactsAlphabetically,SortContactsByCreationDate, GoToContactCreation, SaveMyName
            ,new UnityAction<string>((string c)=>{ m_view.SetButtonActive(false); }), GoToEditMyProfile);
        ContactsCatalogManager.Instance.onContactAdded += new Action(() => { UpdateListOfContacts(ContactsCatalogManager.Instance.m_MyContacts, true); });
        ContactsCatalogManager.Instance.onContactEdited += new Action(() => { UpdateListOfContacts(ContactsCatalogManager.Instance.m_MyContacts, false); });
        ContactsCatalogManager.Instance.onMyProfileEdited += UpdateMyProfile;
        ContactsCatalogManager.Instance.onContactDeleted += new Action(() => { UpdateListOfContacts(ContactsCatalogManager.Instance.m_MyContacts, true); }); ;
    }

    /// <summary>
    /// Sets the data received from the server on the UI Page.
    /// </summary>
    /// <param name="allContacts"></param>
    /// <param name="m_MyProfile"></param>
    public void InitializeMyContacts(List<Contact> allContacts, Contact m_MyProfile) 
    {
        ShowedContacts = allContacts;
        m_ContactsScrollView.Setup(GetMyContactsAlphabetically().ToList<IPoolData>(), m_ContactFieldPrefab);
        m_view.SetMyProfile(m_MyProfile);
    }

    /// <summary>
    /// Filters the contacts in the scroll view, depending on the parameter given.
    /// </summary>
    /// <param name="text"></param>
    public void FilterContacts(string text)
    {   
        List<Contact> filteredContacts = new List<Contact>();
        foreach (Contact contact in ContactsCatalogManager.Instance.m_MyContacts)
        {
            if (contact.FirstName.ToLower().Contains(text.ToLower())) 
            {
                filteredContacts.Add(contact);
            }
        }
        ShowedContacts = filteredContacts;
        m_ContactsScrollView.UpdatePooler(filteredContacts.ToList<IPoolData>(),false,m_ContactFieldPrefab);
        ChangeButtonToClear();
    }

    /// <summary>
    /// Updates the contact fields in the scroll view.
    /// </summary>
    /// <param name="list">The new list with which the scroll view will be updated</param>
    /// <param name="forceUpdate">True: Reload the whole scroll view, but more heavy on performance. Give true only if contact is added or removed.</param>
    public void UpdateListOfContacts(List<Contact> list, bool forceUpdate) 
    {
        ShowedContacts = list;
        m_ContactsScrollView.UpdatePooler(list.ToList<IPoolData>(), forceUpdate, m_ContactFieldPrefab);
    }

    public void ChangeButtonToClear() 
    {
        m_view.ChangeIconToClearImage();
        m_view.SetButtonActive(true);
        m_view.ChangeButtonToClear(ClearSearchBar);
    }

    public void ClearSearchBar() 
    {
        if (m_view.IsSearchBarEmpty())
            return;
        ShowedContacts = ContactsCatalogManager.Instance.m_MyContacts;
        m_ContactsScrollView.UpdatePooler(ShowedContacts.ToList<IPoolData>(), true, m_ContactFieldPrefab);
        m_view.ClearSearchBar();
        m_view.ChangeIconToMagnifierImage();
        m_view.ChangeButtonToSearch();
    }

    public void GoToContactCreation()
    {
        UIManager.Instance.m_CRUDContactController.CreateNewContact();
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_CRUDContactController);
    }

    public void GoToEditMyProfile()
    {
        UIManager.Instance.m_CRUDContactController.EditMyProfile();
        UIManager.Instance.GoToUIPage(UIManager.Instance.m_CRUDContactController);
    }

    public void UpdateMyProfile() 
    {
        m_view.SetMyProfile(ContactsCatalogManager.Instance.m_MyProfile);    
    }

    public void SaveMyName(string myEditedName) 
    {
        ContactsCatalogManager.Instance.EditMyName(myEditedName);
    }

    public void SortContactsAlphabetically() 
    {
        m_ContactsScrollView.UpdatePooler(GetMyContactsAlphabetically().ToList<IPoolData>(), false, m_ContactFieldPrefab);
    }

    public void SortContactsByCreationDate() 
    {
        m_ContactsScrollView.UpdatePooler(GetMyContactsByCreationTime().ToList<IPoolData>(), false, m_ContactFieldPrefab);
    }

    public List<Contact> GetMyContactsAlphabetically() 
    {
        return ShowedContacts.OrderBy(c => c.FirstName).ToList();
    }

    public List<Contact> GetMyContactsByCreationTime()
    {
        return ShowedContacts.OrderBy(c => c.DateAddedTimestamp).ToList();
    }

    public override void OnPageLeaving()
    {
        ClearSearchBar();
    }
}
