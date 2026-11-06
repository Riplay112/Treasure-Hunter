using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimator : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    [Header("Animasi Settings")]
    public float hoverScale = 1.15f;
    public float clickPunch = 0.2f;
    public float duration = 0.2f;

    private Vector3 originalScale;

    void OnEnable()
    {
        transform.DOKill();
        originalScale = transform.localScale;
    }

    // Hover masuk → membesar
    public void OnPointerEnter(PointerEventData e)
    {
        transform.DOKill();
        transform.DOScale(originalScale * hoverScale, duration)
                 .SetEase(Ease.OutBack)
                 .SetUpdate(true); 
    }

    // Hover keluar → kembali normal
    public void OnPointerExit(PointerEventData e)
    {
        transform.DOScale(originalScale, duration)
                 .SetEase(Ease.OutBack)
                 .SetUpdate(true);
    }

    // Klik → bounce
    public void OnPointerClick(PointerEventData e)
    {
        transform.DOPunchScale(Vector3.one * clickPunch, 0.3f, 5, 0.5f)
            .SetUpdate(true);
    }

    public void ResetScale()
    {
        transform.DOKill();
        transform.localScale = originalScale;
    }
}