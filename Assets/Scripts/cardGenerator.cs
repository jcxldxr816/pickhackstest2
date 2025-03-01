using UnityEngine;

public class Building : MonoBehaviour
{
    public static GameObject selectedBuilding = null; // 当前选中的建筑
    public Placeholder currentPlaceholder = null; // 当前占用的 Placeholder 对象
    public bool activated = false;
    public bool inField = false;

    public int state = 0; // 0: in shop, 1: off battle, 2: in battle
    public int HP;
    public int cost;

    private void OnEnable()
    {
        // 订阅 GameManager 的事件
        GameManager.OnRoundStart += OnRoundStart;
        GameManager.OnRoundStart += OnRoundBattle;
    }

    private void OnDisable()
    {
        // 取消订阅 GameManager 的事件，避免内存泄漏或无效调用
        GameManager.OnRoundStart -= OnRoundStart;
    }

    private void OnMouseDown()
    {
        // 如果当前没有选中的卡牌，则选中自己
        if (selectedBuilding == null)
        {
            selectedBuilding = this.gameObject;
            Debug.Log($"Building {gameObject.name} selected.");
        }
        // 如果自己已被选中，则取消选中
        else if (selectedBuilding == this.gameObject)
        {
            selectedBuilding = null;
            Debug.Log($"Building {gameObject.name} deselected.");
        }
    }

    private void Update()
    {
        // 更新建筑的状态颜色
        if (selectedBuilding == this.gameObject)
        {
            GetComponent<Renderer>().material.color = Color.green; // 高亮选中
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white; // 还原颜色
        }
    }

    // 响应 GameManager 的 OnRoundStart 事件
    private void OnRoundStart()
    {
        if (!inField)
        {
            Debug.Log($"Building {gameObject.name} is still in shop during OnRoundStart.");
            // 执行一些逻辑，例如调整状态或准备被刷新
            state = 0; // 仍然在商店中
        }
        else
        {
            Debug.Log($"Building {gameObject.name} is in field during OnRoundStart.");
            // 执行一些逻辑，例如变为不可出售或战斗准备
        }
    }

    // 响应 GameManager 的 OnRoundShop2 事件
    private void OnRoundShop2()
    {
    }
    private void OnRoundBattle()
    {
        state = 2;
    }
}