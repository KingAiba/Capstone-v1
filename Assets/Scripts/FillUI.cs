using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FillUI : MonoBehaviour
{
    public Image foreground;
    public Image background;


    public virtual void UpdateFill(float percent)
    {
        foreground.fillAmount = percent;
        if(percent >= 0.98f)
        {
            DisableFillOnComplete();
        }
    }

    private void DisableFillOnComplete()
    {
        gameObject.SetActive(false);
    }

}
