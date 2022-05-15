using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ReadyPanel : MonoBehaviour
{
    [SerializeField] AudioController audioCo;
    [SerializeField] GameDirector.GameState nextState;
    [SerializeField] TextMeshProUGUI readyText;
    [SerializeField] TextMeshProUGUI startText;

    private bool _chenge; 
    public bool chenge
    {
        get { return _chenge; }
        set
        {
            if(_chenge != value)
            {
                _chenge = value;
                if(_chenge)
                {
                    ChengeText();
                    audioCo.StartAudio();
                }
            }
        }
    }

    void Update()
    {
        if (!chenge)
        {
            if (Input.GetMouseButton(0))
            {
                chenge = true;
            }
        }
    }

    void ChengeText()
    {
        readyText.rectTransform.localScale = Vector3.zero;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(startText.rectTransform.DOScale(Vector3.one, 1))//拡大
            .Join(startText.rectTransform.DOLocalRotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360))//回転しながら
            .OnComplete(() => { GameDirector.gameState = nextState; });//終了したらステートを変える
    }
}
