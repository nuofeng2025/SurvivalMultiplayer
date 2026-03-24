using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace FGame
{
    public class InteractionUi : MonoBehaviour
    {
        public Image SearchUi;



        private bool Searching;


        private void Awake()
        {
            
        }

        private void Start()
        {
            HideSearchUi();
        }

        public void Init()
        {
            EventController.InteractionCenterEnter.AddListener(ShowInteractionUi);
            EventController.InteractionCenterLeave.AddListener(HideInteractionUi);


        }



        public void ShowInteractionUi(IInteractive interactive)
        {
            switch (interactive.Type)
            {
                case InteractiveType.SearchContainer:
                    ShowSearchUi();
                    break;
            
            }


        
        
        }


        public void HideInteractionUi(IInteractive interactive)
        {
            switch (interactive.Type)
            {
                case InteractiveType.SearchContainer:
                    HideSearchUi();
                    break;

            }




        }


        public void ShowSearchUi()
        {
            SearchUi.fillAmount = 0;
            SearchUi.gameObject.SetActive(true);
            //GameManager.Instance.uiSystem.crosshair.SetActive(false);
        }

        public void HideSearchUi()
        {
            if(Searching)
            {
                SearchUi.DOKill();
                Searching = false;
            }
          
            SearchUi.gameObject.SetActive(false);
            //GameManager.Instance.uiSystem.crosshair.SetActive(true);
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

        private void OnDestroy()
        {
            EventController.InteractionCenterEnter.RemoveListener(ShowInteractionUi);
            EventController.InteractionCenterLeave.RemoveListener(HideInteractionUi);
        }

    }

}
