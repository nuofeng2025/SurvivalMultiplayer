using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityGameFramework.Runtime;
namespace FGame
{
    public class InteractionUi : UIFormLogic
    {
        public Image SearchUi;



        private bool Searching;


        private void Awake()
        {
            
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            Debug.Log("Interaction_Init");
            HideSearchUi();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);


        }

       
        public void ShowSearchUi()
        {
            SearchUi.fillAmount = 0;
            SearchUi.gameObject.SetActive(true);
        }

        public void HideSearchUi()
        {
            if(Searching)
            {
                SearchUi.DOKill();
                Searching = false;
            }
          
            SearchUi.gameObject.SetActive(false);
        }







        public void DoSearchProgress(float time, System.Action onComplete = null)
        {
            if (Searching) return;

            // 停止当前动画
            SearchUi.DOKill(); // 停止该对象上的所有 DOTween 动画

            Searching = true;

            // 使用 DOTween 进行填充动画
            SearchUi.DOFillAmount(1f, time)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Searching = false;
                    HideSearchUi();
                    onComplete?.Invoke();
                });
        }



    }

}
