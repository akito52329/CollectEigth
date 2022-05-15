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

    public void PlayBgm(bool on)//BGM��ON/OFF
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
    public void FinishAudio()//�I�������Ƃ��ɂȂ�SE
    {
        finishAudio.Play();
    }

    public void DisplayAudio()//�\�����ꂽ�Ƃ��ɂȂ�SE
    {
        displayAudio.Play();
    }

    public void PlayAudio()//Play�{�^�����������Ƃ���SE
    {
        playAudio.Play();
    }

    public void StartAudio()//�X�^�[�g���ɂȂ�SE
    {
        startAudio.Play();
    }

    public void ClickAudio()//�u���b�N���������Ƃ��ɂȂ�SE
    {
        getAudio.Play();
    }

    public void CloseAudio()
    {
        closeAudio.Play();
    }
    
    public void EnterAudio(int count = 0, bool check = true)//Enter����������SE
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
