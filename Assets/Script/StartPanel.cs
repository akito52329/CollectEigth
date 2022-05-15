using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using NCMB;

public class StartPanel : MonoBehaviour
{
    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject[] buttons;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] int limit = 3;
    int _panelCount;

    private void Start()
    {
        Ranking();
    }

    public void Ranking()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("GameScore");

        //Scoreフィールドの降順でデータを取得
        query.OrderByDescending("score");

        //検索件数を設定
        query.Limit = limit;

        //データストアでの検索を行う
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)//接続に失敗
            {
                nameText.text = "なし";
                scoreText.text = "0";
            }
            else//接続に成功
            {
                nameText.text = objList[0]["name"].ToString();
                scoreText.text = objList[0]["score"].ToString();
            }
        });
    }

    public int panelCount
    {
        get { return _panelCount; }
        set
        {
            if (_panelCount != value)
            {
                if (_panelCount >= 0 && _panelCount <= panels.Length - 1)
                {
                    panels[_panelCount].SetActive(false);//変更前
                    _panelCount = value;
                    panels[_panelCount].SetActive(true);//変更後
                }

                if(_panelCount == 0)//左矢印のON/OFF
                {
                    buttons[0].SetActive(false);
                }
                else
                {
                    buttons[0].SetActive(true);
                }

                if (_panelCount == panels.Length - 1)//右矢印のON/OFF
                {
                    buttons[1].SetActive(false);
                }
                else
                {
                    buttons[1].SetActive(true);
                }
            }
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnMenu(RectTransform rectTransform)//表示
    {
        rectTransform.DOScale(Vector3.one, 1f);
    }

    public void OffMenu(RectTransform rectTransform)//非表示
    {
        rectTransform.DOScale(Vector3.zero, 1f);
    }

    public void ChengePanel(bool chenge)
    {
        if(chenge)
        {
            panelCount++;
        }
        else
        {
            panelCount--;
        }
    }
}
