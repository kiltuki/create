using UnityEngine;

public class CardCheck : MonoBehaviour
{
    //CardManagerを保持する変数
    public GameObject gManager = null;
    //カードの種類を判別するための変数
    public int id;
    //カードがめくられているかどうかを判別するフラグ
    public bool open = false;

    //カード消滅時のエフェクトを格納する配列
    public GameObject[] effects;
    //エフェクトのインスタンスを保存する変数
    public GameObject expl;

    AudioSource source;
    //カードをめくる際の効果音
    public AudioClip OpenCard;
    //カードの絵柄が合致したときの消滅音
    public AudioClip destroyCard;
    //消滅音を鳴らすかどうかのフラグ
    private static bool destroySoundPlay = false;

    void Start()
    {
        open = false;
        source = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        //消滅音を鳴らすためのトリガー
        if (destroySoundPlay)
        {
            source.PlayOneShot(destroyCard);
            destroySoundPlay = false;
        }
    }

    //カードをめくる
    public void Open()
    {
        if (open) return;

        source = GetComponent<AudioSource>();
        source.PlayOneShot(OpenCard);
        iTween.RotateAdd(gameObject, iTween.Hash("z", 180.0f, "time", .3f, "delay", 0.3f, "oncomplete", "Match"));
        open = true;

    }

    //カードをもどす
    public void ReSet()
    {
        source.PlayOneShot(OpenCard);
        //Source.PlayOneShot(se.EffectSounds.CardEffectSound[0].EffectSounds[0]);
        iTween.RotateAdd(gameObject, iTween.Hash("z", 180.0f, "time", 0.2f, "delay", 0.3f));

        open = false;
    }

    //ステージに残っているカードをすべて開きReSet（）を呼び出して元の状態に戻す
    public static void AllOpen(int index)
    {
        iTween.RotateAdd(CardManager.cardList[index], iTween.Hash("z", 180.0f, "time", 2.0f, "delay", 0.3f, "oncomplete", "ReSet"));
    }


    //カードが合致したときにCardManagerにid,gameObject,cardPosを渡す
    private void Match()
    {

        CardManager cm = gManager.GetComponent<CardManager>();
        cm.ClickObj(id, gameObject);
    }

    //エフェクト生成
    public void effect(bool match)
    {
        if (match == true)
        {
            destroySoundPlay = true;
            expl = Instantiate(effects[id], gameObject.transform.position, Quaternion.identity);
            Destroy(expl, 1.5f);

        }
    }
}

