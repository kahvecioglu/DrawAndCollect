using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using GoogleMobileAds.Api;

namespace Bugra

// Start is called before the first frame update
{

    public class BellekYonetim
    {

        public void KontrolEtVeTanýmla()
        {
            if (!PlayerPrefs.HasKey("Puan"))
            {

                PlayerPrefs.SetInt("Puan", 0);
                PlayerPrefs.SetInt("Aktifindex", -1);
                PlayerPrefs.SetInt("GecisReklamSayisi", 0);


            }


        }
    }


    [Serializable]
    public class ItemBilgileri
    {
        public int itemIndex;
        public string item_ad;
        public int puan;
        public bool satinAlmaDurumu;

    }

    public class VeriYonetimi
    {


        public void Save(List<ItemBilgileri> _ItemBilgileri)
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemVerileri2.gd");
            bf.Serialize(file, _ItemBilgileri);
            file.Close();


        }
        public void IlkKurulum(List<ItemBilgileri> _ItemBilgileri)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemVerileri2.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri2.gd");
                bf.Serialize(file, _ItemBilgileri);
                file.Close();
            }




        }

        List<ItemBilgileri> _ItemIcliste;
        public void Load()
        {

            if (File.Exists(Application.persistentDataPath + "/ItemVerileri2.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri2.gd", FileMode.Open);
                _ItemIcliste = (List<ItemBilgileri>)bf.Deserialize(file);
                file.Close();

            }

        }

        public List<ItemBilgileri> ListeyiAktar()
        {


            return _ItemIcliste;

        }

    }

    public class ReklamYonetimi
    {

        private InterstitialAd interstitialAd;
        private RewardedAd RewardedAd;


        // ----------GEÇÝÞ REKLAMI

        public void RequestInterstitial()
        {
            
            string AdUnitId;
#if UNITY_ANDROID
            AdUnitId = "ca-app-pub-2857887652652750/4131304435";
#elif UNITY_IPHONE
                AdUnitId="ca-app-pub-3940256099942544/4411468910";
#else
                AdUnitId="unexpected_platform";
#endif



            interstitialAd = new InterstitialAd(AdUnitId);
            AdRequest request = new AdRequest.Builder().Build();
            interstitialAd.LoadAd(request);

            interstitialAd.OnAdClosed += GecisReklamiKapatildi;


        }
        void GecisReklamiKapatildi(object sender, EventArgs args)
        {
            interstitialAd.Destroy();
            RequestInterstitial();

        }
        public void GecisReklamiGoster()
        {

            if (PlayerPrefs.GetInt("GecisReklamSayisi") == 2)
            {
                if (interstitialAd.IsLoaded())
                {
                    PlayerPrefs.SetInt("GecisReklamSayisi", 0);
                    interstitialAd.Show();

                }
                else
                {
                    interstitialAd.Destroy();
                    RequestInterstitial();
                }

            }
            else
            {
                PlayerPrefs.SetInt("GecisReklamSayisi", PlayerPrefs.GetInt("GecisReklamSayisi") + 1);


            }


        }


        //--------ÖDÜLLÜ REKLAM

        public void RequestRewardedAd()
        {

            string AdUnitId;
#if UNITY_ANDROID
            AdUnitId = "ca-app-pub-2857887652652750/3320785648";
#elif UNITY_IPHONE
                AdUnitId="ca-app-pub-3940256099942544/1712485313";
#else
                AdUnitId="unexpected_platform";
#endif



            RewardedAd = new RewardedAd(AdUnitId);
            AdRequest request = new AdRequest.Builder().Build();
            RewardedAd.LoadAd(request);

            RewardedAd.OnUserEarnedReward += OdulluReklamTamamlandi;
            RewardedAd.OnAdClosed += OdulluReklamKapatildi;
            RewardedAd.OnAdLoaded += OdulluReklamYuklendi;




        }

        private void OdulluReklamTamamlandi(object sender, Reward e)
        {
            string type = e.Type;
            double amount = e.Amount;
            Debug.Log("ödül alýnsýn  " + type + "    " + amount);
            PlayerPrefs.SetInt("Puan", PlayerPrefs.GetInt("Puan") + 20);
        }
        private void OdulluReklamKapatildi(object sender, EventArgs e)
        {
            Debug.Log("reklam kapatýldý");
            RequestRewardedAd();
        }

        private void OdulluReklamYuklendi(object sender, EventArgs e)
        {
            Debug.Log("reklam yüklendi");
        }

        public void OdulluReklamGoster()
        {
            if (RewardedAd.IsLoaded())
            {
                RewardedAd.Show();


            }
        }

    }
}



















    

