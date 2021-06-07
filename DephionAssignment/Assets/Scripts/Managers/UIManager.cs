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

    public AllContactsController m_AllContactsController;
    public CRUDContactController m_CRUDContactController;
    private IUIPage CurrentPage;
    [SerializeField] private RectTransform m_UIPageHolder;
    void Awake()
    {
        instance = this;
    }

    public void GoToUIPage(IUIPage pageToGoTo)
    {
        CurrentPage?.OnPageLeaving();
        m_UIPageHolder.DOAnchorPos(-pageToGoTo.GetPageLocation(), 0.3f);
        CurrentPage = pageToGoTo;
    }
}
