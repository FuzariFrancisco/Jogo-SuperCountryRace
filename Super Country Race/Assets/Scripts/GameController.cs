using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject[] players;
    public GameObject painelInicio, painelFim, painelInGame, painelCreditos;

    public TextMeshProUGUI HudPrincipal, HudPrincipal2;

    public TextMeshProUGUI hudVoltasP1;
    public int voltaAtualP1 = 0;

    public TextMeshProUGUI hudVoltasP2;
    public int voltaAtualP2 = 0;

    public int maximoVoltas = 5;

    private float timer;
    private float delay = 1f;
    private int contador = 4;

    public bool rodando;

    void Start()
    {
        rodando = false;
        Time.timeScale = 0;
        painelFim.SetActive(false);
        painelInicio.SetActive(true);
        painelInGame.SetActive(false);
        timer = Time.time;
        foreach (GameObject m_gameObject in players)
        {
            m_gameObject.GetComponent<PlayerController>().enabled = false;
        }
        hudVoltasP1.GetComponent<TextMeshProUGUI>().text = string.Format("{0}/{1}", voltaAtualP1, maximoVoltas);
        hudVoltasP2.GetComponent<TextMeshProUGUI>().text = string.Format("{0}/{1}", voltaAtualP2, maximoVoltas);
    }

    void Update() {
        if (contador >= 0) {
            if (delay < (Time.time - timer)) {
                timer = Time.time;
                contador--;
            }

            switch (contador) {
                case 4:
                    HudPrincipal.GetComponent<TextMeshProUGUI>().text = "Super Country Race";
                    break;
                case 3:
                    HudPrincipal.GetComponent<TextMeshProUGUI>().text = "PREPARAR!";
                    break;
                case 2:
                    HudPrincipal.GetComponent<TextMeshProUGUI>().text = "APONTAR!";
                    break;
                case 1:
                    HudPrincipal.GetComponent<TextMeshProUGUI>().text = "CORRA!";
                    rodando = true;
                    foreach (GameObject m_gameObject in players) {
                        m_gameObject.GetComponent<PlayerController>().enabled = true;
                    }
                    break;
                case 0:
                    HudPrincipal.GetComponent<TextMeshProUGUI>().text = "";
                    break;
                default:
                    break;
            }
        }
    }

    public void UpdateVoltasP1(int voltasPlayer) {
        if (voltasPlayer > voltaAtualP1)
        {
            voltaAtualP1 = voltasPlayer;
            hudVoltasP1.GetComponent<TextMeshProUGUI>().text = string.Format("{0}/{1}", voltaAtualP1, maximoVoltas);
        }
    }

    public void UpdateVoltasP2(int voltasPlayer)
    {
        if (voltasPlayer > voltaAtualP2)
        {
            voltaAtualP2 = voltasPlayer;
            hudVoltasP2.GetComponent<TextMeshProUGUI>().text = string.Format("{0}/{1}", voltaAtualP2, maximoVoltas);
        }
    }

    public void FinishRace(GameObject winner) {
        foreach (GameObject m_gameObject in players)
        {
            if (m_gameObject == winner)
                m_gameObject.GetComponent<PlayerController>().enabled = false;
            else
                m_gameObject.SetActive(false);
        }
        painelInGame.SetActive(false);
        painelFim.SetActive(true);
        HudPrincipal2.GetComponent<TextMeshProUGUI>().text = string.Format("Player {0} ganhou!!!", winner.GetComponent<PlayerController>().PlayerID);
    }

    public void Comecar()
    {
        painelInGame.SetActive(true);
        painelInicio.SetActive(false);
        Time.timeScale = 1;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AbrirCreditos()
    {
        painelCreditos.SetActive(true);
    }

    public void FecharCreditos()
    {
        painelCreditos.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
