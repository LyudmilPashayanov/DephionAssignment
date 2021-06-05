using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AllContactsController : IUIPage
{
    [SerializeField] private AllContactsView m_view;

    public List<Contact> myContacts = new List<Contact>();
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

    public void Test() {
        m_ContactsScrollView.Setup(GetMyContactsAlphabetically().ToList<IPoolData>(), m_ContactFieldPrefab);       
    }

    public void FilterContacts(string text)
    {
        List<Contact> filteredContacts = new List<Contact>();
        foreach (Contact contact in myContacts)
        {
            if (contact.FirstName.Contains(text)) 
            {
                filteredContacts.Add(contact);
            }
        }
        m_ContactsScrollView.UpdatePooler(filteredContacts.ToList<IPoolData>(), m_ContactFieldPrefab);
    }

    public void AddContact() 
    {
        Contact newContact1 = new Contact()
        {
            FirstName = "ivan",
            LastName = "petrov",
            Description = "tapakov",
            Email = "asd",
            Phone = "_12341151"
        };
        myContacts.Add(newContact1);
        PlayfabManager.Instance.SaveNewContacts(myContacts);
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
        return myContacts.OrderBy(c => c.FirstName).ToList();
    }

    public List<Contact> GetMyContactsByCreationTime()
    {
        return myContacts.OrderBy(c => c.DateAddedTimestamp).ToList();
    }
}
