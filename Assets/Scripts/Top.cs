using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public AudioSource duvarses;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("topgirdi"))
        {
            
            gameObject.SetActive(false);
            gameManager.DevamEt(transform.position);

            if (gameObject.tag == "yýldýzlý")
            {
                
                gameManager.ekstraPuan();
                gameManager.EkstraPaneli(4);
               

            }else if(gameObject.tag== "yýldýzlýiki")
            {

                gameManager.ekstraPuanIki();
                gameManager.EkstraPaneli(5);


            }

        }
        else if (collision.gameObject.CompareTag("oyunbitti"))
        {
            gameObject.SetActive(false);
            gameManager.OyunBitti();

        }


    }
    private void OnCollisionEnter2D(Collision2D collisio)
    {
        if (collisio.gameObject.CompareTag("Player"))
        {
            
            duvarses.Play();

        }
    }

 



}
