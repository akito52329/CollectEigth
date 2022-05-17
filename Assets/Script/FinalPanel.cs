using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using DG.Tweening;

public class FinalPanel : MonoBehaviour
{
    [SerializeField] AudioController audioCo;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] string title = "コレクトエイト";
    [SerializeField] string oneWeek = "unity1week";
    [SerializeField] float scoreTime = 10f;
    [SerializeField] float stopTime = 0.01f;
    [SerializeField] float upTime = 0.5f;
    public Ease Ease_Type;
    public int finalScore = 0;
    private int _score;
    public int score//最終スコア表示
    {
        get { return _score; }
        set
        {
            if(_score != value)
            {
                _score = value;
                finalText.text = _score.ToString();
            }
        }
    }
    private string[] nameColor = { "White", "Red", "Yellow", "Cyan", "Green", "Blue", "Purple", "Gray" };
    static public Dictionary<string, int> color = new Dictionary<string, int>()//各色の計算回数
    {
        {"White", 0},
        {"Red", 0},
        {"Yellow", 0},
        {"Cyan", 0},
        {"Green", 0},
        {"Blue", 0},
        {"Purple", 0},
        {"Gray", 0},     
    };

    [SerializeField] TextMeshProUGUI finalText;
    [SerializeField] Text[] countText;

    public void CallDisplay()
    {
        StartCoroutine("ScoreDisplay");
    }

    IEnumerator ScoreDisplay()//表示コルーチン
    {
        var num = 0;
        rectTransform.DOScale(Vector3.one, upTime);//パネルを拡大させる
        yield return new WaitForSeconds(upTime);

        audioCo.FinishAudio();

        yield return new WaitForSeconds(stopTime);

        //最終スコア表示
        DOTween.To(() => score, num => score = num, finalScore, scoreTime).SetEase(Ease_Type);

        foreach (Text text in countText)
        {
            text.gameObject.SetActive(true);
            audioCo.DisplayAudio();
            text.text = ":" + color[nameColor[num]].ToString() + "回";
            num++;

            yield return new WaitForSeconds(upTime);
        }
    }

    public void OnTwitterButton()//Twitterに投稿処理
    {
        //urlの作成
        string text = $"スコアは『{finalScore}』でした。皆さんも遊んでみてね！　\nhttps://unityroom.com/games/colorcollect2566//";     
        string esctext = UnityWebRequest.EscapeURL(text);
        string esctag1 = UnityWebRequest.EscapeURL(title);
        string esctag2 = UnityWebRequest.EscapeURL(oneWeek);
        string url = "https://twitter.com/intent/tweet?text=" + esctext + "&hashtags=" + esctag1 + "&hashtags=" + esctag2 ;

        //Twitter投稿画面の起動
        Application.OpenURL(url);
    }
}
