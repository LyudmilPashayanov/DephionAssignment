using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PoolController : MonoBehaviour, IBeginDragHandler
{
    [SerializeField] private ScrollRect ScrollRect;
    [SerializeField] private RectTransform ViewPort;
    [SerializeField] private RectTransform DragDetection;
    [SerializeField] private RectTransform Content;

    private float ItemHeight;      // TODO: Replace it with dynamic height
    [SerializeField] private int BufferSize;

    private List<IPoolData> Pool;
    
    private int PoolHead = 0;
    private int PoolTail = 0;

    float DragDetectionAnchorPreviousY = 0;

    int TargetVisibleItemCount { get { return Mathf.Max(Mathf.CeilToInt(ViewPort.rect.height / ItemHeight), 0); } }
    int TopItemOutOfView { get { return Mathf.CeilToInt(Content.anchoredPosition.y / ItemHeight); } }

    public void Setup(List<IPoolData> list, RectTransform prefab)
    {
        Pool = list;
        ItemHeight = prefab.rect.height;
        ScrollRect.onValueChanged.AddListener(OnDragDetectionPositionChange);
        DragDetection.sizeDelta = new Vector2(DragDetection.sizeDelta.x, Pool.Count * ItemHeight);

        for (int i = 0; i < TargetVisibleItemCount+BufferSize; i++)
        {
            if (Pool.Count - 1 < i) break;
            RectTransform itemGO = Instantiate(prefab);
            itemGO.transform.SetParent(Content);
            itemGO.transform.localScale = Vector3.one;
            itemGO.GetComponent<IPoolFields>().UpdateField(Pool[PoolTail]);
            PoolTail++;
        }
    }

    public void OnDragDetectionPositionChange(Vector2 dragNormalizePos)
    {
        float dragDelta = DragDetection.anchoredPosition.y - DragDetectionAnchorPreviousY;

        Content.anchoredPosition = new Vector2(Content.anchoredPosition.x, Content.anchoredPosition.y + dragDelta);

        UpdateContentBuffer();

        DragDetectionAnchorPreviousY = DragDetection.anchoredPosition.y;
    }

    private void UpdateContentBuffer()
    {
        Debug.Log("topItemOutOfView: " + TopItemOutOfView + "  >    bufferSize: " + BufferSize);

        if (TopItemOutOfView > BufferSize)
        {
            Debug.Log("poolTail: " + PoolTail + " >= pool.Count: " + Pool.Count);
            if (PoolTail >= Pool.Count)
            {
                return;
            }

            Transform firstChild = Content.GetChild(0);
            firstChild.SetSiblingIndex(Content.childCount - 1);
            firstChild.gameObject.GetComponent<IPoolFields>().UpdateField(Pool[PoolTail]);
            Content.anchoredPosition = new Vector2(Content.anchoredPosition.x, Content.anchoredPosition.y - firstChild.GetComponent<RectTransform>().rect.height);
            PoolHead++;
            PoolTail++;
        }
        else if (TopItemOutOfView < BufferSize)
        {
            if (PoolHead <= 0)
            {
                return;
            }

            Transform lastChild = Content.GetChild(Content.childCount - 1);
            lastChild.SetSiblingIndex(0);
            PoolHead--;
            PoolTail--;
            lastChild.gameObject.GetComponent<IPoolFields>().UpdateField(Pool[PoolHead]);
            Content.anchoredPosition = new Vector2(Content.anchoredPosition.x, Content.anchoredPosition.y + lastChild.GetComponent<RectTransform>().rect.height);

        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragDetectionAnchorPreviousY = DragDetection.anchoredPosition.y;
    }
}