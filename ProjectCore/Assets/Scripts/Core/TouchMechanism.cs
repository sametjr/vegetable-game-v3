using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchMechanism : MonoBehaviour
{
    private bool onDrag = false;
    private Vector3 offset;
    private float previousZPos;
    public bool isActive = true;

    // private PanIngredients pan;

    #region DoubleClickVariables
    private int clickCount = 0;
    private float thresholdValue = .3f;
    private double lastClick;
    #endregion

    private void OnMouseDown()
    {
        onDrag = true;

        if (ControlDoubleClick() && isActive && GameManager.Instance.canPlayerInteract)
        {
            Debug.Log("Double Clicked");
            SoundManager.Instance.PlaySound(SoundManager.Sounds.Rollover);
            SendObjectToPan();
            onDrag = false;
        }

        previousZPos = transform.position.z;
        offset = transform.position - GameManager.Instance.cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void SendObjectToPan()
    {
        // LeanTween.move(this.transform.parent.gameObject, new Vector3(0, 3, 0), .5f).setEaseInBack().setOnComplete(() => LeanTween.move(this.transform.parent.gameObject, GameManager.Instance.pan.transform.position + Vector3.back, .4f).setEaseOutBack());
        Pan.Instance.SendObjectToPan(this.transform.parent.gameObject);

    }

    private bool ControlDoubleClick()
    {
        if (clickCount == 0)
        {
            lastClick = Time.time;
            clickCount++;
        }

        else if (clickCount == 1 && Time.time - lastClick <= thresholdValue)
        {
            clickCount = 0;
            return true;
        }

        else
        {
            clickCount = 1;
            lastClick = Time.time;
            return false;
        }

        return false;

    }

    private void OnMouseUp()
    {
        onDrag = false;
        transform.parent.transform.position = new Vector3(transform.position.x, transform.position.y, previousZPos);
    }

    private void FixedUpdate()
    {
        if (onDrag && isActive && GameManager.Instance.canPlayerInteract)
        {
            transform.parent.transform.position = GameManager.Instance.cam.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.parent.transform.position = new Vector3(transform.position.x, transform.position.y, 81);
        }
    }
}
