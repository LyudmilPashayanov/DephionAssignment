using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    public SpriteAtlas m_ContactsAtlas;
    public SpriteAtlas m_ProfileImagesAtlas;

    public AllContactsController m_AllContactsController;
    public CRUDContactController m_CRUDContactController;
    private IUIPage CurrentPage;
    [SerializeField] private RectTransform UIPageHolder;
    [SerializeField] private RectTransform LoadingScreen;
    private CanvasScaler CanvasScaler;
    void Awake()
    {
        instance = this;
        SetLoadingScreen(true);
        CanvasScaler = gameObject.GetComponent<CanvasScaler>();
    }

    public void GoToUIPage(IUIPage pageToGoTo)
    {
        CurrentPage?.OnPageLeaving();
        UIPageHolder.DOAnchorPos(-pageToGoTo.GetPageLocation(), 0.3f);
        CurrentPage = pageToGoTo;
    }

    public Vector2 GetCanvasSize() 
    {
        return new Vector2(CanvasScaler.referenceResolution.x,CanvasScaler.referenceResolution.y);
    }
    public void SetLoadingScreen(bool active) 
    {
        LoadingScreen.gameObject.SetActive(active);
    }
}
