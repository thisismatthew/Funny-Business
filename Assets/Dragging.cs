using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private Canvas canvas;

    private Vector2 offset;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPointerPosition);

        RectTransform parentRectTransform = transform.parent as RectTransform;

        // Calculate the offset from the parent's pivot to the mouse position
        offset = parentRectTransform.localPosition - (Vector3)localPointerPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (transform.parent == null) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPointerPosition);

        // Apply the offset while setting the new position
        transform.parent.localPosition = localPointerPosition + offset;
    }
}
