
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    /// <summary>
    /// �ش� ��ũ��Ʈ�� ������ ī�޶� �߰�
    /// ������ Screen Match Mode �� Expand�� �������
    /// </summary>
    private void Awake()
    {
        //Camera cam = GetComponent<Camera>();

        //// ī�޶� ������Ʈ�� Viewport Rect
        //Rect rt = cam.rect;

        //// ���� ���� ��� 9:16, �ݴ�� �ϰ� ������ 16:9�� �Է�.
        //float scale_height = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (���� / ����)
        //float scale_width = 1f / scale_height;

        //if (scale_height < 1)
        //{
        //    rt.height = scale_height;
        //    rt.y = (1f - scale_height) / 2f;
        //}
        //else
        //{
        //    rt.width = scale_width;
        //    rt.x = (1f - scale_width) / 2f;
        //}

        //cam.rect = rt;
    }
}