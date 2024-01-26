using System.Collections;
using System.Collections.Generic;
using Bugra;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("-----TOP VE TEKNÝK OBJELER")]
    [SerializeField] private TopAtmaMerkezi topAtmaMerkezi;
    [SerializeField] private CizgiCiz cizgiCiz;

    public TextMeshProUGUI cizmehakkitext;
    int yildizSayici;
    int yildizSayiciIki;
    private static int hangipanel;

    bool panelciktimi;
    ReklamYonetimi _ReklamYonetimi=new ReklamYonetimi();
    public GameObject upperBoundary;
    private Camera camera;

    int girenTopSayisi;
    [Header("-----GENEL OBJELER")]
    [SerializeField] private ParticleSystem potaGirme;
    [SerializeField] private ParticleSystem bestScoreGecis;
    
    [SerializeField] private AudioSource[] sesler;
    [SerializeField] private GameObject[] toplar;

    [Header("-----UI OBJELER")]
    [SerializeField] private GameObject[] paneller;
    [SerializeField] private TextMeshProUGUI[] scoreTextleri;
   

    void Start()
    {
        camera = Camera.main; // Ana kamerayý al

        AdjustCamera();
        _ReklamYonetimi.RequestInterstitial();
        _ReklamYonetimi.RequestRewardedAd();



        yildizSayici = 0;
        panelciktimi=false;





        if (PlayerPrefs.HasKey("BestScore"))
        {
            scoreTextleri[0].text = PlayerPrefs.GetInt("BestScore").ToString();
            scoreTextleri[1].text = PlayerPrefs.GetInt("BestScore").ToString();
            scoreTextleri[4].text = PlayerPrefs.GetInt("Puan").ToString();


        }
        else
        {
            PlayerPrefs.SetInt("BestScore",0);
            scoreTextleri[0].text = "0";
            scoreTextleri[1].text = "0";
            PlayerPrefs.SetInt("Puan", 0);
            scoreTextleri[4].text= "0";
            PlayerPrefs.SetInt("Aktifindex", -1);
            PlayerPrefs.SetInt("GecisReklamSayisi", 0);







        }



    }

    public void AdjustCamera()
    {
        float cameraHeight = Camera.main.orthographicSize;
        // Üst sýnýr gameobject'inin pozisyonunu güncelleyin
        upperBoundary.transform.position = new Vector3(upperBoundary.transform.position.x, cameraHeight+3, upperBoundary.transform.position.z);

    }
    void Update()
    {
       
        scoreTextleri[4].text = PlayerPrefs.GetInt("Puan").ToString();
        scoreTextleri[3].text = girenTopSayisi.ToString();

        if (girenTopSayisi > PlayerPrefs.GetInt("BestScore") && !panelciktimi)
        {

            paneller[3].SetActive(true);
            Invoke("YeniSkorPanelÝptal", 5f);
          
        }


       
    }

    void YeniSkorPanelÝptal()
    {

        paneller[3].SetActive(false);
        panelciktimi = true;

    }
    public void DevamEt(Vector2 pozisyon)
    {

        potaGirme.transform.position = pozisyon;
        potaGirme.gameObject.SetActive(true);
        potaGirme.Play();

        girenTopSayisi++;
        sesler[0].Play();

       topAtmaMerkezi.DevamEt();
        cizgiCiz.DevamEt();
    }

    public void OyunBitti()
    {
        foreach (var item in toplar)
        {
            if (item.activeSelf)
            {
                item.SetActive(false);

            }


        }


        _ReklamYonetimi.GecisReklamiGoster();

        cizgiCiz.saatsifirla();
        paneller[1].SetActive(true);
        paneller[2].SetActive(false);
        sesler[1].Play();

        scoreTextleri[1].text = PlayerPrefs.GetInt("BestScore").ToString();
        scoreTextleri[2].text = girenTopSayisi.ToString();
        scoreTextleri[5].text= girenTopSayisi.ToString();
        PlayerPrefs.SetInt("Puan", PlayerPrefs.GetInt("Puan") + girenTopSayisi);

        if (girenTopSayisi > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", girenTopSayisi);
            scoreTextleri[1].text= PlayerPrefs.GetInt("BestScore").ToString();

            bestScoreGecis.gameObject.SetActive(true);
            bestScoreGecis.Play();

        }

        topAtmaMerkezi.TopAtmaDurdur();
        cizgiCiz.CizgiCiziDurdur();

        

        
    }

    public void OyunBaslasin()
    {
        TopAtmaMerkezi.kilit = false;
        sesler[3].Play();
        paneller[0].SetActive(false);
        topAtmaMerkezi.AtisBaslasin();
        cizgiCiz.CizgiCizBaslat();
        paneller[2].SetActive(true);
        
    }

    public void YenidenOyna(string buton)
    {
        sesler[2].Play();
        if (buton == "Menu")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            TopAtmaMerkezi.aktifTopIndex = 0;
            yildizSayici = 0;
        }
        else if (buton == "Tekrar")
        {
            
           TopAtmaMerkezi.topatissayisi = 0;
           TopAtmaMerkezi.atilantopsayisi = 0;
            CizgiCiz.cizmeHakki = 3;
            girenTopSayisi= 0;
            TopAtmaMerkezi.aktifTopIndex = 0;
            cizmehakkitext.text = CizgiCiz.cizmeHakki.ToString();
            topAtmaMerkezi.PotayiFalsseT();                     
            TopAtmaMerkezi.kilit = false;
            topAtmaMerkezi.AtisBaslasin();
            cizgiCiz.CizgiCizBaslat();
            sesler[3].Play();
            paneller[0].SetActive(false);
            paneller[1].SetActive(false);
            paneller[2].SetActive(true);
            panelciktimi = false;



            if (TopAtmaMerkezi.atilantopsayisi == 0)
            {
                foreach (var item in cizgiCiz.cizgiler)
                {
                    Destroy(item.gameObject);

                }

                cizgiCiz.cizgiler.Clear();



            }


        }
    }

    public void ekstraPuan()
    {
        yildizSayici++;
        int sonuc = yildizSayici * 5;
        scoreTextleri[6].text = sonuc.ToString();       
        
        PlayerPrefs.SetInt("Puan", PlayerPrefs.GetInt("Puan") + 5);       
           
    }
    public void ekstraPuanIki()
    {
        yildizSayiciIki++;
        int sonuc = yildizSayiciIki * 10;
        scoreTextleri[7].text = sonuc.ToString();

        PlayerPrefs.SetInt("Puan", PlayerPrefs.GetInt("Puan") + 10);

    }

    public void SahneSec(string isim)
    {
        sesler[2].Play();

        if (isim == "Shopping")
        {
          
            SceneManager.LoadScene(1);

        }


    }

    public void OdulluReklamGoster()
    {

        _ReklamYonetimi.OdulluReklamGoster();


    }

    public void EkstraPaneli(int sayi)
    {
        hangipanel = sayi;
        paneller[sayi].SetActive(true);
        Invoke("EkstraPanelleriKapat",2);

    }

    public void EkstraPanelleriKapat()
    {
        paneller[hangipanel].SetActive(false);


    }


}
