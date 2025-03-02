using UnityEngine;

public class Placeholder : MonoBehaviour
{
    public bool isOccupied = false; // 表示此占位符是否被卡牌占用
    public Phases currentCard = null; // 当前占用此占位符的卡牌对象
    public int line; // 行号
    public int row; // 列号
    public bool isEnemy = false; // 是否是敌人的占位符

    private void OnMouseDown()
    {
        // 确保有卡牌被选中，并且当前占位符未被占用
        if (Phases.selectedCard != null && !this.isOccupied)
        {
            // 获取当前选中的卡牌
            GameObject card = Phases.selectedCard;
            Phases cardScript = card.GetComponent<Phases>();

            // 如果卡牌已经在另一个占位符上，将原占位符的状态清空，并从数组中删除
            if (cardScript.currentPlaceholder != null)
            {
                Placeholder oldPlaceholder = cardScript.currentPlaceholder;

                // 从 Card2DArray 移除旧的卡牌引用
                Phases.RemoveCardFromArray(oldPlaceholder.line, oldPlaceholder.row);

                // 清空旧占位符的状态
                oldPlaceholder.isOccupied = false;
                oldPlaceholder.currentCard = null;
                Debug.Log($"Placeholder {oldPlaceholder.gameObject.name} at ({oldPlaceholder.line}, {oldPlaceholder.row}) is now unoccupied.");
            }

            // 将卡牌移动到当前占位符的位置
            card.transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);

            // 更新卡牌和占位符之间的关联
            cardScript.currentPlaceholder = this;
            this.isOccupied = true;
            this.currentCard = cardScript;

            // 更新卡牌的属性
            cardScript.inField = true; // 标记卡牌处于战场中

            // 将卡牌添加到 Phases 的静态二维数组中
            Phases.AddCardToArray(cardScript, this.line, this.row);

            // 清除已选中的卡牌状态
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
        // 高亮显示占位符：如果可以放置卡牌，显示绿色
        if (Phases.selectedCard != null && !isOccupied)
        {
            GetComponent<Renderer>().material.color = Color.green; // 高亮显示可放置的占位符
        }
    }

    private void OnMouseExit()
    {
        // 移除高亮
        GetComponent<Renderer>().material.color = Color.white; // 恢复颜色
    }
}