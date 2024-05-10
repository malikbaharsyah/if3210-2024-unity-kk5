using System.Collections;
using UnityEngine;
using QFSW.QC;

public class PlayerMovement : MonoBehaviour
{
    public GlobalStatistics statMg;
    public LocalStatistics locStatMg;
    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;
    bool isIncreaseSpeed = false;
    float multiplier = 1.2f;
    Coroutine increaseSpeedCourutine;

    private void Awake()
    {
        statMg = FindObjectOfType<GlobalStatistics>();
        locStatMg = FindObjectOfType<LocalStatistics>();
        //mendapatkan nilai mask dari layer yang bernama Floor
        floorMask = LayerMask.GetMask("Floor");

        //Mendapatkan komponen Animator
        anim = GetComponent<Animator>();

        //Mendapatkan komponen Rigidbody
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Mendapatkan nilai input horizontal (-1,0,1)
        float h = Input.GetAxisRaw("Horizontal");

        //Mendapatkan nilai input vertical (-1,0,1)
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    //Method player dapat berjalan
    public void Move(float h, float v)
    {
        //Set nilai x dan y
        movement.Set(h, 0f, v);

        //Menormalisasi nilai vector agar total panjang dari vector adalah 1
        movement = movement.normalized * speed * Time.deltaTime;
        float distance = movement.magnitude;
        statMg.RecordDistance(distance);
        locStatMg.RecordDistance(distance);
        //Move to position
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        //Buat Ray dari posisi mouse di layar
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Buat raycast untuk floorHit
        RaycastHit floorHit;

        //Lakukan raycast
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            //Mendapatkan vector daro posisi player dan posisi floorHit
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            //Mendapatkan look rotation baru ke hit position
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            //Rotasi player
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    public void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

    public void IncreaseSpeedByOrb()
    {
        if (isIncreaseSpeed)
        {
            Debug.Log("Speed is already increased");
            if (increaseSpeedCourutine != null)
            {
                Debug.Log("Stop Coroutine");
                StopCoroutine(increaseSpeedCourutine);
                increaseSpeedCourutine = null;
            }
            speed /= multiplier;
        }
        isIncreaseSpeed = true;
        speed *= multiplier;
        Debug.Log("Speed increased by orb: " + speed);
        increaseSpeedCourutine = StartCoroutine(IncreaseSpeed());
    }

    IEnumerator IncreaseSpeed()
    {
        Debug.Log("Coroutine Increase Speed started");
        yield return new WaitForSeconds(15);
        speed /= multiplier;
        Debug.Log("Speed returned to normal: " + speed);
        isIncreaseSpeed = false;
    }

    public void SetDoubleSpeed()
    {
        speed *= 2;
    }

    [Command("x2")]
    private void DoubleSpeed()
    {
        SetDoubleSpeed();
        Debug.Log("Cheat Double Speed activated");
    }
}
