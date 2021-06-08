using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileImageFieldController : MonoBehaviour, IPoolFields
{
    [SerializeField] private ProfileImageFieldView m_view;
    private ProfileImage ThisProfileImage;
    public void UpdateField(IPoolData objectToUpdate)
    {
        ThisProfileImage = (ProfileImage)objectToUpdate;
        m_view.SetImage(ThisProfileImage.PhotoSprite);
        if (ThisProfileImage.PhotoName == UIManager.Instance.m_CRUDContactController.m_CurrentlyEditedContact.Photo)
        {
            m_view.SetSelectedActive(true);
        }
        else { m_view.SetSelectedActive(false); }
        m_view.RemoveListeners();
        m_view.SetListeners(SelectImage);
        
    }

    public void SelectImage() 
    {
        m_view.SetSelectedActive(true);
        UIManager.Instance.m_CRUDContactController.SetNewProfilePicture(ThisProfileImage.PhotoName);
    }
}
