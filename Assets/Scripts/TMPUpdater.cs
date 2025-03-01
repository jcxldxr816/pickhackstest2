using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TMPUpdater : MonoBehaviour
{
    public TextMeshProUGUI myText;  // ����Text���
    private int value;   // ������ʾ�ı���
    void Start()
    {
        value = 0; // ��ʼ������
        UpdateText(); // �����ı���ʾ
    }

    void Update()
    {
        // ��ʾ����Ϊ��̬���µļ����ӣ����ո�ʱ������ֵ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            value += 1;
            UpdateText();
        }
    }

    void UpdateText()
    {
        // ����Text��ʾ������
        myText.text = "Value: " + value.ToString();
    }
}