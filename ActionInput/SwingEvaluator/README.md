# 挥动探测器 - SwingEvaluator

## 工作原理

- 在 FixedUpdate 上计算 XR 控制器的位置与上一帧位置的模来得出加速度。
- 当加速度大于起始阈值时，进入捕捉状态。
- 当捕捉状态下，加速度小于结束阈值时，结束捕捉状态。通过 onTrigger 事件通知。

## 简单使用

```csharp
public class TestMove : MonoBehaviour
{
  private SwingEvaluator swingActionInput;

  void Awake() {
    // controllerTransform: 控制器
    // startVelocity: 判断开始挥动的加速度值
    // endVelocity: 判断结束挥动的加速度值
    SwingEvaluator swingActionInput = new SwingEvaluator(controllerTransform, 4.0f, 2.0f);
    swingActionInput.onTrigger += onSwing;
    swingActionInput.Enable();
  }

  // 更新函数调用
  void FixedUpdate() {
    swingActionInput.FixedUpdate();
  }

  void OnSwing(Transform swingObject, Vector3 startPos, Vector3 endPos)
  {
    //TODO
  }
}
```

### V1.0.0

- 初始版本
