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

    public List<Contact> showedContacts = new List<Contact>();

    public RectTransform m_ContactFieldPrefab;
    public PoolController m_ContactsScrollView;

    private void Start()
    {
        m_view.RemoveAllListeners();
        m_view.AddListeners(FilterContacts,SortContactsAlphabetically,SortContactsByCreationDate, GoToContactCreation, SaveMyName
            ,new UnityAction<string>((string c)=>{ m_view.SetButtonActive(false); }), GoToEditMyProfile);

        ContactsCatalogManager.Instance.onContactAdded += new Action(() => { UpdateListOfContacts(ContactsCatalogManager.Instance.m_MyContacts, true); });
        ContactsCatalogManager.Instance.onContactEdited += new Action(() => { UpdateListOfContacts(ContactsCatalogManager.Instance.m_MyContacts, false); });
        ContactsCatalogManager.Instance.onMyProfileEdited += UpdateMyProfile;
        ContactsCatalogManager.Instance.onContactDeleted += new Action(() => { UpdateListOfContacts(ContactsCatalogManager.Instance.m_MyContacts, true); }); ;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
        }
    }

    public void InitializeMyContacts(List<Contact> allContacts, Contact m_MyProfile) 
    {
        showedContacts = allContacts;
        m_ContactsScrollView.Setup(GetMyContactsAlphabetically().ToList<IPoolData>(), m_ContactFieldPrefab);
        m_view.SetMyProfile(m_MyProfile);
    }

    public void FilterContacts(string text)
    {   
        List<Contact> filteredContacts = new List<Contact>();
        foreach (Contact contact in ContactsCatalogManager.Instance.m_MyContacts)
        {
            if (contact.FirstName.Contains(text)) 
            {
                filteredContacts.Add(contact);
            }
        }
        showedContacts = filteredContacts;
        m_ContactsScrollView.UpdatePooler(filteredContacts.ToList<IPoolData>(),false,m_ContactFieldPrefab);
        Debug.Log("filtering contacts.");
        ChangeButtonToClear();
    }

    public void UpdateListOfContacts(List<Contact> list, bool forceUpdate) 
    {
        showedContacts = list;
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
        showedContacts = ContactsCatalogManager.Instance.m_MyContacts;
        m_ContactsScrollView.UpdatePooler(showedContacts.ToList<IPoolData>(), true, m_ContactFieldPrefab);
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
        return showedContacts.OrderBy(c => c.FirstName).ToList();
    }

    public List<Contact> GetMyContactsByCreationTime()
    {
        return showedContacts.OrderBy(c => c.DateAddedTimestamp).ToList();
    }

    public override void OnPageLeaving()
    {
        ClearSearchBar();
    }
}
