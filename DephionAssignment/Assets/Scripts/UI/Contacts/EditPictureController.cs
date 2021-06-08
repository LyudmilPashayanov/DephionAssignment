using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using System.Linq;

public class EditPictureController : MonoBehaviour
{
    [SerializeField] private EditPictureView m_view;
    public List<ProfileImage> m_ShowedImages = new List<ProfileImage>();
    public RectTransform m_ProfilePicturePrefab;
    public PoolController m_ProfilePicturesScrollView;
    private bool IsScrollFull;

    /// <summary>
    /// Shows all available images you can choose from for a profile image.
    /// </summary>
    /// <param name="SavePicture"></param>
    /// <param name="ImagesToShow"></param>
    public void InitEditPicture(UnityAction<string> SavePicture, List<ProfileImage> ImagesToShow)
    {
        m_view.SetHeaderText("select image");
        m_ShowedImages = ImagesToShow;
        if (!IsScrollFull)
        {       
            m_ProfilePicturesScrollView.Setup(m_ShowedImages.ToList<IPoolData>(), m_ProfilePicturePrefab);
            IsScrollFull = true;
        }
        else 
        {
            m_ProfilePicturesScrollView.UpdatePooler(m_ShowedImages.ToList<IPoolData>(), false);
        }
    }
}
