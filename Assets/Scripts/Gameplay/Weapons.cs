using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class Weapons : NetworkBehaviour
{
	public Transform cameraTransform;

	public Animator itemSlotAnimator;
	[HideInInspector]
	public Animator itemInHandAnimator;

	public Gun[] guns;
	public int selectedGunTypeIdx = 0;
	public int currentMagFill = 0;
	private float cooldownTime = 0f;

	public bool isADS = false;
	public bool isFiring = false;
	public bool isReloading = false;

    public int selectedWeaponTypeIdx = 0;
    public GameObject itemSlot;

    void Start()
    {
		if (!IsOwner) return;

		SelectWeapon();
    }

	private void Update()
	{
		if (!IsOwner) return;

		itemInHandAnimator = GameObject.FindGameObjectWithTag("WeaponInHand").GetComponent<Animator>();

		itemSlotAnimator.SetBool("isADS", isADS);
		itemInHandAnimator.SetBool("isADS", isADS);
		isADS = Input.GetButton("Fire2");

		Reload();
		Attack();
	}
	private void Reload()
	{
		if(Input.GetButtonDown("Reload") && !isReloading && !isADS)
		{
			isReloading = Input.GetButton("Reload");
			itemInHandAnimator.SetTrigger("isReloading");

			currentMagFill = guns[selectedGunTypeIdx].magazineSize;

			StartCoroutine(WaitForReload());
		}
	}
	private void Attack()
	{
		isADS = Input.GetButton("Fire2");
		isFiring = Input.GetButtonDown("Fire1");

		switch (selectedWeaponTypeIdx)
		{
			case 0: AttackGun(); break;
			case 1: AttackMelee(); break;
			case 2: SelectThrowable(); break;
			default: SelectGun(); break;
		}
	}

	private void AttackGun()
	{
		switch (guns[selectedGunTypeIdx].gunCategoryIdx) 
		{
			case 0: Handgun(); break;
			case 1:	MachineGun(); break;
			case 2: SniperRifle(); break;
			case 3: Shotgun(); break;
			default: Handgun(); break;
		}
	}

	void Handgun()
	{
		if(Input.GetButtonDown("Fire1") && isADS)
		{

			RaycastHit hit;
			if(Physics.Raycast(cameraTransform.position, cameraTransform.transform.forward, out  hit, guns[selectedGunTypeIdx].gunRange) && currentMagFill > 0 && Time.time >= cooldownTime)
			{
				itemInHandAnimator.SetTrigger("isFiring");

				currentMagFill = currentMagFill - 1;

				float damage = guns[selectedGunTypeIdx].damageOverDistance.Evaluate(hit.distance / guns[selectedGunTypeIdx].gunRange) * guns[selectedGunTypeIdx].damage;
				
				Health health = hit.transform.GetComponent<Health>();
				health.Damage((int)damage);

				cooldownTime = Time.time + 1 / guns[selectedGunTypeIdx].fireRate;
			}
			else if (currentMagFill > 0)
			{
				itemInHandAnimator.SetTrigger("isFiring");

				currentMagFill = currentMagFill - 1;

				cooldownTime = Time.time + 1 / guns[selectedGunTypeIdx].fireRate;
			}
		}
	}

	void MachineGun()
	{
		if (Input.GetButton("Fire1") && isADS)
		{
			RaycastHit hit;
			if (Physics.Raycast(cameraTransform.position, cameraTransform.transform.forward, out hit, guns[selectedGunTypeIdx].gunRange) && currentMagFill > 0 && Time.time >= cooldownTime)
			{
				itemInHandAnimator.SetTrigger("isFiring");

				currentMagFill -= 1;

				float damage = guns[selectedGunTypeIdx].damageOverDistance.Evaluate(hit.distance / guns[selectedGunTypeIdx].gunRange) * guns[selectedGunTypeIdx].damage;

				Health health = hit.transform.GetComponent<Health>();
				health.Damage((int)damage);

				cooldownTime = Time.time + 1 / guns[selectedGunTypeIdx].fireRate;
			} else if(currentMagFill> 0)
			{
				itemInHandAnimator.SetTrigger("isFiring");

				currentMagFill = currentMagFill - 1;

				cooldownTime = Time.time + 1 / guns[selectedGunTypeIdx].fireRate;
			}
		}
	}

	void SniperRifle()
	{
		//to be added
	}

	void Shotgun()
	{
		//To be added
	}

	void AttackMelee()
	{
		//To be added
	}

	void AttackThrowable()
	{
		//To be added
	}

	public void SelectWeapon()
	{
		switch (selectedWeaponTypeIdx)
        {
            case 0: SelectGun(); break;
            case 1: SelectMelee(); break;
            case 2: SelectThrowable(); break;
            default: SelectGun(); break;
        }
	}

	void SelectGun()
	{
        foreach(Transform child in itemSlot.transform) 
        {
            Destroy(child.gameObject);
        }

		currentMagFill = guns[selectedGunTypeIdx].magazineSize;
		Gun selectedGun = guns[selectedGunTypeIdx];
        Instantiate(selectedGun.gunPrefab, itemSlot.transform.position + selectedGun.position.position, itemSlot.transform.rotation, itemSlot.transform);
	}

	void SelectMelee()
	{
        //To be added.
	}

	void SelectThrowable()
	{
        //To be added.
	}

	IEnumerator WaitForReload() 
	{
		yield return new WaitForSeconds(guns[selectedGunTypeIdx].reloadTime);

		isReloading = false;
	}

	/*
     * Legend:
     * 
     * selectedWeaponTypeIdx
     * 
     *    0)    Gun
     *    1)    Melee (to be added)
     *    2)    Throwable (to be added)
     *    
     * selectedGunTypeIdx
     * 
     *    Defined by contents in guns Array
     *    
     *        Defaults:
     *        
     *            0)    M1911
     */
}