using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] GameDirector.GameState nextState;
    [SerializeField] Renderer blockRenderer;
    [SerializeField] string over = "Over";
    [SerializeField] Color defaultColor;
    private Color zeroColor = new Color(0, 0, 0, 0);

    private void Start()
    {
        if(defaultColor !=  zeroColor)//�J�n���ɐ����p�ɐF��n��
        {
            blockRenderer.material.color = defaultColor;
        }
    }

    public void ColorChenge(Color color)//�u���b�N�����ԂŐ������ꂽ�Ƃ��ɌĂ΂��
    {
        blockRenderer.material.color = color;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == over)
        {
            GameDirector.gameState = nextState;
        }
    }
}
