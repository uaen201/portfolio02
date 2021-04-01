using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : UIScript
{
    [SerializeField]
    Camera maincam;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 euler = maincam.transform.rotation.eulerAngles;
            euler.y = euler.y +1;
            maincam.transform.rotation = Quaternion.Euler(euler);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 euler = maincam.transform.rotation.eulerAngles;
            euler.y = euler.y - 1;
            maincam.transform.rotation = Quaternion.Euler(euler);
        }
    }
    public void set()
    {
    }
}
