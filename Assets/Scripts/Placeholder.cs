using UnityEngine;

public class Placeholder : MonoBehaviour
{
    public bool isOccupied = false; // 是否已被放置卡牌

    private void OnMouseDown()
    {
        // 确保有选中的卡片，并且此 Placeholder 未被占用
        if (cardGenerator.selectedCard != null && !isOccupied)
        {
            // 获取当前选中的卡片
            GameObject card = cardGenerator.selectedCard;
            cardGenerator cardScript = card.GetComponent<cardGenerator>();

            // 如果卡片当前已经在一个 Placeholder 上，先解除原 Placeholder 的占用
            if (cardScript.currentPlaceholder != null)
            {
                cardScript.currentPlaceholder.isOccupied = false;
                Debug.Log($"Placeholder {cardScript.currentPlaceholder.gameObject.name} is now unoccupied.");
            }

            // 更新卡片的位置到当前 Placeholder
            card.transform.position = transform.position; // 移动卡片到当前 Placeholder 的位置
            cardScript.currentPlaceholder = this; // 更新卡片所在的 Placeholder
            isOccupied = true; // 标记当前 Placeholder 为已占用

            // 清空选中状态
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
            GetComponent<Renderer>().material.color = Color.green; // 高亮可用占位符
        }
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white; // 还原颜色
    }
}