using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnDie : MonoBehaviour
{
    public GameObject icon;
    public float dieTime = 0.5f;

    public void Die()
    {
        this.gameObject.transform.DOScale(0, dieTime).OnComplete(LeaveSkull);
    }

    private void LeaveSkull()
    {
        Instantiate(icon, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
