using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Vector3 nextPosition;
    [SerializeField] BlockGenerator blockGenerator;
    [SerializeField] GameObject blockParent;
    [SerializeField] Slider timerSlider;//�^�C�}�[�p�̃X���C�_�\
    [SerializeField] private float _time = 1.0f;
    public float time
    {
        get{ return _time; }
        set 
        {
            if(_time != value)
            {
                _time = value;
                timerSlider.value = _time;
            }
        }
    }
    [SerializeField] private float timeMax = 1.0f;
    [SerializeField] private float timeMin = 0.5f;
    [SerializeField] private int maxTimeUpCount;//�ő�̃^�C�����O�ɂȂ�����
    private int _timeUpCount;
    public int timeUpCount
    {
        get { return _timeUpCount; }
        set
        {
            if(_timeUpCount != value)
            {
                _timeUpCount = value;
                if(_timeUpCount == maxTimeUpCount && timeMax > timeMin)//�J�E���g���B������
                {
                    timeMax -= timeMin;//�ő�l����ŏ��l���Ђ�
                    _timeUpCount = 0;
                }
                _time = timeMax;
                timerSlider.maxValue = timeMax;
            }
            
        }
    }
    public bool timeCheck = false;//�^�C�}�[�X�^�[�g���邩�ǂ���


    void Update()
    {
        if (timeCheck)
        {
            if (time < 0)
            {
                blockGenerator.GenerationPos();
                BlockMove();
                timeUpCount++;
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
    }

    void BlockMove()
    {
        blockParent.transform.position += nextPosition;
    }
}