using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AdminPanel : MonoBehaviour
{
    public TMP_InputField inputField;

    public GameObject adminButton;

    public List<GameObject> allScenes; //Sahne objelerini Inspector k�sm�ndan s�r�kle b�rak yapman�z gerekmektedir. (�u anda 120 obje mevcut, sahne ekledik�e inspector k�sm�ndan liste de�erine ekleyiniz. Liste de�erine eklenmeyen objeler bulunamaz.)

    public void GetScenes()
    {
        //Bu kod blo�u sahne ismi mant���na g�re �al���r. Text Input k�sm�na istedi�iniz sahnenin numaras�n� yaz�n. Ard�ndan sahneye git butonuna bas�n. �stedi�iniz sahne a��lmaktad�r. �rnek: Input k�sm�na 61 yazd���n�zda, Scene61 ve OverlayCanvas61 objeleri aktif hale gelir. L�tfen obje isimlerini bu formata uygun yap�n�z.

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
