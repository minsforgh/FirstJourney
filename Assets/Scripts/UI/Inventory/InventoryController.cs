using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryCanvas; 

    // Inventory UI 동작 시 Player Movement, Attack 제한
    // Flip(CanMove, CanAttack) 2개를 listener로 등록
    public UnityEvent inventoryOnOffEvent;

    private InputAction toggleInventoryAction;

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

   // 부착된 게임 오브젝트가 활성화/ 비활성화 될 때 호출
   // Inventory에 부착했으니, 사실상 게임 시작 시에 Enable 되어서 toggleInventoryAction을 Enable 하는 역할
    private void OnEnable()
    {   
        toggleInventoryAction.Enable();
    }

    private void OnDisable()
    {   
        toggleInventoryAction.Disable();
    }

    private void OnToggleInventory(InputAction.CallbackContext context)
    {   
        inventoryOnOffEvent.Invoke();
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
    }
}
