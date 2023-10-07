using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour, IDropHandler, IPointerExitHandler, IPointerEnterHandler
{
    bool ocupado;
    GameObject ocupando;
    Vector2 centro;
    RectTransform rect;
    GameManager gm;

    void Awake()
    {
        ocupado = false;
        rect = gameObject.GetComponent<RectTransform>();
        centro = new Vector2(rect.rect.width/2, -rect.rect.height/2);
        gm = FindObjectOfType<GameManager>();
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<DragDrop>().SetLocal(false);
        }
    }

    public void OnDrop (PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<CardDisplay>().CardInfo().cost > gm.InvestimentoAtual())
            {
                eventData.pointerDrag.GetComponent<DragDrop>().SetLocal(true);
                return;
            }

            if (ocupado)
            {
                if (ocupando.GetComponent<DragDrop>().Travado())
                {
                    eventData.pointerDrag.GetComponent<DragDrop>().SetLocal(true);
                    return;
                }

                gm.InvestimentoChange(ocupando.GetComponent<CardDisplay>().CardInfo().cost);
                ocupando.GetComponent<DragDrop>().ReturnToBegin();
            }

            ocupando = eventData.pointerDrag;
            eventData.pointerDrag.transform.SetParent(gameObject.transform, false);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = centro;
            gm.InvestimentoChange(-ocupando.GetComponent<CardDisplay>().CardInfo().cost);
            ocupado = true;
        }
    }

    public void OnPointerExit (PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<DragDrop>().SetLocal(true);

            if (eventData.pointerDrag == ocupando)
            {
                gm.InvestimentoChange(ocupando.GetComponent<CardDisplay>().CardInfo().cost);
                ocupando = null;
                ocupado = false;
            }
        }
    }

    public void DestroyCardHere()
    {
        ocupado = false;
    }
}
