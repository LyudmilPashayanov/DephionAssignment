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
        m_view.AddListeners(FilterContacts,SortContactsAlphabetically,SortContactsByCreationDate); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
        }
    }

    public void InitializeMyContacts(List<Contact> allContacts) 
    {
        showedContacts = allContacts;
        m_ContactsScrollView.Setup(GetMyContactsAlphabetically().ToList<IPoolData>(), m_ContactFieldPrefab);       
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
        m_ContactsScrollView.UpdatePooler(filteredContacts.ToList<IPoolData>(), m_ContactFieldPrefab);
        Debug.Log("filtering contacts.");
        ChangeButtonToClear();
    }

    public void ChangeButtonToClear() 
    {
        m_view.ChangeIconToClearImage();
        UnityAction newAction = new UnityAction(()=>
        {
            m_ContactsScrollView.UpdatePooler(ContactsCatalogManager.Instance.m_MyContacts.ToList<IPoolData>(), m_ContactFieldPrefab);
            showedContacts = ContactsCatalogManager.Instance.m_MyContacts;
            m_view.ClearSearchBar();
            m_view.ChangeIconToMagnifierImage();
            m_view.ChangeButtonToSearch();
        });

        m_view.ChangeButtonToClear(newAction);
    }

    
    public void SortContactsAlphabetically() 
    {
        m_ContactsScrollView.UpdatePooler(GetMyContactsAlphabetically().ToList<IPoolData>(), m_ContactFieldPrefab);
    }

    public void SortContactsByCreationDate() 
    {
        m_ContactsScrollView.UpdatePooler(GetMyContactsByCreationTime().ToList<IPoolData>(), m_ContactFieldPrefab);
    }

    public List<Contact> GetMyContactsAlphabetically() 
    {   
        return showedContacts.OrderBy(c => c.FirstName).ToList();
    }

    public List<Contact> GetMyContactsByCreationTime()
    {
        return showedContacts.OrderBy(c => c.DateAddedTimestamp).ToList();
    }
}
