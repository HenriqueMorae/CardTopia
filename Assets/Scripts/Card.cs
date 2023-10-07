using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Tipo
{
    Economia,
    MeioAmbiente,
    PlanejamentoUrbano,
    Tecnologia,
    Mobilidade
}

public enum Efeito
{
    Descarte,
    Constante
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public Tipo type;
    public int cost;
    [TextAreaAttribute] public string description;
    public Sprite artwork;
    [SerializeField] Efeito effect;
    [SerializeField] int ability;

    // Realizar o efeito da carta
    public void EffectOn()
    {
        FindObjectOfType<CardEffects>().Habilidade(ability);
    }

    public Efeito EffectType()
    {
        return effect;
    }

    public Tipo CardType()
    {
        return type;
    }
}
