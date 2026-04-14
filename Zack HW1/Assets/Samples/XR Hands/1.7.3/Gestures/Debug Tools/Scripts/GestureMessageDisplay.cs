using TMPro;
using UnityEngine;

public class GestureMessageDisplay : MonoBehaviour
{
    public TMP_Text messageText;

    private void Start()
    {
        if (messageText != null)
        {
            messageText.text = "Gesture messages appear here";
        }
    }

    public void ClearMessage()
    {
        if (messageText != null)
        {
            messageText.text = "";
        }
    }

    public void LeftThumbsUp()
    {
        if (messageText != null)
        {
            messageText.text = "Left hand thumbs up!";
        }
    }

    public void RightThumbsUp()
    {
        if (messageText != null)
        {
            messageText.text = "Right hand thumbs up!";
        }
    }

    public void LeftFist()
    {
        if (messageText != null)
        {
            messageText.text = "Left hand fist!";
        }
    }

    public void RightFist()
    {
        if (messageText != null)
        {
            messageText.text = "Right hand fist!";
        }
    }

    public void LeftPeace()
    {
        if (messageText != null)
        {
            messageText.text = "Left-hand Shaka!";
        }
    }

    public void RightPeace()
    {
        if (messageText != null)
        {
            messageText.text = "Right-hand Shaka!";
        }
    }
}