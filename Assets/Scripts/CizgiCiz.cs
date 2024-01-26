using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CizgiCiz : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject cizgi;
    [SerializeField] private GameManager gameManager;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> parmakPozisyonListesi;
    public List<GameObject> cizgiler;
    [SerializeField] private TextMeshProUGUI cizmehakkitext;
    [SerializeField] private TextMeshProUGUI saatText;

    public static bool cizmekMumkunmu;
    public static int cizmeHakki;
    public static float saat = 6;

    private void Start()
    {
        cizmekMumkunmu = false;
        cizmeHakki = 3;
        saat = 6;
        saatText.text = saat.ToString();   
        cizmehakkitext.text=cizmeHakki.ToString();
    }
    void Update()
    {

        if(saat>0 && cizmekMumkunmu==true) 
        
        {
            saat -= Time.deltaTime;  // zamansayacý=zamansayacý-Time.deltaTime demek
            saatText.text = (int)saat + "";


            if (cizmekMumkunmu == true && cizmeHakki != 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CizgiOlustur();
                }

                if (Input.GetMouseButton(0))
                {
                    Vector2 parmakPozisyonu = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    if (Vector2.Distance(parmakPozisyonu, parmakPozisyonListesi[^1]) > 0.1f)
                    {
                        CizgiyiGuncelle(parmakPozisyonu);

                    }

                }

            }

        }

           
        
        if(cizgiler.Count!=0 && cizmeHakki != 0)
        {

            if(Input.GetMouseButtonUp(0))
            {
                cizmeHakki--;
                cizmehakkitext.text= cizmeHakki.ToString();


            }



        }
        if (cizmeHakki<0 || saat<0)
        {
           
            gameManager.OyunBitti();
           
           
            
        }

        

    }

    void CizgiOlustur()
    {
        cizgi=Instantiate(linePrefab,Vector2.zero,Quaternion.identity);
        cizgiler.Add(cizgi);
        lineRenderer=cizgi.GetComponent<LineRenderer>();
        edgeCollider=cizgi.GetComponent<EdgeCollider2D>();
        parmakPozisyonListesi.Clear();

        parmakPozisyonListesi.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        parmakPozisyonListesi.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        lineRenderer.SetPosition(0, parmakPozisyonListesi[0]);
        lineRenderer.SetPosition(1, parmakPozisyonListesi[1]);

        edgeCollider.points=parmakPozisyonListesi.ToArray();
    }

    void CizgiyiGuncelle(Vector2 gelenParmakPozisyon)
    {
        parmakPozisyonListesi.Add(gelenParmakPozisyon);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, gelenParmakPozisyon);
        edgeCollider.points = parmakPozisyonListesi.ToArray();
    }

    public void DevamEt()
    {
        if(TopAtmaMerkezi.atilantopsayisi==0)
        {
            foreach (var item in cizgiler)
            {
                Destroy(item.gameObject);

            }

            cizgiler.Clear();
            cizmeHakki = 3;
            cizmehakkitext.text = cizmeHakki.ToString();
            saat = 6;
            saatText.text = saat.ToString();


        }
     
    }

    public void CizgiCiziDurdur()
    {

        cizmekMumkunmu = false;


    }
    public void CizgiCizBaslat()
    {
        cizmeHakki = 3;
        saat = 6;
        cizmekMumkunmu = true;
    }

  
    public void saatsifirla()
    {

        saat = 0;
        cizmeHakki = 0;

    }

}
