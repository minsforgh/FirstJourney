using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerState playerState;

    public GameObject inventoryCanvas;
    private InputAction toggleInventoryAction;

    public UnityEvent ShowUIEvent;
    private bool isInventoryActive = false;

    // InventoryControls(Input Actions) - Player(Map) - ToggleInventory(Action)
    private void Awake()
    {
        // Load Input Actions Inventory Controls
        var inputActions = new InventoryControls();

        // Player Action Map의 ToggleInventory Acton을 가져옴
        toggleInventoryAction = inputActions.Player.ToggleInventory;

        // ToggleInventory action에 method 등록(listener)
        toggleInventoryAction.performed += OnToggleInventory;
    }

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerState = playerController.GetPlayerState();
    }
    
    // 부착된 게임 오브젝트가 활성화/ 비활성화 될 때 호출
    // Inventory에 부착했으니, 사실상 게임 시작 시에 Enable 되어서 toggleInventoryAction을 Enable 하는 역할
    private void OnEnable()
    {
        toggleInventoryAction.Enable();
        ShowUIEvent.AddListener(() => playerState.SetIsInteracting(true));
    }

    private void OnDisable()
    {
        toggleInventoryAction.Disable();
        ShowUIEvent.RemoveAllListeners();
    }

    private void OnToggleInventory(InputAction.CallbackContext context)
    {
        if (isInventoryActive)
        {
            playerState.SetIsInteracting(false);
            inventoryCanvas.SetActive(false);
        }
        else
        {
            ShowUIEvent?.Invoke();
            inventoryCanvas.SetActive(true);
        }

        isInventoryActive = !isInventoryActive;
    }
}
