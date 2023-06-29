using UnityEngine;

public class PhysicsPickup : MonoBehaviour
{
    [SerializeField] private LayerMask PickupMask;

    [SerializeField] private Camera playerCam;

    [SerializeField] private Transform pickUpTarget;

    [SerializeField] private float pickupRange;

    [SerializeField] private float forceThrow;

    private Rigidbody currentObj;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (currentObj)
            {
                currentObj.useGravity = true;
                currentObj = null;
                return;
            }

            var CameraRay = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(CameraRay, out var HitInfo, pickupRange, PickupMask))
            {
                currentObj = HitInfo.rigidbody;
                currentObj.useGravity = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
            if (currentObj)
            {
                currentObj.useGravity = true;

                currentObj.AddForce(pickUpTarget.forward * forceThrow * 10f);
                currentObj = null;
            }
    }

    private void FixedUpdate()
    {
        if (currentObj)
        {
            var DirectionToPoint = pickUpTarget.position - currentObj.position;
            var DistanceToPoint = DirectionToPoint.magnitude;

            currentObj.velocity = DirectionToPoint * 12f * DistanceToPoint;
        }
    }
}