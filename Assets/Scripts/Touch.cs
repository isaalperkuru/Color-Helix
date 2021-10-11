using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private static bool pressing;

    public static bool IsPressing()
    {
        return pressing;
    }

   public void OnPointerDown(PointerEventData data)
    {
        pressing = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        pressing = false;
    }
}
