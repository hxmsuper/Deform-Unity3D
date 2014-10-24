using UnityEngine;
using System.Collections;

public class CronGame : MonoBehaviour {

	int mKey1 = 1;//这个是正常使用的数据

    int mKey2//这个是需要加密处理的数据
    {
        get { 
            return (key2^mEncryptKey); 
        }
        set{
            key2=(value^=mEncryptKey); 
        }
    }
    int key2;

    private int mEncryptKey = 1232; //密钥可以是任意多位正整数

	delegate int GetValue(int value); 

	GetValue getValue;
	void Start () {
        mKey2 = 1;
		getValue = x=>x*x+x;
	}
	
	void OnGUI()
	{
		GUILayout.Label(mKey1.ToString());
		if(GUILayout.Button("Change"))//普通值修改
		{
			RandomAdd(ref mKey1);
		}
        GUILayout.Space(120);

        GUILayout.Label(mKey2.ToString());
        if (GUILayout.Button("Change"))//加密值修改
        { 
            int x = mKey2;
            mKey2 +=RandomAdd();
        }
	}

	void RandomAdd(ref int key)
	{ 
		int k = Random.Range(1,5);
		key += getValue(k); 
	}
    int RandomAdd()
    {
        int k = Random.Range(1, 5);
        
        return getValue(k);
    }
}
