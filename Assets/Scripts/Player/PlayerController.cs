using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<string> registeredKeyboardInputs = new List<string>()
    {
        "Horizontal",
        "Vertical",
        "Jump",
        "Sprint"
    };

    public Dictionary<string, float> activatedKeys = new Dictionary<string, float>();
    public float walkSpeed = 1;
    
    public Rigidbody2D PlayerRB;
    void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
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
                
            }
        }
        return activatedKeys;
    }

    void HandleKeyPress(Dictionary<string, float> activatedKeys)
    {
        foreach (KeyValuePair<string, float> item in activatedKeys)
        {
            switch (item.Key)
            {
                case "Horizontal":
                    PlayerRB.AddForce(new Vector2(item.Value, 0)*walkSpeed);
                    break;
                // case "Vertical":
                //     PlayerRB.AddForce(new Vector2(0, item.Value));
                //     break;
                case "Jump":
                    PlayerRB.AddForce(new Vector2(0, item.Value));
                    break;
                case "Sprint":
                    walkSpeed
                
            }
        }
    }
}
