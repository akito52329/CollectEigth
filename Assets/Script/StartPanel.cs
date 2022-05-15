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

        //Score�t�B�[���h�̍~���Ńf�[�^���擾
        query.OrderByDescending("score");

        //����������ݒ�
        query.Limit = limit;

        //�f�[�^�X�g�A�ł̌������s��
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)//�ڑ��Ɏ��s
            {
                nameText.text = "�Ȃ�";
                scoreText.text = "0";
            }
            else//�ڑ��ɐ���
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
                    panels[_panelCount].SetActive(false);//�ύX�O
                    _panelCount = value;
                    panels[_panelCount].SetActive(true);//�ύX��
                }

                if(_panelCount == 0)//������ON/OFF
                {
                    buttons[0].SetActive(false);
                }
                else
                {
                    buttons[0].SetActive(true);
                }

                if (_panelCount == panels.Length - 1)//�E����ON/OFF
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

    public void OnMenu(RectTransform rectTransform)//�\��
    {
        rectTransform.DOScale(Vector3.one, 1f);
    }

    public void OffMenu(RectTransform rectTransform)//��\��
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
