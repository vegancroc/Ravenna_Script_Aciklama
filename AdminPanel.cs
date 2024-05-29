using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AdminPanel : MonoBehaviour
{
    public TMP_InputField inputField;

    public GameObject adminButton;

    public List<GameObject> allScenes; //Sahne objelerini Inspector kýsmýndan sürükle býrak yapmanýz gerekmektedir. (Þu anda 120 obje mevcut, sahne ekledikçe inspector kýsmýndan liste deðerine ekleyiniz. Liste deðerine eklenmeyen objeler bulunamaz.)

    public void GetScenes()
    {
        //Bu kod bloðu sahne ismi mantýðýna göre çalýþýr. Text Input kýsmýna istediðiniz sahnenin numarasýný yazýn. Ardýndan sahneye git butonuna basýn. Ýstediðiniz sahne açýlmaktadýr. Örnek: Input kýsmýna 61 yazdýðýnýzda, Scene61 ve OverlayCanvas61 objeleri aktif hale gelir. Lütfen obje isimlerini bu formata uygun yapýnýz.

        List<GameObject> foundScenes1 = allScenes.FindAll(scene => scene.name == ("Scene" + inputField.text));
        List<GameObject> foundScenes2 = allScenes.FindAll(scene => scene.name == ("OverlayCanvas" + inputField.text));

        foreach (GameObject scene in foundScenes1)
        {
            scene.SetActive(true);
        }

        foreach (GameObject scene in foundScenes2)
        {
            scene.SetActive(true);
        }

        adminButton.SetActive(true);

        this.gameObject.SetActive(false);

    }

    public void CloseScenes()
    {
        foreach (GameObject item in allScenes)
        {
            item.SetActive(false);
        }
    }
}
