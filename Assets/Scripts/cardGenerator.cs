using UnityEngine;

public class cardGenerator : MonoBehaviour
{
    public static GameObject selectedCard = null; // 当前选中的卡片
    public Placeholder currentPlaceholder = null; // 当前占用的 Placeholder 对象

    private void OnMouseDown()
    {
        // 如果当前没有选中的卡牌，则选中自己
        if (selectedCard == null)
        {
            selectedCard = this.gameObject;
            Debug.Log($"Card {gameObject.name} selected.");
        }
        // 如果自己已被选中，则取消选中
        else if (selectedCard == this.gameObject)
        {
            selectedCard = null;
            Debug.Log($"Card {gameObject.name} deselected.");
        }
    }
}
