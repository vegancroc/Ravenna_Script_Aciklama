using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ParabolicMotion : MonoBehaviour
{
    public Transform startingPoint;

    public Transform endingPoint;

    public float curveValue = .5f;

    public TextMeshProUGUI hizText;

    public TextMeshProUGUI enerjiText;

    public TextMeshProUGUI turZamaniText;

    public TextMeshProUGUI enerjilerText;

    public Slider gravity;

    public Slider friction;

    public Slider mass;

    private Vector3 point3;

    public SpriteRenderer sr;

    bool changeDirection = false;

    float count = 0;

    float heightSpeedValue = .5f;

    float hiz; //Kullanýlacak deðer.

    float enerji; //Kullanýlacak deðer.

    float birTurZamani; //Kullanýlacak deðer.

    private void Start()
    {
        point3 = startingPoint.position + (endingPoint.position - startingPoint.position) / 2 + Vector3.up * curveValue;
    }

    private void OnTriggerEnter2D(Collider2D collision) //Sahnedeki colliderlara göre objenin hareketi heightSpeedValue deðiþkeni ile kontrol edilir. (FixedUpdate içerisindeki kod bloðu)
    {
        if (collision.name == "upPoint" || collision.name == "lastPoint")
        {
            heightSpeedValue = 0.05f;
        }

        if (collision.name == "middlePoint")
        {
            heightSpeedValue = .3f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        heightSpeedValue = .1f;
    }

    public void FixedUpdate()
    {
        hiz = (10 * heightSpeedValue) / friction.value * (1/mass.value) * (1/gravity.value); //Ekrana yazýlacak olan hýz deðeri. (Objenin hareketi için kullanýlmaz.) Eðer fizik hesaplamalarýnda hata bulunuyorsa deðerlerle oynayabilirsiniz.

        enerji = (mass.value * hiz * hiz) + (mass.value * gravity.value * 1 / heightSpeedValue) + friction.value; //Ayný þekilde sadece ekrana yazdýrýlmak için kullanýlýr. Eðer fizik hesaplamalarýnda hata bulunuyorsa deðerlerle oynayabilirsiniz. Mesela burada ekstra ýsý enerjisi hesaplanmak yerine direkt friction deðeri üzerine ýsý enerjisi gibi eklenmiþtir.

        enerjilerText.text = "Kinetik Enerji: " + Mathf.Round((mass.value * hiz * hiz)).ToString() + "\nPotansiyel Enerji: " + Mathf.Round((mass.value * gravity.value * 1 / heightSpeedValue)).ToString() + "\nIsý Enerjisi:" + (friction.value) + "\nToplam Enerji: " + Mathf.Round(enerji).ToString();
        //Tüm enerji türlerinin ekrana yazdýrýlmasý için mevcut deðerlerin hesaplanýr ve text'e eþitlenir.

        hizText.text = "Hýz: " + Mathf.Round(hiz).ToString() + " m/s"; //Ekrana yazdýrmak için kod bloðu

        enerjiText.text = "Enerji: " + Mathf.Round(enerji).ToString() + " Joule"; //Ekrana yazdýrmak için kod bloðu

        if (birTurZamani.ToString().ToCharArray().Length > 1) //Virgülden sonraki basamaðýn yazýlmamasý için kod bloðu
        {
            turZamaniText.text = "Tur Zamaný: " + birTurZamani.ToString().Substring(0, 1) + " saniye";
        }

        else
        {
            turZamaniText.text = "Tur Zamaný: " + birTurZamani.ToString() + " saniye";
        }


        if (count < 1)
        {
            count += (heightSpeedValue * Time.fixedDeltaTime) / friction.value * (1/mass.value) * (1/gravity.value); //count'u hareketin kaç hamlede yapýlacaðý gibi düþünün. Slider'larýn friction, mass ve gravity deðerlerine göre count deðerini deðiþtiriyoruz. Böylece objenin hareketi de deðiþmiþ oluyor. Eðer fizik hesaplamalarýnda hata bulunuyorsa, bu deðerlerle oynayabilirsiniz. 

            birTurZamani += Time.fixedDeltaTime; //Bir tur zamanýnýn hesaplanýþ þekli. Hareket baþladýðýnda kaç saniye geçtiði hesaplanýr sonrasýnda sýfýrlanýr.
            Vector3 m1 = Vector3.zero;
            Vector3 m2 = Vector3.zero;

            if (changeDirection == false) //Parabolik hareket için kod bloðu (changeDirection hareketin yön deðiþtirmesi için gereken deðiþken.))
            {
                m1 = Vector3.Lerp(startingPoint.position, point3, count);
                m2 = Vector3.Lerp(point3, endingPoint.position, count);
            }
            else
            {
                m1 = Vector3.Lerp(endingPoint.position, point3, count);
                m2 = Vector3.Lerp(point3, startingPoint.position, count);
            }


            this.transform.position = Vector3.Lerp(m1, m2, count);
        }
        else
        {
            birTurZamani = 0;
            MoveAgain(); 
        }
    }

    public void MoveAgain() //Hareketin devamý için count deðerini sýfýra eþitler ayrýca sprite'ý döndürür.
    {

        count = 0;
        changeDirection = !changeDirection;
        sr.flipX = !sr.flipX;

    }

}
