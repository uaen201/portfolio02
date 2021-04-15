using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 공격 범위 오브젝트
 */

public class RangePlane : MonoBehaviour
{
    [SerializeField]
    private Transform MyTransform;
    private void Awake()
    {
        MyTransform = gameObject.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameManager.Instance.GetBattleSystem().
                AddEnemyOnOperatorRange(MyTransform.parent.gameObject,
                other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //- remove
            GameManager.Instance.GetBattleSystem().
                RemoveEnemyOnOperatorRange(MyTransform.parent.gameObject,
                other.gameObject);
        }
    }
}
