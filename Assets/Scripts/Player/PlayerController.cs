using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<string> registeredKeyboardInputs = new List<string>()
    {
        "Horizontal",
        "Vertical"
    };

    public Dictionary<string, float> activatedKeys = new Dictionary<string, float>();
    public Dictionary<string, float> deactivatedKeys = new Dictionary<string, float>();
    
    public float walkSpeed = 10;
    public float sprintSpeed = 15;
    
    public Rigidbody2D PlayerRB;
    void Start()
    {
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

    void HandleKeyPress(Dictionary<string, float> activatedKeys)
    {
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
            }
        }

        foreach (KeyValuePair<string, float> item in deactivatedKeys)
        {
            switch (item.Key)
            {
                
            }
        }
        
        PlayerRB.AddForce(MoveVector);
    }
}
