using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource getAudio;
    [SerializeField] AudioSource[] enterAudio;
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource startAudio;
    [SerializeField] AudioSource playAudio;
    [SerializeField] AudioSource displayAudio;
    [SerializeField] AudioSource closeAudio;
    [SerializeField] AudioSource finishAudio;

    public void PlayBgm(bool on)//BGMのON/OFF
    {
        if(!on)
        {
            bgm.Play();
        }
        else
        {
            bgm.Stop();
        }
    }
    public void FinishAudio()//終了したときになるSE
    {
        finishAudio.Play();
    }

    public void DisplayAudio()//表示されたときになるSE
    {
        displayAudio.Play();
    }

    public void PlayAudio()//Playボタンを押したときのSE
    {
        playAudio.Play();
    }

    public void StartAudio()//スタート時になるSE
    {
        startAudio.Play();
    }

    public void ClickAudio()//ブロックを押したときになるSE
    {
        getAudio.Play();
    }

    public void CloseAudio()
    {
        closeAudio.Play();
    }
    
    public void EnterAudio(int count = 0, bool check = true)//Enter押した時のSE
    {
        if (check)
        {
            switch (count)
            {
                case 2:
                case 3:
                case 4:
                    enterAudio[0].Play();
                    break;
                case 5:
                    enterAudio[1].Play();
                    break;
            }
        }
        else
        {
            enterAudio[2].Play();
        }
    }
}
