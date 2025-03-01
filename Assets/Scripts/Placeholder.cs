using UnityEngine;

public class Placeholder : MonoBehaviour
{
    public bool isOccupied = false; // �Ƿ��ѱ����ÿ���

    private void OnMouseDown()
    {
        // ȷ����ѡ�еĿ�Ƭ�����Ҵ� Placeholder δ��ռ��
        if (cardGenerator.selectedCard != null && !isOccupied)
        {
            // ��ȡ��ǰѡ�еĿ�Ƭ
            GameObject card = cardGenerator.selectedCard;
            cardGenerator cardScript = card.GetComponent<cardGenerator>();

            // �����Ƭ��ǰ�Ѿ���һ�� Placeholder �ϣ��Ƚ��ԭ Placeholder ��ռ��
            if (cardScript.currentPlaceholder != null)
            {
                cardScript.currentPlaceholder.isOccupied = false;
                Debug.Log($"Placeholder {cardScript.currentPlaceholder.gameObject.name} is now unoccupied.");
            }

            // ���¿�Ƭ��λ�õ���ǰ Placeholder
            card.transform.position = transform.position; // �ƶ���Ƭ����ǰ Placeholder ��λ��
            cardScript.currentPlaceholder = this; // ���¿�Ƭ���ڵ� Placeholder
            isOccupied = true; // ��ǵ�ǰ Placeholder Ϊ��ռ��

            // ���ѡ��״̬
            cardGenerator.selectedCard = null;
            Debug.Log($"Card moved to Placeholder {gameObject.name}.");
        }
        else
        {
            Debug.Log("Cannot place card: Placeholder is occupied or no card is selected.");
        }
    }
    private void OnMouseEnter()
    {
        if (cardGenerator.selectedCard != null && !isOccupied)
        {
            GetComponent<Renderer>().material.color = Color.green; // ��������ռλ��
        }
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white; // ��ԭ��ɫ
    }
}