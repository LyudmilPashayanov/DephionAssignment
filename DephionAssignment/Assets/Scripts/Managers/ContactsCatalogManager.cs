using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactsCatalogManager : MonoBehaviour
{
    static ContactsCatalogManager instance;
    public static ContactsCatalogManager Instance { get { return instance; } }

    public List<Contact> m_MyContacts = new List<Contact>();

    void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        UIManager.Instance.m_ContactsUIController.InitializeMyContacts(m_MyContacts);
    }

}
