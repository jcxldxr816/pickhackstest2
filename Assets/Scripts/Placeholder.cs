using UnityEngine;
using UnityEngine.SceneManagement;

public class Placeholder : MonoBehaviour
{
    public bool isOccupied = false; // ��ʾ��ռλ���Ƿ񱻿���ռ��
    public Phases currentCard = null; // ��ǰռ�ô�ռλ���Ŀ��ƶ���
    public int line; // �к�
    public int row; // �к�
    public bool isEnemy = false; // �Ƿ��ǵ��˵�ռλ��

    private void OnMouseDown()
    {
        // ȷ���п��Ʊ�ѡ�У����ҵ�ǰռλ��δ��ռ��
        if (Phases.selectedCard != null && !this.isOccupied)
        {
            // ��ȡ��ǰѡ�еĿ���
            GameObject card = Phases.selectedCard;
            Phases cardScript = card.GetComponent<Phases>();

            // ��������Ѿ�����һ��ռλ���ϣ���ԭռλ����״̬��գ�����������ɾ��
            if (cardScript.currentPlaceholder != null)
            {
                Placeholder oldPlaceholder = cardScript.currentPlaceholder;

                // �� Card2DArray �Ƴ��ɵĿ�������
                Phases.RemoveCardFromArray(oldPlaceholder.line, oldPlaceholder.row);

                // ��վ�ռλ����״̬
                oldPlaceholder.isOccupied = false;
                oldPlaceholder.currentCard = null;
                Debug.Log($"Placeholder {oldPlaceholder.gameObject.name} at ({oldPlaceholder.line}, {oldPlaceholder.row}) is now unoccupied.");
            }

            // �������ƶ�����ǰռλ����λ��
            card.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            // ���¿��ƺ�ռλ��֮��Ĺ���
            cardScript.currentPlaceholder = this;
            this.isOccupied = true;
            this.currentCard = cardScript;

            // ���¿��Ƶ�����
            cardScript.inField = true; // ��ǿ��ƴ���ս����

            // ��������ӵ� Phases �ľ�̬��ά������
            Phases.AddCardToArray(cardScript, this.line, this.row);

            // �����ѡ�еĿ���״̬
            Phases.selectedCard = null;

            Debug.Log($"Phases {card.name} moved to Placeholder {gameObject.name} at ({line}, {row}).");
        }
        else
        {
            Debug.Log("Cannot place card: Placeholder is occupied or no card is selected.");
        }
    }

    private void OnMouseEnter()
    {
        // ������ʾռλ����������Է��ÿ��ƣ���ʾ��ɫ
        if (Phases.selectedCard != null && !isOccupied && SceneManager.GetActiveScene().name != "MainMenu")
        {
            GetComponent<Renderer>().material.color = Color.green; // ������ʾ�ɷ��õ�ռλ��
        }
    }

    private void OnMouseExit()
    {
        // �Ƴ�����
        GetComponent<Renderer>().material.color = Color.white; // �ָ���ɫ
    }
}