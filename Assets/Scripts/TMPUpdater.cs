using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TMPUpdater : MonoBehaviour
{
    public TextMeshProUGUI myText;  // 引用Text组件
    private int value;   // 用于显示的变量
    void Start()
    {
        value = 0; // 初始化变量
        UpdateText(); // 更新文本显示
    }

    void Update()
    {
        // 在示例中为动态更新的简单例子，按空格时增加数值
        if (Input.GetKeyDown(KeyCode.Space))
        {
            value += 1;
            UpdateText();
        }
    }

    void UpdateText()
    {
        // 更新Text显示的内容
        myText.text = "Value: " + value.ToString();
    }
}