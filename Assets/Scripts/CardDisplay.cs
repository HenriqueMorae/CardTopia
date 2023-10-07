using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    Card card;

    [Header("Display Info")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI typeName;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Image artworkImage;
    [SerializeField] TextMeshProUGUI costText;

    [Header("Display Info 2")]
    [SerializeField] TextMeshProUGUI nameText2;
    [SerializeField] TextMeshProUGUI typeName2;
    [SerializeField] TextMeshProUGUI descriptionText2;
    [SerializeField] TextMeshProUGUI costText2;

    public void StartCard (Card carta)
    {
        card = carta;

        nameText.text = card.cardName;
        typeName.text = card.type.ToString();
        descriptionText.text = card.description;
        artworkImage.sprite = card.artwork;
        costText.text = card.cost.ToString();

        nameText2.text = card.cardName;
        typeName2.text = card.type.ToString();
        descriptionText2.text = card.description;
        costText2.text = card.cost.ToString();
    }

    public void ActivateEffect()
    {
        card.EffectOn();
    }

    public Efeito GetEffectType()
    {
        return card.EffectType();
    }

    public Card CardInfo()
    {
        return card;
    }
}
