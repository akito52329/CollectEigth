using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using NCMB;
using TMPro;
using DG.Tweening;

public class GameDirector : MonoBehaviour
{
    public enum GameState { Ready, InGame, GameOver}

    [SerializeField] Timer timer;
    [SerializeField] ReadyPanel readyPanel;
    [SerializeField] PlayerController player;
    [SerializeField] AudioController audioCo;
    [SerializeField] FinalPanel finalPanel;
    [SerializeField] PlayPanel playPanel;

    //ハイスコア更新用
    [SerializeField] TMP_InputField inputField;
    [SerializeField] RectTransform rect;
    [SerializeField] int limit = 3;

    int one = 1;

    private GameState _gameState = GameState.Ready;
    static public GameState gameState
    {
        set
        {
            if (director._gameState != value)
            {
                director._gameState = value;
                director.OnChangeGameState();
            }
        }
    }
    // シングルトン用の参照
    static GameDirector director = null;

    private void Awake()
    {
        if (director == null)
        {
            director = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        gameState = GameState.Ready;
    }

    public void OnChangeGameState()//ゲームのステート変更時に行われる処理
    {
        switch(_gameState)
        {
            case GameState.Ready:
                readyPanel.gameObject.SetActive(true);
                playPanel.gameObject.SetActive(true);
                break;
            case GameState.InGame:
                audioCo.PlayBgm(false);
                timer.timeCheck = true;
                player.gameObject.SetActive(true);
                readyPanel.gameObject.SetActive(false);
                break;
            case GameState.GameOver:
                finalPanel.gameObject.SetActive(true);
                finalPanel.finalScore = playPanel.score;
                finalPanel.CallDisplay();
                playPanel.gameObject.SetActive(false);
                audioCo.PlayBgm(true);
                timer.timeCheck = false;
                player.gameObject.SetActive(false);
                CheckHighScore();
                break;
        }
    }

    private void CheckHighScore()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("GameScore");

        //Scoreフィールドの降順でデータを取得
        query.OrderByDescending("score");

        //検索件数を設定
        query.Limit = limit;

        //データストアでの検索を行う
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if(Convert.ToInt32(objList[0]["score"]) < finalPanel.finalScore)//スコアが更新されたら
            {
                rect.DOScale(Vector3.one, one);
            }
        });
    }

    public void OnHighScore(RectTransform rectTransform)//ハイスコアを更新
    {
        // クラスのNCMBObjectを作成
        NCMBObject nCMBObject = new NCMBObject("GameScore");

        nCMBObject["name"] = inputField.text;
        nCMBObject["score"] = finalPanel.finalScore;
        // データストアへの登録
        nCMBObject.SaveAsync();

        rectTransform.DOScale(Vector3.zero, one);
    }

    public void ChengeScene(bool chenge)//シーン切り替えボタン
    {
        audioCo.PlayAudio();

        StartCoroutine("LoadScene", chenge);
    }

    IEnumerator LoadScene(bool chenge)//音を流してからシーンを変える
    {
        yield return new WaitForSeconds(one);

        if (chenge)
        {
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
