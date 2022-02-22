using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    List<Controller> controllers;

    void Awake()
    {
        controllers = FindObjectsOfType<Controller>().ToList();

        int index = 1;
        foreach(var controller in controllers)
        {
            controller.SetIndex(index);
            index++;
        }
    }

    void Update()
    {
        foreach(var controller in controllers)
        {
            if (controller.IsAssigned == false && controller.AnyButtonDown())
                AssignController(controller);
        }
    }

    void AssignController(Controller controller)
    {
        controller.IsAssigned = true;
        Debug.Log("Controller " + controller.gameObject.name + " is Assigned");

        FindObjectOfType<PlayerManager>().AddPlayerToGame(controller);
    }
}
