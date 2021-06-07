using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContactsCatalogManager : MonoBehaviour
{
    static ContactsCatalogManager instance;
    public static ContactsCatalogManager Instance { get { return instance; } }

    public List<Contact> m_MyContacts = new List<Contact>();
    public Contact m_MyProfile = new Contact();

    void Awake()
    {
        instance = this;
    }

    public void EditMyName(string editedName) 
    {
        if (m_MyProfile.FirstName == null || !m_MyProfile.FirstName.Equals(editedName))
        {
            m_MyProfile.FirstName = editedName;
            PlayfabManager.Instance.SaveMyProfile(m_MyProfile);
        }
    }

    public void Init(List<Contact> myContacts, Contact myProfile)
    {
        m_MyContacts = myContacts;
        m_MyProfile = myProfile;
        UIManager.Instance.m_AllContactsController.InitializeMyContacts(m_MyContacts, m_MyProfile);
    }

    public void CreateContact(Contact contact) 
    {
        m_MyContacts.Add(contact);
        m_MyContacts = m_MyContacts.OrderBy(c => c.FirstName).ToList();
        UIManager.Instance.m_AllContactsController.UpdatePooler(m_MyContacts,true);
        PlayfabManager.Instance.SaveNewContacts();
    }

    public void EditedContact() 
    {
        m_MyContacts = m_MyContacts.OrderBy(c => c.FirstName).ToList();
        UIManager.Instance.m_AllContactsController.UpdatePooler(m_MyContacts, false);
        PlayfabManager.Instance.SaveNewContacts();
    }
}
