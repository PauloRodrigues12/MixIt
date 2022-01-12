using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILivro : MonoBehaviour
{
    public int pagina = 0;
    public GameObject SetaEsq;
    public GameObject SetaDir;
    public GameObject pag0;
    public GameObject pag1;
    public GameObject pag2;
    public GameObject pag3;
    public GameObject pag4;
    public GameObject pag5;
    public GameObject pag6;

    public AudioClip[] sonsPag;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (pagina == 0)
        {
            SetaEsq.SetActive(false);
            pag0.SetActive(true);
            pag1.SetActive(false);
            pag2.SetActive(false);
            pag3.SetActive(false);
            pag4.SetActive(false);
            pag5.SetActive(false);
            pag6.SetActive(false);
        }
        else if (pagina == 1)
        {
            SetaEsq.SetActive(true);
            pag0.SetActive(false);
            pag1.SetActive(true);
            pag2.SetActive(false);
            pag3.SetActive(false);
            pag4.SetActive(false);
            pag5.SetActive(false);
            pag6.SetActive(false);
        }
        else if (pagina == 2)
        {
            pag0.SetActive(false);
            pag1.SetActive(false);
            pag2.SetActive(true);
            pag3.SetActive(false);
            pag4.SetActive(false);
            pag5.SetActive(false);
            pag6.SetActive(false);
        }
        else if (pagina == 3)
        {
            pag0.SetActive(false);
            pag1.SetActive(false);
            pag2.SetActive(false);
            pag3.SetActive(true);
            pag4.SetActive(false);
            pag5.SetActive(false);
            pag6.SetActive(false);
        }
        else if (pagina == 4)
        {
            pag0.SetActive(false);
            pag1.SetActive(false);
            pag2.SetActive(false);
            pag3.SetActive(false);
            pag4.SetActive(true);
            pag5.SetActive(false);
            pag6.SetActive(false);
        }
        else if (pagina == 5)
        {
            SetaDir.SetActive(true);
            pag0.SetActive(false);
            pag1.SetActive(false);
            pag2.SetActive(false);
            pag3.SetActive(false);
            pag4.SetActive(false);
            pag5.SetActive(true);
            pag6.SetActive(false);
        }
        else
        {
            SetaDir.SetActive(false);
            pag0.SetActive(false);
            pag1.SetActive(false);
            pag2.SetActive(false);
            pag3.SetActive(false);
            pag4.SetActive(false);
            pag5.SetActive(false);
            pag6.SetActive(true);
        }
    }

    public void ClicarPraTras()
    {
        pagina -= 1;

        int randomNr = Random.Range(0, sonsPag.Length);
        audioSource.clip = sonsPag[randomNr];
        audioSource.Play();
    }

    public void ClicarPraFrente()
    {
        pagina += 1;

        int randomNr = Random.Range(0, sonsPag.Length);
        audioSource.clip = sonsPag[randomNr];
        audioSource.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
