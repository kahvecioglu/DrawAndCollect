using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TopAtmaMerkezi : MonoBehaviour
{
    [SerializeField] private GameObject[] toplar;
    [SerializeField] private GameObject topAtmaMerkezi;
    [SerializeField] private GameObject pota;
    [SerializeField] private GameObject[] pota_noktalari;

    public List<Sprite> spriteList = new List<Sprite>();


    SpriteRenderer spriteRenderer;

    int randomPotaIndexi;
    public static int aktifTopIndex;
    int aktifIndex;
    public static bool kilit;

    public static int atilantopsayisi;
    public static int topatissayisi;


    private void Start()
    {
        topatissayisi=0;
        atilantopsayisi=0;
        
    }



    public void AtisBaslasin()
    {
       
        StartCoroutine(TopAtisSistemi());
    }


    IEnumerator TopAtisSistemi()
    {

        while (true)
        {
            if(!kilit)
            {

                yield return new WaitForSeconds(0.5f);

                if(topatissayisi !=0 && topatissayisi %3 == 0 && topatissayisi>10)
                {
                    for(int i=0; i<2; i++)
                    {
                        spriteRenderer = toplar[aktifTopIndex].GetComponent<SpriteRenderer>();
                        toplar[aktifTopIndex].transform.position = topAtmaMerkezi.transform.position;
                        if (PlayerPrefs.GetInt("Aktifindex") != -1 && !toplar[aktifTopIndex].CompareTag("yýldýzlý") && !toplar[aktifTopIndex].CompareTag("yýldýzlýiki")) 
                        {
                            spriteRenderer.sprite = spriteList[PlayerPrefs.GetInt("Aktifindex")];

                        }
                       
                       
                        toplar[aktifTopIndex].SetActive(true);
                        

                        Vector3 pozisyon = Quaternion.AngleAxis(AciVer(70f, 110f), Vector3.forward) * Vector3.right;
                        toplar[aktifTopIndex].GetComponent<Rigidbody2D>().AddForce(pozisyon * 750);


                        if (aktifTopIndex != toplar.Length - 1)
                        {
                            aktifTopIndex++;

                        }
                        else
                        {
                            aktifTopIndex = 0;
                        }


                    }

                    atilantopsayisi = 2;
                    topatissayisi++;


                }
                else
                {
                    
                    spriteRenderer = toplar[aktifTopIndex].GetComponent<SpriteRenderer>();
                    toplar[aktifTopIndex].transform.position = topAtmaMerkezi.transform.position;
                    if (PlayerPrefs.GetInt("Aktifindex") != -1 && !toplar[aktifTopIndex].CompareTag("yýldýzlý") && !toplar[aktifTopIndex].CompareTag("yýldýzlýiki"))
                    {
                        spriteRenderer.sprite = spriteList[PlayerPrefs.GetInt("Aktifindex")];

                    }


                    toplar[aktifTopIndex].SetActive(true);


                    Vector3 pozisyon = Quaternion.AngleAxis(AciVer(70f, 110f), Vector3.forward) * Vector3.right;
                    toplar[aktifTopIndex].GetComponent<Rigidbody2D>().AddForce(pozisyon * 750);


                    if (aktifTopIndex != toplar.Length - 1)
                    {
                        aktifTopIndex++;

                    }
                    else
                    {
                        aktifTopIndex = 0;
                    }

                    atilantopsayisi = 1;
                    topatissayisi++;

                }
               


                yield return new WaitForSeconds(0.7f);
                randomPotaIndexi = Random.Range(0, pota_noktalari.Length - 1);
                pota.transform.position = pota_noktalari[randomPotaIndexi].transform.position;
                pota.SetActive(true);
                kilit = true;

            }
            else
            {



                yield return null;
            }


        }


    }



    float AciVer(float deger1,float deger2)
    {

        return Random.Range(deger1,deger2);

    }


    public void DevamEt()
    {
        if (atilantopsayisi == 1)
        {
            kilit = false;
            pota.SetActive(false);
            atilantopsayisi--;
        }
        else
        {
            atilantopsayisi--;

        }
        
    }

    public void TopAtmaDurdur()
    {
        StopAllCoroutines();

    }

   public void PotayiFalsseT()
    {
        pota.SetActive(false);

    }

   

}
