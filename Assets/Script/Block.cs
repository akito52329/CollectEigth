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
        if(defaultColor !=  zeroColor)//開始時に生成用に色を渡す
        {
            blockRenderer.material.color = defaultColor;
        }
    }

    public void ColorChenge(Color color)//ブロックが時間で生成されたときに呼ばれる
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
