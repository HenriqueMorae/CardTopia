using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Canvas canvas;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    Vector2 retorno;
    Transform originalParent;
    bool foraDoLocal;
    bool travado;

    void Awake()
    {
        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        retorno = rectTransform.anchoredPosition;
        originalParent = gameObject.transform.parent;
        foraDoLocal = true;
        travado = false;
    }

    public void SetTravado(bool estado)
    {
        travado = estado;
    }

    public bool Travado()
    {
        return travado;
    }

    public void OnPointerDown (PointerEventData eventData)
    {
        if (travado) return;
    }

    public void OnBeginDrag (PointerEventData eventData)
    {
        if (travado) return;

        FindObjectOfType<AudioManager>().Play(GetComponent<CardDisplay>().CardInfo().CardType().ToString());        

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .75f;
    }

    public void OnDrag (PointerEventData eventData)
    {
        if (travado) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        if (travado) return;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if (foraDoLocal) ReturnToBegin();
    }

    public void ReturnToBegin()
    {
        gameObject.transform.SetParent(null, false);
        gameObject.transform.SetParent(originalParent, false);
        foraDoLocal = true;
    }

    public void SetLocal(bool local)
    {
        foraDoLocal = local;
    }
}
