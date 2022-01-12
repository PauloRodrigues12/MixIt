using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILivroAnimations : MonoBehaviour
{
    private BookBehavior bookBehavior;
    private Animator anim;
    private GameObject player;
    public Camera cam;
    private AudioSource audioSource;
    public AudioClip pressButton;

    private bool isOpen;

    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("AnimCam").GetComponent<Animator>();
        bookBehavior = GameObject.FindGameObjectWithTag("Book").GetComponent<BookBehavior>();
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (bookBehavior.bookPressed == true)
            isOpen = true;
    }

    public void FecharLivro()
    {
        audioSource.clip = pressButton;
        audioSource.Play();
        anim.SetBool("isOpen", false);
        StartCoroutine(WaitForAnim());
    }

    public void FecharJogo()
    {
        audioSource.Play();
        Application.Quit();
    }

    private IEnumerator WaitForAnim()
    {
        if (isOpen == true)
        {
            yield return new WaitForSeconds(2);

            player.SetActive(true);
            cam.enabled = true;
            bookBehavior.pickUpIcon.enabled = true;

            isOpen = false;
        }
    }
}
