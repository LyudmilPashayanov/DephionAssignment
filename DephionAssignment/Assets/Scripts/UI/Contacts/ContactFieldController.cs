using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactFieldController : MonoBehaviour, IPoolFields
{
    public ContactFieldView m_view;

    public void UpdateField(IPoolData contactObject)
    {
        Contact contact = (Contact)contactObject;
        gameObject.name = contact.FirstName;
        if (contact.Photo != null)
            m_view.UpdateProfilePhoto(UIManager.Instance.m_ContactsAtlas.GetSprite(contact.Photo));
        else
        {

        }

        m_view.UpdateName(contact.FirstName, contact.LastName);
        if(contact.Phone != null)
            m_view.UpdateSecondaryText(contact.Phone);
        else if(contact.Email != null)
            m_view.UpdateSecondaryText(contact.Email);
        else
            m_view.UpdateSecondaryText(contact.Twitter);
    }
}
