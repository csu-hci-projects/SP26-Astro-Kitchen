using UnityEngine;

public class StoveBurner : MonoBehaviour
{
    public bool isHot = false;

    // You can link these to a VR dial, button, or switch event!
    public void TurnOnStove()
    {
        isHot = true;
    }

    public void TurnOffStove()
    {
        isHot = false;
    }
}