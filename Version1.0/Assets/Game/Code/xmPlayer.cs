using UnityEngine;
using System.Collections;

public class xmPlayer : MonoBehaviour
{

    public Transform thisT;
    public float m_moveSpeed = .3f;
    public float m_rotateSpeed = 5;

    public Vector3[] m_circlePosList;
    void Awake()
    {
        thisT = this.transform;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float xx = Input.GetAxis("Horizontal");
        float yy = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(xx, 0, yy);
        thisT.position += dir * m_moveSpeed*Time.deltaTime;

        float rotxx = Input.GetAxis("Mouse X");
        float rotyy = Input.GetAxis("Mouse Y");

        if(Mathf.Abs(rotxx)>0 || Mathf.Abs(rotyy)>0)
        {
            Vector3 rot = new Vector3(rotxx, 0, rotyy);
            thisT.rotation = Quaternion.Lerp(thisT.rotation, Quaternion.LookRotation(rot),m_rotateSpeed* Time.deltaTime);
        }
        
        //rigidbody.MoveRotation(Quaternion.LookRotation(rot));
        //rigidbody.MovePosition(dir);
    }

    public bool showGizmos;
    public int num = 10;
    void OnDrawGizmos2()
    {
        if(showGizmos)
        {
            //Gizmos.DrawLine(transform.position, transform.forward * 2);
            float t = Mathf.PI / (num / 2f);
            for (int i = 0; i < num; i++)
            {
                float m = 0;
                if (num % 2 == 0)
                {
                    if (i % 2 == 0)
                    {
                        m = transform.localEulerAngles.y * Mathf.Deg2Rad - t / 2f + t * (i + 1) / 2f;
                    }
                    else
                    {
                        m = transform.localEulerAngles.y * Mathf.Deg2Rad - t / 2f - t * (i / 2f);
                    }

                }
                else
                {
                    if (i % 2 == 0)
                    {
                        if (i == 0)
                        {
                            m = transform.localEulerAngles.y * Mathf.Deg2Rad + 0;
                        }
                        else
                        {
                            m = transform.localEulerAngles.y * Mathf.Deg2Rad + t * (i / 2);
                        }

                    }
                    else
                    {
                        m = transform.localEulerAngles.y * Mathf.Deg2Rad - t * (i + 1) / 2;
                    }
                }

                if (i == 0)
                    Gizmos.color = Color.red;
                else if (i == 1)
                    Gizmos.color = Color.yellow;
                else if (i == 2)
                    Gizmos.color = Color.blue;
                else if (i == 3)
                    Gizmos.color = Color.green;
                else if (i == 4)
                    Gizmos.color = Color.magenta;
                else
                    Gizmos.color = Color.gray;
                m_circlePosList[i] = new Vector3(Mathf.Sin(m) * 2, 0, Mathf.Cos(m) * 2);
                Gizmos.DrawSphere(transform.position + new Vector3(Mathf.Sin(m) * 2, 0, Mathf.Cos(m) * 2), .2f);
            }
        }
        }
        
}
