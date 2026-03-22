using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FGame
{
    public class CharacterMovement : MonoBehaviour
    {
      





        #region 参数



        private float currentVelocity;

        #endregion


        #region 组件

        #endregion


        #region 生命周期

        #endregion


        #region API
        public void MoveCharacter(UnityEngine.CharacterController characterController,float targetAngle, float rotateSmooth,float Speed)
        {
        
            // 移动方向就是角色面朝方向（不是相机方向）
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            // 平滑转向（角色逐渐转向移动方向，而不是瞬间转）
            float smoothedAngle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref currentVelocity,
                rotateSmooth  // 瞄准时转向更快
            );
            transform.rotation = Quaternion.Euler(0, smoothedAngle, 0);

            /*   // 决定速度
               float targetSpeed = isAiming ? walkSpeed * 0.6f : (isRunningInput ? runSpeed : walkSpeed);
               float speedCurve = accelerationCurve.Evaluate(Mathf.Clamp01(inputDir.magnitude));
               CurMoveSpeed = Mathf.Lerp(currentSpeed, targetSpeed * speedCurve, Time.deltaTime * 8f);*/




            // 移动角色
            characterController.Move(moveDir * Speed * Time.deltaTime);

        }


        #endregion











    }


}


