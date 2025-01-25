using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using MEC;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<string> registeredKeyboardInputs = new List<string>()
    {
        "Horizontal",
        "Vertical",
        "Fire1",
        "Switcher"
    };

    public Dictionary<string, float> activatedKeys = new Dictionary<string, float>();
    public Dictionary<string, float> deactivatedKeys = new Dictionary<string, float>();
    [Header("Player Settings")]
    public float walkSpeed = 10;
    public float sprintSpeed = 15;

    [Header("Object Links")] 
    public GameObject WeaponController;
    public Rigidbody2D PlayerRB;

    private WeaponController _weaponController;
    void Start()
    {
        _weaponController = WeaponController.GetComponent<WeaponController>();
    }
    
    void Update()
    {
        activatedKeys = CheckKeyPress();
    }

    void FixedUpdate()
    {
        HandleKeyPress(activatedKeys);
    }

    Dictionary<string, float> CheckKeyPress()
    {
        Dictionary<string, float> activatedKeys = new Dictionary<string, float>();
        Dictionary<string, float> deactivatedKeys = new Dictionary<string, float>();
        foreach (string k in registeredKeyboardInputs)
        {
            float inputLevel = Input.GetAxis(k);
            if (inputLevel != 0)
            {
                activatedKeys.Add(k, inputLevel);
            }

            if (inputLevel == 0)
            {
                deactivatedKeys.Add(k, inputLevel);
            }
        }

        this.deactivatedKeys = deactivatedKeys;
        return activatedKeys;
    }

    //TimerVars
    private bool WeaponSwitcherTimeout = false;
    
    void HandleKeyPress(Dictionary<string, float> activatedKeys)
    {
        //print(activatedKeys.FirstOrDefault(x => x.Key == "Fire1").Value);
        Vector2 MoveVector = new();
        foreach (KeyValuePair<string, float> item in activatedKeys)
        {
            switch (item.Key)
            { 
                case "Horizontal":
                    MoveVector += new Vector2(item.Value*20*(Input.GetAxisRaw("Sprint") != 0 ? sprintSpeed : walkSpeed), 0);
                    break;
                case "Vertical":
                    MoveVector += new Vector2(0, item.Value*20*(Input.GetAxisRaw("Sprint") != 0 ? sprintSpeed : walkSpeed));
                    break;
                case "Fire1":
                    GetComponentInChildren<WeaponController>().FireWeapon();
                    break;
                case "Switcher":
                    if (WeaponSwitcherTimeout) continue;
                    WeaponSwitcherTimeout = true;
                    var ic = GetComponent<InventoryController>();
                    _weaponController.SwitchWeapon(ic.weapons.FirstOrDefault(x => x.Id != _weaponController.currentWeapon.Id).Type);
                    Timing.CallDelayed(0.25f, () =>
                    {
                        WeaponSwitcherTimeout = false;
                    });
                    break;
            }
        }

        foreach (KeyValuePair<string, float> item in deactivatedKeys)
        {
            switch (item.Key)
            {
                case "Fire1":
                    GetComponentInChildren<WeaponController>().StopWeapon();
                    break;
            }
        }
        
        PlayerRB.AddForce(MoveVector);
    }
}
