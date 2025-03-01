using UnityEngine;
using System.Collections.Generic;

public class Building : MonoBehaviour
{
    public static GameObject selectedBuilding = null; // ��ǰѡ�еĽ���
    public Placeholder currentPlaceholder = null; // ��ǰռ�õ� Placeholder ����
    public bool activated = false;
    public bool inField = false;

    public int state = 0; // 0: in shop, 1: off battle, 2: in battle
    public int HP;
    public int cost;

    private void OnEnable()
    {
        // ���� GameManager ���¼�
        GameManager.OnRoundStart += OnRoundStart;
        GameManager.OnRoundStart += OnRoundBattle;
    }

    private void OnDisable()
    {
        // ȡ������ GameManager ���¼��������ڴ�й©����Ч����
        GameManager.OnRoundStart -= OnRoundStart;
    }

    private void OnMouseDown()
    {
        // �����ǰû��ѡ�еĿ��ƣ���ѡ���Լ�
        if (selectedBuilding == null)
        {
            selectedBuilding = this.gameObject;
            Debug.Log($"Building {gameObject.name} selected.");
        }
        // ����Լ��ѱ�ѡ�У���ȡ��ѡ��
        else if (selectedBuilding == this.gameObject)
        {
            selectedBuilding = null;
            Debug.Log($"Building {gameObject.name} deselected.");
        }
    }

    private void Update()
    {
        // ���½�����״̬��ɫ
        if (selectedBuilding == this.gameObject)
        {
            GetComponent<Renderer>().material.color = Color.green; // ����ѡ��
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white; // ��ԭ��ɫ
        }
    }

    // ��Ӧ GameManager �� OnRoundStart �¼�
    private void OnRoundStart()
    {
        if (!inField)
        {
            Debug.Log($"Building {gameObject.name} is still in shop during OnRoundStart.");
            // ִ��һЩ�߼����������״̬��׼����ˢ��
            state = 0; // ��Ȼ���̵���
        }
        else
        {
            Debug.Log($"Building {gameObject.name} is in field during OnRoundStart.");
            // ִ��һЩ�߼��������Ϊ���ɳ��ۻ�ս��׼��
        }
    }

    // ��Ӧ GameManager �� OnRoundShop2 �¼�
    private void OnRoundShop2()
    {
    }
    private void OnRoundBattle()
    {
        state = 2;
    }
}
