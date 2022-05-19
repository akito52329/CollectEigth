using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CrisisAlarm : MonoBehaviour
{
    [SerializeField] GameDirector.GameState overState;
    [SerializeField] AudioController audioCo;
    [SerializeField] GameObject rayObject;
    [SerializeField] Image vigilance;//警戒用画像
    [SerializeField] float maxClear = 0.45f;//透明度
    [SerializeField] string alarmName;
    [SerializeField] Color defColor;
    RaycastHit hit;

    private void Update()
    {
        if (GameDirector.gameState != overState)
        {
            Debug.DrawLine(rayObject.transform.position, rayObject.transform.position + Vector3.right * 100, Color.blue);
            if (Physics.Linecast(rayObject.transform.position, rayObject.transform.position + Vector3.right * 100, out hit))//ピンチかどうかをRayで検知
            {
                audioCo.AlarmAudio(!(hit.collider.tag == alarmName));//アラームを鳴らす
                DangerColor(hit.collider.tag == alarmName);
            }
        } 
    }

    private void DangerColor(bool hit)
    {
        if(hit)
        {
            DOTween.ToAlpha(
                   () => vigilance.color,
                   color => vigilance.color = color,
                   maxClear, // 目標値
                   1f // 所要時間
               ).SetLoops(1, LoopType.Yoyo);
        }
        else
        {
            vigilance.color = defColor;
        }

    }
}
