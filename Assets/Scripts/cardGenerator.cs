using UnityEngine;

public class cardGenerator : MonoBehaviour
{
    public static GameObject selectedCard = null; // ��ǰѡ�еĿ�Ƭ
    public Placeholder currentPlaceholder = null; // ��ǰռ�õ� Placeholder ����

    private void OnMouseDown()
    {
        // �����ǰû��ѡ�еĿ��ƣ���ѡ���Լ�
        if (selectedCard == null)
        {
            selectedCard = this.gameObject;
            Debug.Log($"Card {gameObject.name} selected.");
        }
        // ����Լ��ѱ�ѡ�У���ȡ��ѡ��
        else if (selectedCard == this.gameObject)
        {
            selectedCard = null;
            Debug.Log($"Card {gameObject.name} deselected.");
        }
    }
}
