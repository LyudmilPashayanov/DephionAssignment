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
        m_view.AddListeners(FilterContacts); 
    }

    public void Test() {
        List<Contact> newlist = new List<Contact>();
        for (int i = 0; i < 1; i++)
        {
            newlist.Add(myContacts[i]);
        }
        m_ContactsScrollView.Setup(myContacts.ToList<IPoolData>(), m_ContactFieldPrefab);       
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

    public void SortMyContactsAlphabetically() 
    {
        myContacts.OrderBy(c => c.FirstName); 
    }

    public void SortMyContactsCreationTime()
    {
        myContacts.OrderBy(c => c.DateAddedTimestamp);
    }
}
