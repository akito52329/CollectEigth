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
    [SerializeField] string title = "�R���N�g�G�C�g";
    [SerializeField] string oneWeek = "unity1week";
    [SerializeField] float scoreTime = 10f;
    [SerializeField] float stopTime = 0.01f;
    [SerializeField] float upTime = 0.5f;
    public Ease Ease_Type;
    public int finalScore = 0;
    private int _score;
    public int score//�ŏI�X�R�A�\��
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
    static public Dictionary<string, int> color = new Dictionary<string, int>()//�e�F�̌v�Z��
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

    IEnumerator ScoreDisplay()//�\���R���[�`��
    {
        var num = 0;
        rectTransform.DOScale(Vector3.one, upTime);//�p�l�����g�傳����
        yield return new WaitForSeconds(upTime);

        audioCo.FinishAudio();

        yield return new WaitForSeconds(stopTime);

        //�ŏI�X�R�A�\��
        DOTween.To(() => score, num => score = num, finalScore, scoreTime).SetEase(Ease_Type);

        foreach (Text text in countText)
        {
            text.gameObject.SetActive(true);
            audioCo.DisplayAudio();
            text.text = ":" + color[nameColor[num]].ToString() + "��";
            num++;

            yield return new WaitForSeconds(upTime);
        }
    }

    public void OnTwitterButton()//Twitter�ɓ��e����
    {
        //url�̍쐬
        string text = $"�X�R�A�́w{finalScore}�x�ł����B�F������V��ł݂ĂˁI�@\nhttps://unityroom.com/games/colorcollect2566//";     
        string esctext = UnityWebRequest.EscapeURL(text);
        string esctag1 = UnityWebRequest.EscapeURL(title);
        string esctag2 = UnityWebRequest.EscapeURL(oneWeek);
        string url = "https://twitter.com/intent/tweet?text=" + esctext + "&hashtags=" + esctag1 + "&hashtags=" + esctag2 ;

        //Twitter���e��ʂ̋N��
        Application.OpenURL(url);
    }
}
