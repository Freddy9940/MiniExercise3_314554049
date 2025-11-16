using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // 公開變數，讓我們可以在 Inspector 視窗中指定
    public Transform target; // 我們要跟隨的目標 (哥布林)

    // 攝影機相對於目標的「偏移量」(例如：在目標的後上方)
    public Vector3 offset = new Vector3(0f, 3f, -5f);

    // 攝影機跟隨的「平滑速度」(值越小跟越緊，越大跟越慢)
    public float smoothSpeed = 0.125f;

    // LateUpdate 在所有 Update() 都執行完畢 "之後" 才執行
    // 這是跟隨攝影機的最佳選擇，可以防止抖動
    void LateUpdate()
    {
        // 1. 安全檢查：如果沒有指定目標，就什麼也不做
        if (target == null)
        {
            return;
        }

        // 2. 計算攝影機的「期望位置」
        //    (目標的 "目前位置" + 我們設定的 "偏移量")
        Vector3 desiredPosition = target.position + offset;

        // 3. 使用 Lerp (線性插值) "平滑地" 移動攝影機
        //    從 "攝影機目前位置" 朝 "期望位置" 移動一小步 (由 smoothSpeed 控制)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 4. 更新攝影機的實際位置
        transform.position = smoothedPosition;

        // 5. (可選) 讓攝影機永遠「看向」目標
        transform.LookAt(target);
    }
}