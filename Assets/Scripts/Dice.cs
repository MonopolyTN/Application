using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour {

	public Rigidbody rb;
	private Vector3 initPosition;

	public System.Boolean thrown;
	public System.Boolean hasLanded;

	public int diceValue;

	private DiceSide[] diceSides;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		initPosition = transform.position;
		rb.useGravity = false;
		diceSides = GetComponentsInChildren<DiceSide>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void RollDice()
    {
		rb.isKinematic = false;
		thrown = true;
		rb.useGravity = true;

		float dirX = Random.Range(0, 500);
		float dirY = Random.Range(0, 500);
		float dirZ = Random.Range(0, 500);

		Vector3 fromPosition = transform.position;
		Vector3 toPosition = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
		Vector3 direction = toPosition - fromPosition;
		direction = new Vector3(direction.x, direction.y + 8, direction.z);
		rb.AddForce(direction * 30, ForceMode.Acceleration);
		rb.AddTorque(dirX, dirY, dirZ);
    }

    public void Reset(float zPosition)
    {
		transform.position = new Vector3(initPosition.x, initPosition.y, zPosition);
		transform.rotation = Quaternion.Euler(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.isKinematic = true;
		thrown = false;
		hasLanded = false;
		rb.useGravity = false;
    }

	public int SideValueCheck()
    {
		foreach (DiceSide side in diceSides)
        {
			if (side.OnGround())
            {
				return side.sideValue;
            }
        }
		return 0;
    }

	public interface DiceReset
    {
		void Reset();
    }
}
