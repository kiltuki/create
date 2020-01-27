using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemMouseGet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    //アイテムイメージ取得用変数
    public Image itemImage
    {
        get
        {
            return GetComponent<Image>();
        }
    }

    //Imageアイテムの上でマウスオーバーした時の処理
    public void OnPointerEnter(PointerEventData eventData)
    {
        //アイテムImageについているタグによってボタンの表示分岐を行う
        if (itemImage.CompareTag("i_time"))
        {       
            CanvasControl.SetActive("TimerAdd", true);
        }
        else if (itemImage.CompareTag("i_open"))
        {
            CanvasControl.SetActive("CardAllOpen", true);
        }
    }

    //マウスオーバーをやめた時の処理
    public void OnPointerExit(PointerEventData eventData)
    {   
        Destroy(itemImage);
        ItemControl.itemNum--;
    }
}
