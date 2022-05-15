using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PlayPanel : MonoBehaviour
{
    [SerializeField] AudioController audioCo;
    [SerializeField] int defaultScore = 10;
    int zero = 0;
    int ten = 10;
    [SerializeField] int maxCount = 5;
    [SerializeField] int[] magniCriteria;//倍率基準
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
    [SerializeField] List<int> colorKind;
    private Color clear = Color.clear;


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

    public void GetColor(Color color)
    {
        uiColor.Add(color);//回収した色を追加する
        colorImage[count].color = color;
        count++;
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
                score += defaultScore * uiColor.Count() * Magnification(uiColor.Count);
            }
            else
            {
                audioCo.EnterAudio(zero ,false);
                score -= ten;
            }
            
            uiColor.Clear();

            foreach (Image image in colorImage)//表示をすべて透明にする
            {
                image.color = clear;
            }

            count = zero;
            maxClick = false;
        }
    }
}
