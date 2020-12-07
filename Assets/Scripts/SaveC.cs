using System;
using UnityEngine;
using System.IO;

public class SaveC : MonoBehaviour
{
	public GameObject car;
	public SaveObj sv =new SaveObj();
	private string patch;
	
	private void Start()
	{
		patch = Path.Combine(Application.dataPath + "Save.json");
		if(File.Exists(patch))
		{
			sv = JsonUtility.FromJson<SaveObj>(File.ReadAllText(patch));
			car.GetComponent<Renderer>().material.color = new Color(sv.cr, sv.cg, sv.cb);
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
		public float cr;
		public float cg;
		public float cb;
	}
}
