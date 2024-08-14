using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnReturnButton);
    }

    public void OnReturnButton()
    {   
        AudioManager.Instance.PlayAudio(AudioClipType.Decline);
        Destroy(transform.parent.gameObject);
    }
}
