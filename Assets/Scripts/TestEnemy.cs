using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 아직 임시
 */
public class TestEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform MyTransform;

    public Transform GetTransform()
    {
        return MyTransform;
    }

    private void Awake()
    {
        GameManager.Instance.GetBattleSystem().AddEnemy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
    }
}
