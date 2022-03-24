using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : FillUI
{
    public Transform target;
    public Vector3 offset;

    public EnemyController owner;

    public void UpdatePosition()
    {
        Vector3 dir = (target.position - Camera.main.transform.position).normalized;
        bool isBehind = Vector3.Dot(dir, Camera.main.transform.transform.forward) <= 0.0f;
        foreground.enabled = !isBehind;
        background.enabled = !isBehind;
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);

    }

    public override void UpdateFill(float percent)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float newWidth = parentWidth * percent;
        foreground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
    }


    private void Start()
    {
        owner = GetComponentInParent<EnemyController>();
        owner.OnDamage += UpdateFill;
    }
    private void LateUpdate()
    {
        UpdatePosition();
    }

    private void OnDestroy()
    {
        owner.OnDamage -= UpdateFill;
    }
}
