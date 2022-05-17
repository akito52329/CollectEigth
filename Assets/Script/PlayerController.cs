using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayPanel playPanel;
    [SerializeField] AudioController audioCo;
    void Update()
    {
        if (!playPanel.maxClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // スクリーン位置から3Dオブジェクトに対してRay（光線）を発射
                Ray rayOrigine = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Rayがオブジェクトにヒットした場合
                if (Physics.Raycast(rayOrigine, out RaycastHit hitInfo))
                {
                    // タグがBlockだったら
                    if (hitInfo.collider.CompareTag("Block"))
                    {                        
                        OnClick(hitInfo.collider.GetComponent<MeshRenderer>().material.color , Input.mousePosition);
                        Destroy(hitInfo.collider.gameObject);
                    }
                }
            }
        }

    }

    private void OnClick(Color color , Vector3 clickPos)
    {
        audioCo.ClickAudio();
        playPanel.GetColor(color, clickPos);
    }
}
