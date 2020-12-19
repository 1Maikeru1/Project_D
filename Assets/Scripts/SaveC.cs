using System;
using UnityEngine;
using System.IO;

public class SaveC : MonoBehaviour
{
	public GameObject[] colorcar;
	public SaveObj sv =new SaveObj();
	private string patch;
	
	private void Awake()
	{
		patch = Path.Combine(Application.dataPath + "Save.json");
		if(File.Exists(patch))
		{
			sv = JsonUtility.FromJson<SaveObj>(File.ReadAllText(patch));
			for (int i=0;i<5;i++){
				colorcar[i].GetComponent<Renderer>().material.color = new Color(sv.cr[i], sv.cg[i], sv.cb[i]);
			}
		}
	}

	private void OnApplicationQuit()
	{
		File.WriteAllText(patch, JsonUtility.ToJson (sv));
	}

	public void Save()
	{
		File.WriteAllText(patch, JsonUtility.ToJson (sv));
	}

	[Serializable]
	public class SaveObj
	{
		public int coins;
		public float[] cr;
		public float[] cg;
		public float[] cb;
		public bool[] buyItem;
		public bool[] itemActive;
	}
}
