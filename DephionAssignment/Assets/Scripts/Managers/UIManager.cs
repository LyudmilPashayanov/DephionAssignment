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

    public AllContactsUIController m_ContactsUIController;
    public CRUDContactController m_CRUDContactController;

    [SerializeField] private RectTransform m_UIPageHolder;
    void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            GoToUIPage(m_ContactsUIController);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GoToUIPage(m_CRUDContactController);
        }
    }
    public void GoToUIPage(IUIPage pageToGoTo) 
    {
        m_UIPageHolder.DOAnchorPos(-pageToGoTo.GetPageLocation(), 0.3f);
    }
}
