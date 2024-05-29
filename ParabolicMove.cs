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

    float hiz; //Kullan�lacak de�er.

    float enerji; //Kullan�lacak de�er.

    float birTurZamani; //Kullan�lacak de�er.

    private void Start()
    {
        point3 = startingPoint.position + (endingPoint.position - startingPoint.position) / 2 + Vector3.up * curveValue;
    }

    private void OnTriggerEnter2D(Collider2D collision) //Sahnedeki colliderlara g�re objenin hareketi heightSpeedValue de�i�keni ile kontrol edilir. (FixedUpdate i�erisindeki kod blo�u)
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
        hiz = (10 * heightSpeedValue) / friction.value * (1/mass.value) * (1/gravity.value); //Ekrana yaz�lacak olan h�z de�eri. (Objenin hareketi i�in kullan�lmaz.) E�er fizik hesaplamalar�nda hata bulunuyorsa de�erlerle oynayabilirsiniz.

        enerji = (mass.value * hiz * hiz) + (mass.value * gravity.value * 1 / heightSpeedValue) + friction.value; //Ayn� �ekilde sadece ekrana yazd�r�lmak i�in kullan�l�r. E�er fizik hesaplamalar�nda hata bulunuyorsa de�erlerle oynayabilirsiniz. Mesela burada ekstra �s� enerjisi hesaplanmak yerine direkt friction de�eri �zerine �s� enerjisi gibi eklenmi�tir.

        enerjilerText.text = "Kinetik Enerji: " + Mathf.Round((mass.value * hiz * hiz)).ToString() + "\nPotansiyel Enerji: " + Mathf.Round((mass.value * gravity.value * 1 / heightSpeedValue)).ToString() + "\nIs� Enerjisi:" + (friction.value) + "\nToplam Enerji: " + Mathf.Round(enerji).ToString();
        //T�m enerji t�rlerinin ekrana yazd�r�lmas� i�in mevcut de�erlerin hesaplan�r ve text'e e�itlenir.

        hizText.text = "H�z: " + Mathf.Round(hiz).ToString() + " m/s"; //Ekrana yazd�rmak i�in kod blo�u

        enerjiText.text = "Enerji: " + Mathf.Round(enerji).ToString() + " Joule"; //Ekrana yazd�rmak i�in kod blo�u

        if (birTurZamani.ToString().ToCharArray().Length > 1) //Virg�lden sonraki basama��n yaz�lmamas� i�in kod blo�u
        {
            turZamaniText.text = "Tur Zaman�: " + birTurZamani.ToString().Substring(0, 1) + " saniye";
        }

        else
        {
            turZamaniText.text = "Tur Zaman�: " + birTurZamani.ToString() + " saniye";
        }


        if (count < 1)
        {
            count += (heightSpeedValue * Time.fixedDeltaTime) / friction.value * (1/mass.value) * (1/gravity.value); //count'u hareketin ka� hamlede yap�laca�� gibi d���n�n. Slider'lar�n friction, mass ve gravity de�erlerine g�re count de�erini de�i�tiriyoruz. B�ylece objenin hareketi de de�i�mi� oluyor. E�er fizik hesaplamalar�nda hata bulunuyorsa, bu de�erlerle oynayabilirsiniz. 

            birTurZamani += Time.fixedDeltaTime; //Bir tur zaman�n�n hesaplan�� �ekli. Hareket ba�lad���nda ka� saniye ge�ti�i hesaplan�r sonras�nda s�f�rlan�r.
            Vector3 m1 = Vector3.zero;
            Vector3 m2 = Vector3.zero;

            if (changeDirection == false) //Parabolik hareket i�in kod blo�u (changeDirection hareketin y�n de�i�tirmesi i�in gereken de�i�ken.))
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

    public void MoveAgain() //Hareketin devam� i�in count de�erini s�f�ra e�itler ayr�ca sprite'� d�nd�r�r.
    {

        count = 0;
        changeDirection = !changeDirection;
        sr.flipX = !sr.flipX;

    }

}
