using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
	public string itemName;

	public Mesh mesh;
	public Texture2D icon;

	public int stackSize = 1;

	public GameObject droppedPrefab;
}
