using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Transform originalParent;
    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; // 드래그 시작 시 원래 부모 저장
        originalPosition = rectTransform.anchoredPosition; // 드래그 시작 시 원래 위치 저장

        transform.SetParent(canvas.transform); // 드래그 중에는 최상위 Canvas의 자식으로 이동
        canvasGroup.blocksRaycasts = false; // 드래그 중 다른 UI와 상호작용하지 않도록 설정
        canvasGroup.alpha = 0.6f; // 드래그 중 투명도 조절
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // 드래그 위치 업데이트
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent); // 드래그 종료 시 원래 부모로 돌아가기
        canvasGroup.blocksRaycasts = true; // UI 상호작용 다시 활성화
        canvasGroup.alpha = 1.0f; // 투명도 원상복구

        // 원래 위치로
        rectTransform.anchoredPosition = originalPosition;
    }
}

