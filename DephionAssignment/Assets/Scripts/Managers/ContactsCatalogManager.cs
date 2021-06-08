using System;
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
    public List<ProfileImage> m_ProfileImages = new List<ProfileImage>();

    public event Action onContactAdded;
    public event Action onContactEdited;
    public event Action onMyProfileEdited;
    public event Action onContactDeleted;


    void Awake()
    {
        instance = this;
    }

    public void EditMyName(string editedName) 
    {
        if (m_MyProfile.FirstName == null || !m_MyProfile.FirstName.Equals(editedName))
        {
            m_MyProfile.FirstName = editedName;
            PlayfabManager.Instance.SaveMyProfile();
        }
    }

    public void Init(List<Contact> myContacts, Contact myProfile)
    {
        m_MyContacts = myContacts;
        m_MyProfile = myProfile;
        UIManager.Instance.m_AllContactsController.InitializeMyContacts(m_MyContacts, m_MyProfile);
    }

    public void SetAvailablePictures(List<string> imagesAvailable) 
    {
        foreach (var imageName in imagesAvailable)
        {
            m_ProfileImages.Add(new ProfileImage(imageName, UIManager.Instance.m_ProfileImagesAtlas.GetSprite(imageName)));
        }
    }

    public Sprite GetProfileImage(string imageName) 
    {
        return m_ProfileImages.FirstOrDefault(n => n.PhotoName == imageName).PhotoSprite;
    }

    public void CreateContact(Contact contact) 
    {
        m_MyContacts.Add(contact);
        m_MyContacts = m_MyContacts.OrderBy(c => c.FirstName).ToList();
        onContactAdded?.Invoke();
        PlayfabManager.Instance.SaveNewContacts();
    }

    public void EditedContact() 
    {
        m_MyContacts = m_MyContacts.OrderBy(c => c.FirstName).ToList();
        onContactEdited?.Invoke();
        PlayfabManager.Instance.SaveNewContacts();
    }

    public void UpdatedMyProfile() 
    {
        onMyProfileEdited?.Invoke();
        PlayfabManager.Instance.SaveMyProfile();
    }

    public void DeleteContact(Contact contact) 
    {
        m_MyContacts.Remove(contact);
        onContactDeleted?.Invoke();
        PlayfabManager.Instance.SaveNewContacts();
    }
}
