using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContactFieldController : MonoBehaviour, IPoolFields
{
    public ContactFieldView m_view;

    public void UpdateField(IPoolData contactObject)
    {
        Contact contact = (Contact)contactObject;
        gameObject.name = contact.FirstName;
        m_view.UpdateName(contact.FirstName, contact.LastName);
        if (contact.Photo != null)
            m_view.UpdateProfilePhoto(ContactsCatalogManager.Instance.GetProfileImage(contact.Photo));
        if(!string.IsNullOrEmpty(contact.Phone))
            m_view.UpdateSecondaryText(contact.Phone);
        else if(!string.IsNullOrEmpty(contact.Email))
            m_view.UpdateSecondaryText(contact.Email);
        else
            m_view.UpdateSecondaryText(contact.Twitter);
        UnityAction newAction = new UnityAction(() =>
        {
            UIManager.Instance.m_CRUDContactController.EditExistingContact((Contact)contactObject);
            UIManager.Instance.GoToUIPage(UIManager.Instance.m_CRUDContactController);
        });
        m_view.OnClickBehaviour(newAction);
    }
}
