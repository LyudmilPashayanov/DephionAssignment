using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    public SpriteAtlas m_ContactsAtlas;
    public SpriteAtlas m_ProfileImagesAtlas;


    public AllContactsController m_ContactsUIController;
    public CRUDContactController m_CRUDContactController;

    [SerializeField] private RectTransform m_UIPageHolder;
    void Awake()
    {
        instance = this;
    }
    private void Update()
    {

    }

    public void GoToUIPage(IUIPage pageToGoTo) 
    {
        m_UIPageHolder.DOAnchorPos(-pageToGoTo.GetPageLocation(), 0.3f);
    }
}
