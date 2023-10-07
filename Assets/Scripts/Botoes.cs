using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botoes : MonoBehaviour
{
    [SerializeField] bool tocarMusica;
    [SerializeField] int numero;

    void Start() {
        if (tocarMusica)
            FindObjectOfType<AudioManager>().Play("musica" + numero);
    }

    public void MenuPrincipal() {
        FindObjectOfType<AudioManager>().StopAll();
        SceneManager.LoadScene("MenuScene");
    }

    public void Jogo() {
        FindObjectOfType<AudioManager>().StopAll();
        SceneManager.LoadScene("GameScene");
    }

    public void Sair() {
        FindObjectOfType<AudioManager>().StopAll();
        Application.Quit();
    }
}
