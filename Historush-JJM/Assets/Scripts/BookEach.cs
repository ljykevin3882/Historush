using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BookEach : MonoBehaviour
{
    public GameObject[] btns;  // �ʱ� ����ǥ�ڽ� ���� 10��
    public bool[] IsMat = new bool[10];  // ������ �Ծ����� Ȯ��
    public Sprite[] MatImages; // �ٲ� �����̹���
    public GameObject bookmenu;


    void Update()
    {
        // ���� �� ������ �Ծ����� Ȯ�� �� �Ծ��ٸ� �������
        for (int i = 0; i < 10; i++)
        {
            if (IsMat[i] == true)
            {
                btns[i].GetComponent<Image>().sprite = MatImages[i];
            }

        }

    }

    // �ڷΰ��� ��ư
    public void ClickBack()
    {

    }

}
