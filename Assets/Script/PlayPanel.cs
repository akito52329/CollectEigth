using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;

public class PlayPanel : MonoBehaviour
{
    [SerializeField] AudioController audioCo;
    [SerializeField] Camera mineCamera;
    [SerializeField] GameObject imagePatent;//生成した画像の親
    [SerializeField] GameObject effImage;//クリック時のエフェクトの画像
    [SerializeField] Vector3 rotateZ = new Vector3(0, 0, 360);//Z回転
    [SerializeField] int defaultScore = 10;
    [SerializeField] float speed = 0.15f;//変移時間
    [SerializeField] float requiredTime = 0.5f;//変移時間
    [SerializeField] int maxCount = 5;
    [SerializeField] int[] magniCriteria;//倍率基準
    int zero = 0;
    int ten = 10;
    private int _count;
    public int count//ブロックが何個あるか
    {
        get { return _count; }
        set
        {
            if (_count != value)
            {
                _count = value;
                if (_count == maxCount)
                {
                    maxClick = true;
                    Score();
                }
            }
        }
    }

    [SerializeField] TextMeshProUGUI scoreText;
    private int _score;
    public int score
    {
        get { return _score; }
        set
        {
            if(score != value)
            {
                _score = value;
                scoreText.text = _score.ToString();
            }
        }

    }
    public bool maxClick = false;//最大クリックに達しているか

    [SerializeField] List<Image> colorImage;
    [SerializeField] List<Color> uiColor;


    void Update()
    {
        if (!maxClick)
        {
            if (Input.GetKeyDown(KeyCode.Return))//Enterを押したら
            {
                Score();
            }
        }
    }

    public void GetColor(Color color, Vector3 start)
    {
        maxClick = true;
        Vector3[] curvers = new Vector3[2];
        curvers[0] = start;//クリック位置
        curvers[1] = colorImage[count].transform.localPosition;//結果表示の位置
       
        GameObject eff = Instantiate(effImage, start, effImage.transform.rotation, imagePatent.transform); //エフェクトのオブジェクトを生成
        eff.GetComponent<Image>().color = color;//色を変える
        eff.transform.DOLocalPath(curvers, speed, PathType.Linear ,PathMode.Sidescroller2D)//放物線で移動させる
            .OnComplete(() => { colorImage[count].color = color; count++; Destroy(eff); maxClick = false; }).SetEase(Ease.OutSine);

        uiColor.Add(color);//回収した色を追加する
    }

    private void PlusColorCount(Color getColor)//そろえた回数を与える
    {
       Color[] colorConst = { Color.white, Color.red, Color.blue, Color.green, Color.cyan, Color.magenta, Color.yellow, new Color(72, 72, 72, 255) };//カラーコード

        for(int c = 0; c < colorConst.Length; c++)
        {
            if(getColor == colorConst[c])
            {
                switch(c)
                {
                    case 0:
                        FinalPanel.color["White"]++;
                        break;
                    case 1:
                        FinalPanel.color["Red"]++;
                        break;
                    case 2:
                        FinalPanel.color["Blue"]++;
                        break;
                    case 3:
                        FinalPanel.color["Green"]++;
                        break;
                    case 4:
                        FinalPanel.color["Cyan"]++;
                        break;
                    case 5:
                        FinalPanel.color["Purple"]++;
                        break;
                    case 6:
                        FinalPanel.color["Yellow"]++;
                        break;
                    case 7:
                        FinalPanel.color["Gray"]++;
                        break;
                    default:
                        break;
                }
                break;
            }
        }
    }

    private int Magnification(int get)//倍率をかえす
    {
        switch(get)
        {
            case 2:
                return magniCriteria[0];
            case 3:
                return magniCriteria[1];
            case 4:
                return magniCriteria[2];
            case 5:
                return magniCriteria[3];
            default:
                return zero;
        }
    }

    public void Score()//スコアの計算
    {
        if (uiColor.Count != 0)
        {
            var topColor = uiColor.First();
            if (uiColor.All(value => value == topColor) && uiColor.Count > 1)//すべて同じ色かどうか
            {
                audioCo.EnterAudio(uiColor.Count);
                PlusColorCount(topColor);
                score += defaultScore * uiColor.Count() * Magnification(uiColor.Count);//スコアの計算
                mineCamera.backgroundColor = topColor;//背景画像の色を変える
            }
            else
            {
                mineCamera.backgroundColor = Color.black;
                audioCo.EnterAudio(zero ,false);
                score -= ten;//スコアをマイナス
            }
            
            uiColor.Clear();

            foreach (Image image in colorImage)//表示をすべて透明にする
            {
                DOTween.ToAlpha(() => image.color, color => image.color = color, zero, requiredTime);//透明度
                image.rectTransform.DOLocalRotate(rotateZ, requiredTime, RotateMode.FastBeyond360);//回転


                //image.color = Color.clear;
            }

            count = zero;
            maxClick = false;
        }
    }
}
