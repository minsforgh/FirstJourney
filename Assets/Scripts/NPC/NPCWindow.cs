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

    private void OnPressQuit()
    {
        nPCInteraction.HideInteractionUI();
        Destroy(gameObject);
    }

    protected void BasicSet()
    {
        quitButton.onClick.AddListener(OnPressQuit);
        npcIcon.sprite = nPCData.Icon;
        npcName.text = nPCData.Name;
        npcTalk.text = nPCData.Talk;
    }
}
