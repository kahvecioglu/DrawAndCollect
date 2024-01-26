using System.Collections;
using System.Collections.Generic;
using Bugra;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ozellestirme_Manager : MonoBehaviour
{
    int aktifIndex = -1;
    public TextMeshProUGUI[] textler;   
    Image resim;
    Image kusanresmi;
    public Sprite varsayilanSprite;
    public List<ItemBilgileri> Varsayilan_ItemBilgileri = new List<ItemBilgileri>();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    BellekYonetim _BellekYonetim= new BellekYonetim();

    public Animator[] islemAnimasyonlari;
    public AudioSource[] sesler;
 
    public List<Sprite> spriteList = new List<Sprite>();
    public GameObject top;
    public GameObject kusanbutonu;
    public Button[] ilerigeriButonlar;
    public Button[] islemButonlar;
    void Start()
    {
        _BellekYonetim.KontrolEtVeTanýmla();
        textler[1].text = "DEFAULT";
        textler[0].text = "FREE";
        islemButonlar[0].interactable = false;
        sesler[0].Stop();
        islemButonlar[1].interactable = true;
        _VeriYonetimi.IlkKurulum(Varsayilan_ItemBilgileri);
        resim = top.GetComponent<Image>();
        kusanresmi=kusanbutonu.GetComponent<Image>();
        _VeriYonetimi.Load();
        Varsayilan_ItemBilgileri = _VeriYonetimi.ListeyiAktar();        
        textler[2].text = PlayerPrefs.GetInt("Puan").ToString();
        kusanresmi.sprite = varsayilanSprite;
       

    }

    // Update is called once per frame
  

 
    public void AnamenuyeDon()
    {
        sesler[0].Play();

        _VeriYonetimi.Save(Varsayilan_ItemBilgileri);
        SceneManager.LoadScene(0);

    }

    public void IleriGeri(string yon)
    {
        

        sesler[0].Play();
        if (yon == "ileri")
        {
            
            if (aktifIndex == -1)
            {

                
                ilerigeriButonlar[1].interactable = true;
                ilerigeriButonlar[0].interactable = true;
                aktifIndex = 0;
                kusanresmi.sprite = spriteList[aktifIndex];
                Sprite yenisprit = spriteList[aktifIndex];                
                resim.sprite= yenisprit;
                textler[1].text = Varsayilan_ItemBilgileri[aktifIndex].item_ad;
                textler[0].text = Varsayilan_ItemBilgileri[aktifIndex].puan.ToString();

                if (Varsayilan_ItemBilgileri[aktifIndex].satinAlmaDurumu == true)
                {
                    islemButonlar[0].interactable = false;
                    islemButonlar[1].interactable = true;

                }
                else
                {
                    
                    islemButonlar[0].interactable = true;
                    islemButonlar[1].interactable = false;

                }

            }

            else
            {

                ilerigeriButonlar[1].interactable = true;
                aktifIndex++;
                kusanresmi.sprite = spriteList[aktifIndex];

                Sprite yenisprit = spriteList[aktifIndex];
                resim.sprite = yenisprit;
                textler[1].text = Varsayilan_ItemBilgileri[aktifIndex].item_ad;
                textler[0].text = Varsayilan_ItemBilgileri[aktifIndex].puan.ToString();

                if (Varsayilan_ItemBilgileri[aktifIndex].satinAlmaDurumu == true)
                {
                    islemButonlar[0].interactable = false;
                    islemButonlar[1].interactable = true;

                }
                else
                {
                    
                        islemButonlar[0].interactable = true;
                    islemButonlar[1].interactable = false;

                }
            }

           if(aktifIndex == spriteList.Count-1)
            {
                ilerigeriButonlar[0].interactable = false;


            }

           if (PlayerPrefs.GetInt("Puan") < Varsayilan_ItemBilgileri[aktifIndex].puan)
            {
                islemButonlar[0].interactable = false;
                

            }

            if (Varsayilan_ItemBilgileri[aktifIndex].satinAlmaDurumu==true)
            {

                islemButonlar[1].interactable = true;
            }
            else
            {

                islemButonlar[1].interactable = false;
            }



        }
        else if (yon == "geri")
        {

            if (aktifIndex == -1)
            {
                kusanresmi.sprite = varsayilanSprite;
                islemButonlar[1].interactable = true;
                ilerigeriButonlar[1].interactable=false;
                Sprite yenisprit = varsayilanSprite;
                resim.sprite = yenisprit;
                textler[1].text = Varsayilan_ItemBilgileri[aktifIndex].item_ad;
                textler[0].text = Varsayilan_ItemBilgileri[aktifIndex].puan.ToString();

            

            }

            else
            {
                if (aktifIndex!= 0)
                {

                    aktifIndex--;
                    kusanresmi.sprite = spriteList[aktifIndex];

                    ilerigeriButonlar[1].interactable = true;
                    ilerigeriButonlar[0].interactable = true;
                    Sprite yenisprit = spriteList[aktifIndex];
                    resim.sprite = yenisprit;
                    textler[1].text = Varsayilan_ItemBilgileri[aktifIndex].item_ad;
                    textler[0].text = Varsayilan_ItemBilgileri[aktifIndex].puan.ToString();

                    if (Varsayilan_ItemBilgileri[aktifIndex].satinAlmaDurumu == true)
                    {
                        islemButonlar[0].interactable = false;
                        islemButonlar[1].interactable = true;

                    }
                    else
                    {
                       
                            islemButonlar[0].interactable = true;
                            islemButonlar[1].interactable = false;

                    }


                    if (PlayerPrefs.GetInt("Puan") < Varsayilan_ItemBilgileri[aktifIndex].puan)
                    {
                        islemButonlar[0].interactable = false;
                       

                    }


                    if (Varsayilan_ItemBilgileri[aktifIndex].satinAlmaDurumu == true)
                    {

                        islemButonlar[1].interactable = true;
                    }
                    else
                    {

                        islemButonlar[1].interactable = false;
                    }

                }
                else
                {
                    aktifIndex = -1;
                    kusanresmi.sprite = varsayilanSprite;

                    islemButonlar[0].interactable = false;
                    islemButonlar[1].interactable = true;
                    ilerigeriButonlar[1].interactable = false;
                    Sprite yenisprit = varsayilanSprite;
                    resim.sprite = yenisprit;
                    textler[1].text = "DEFAULT";
                    textler[0].text = "FREE";

                }


          


            }

            


        }








    }

    public void SatinAl()
    {
        sesler[1].Play();
        islemAnimasyonlari[2].SetBool("SatinAlindiIki", true);
        islemAnimasyonlari[0].SetBool("SatinAlindi", true);
        Varsayilan_ItemBilgileri[aktifIndex].satinAlmaDurumu = true;
        islemButonlar[0].interactable=false;
        islemButonlar[1].interactable = true;
        PlayerPrefs.SetInt("Puan", PlayerPrefs.GetInt("Puan") - Varsayilan_ItemBilgileri[aktifIndex].puan);
        textler[2].text = PlayerPrefs.GetInt("Puan").ToString();



    }

    public void Kaydet()
    {
        sesler[2].Play();
        islemAnimasyonlari[1].SetBool("Kusanildi", true);
        islemAnimasyonlari[2].SetBool("KusanildiIki", true);
        PlayerPrefs.SetInt("Aktifindex", aktifIndex);

    }


    
}
