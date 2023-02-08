using UnityEngine;
using Unity.Netcode;

public class PlayerLook : NetworkBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    private float xRotation = 0f;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
    {
		if (!IsOwner) return;

		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
				Cursor.lockState = CursorLockMode.None;
                Debug.Log("Unlocked Cursor");
			} else
            {
				Cursor.lockState = CursorLockMode.Locked;
				Debug.Log("Locked Cursor");
			}
        }
	}
}
