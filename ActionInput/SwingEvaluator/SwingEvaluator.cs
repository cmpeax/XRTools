using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Cmepax.XRI.ActionInput
{
  // 挥动探测器
  public class SwingEvaluator
  {
    private Transform target; // 捕捉对象
    private bool enabled = false; // 是否启用

    private bool catching = false; // 是否正在捕捉
    /* 
      阈值参数
    */
    private float startVelocity; // 判断挥动开始的加速度阈值
    private float endVelocity; // 判断挥动结束的加速度阈值
    private float distance; // 距离阈值

    // 
    private Vector3 lastPos = Vector3.zero; // 上一个位置.
    private Vector3 startCatchPos = Vector3.zero; // 开始捕捉的位置

    public UnityEvent<Transform, Vector3, Vector3> onTrigger = new UnityEvent<Transform, Vector3, Vector3>();

    /// <summary>
    ///   挥动探测器
    /// </summary>
    /// <param name="target">控制器的模板</param>
    /// <param name="startVelocity">判断开始挥动的加速度</param>
    /// <param name="endVelocity">判断结束挥动的加速度</param>
    /// <param name="distance">判断距离识别</param>
    public SwingEvaluator(Transform target, float startVelocity, float endVelocity, float distance)
    {
      this.target = target;
      this.startVelocity = startVelocity;
      this.endVelocity = endVelocity;
      this.distance = distance;
    }

    public void Enable() => this.enabled = true;
    public void Disable() => this.enabled = false;

    public void FixedUpdate()
    {
      // 如果不启用，则退出函数
      if (!this.enabled) return;
      Vector3 nowPos = target.position;

      // 初始化成功后，开始判断
      if (this.lastPos != Vector3.zero)
      {
        // 当前加速度
        float nowVelocity = (nowPos - this.lastPos).magnitude / Time.fixedDeltaTime;

        // 捕捉状态中,如果当前加速度低于结束阈值
        if (this.catching && nowVelocity < endVelocity)
        {
          this.onTrigger.Invoke(target, this.startCatchPos, nowPos);
          this.catching = false;
        }
        // 非捕捉状态中，如果当前加速度高于开始阈值
        else if (!this.catching && nowVelocity >= startVelocity)
        {
          this.startCatchPos = nowPos;
          this.catching = true;
        }
      }
      this.lastPos = nowPos;
    }

  }

}