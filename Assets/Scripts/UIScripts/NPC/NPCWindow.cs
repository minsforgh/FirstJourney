using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class NPCWindow : MonoBehaviour
{
    public NPCInteraction nPCInteraction;
    // 자식의 Inspecotr 창에서 설정할 수 있도록 SerializeField로 설정
    // 객체 지향 철학과 맞지 않지만, 사용자 편의성을 위한 Unity의 기능
    [SerializeField] private Image npcIcon;
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private TextMeshProUGUI npcTalk;
    [SerializeField] private Button quitButton;

    public NPCData nPCData;

    protected virtual void OnEnable()
    {
        quitButton.onClick.AddListener(OnPressQuit);
    }

    protected virtual void OnDisable()
    {
        quitButton.onClick.RemoveListener(OnPressQuit);
    }

    protected void OnPressQuit()
    {   
        nPCInteraction?.HideInteractionUI();
        // Quit 버튼을 통해 끌 경우 interactionUIInstance가 null이라서, 직접 gameObject 파괴
        UIManager.Instance.DestroyUI(gameObject);
    }

    protected void BasicSet()
    {
        npcIcon.sprite = nPCData.Icon;
        npcName.text = nPCData.Name;
        npcTalk.text = nPCData.Talk;
    }
}
