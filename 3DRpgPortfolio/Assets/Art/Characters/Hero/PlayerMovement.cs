using Rpg.Saving;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, ISaveable
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Animator animator;

    // Update is called once per frame
    void Update()
    {
        Vector3 movementVector = Vector3.zero; // (0,0,0)
        
        movementVector += Vector3.right * (Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime);
        movementVector += Vector3.forward * (Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime);

        animator.SetBool("IsMovement", movementVector != Vector3.zero);

        transform.Translate(movementVector);

        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");
        }
    }

    public object CaptureState()
    {
        SerializableVector3 position = new SerializableVector3(transform.position);
        return position;
    }

    public void RestoreState(object state)
    {
        var loadedPosition = state as SerializableVector3;
        if (loadedPosition != null)
        {
            transform.position = loadedPosition.ToVector();
        }
    }
}
