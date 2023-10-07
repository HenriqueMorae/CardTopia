using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEffects : MonoBehaviour
{
    public void Habilidade(int ability)
    {
        switch (ability)
        {
        case 1: InvestimentoExtra(); break;
        case 2: Banco(); break;
        case 3: Onibus(); break;
        case 4: Rodoviaria(); break;
        case 5: Reciclagem(); break;
        case 6: Bosque(); break;
        case 7: AutomacaoInteligente(); break;
        case 8: InternetDasCoisas(); break;
        case 9: Expansao(); break;
        case 10: OtimizacaoDeEspaco(); break;
        case 11: Bicicleta(); break;
        case 12: Rodovia(); break;
        case 13: Ipe(); break;
        case 14: Reforma(); break;
        case 15: WiFiPublico(); break;
        case 16: Parque(); break;
        default: break;
        }
    }

    public void InvestimentoExtra()
    {
        FindObjectOfType<GameManager>().AddInvestimentoExtra(2);
    }

    public void Banco()
    {

    }

    public void Onibus()
    {
        // EM BREVE
        FindObjectOfType<GameManager>().AddCartasPraComprar(2);
    }

    public void Rodoviaria()
    {
        // EM BREVE
        FindObjectOfType<GameManager>().ChangeCardsToDrawBase(2);
    }

    public void Reciclagem()
    {

    }

    public void Bosque()
    {

    }

    public void AutomacaoInteligente()
    {
        FindObjectOfType<GameManager>().AddDesenvolvimento(1);
    }

    public void InternetDasCoisas()
    {
        FindObjectOfType<GameManager>().AddDesenvolvimento(2);
    }

    public void Expansao()
    {
        FindObjectOfType<GameManager>().AddOrRemoveCardSpace(1);
    }

    public void OtimizacaoDeEspaco()
    {
        FindObjectOfType<GameManager>().AddOrRemoveCardSpace(-1);
        FindObjectOfType<GameManager>().AddDesenvolvimento(4);
    }

    public void Bicicleta()
    {
        FindObjectOfType<GameManager>().AddCartasPraComprar(1);
    }

    public void Rodovia()
    {
        // EM BREVE
    }

    public void Ipe()
    {

    }

    public void Reforma()
    {

    }

    public void WiFiPublico()
    {
        // EM BREVE
    }

    public void Parque()
    {

    }
}
