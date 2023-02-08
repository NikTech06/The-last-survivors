using UnityEngine;

[CreateAssetMenu]
public class Gun : Item
{
	public Transform position;//

	public GameObject gunPrefab;//

	public int gunCategoryIdx = 0; //

	public int magazineSize = 7; //
	public int fireRate = 5; //
	public int damage = 7; //
	public int maxHeat = 10;
	public int heatColldownSpeed = 1;
	public float gunRange = 10f; //
	public float reloadTime = 2f;

	public AnimationCurve damageOverDistance; //

	public AudioClip fireSound;
	public AudioClip reloadSound;

	public GameObject muzzleFlashPrefab;
	public Transform muzzleFlashTransform;

	//recoil needed to be added

	/*
	 * Legend:
	 * 
	 *	gunCategoryIdx:
	 *	
	 *		0)	normal handgun
	 *		1)	machine gun
	 *		2)	sniper rifle
	 *		3)	shotgun
	 */
}
