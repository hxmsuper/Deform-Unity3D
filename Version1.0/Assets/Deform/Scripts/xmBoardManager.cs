using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class xmBoardManager : MonoBehaviour
{ 
    [SerializeField]
    public List<xmCube> m_nodeList = new List<xmCube>();
    public Transform thisT;
    public float m_R = 2;//radius
    public float m_Space = 1;//scape of each item

    public int m_nodeNum = 4;//box num
    public int m_circleItemNum = 14;//pre
    public static xmBoardManager Instance;
    public Vector3[] m_circlePosList;
    public Vector3[] m_linePosList;

    public enum ShapeType
    {
        Line,
        Circle,
        Square,
    }
    public ShapeType m_shapeType = ShapeType.Line;
    bool isAnimating = false;
    // Use this for initialization

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        thisT = transform;
    }
    void Start()
    {
        Init();
        UpdateLinePos();
        //StartCoroutine(WingAnim());
    }

    protected void Init()
    {
        for (int i = 0; i < m_nodeNum; i++)
        {
            GameObject a = Instantiate(Resources.Load("BoardItem")) as GameObject;
            a.transform.parent = transform;
            m_nodeList.Add(a.GetComponent<xmCube>());
        }
    }
	/// <summary>
	///add new item to list.
	/// </summary>
	protected void AddItemToList()
	{
		GameObject a = Instantiate(Resources.Load("BoardItem")) as GameObject;
		a.transform.parent = transform;
		m_nodeList.Add(a.GetComponent<xmCube>()); 
	}

    public void SetShapeType(ShapeType m)
    {
        switch (m)
        {
            case ShapeType.Line:
                UpdateLinePos();
                break;
            case ShapeType.Circle:
                UpdateCirclePos();
                break;
        }
    }
    protected void UpdateCirclePos()
    {
        UpdateCirclePosList();
        for (int i = 0; i < m_nodeList.Count; i++)
        {
            m_nodeList[i].thisT.position = Vector3.Lerp(m_nodeList[i].thisT.position, m_circlePosList[i], .1f);
            if (isHorizontal)
            {
                m_nodeList[i].thisT.localEulerAngles = Vector3.Lerp(m_nodeList[i].thisT.localEulerAngles, new Vector3(0, 90, 0), .1f);
            }
            else
            {
                m_nodeList[i].thisT.localEulerAngles = Vector3.Lerp(m_nodeList[i].thisT.localEulerAngles, Vector3.zero, .1f);
            } 
        }

    } 
    IEnumerator WingAnim()
    { 
        while(true)
        {
            yield return new WaitForSeconds(1);
            isLine = !isLine;
        }
    }
    protected void UpdateLinePos()
    {

        UpdateLinePosList();
        for (int i = 0; i < m_nodeList.Count; i++)
        {
            m_nodeList[i].thisT.localPosition = Vector3.Lerp(m_nodeList[i].thisT.localPosition, m_linePosList[i], .1f);
            if (isHorizontal)
            {
                m_nodeList[i].thisT.localEulerAngles = Vector3.Lerp(m_nodeList[i].thisT.localEulerAngles, new Vector3(0, 90, 0), .1f);
                //m_nodeList[i].thisT.localEulerAngles = new Vector3(0,90,0);
            }
            else
                m_nodeList[i].thisT.localEulerAngles = Vector3.Lerp(m_nodeList[i].thisT.localEulerAngles, Vector3.zero,.1f);

        }
    }

    protected void UpdateLinePosList()
    {
        m_linePosList = new Vector3[m_nodeList.Count];
        if ((m_nodeList.Count) % 2 == 0)//偶数个
        {
            for (int i = 0; i < m_nodeList.Count; i++)
            {
                if (i % 2 == 0)//奇数位 （从0开始）
                { 
                    m_linePosList[i]= new Vector3(0 - m_Space * (i + 1), 0, 0 + m_R); 
                }
                else
                {
                    m_linePosList[i] = new Vector3(0 + m_Space * (i), 0, 0 + m_R); 
                }
            }
        }
        else//奇数个
        {
            for (int i = 0; i < m_nodeList.Count; i++)
            {
                if (i % 2 == 0)//奇数位 （从0开始）
                {
                    m_linePosList[i] = new Vector3(0 - m_Space * (i), 0, 0 + m_R); 
                }
                else
                {
                    m_linePosList[i] = new Vector3(0 + m_Space * (i + 1), 0, 0 + m_R); 
                }
            }
        }
    } 
    // Update is called once per frame
    bool isLine = true;
    bool isHorizontal = true;
    void Update()
    { 
        if (Input.GetMouseButtonDown(1))
        {
            isLine = !isLine;
        }
        if (isLine)
        {
            //isLine = false;
            UpdateCirclePos();
        }
        else
        {
            //isLine = true;
            UpdateLinePos();
        } 
        if(Input.GetMouseButtonDown(0))
        {
            isHorizontal = !isHorizontal;
            
        } 
        m_R +=Input.GetAxis("Mouse ScrollWheel")*2;
        if(m_R<.5f)
            m_R = .5f;
        m_circleItemNum =(int)m_R * 10;
         
    }

    void UpdateCirclePosList()
    {
        int num = m_circleItemNum;
        m_circlePosList = new Vector3[num];
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

            m_circlePosList[i] = thisT.position+new Vector3(Mathf.Sin(m) * 2, 0, Mathf.Cos(m) * 2);
        }

    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < m_circlePosList.Length; i++)
        { 
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
            Gizmos.DrawSphere(m_circlePosList[i],.2f);
        }
    } 

	void OnGUI()
	{
		if(GUILayout.Button("Add Item"))
		{
            if(m_nodeNum<20)
            {
                AddItemToList();
                m_nodeNum += 1; 
            } 
		}
        if(GUILayout.Button("PlayAnim"))
        {
            isAnimating = !isAnimating;
            if (isAnimating)
                StartCoroutine(WingAnim());
            else
                StopCoroutine("WingAnim");
        }

	}
}
