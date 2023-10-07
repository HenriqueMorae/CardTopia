using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Cenas")]
    [SerializeField] GameObject cenaJogo;
    [SerializeField] GameObject cenaVenceu;
    [SerializeField] GameObject cenaPerdeu;

    [Header("Elementos")]
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject cardSpacePrefab;
    [SerializeField] Transform hand;
    [SerializeField] Transform cardSpaceSpace;
    [SerializeField] List<GameObject> cardSpaces = new List<GameObject>();
    [SerializeField] TextMeshProUGUI deckSizeText;
    [SerializeField] TextMeshProUGUI discardPileText;
    [SerializeField] TextMeshProUGUI rodadaText;
    [SerializeField] TextMeshProUGUI investimentoText;
    [SerializeField] TextMeshProUGUI desenvolvimentoText;

    [Header("Parâmetros")]
    [SerializeField] int maxCardsInHand;
    [SerializeField] int investimentoBase;
    [SerializeField] int desenvolvimento;

    [SerializeField] List<Card> deck = new List<Card>();
    List<Card> discardPile = new List<Card>();
    int cardsInHand;
    int cardsToDraw;
    int cardsToDrawBase;
    int cardsToDrawExtra;
    int rodada;
    int investimentoExtra;
    int investimento;
    int spacesToAddOrRemove;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("musica1");

        cardsInHand = 0;
        cardsToDrawBase = 1;
        cardsToDrawExtra = 2;
        discardPileText.text = "0";
        deckSizeText.text = deck.Count.ToString();
        rodada = 0;
        investimentoExtra = 0;
        investimento = investimentoBase;
        desenvolvimento = 0;
        spacesToAddOrRemove = 0;

        investimentoText.text = investimentoBase.ToString();
        desenvolvimentoText.text = desenvolvimento.ToString();

        StartTurn();
    }

    void Final()
    {
        cenaJogo.SetActive(false);

        if (desenvolvimento >= 20)
        {
            cenaVenceu.SetActive(true);
        }
        else
        {
            cenaPerdeu.SetActive(true);
        }
    }

    public void ChangeCardsToDrawBase(int qtd)
    {
        cardsToDrawBase = qtd;
    }

    public void AddOrRemoveCardSpace(int qtd)
    {
        spacesToAddOrRemove += qtd;
    }

    public void AddCardSpace()
    {
        GameObject novoEspaco = Instantiate(cardSpacePrefab, cardSpaceSpace);
        cardSpaces.Add(novoEspaco);
    }

    public void RemoveCardSpace()
    {
        GameObject espaco = cardSpaces[Random.Range(0, cardSpaces.Count)];
        cardSpaces.Remove(espaco);
        
        if (espaco.transform.childCount > 0) 
        {
            discardPile.Add(espaco.transform.GetChild(0).GetComponent<CardDisplay>().CardInfo());
            Destroy(espaco.transform.GetChild(0).gameObject);
        }

        Destroy(espaco);
    }

    public void AddDesenvolvimento(int qtd)
    {
        desenvolvimento += qtd;
        desenvolvimentoText.text = desenvolvimento.ToString();
    }

    public void AddInvestimentoExtra(int qtd)
    {
        investimentoExtra += qtd;
    }

    public void AddCartasPraComprar(int qtd)
    {
        cardsToDrawExtra += qtd;
    }

    public void InvestimentoChange (int preco)
    {
        investimento += preco;
        investimentoText.text = investimento.ToString();
    }

    public int InvestimentoAtual()
    {
        return investimento;
    }

    public void StartTurn()
    {
        rodada++;
        rodadaText.text = "Rodada " + rodada.ToString();

        if (rodada > 10)
        {
            Final();
        }

        cardsToDraw = cardsToDrawBase + cardsToDrawExtra;
        for (int i = 0; i < cardsToDraw; i++)
        {
            DrawCard();
        }
        cardsToDrawExtra = 0;

        investimento = investimentoBase + investimentoExtra;
        investimentoExtra = 0;
        investimentoText.text = investimento.ToString();
    }

    public void DrawCard()
    {
        cardsInHand = hand.childCount;

        if (deck.Count >= 1 && cardsInHand < maxCardsInHand)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];

            GameObject newCard = Instantiate(cardPrefab, hand);
            newCard.GetComponent<CardDisplay>().StartCard(randCard);

            deck.Remove(randCard);
            deckSizeText.text = deck.Count.ToString();
        }
    }

    public void EndTurn()
    {
        foreach (GameObject space in cardSpaces)
        {
            if (space.transform.childCount == 1)
            {
                CardDisplay cartaNoLocal = space.transform.GetChild(0).GetComponent<CardDisplay>();
                cartaNoLocal.ActivateEffect();

                if (cartaNoLocal.GetEffectType() == Efeito.Descarte)
                {
                    discardPile.Add(cartaNoLocal.CardInfo());
                    space.GetComponent<CardSlot>().DestroyCardHere();
                    Destroy(space.transform.GetChild(0).gameObject);
                }
                else
                {
                    cartaNoLocal.gameObject.GetComponent<DragDrop>().SetTravado(true);
                }
            }
        }

        ChangeSpaces();

        discardPileText.text = discardPile.Count.ToString();

        StartTurn();
    }

    void ChangeSpaces()
    {
        if (spacesToAddOrRemove > 0)
        {
            for (int i = 0; i < spacesToAddOrRemove; i++)
            {
                AddCardSpace();
            }
        }
        else if (spacesToAddOrRemove < 0)
        {
            for (int i = 0; i > spacesToAddOrRemove; i--)
            {
                RemoveCardSpace();
            }
        }

        spacesToAddOrRemove = 0;
    }
}
