using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public int Index { get; private set; }
    public bool IsAssigned { get; set; }

    public bool attack;
    public bool attackPressed = false;
    public bool specialAttackPressed = false;
    public bool jumpPressed = false;

    public float horizontal;
    public float vertical;

    string attackButton;
    string specialAttackButton;
    string jumpButton;
    string horiAxis;
    string vertAxis;

    void Update()
    {
        if (!string.IsNullOrEmpty(attackButton))
        {
            attack = Input.GetButton(attackButton);
            attackPressed = Input.GetButtonDown(attackButton);
            specialAttackPressed = Input.GetButtonDown(specialAttackButton);
            jumpPressed = Input.GetButtonDown(jumpButton);
            horizontal = Input.GetAxis(horiAxis);
            vertical = Input.GetAxis(vertAxis);
        }
    }

    internal bool ButtonDown(PlayerButton button)
    {
        switch (button)
        {
            case PlayerButton.Attack:
                {

                return attackPressed;
                }

            case PlayerButton.SpecialAttack:
                {

                    return specialAttackPressed;
                }

            case PlayerButton.Jump:
                {

                    return jumpPressed;
                }

            default:
                return false;
        }
    }

    internal void SetIndex(int index)
    {
        Index = index;
        attackButton = "Attack" + Index;
        specialAttackButton = "Special" + Index;
        jumpButton = "Jump" + Index;

        horiAxis = "Horizontal" + Index;
        vertAxis = "Vertical" + Index;
        gameObject.name = "Controller" + Index;
    }

    internal bool AnyButtonDown()
    {
        return attack;
    }

    internal Vector3 GetDirection()
    {
        return new Vector3(horizontal, 0, -vertical);
    }
}