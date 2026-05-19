using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageFloatingPopup : MonoBehaviour
{
    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private TextMeshProUGUI _damageText;

    private void Awake()
    {
        _canvas = GetComponentInChildren<Canvas>();
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        _damageText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(float damage, bool isCritical)
    {
        _canvasGroup.alpha = 0;
        _damageText.text = $"{damage:F0}";
        _damageText.color = isCritical ? Color.red : Color.green;

        Show().Forget();
    }

    private async UniTaskVoid Show()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, .2f).SetEase(Ease.InCirc);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));

        _canvasGroup.DOFade(0, .2f).SetEase(Ease.OutCirc);
        await UniTask.Delay(TimeSpan.FromSeconds(.5f));
        Destroy(gameObject);
    }
}