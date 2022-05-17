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
                // �X�N���[���ʒu����3D�I�u�W�F�N�g�ɑ΂���Ray�i�����j�𔭎�
                Ray rayOrigine = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Ray���I�u�W�F�N�g�Ƀq�b�g�����ꍇ
                if (Physics.Raycast(rayOrigine, out RaycastHit hitInfo))
                {
                    // �^�O��Block��������
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
