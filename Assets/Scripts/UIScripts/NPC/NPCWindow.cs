using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class NPCWindow : MonoBehaviour
{
    public NPCInteraction nPCInteraction;
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

    private void OnPressQuit()
    {
        AudioManager.Instance.PlayAudio(AudioClipType.Decline);
        nPCInteraction.HideInteractionUI();
        Destroy(gameObject);
    }

    protected void BasicSet()
    {
        npcIcon.sprite = nPCData.Icon;
        npcName.text = nPCData.Name;
        npcTalk.text = nPCData.Talk;
    }
}
