using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class camera : MonoBehaviour {

    public Transform Target;
    public float zOffset = 0;
    public float Distance = 4.5f;
    public float ZoomStep = 1.0f;
    public float MoveSpeed = 5f;
    public float Pitch = 30f;
    public float yaw = 45f;
    public float TargetMoveSpeed = 3f;
    public float RotateSpeed = 60f;
    private Animator[] animator;

    private Vector3 TargetPos;
    private Vector3 LookPoint;

    void Start()
    {
        transform.position = GetPosition();
        LookPoint = Target.position;
        animator = GameObject.FindObjectsOfType<Animator>();
        Debug.Log("animators = " + animator.Length);
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (Input.GetKeyDown(KeyCode.X)) // 
        {
            foreach (Animator a in animator)
            {
                a.SetBool("normal_walk", !a.GetBool("normal_walk"));
                a.SetBool("aggressive_walk", !a.GetBool("aggressive_walk"));
                a.SetBool("run", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.C)) // 
        {
            foreach (Animator a in animator)
            {
                a.SetBool("run", !a.GetBool("run"));
                a.SetBool("normal_walk", false);
                a.SetBool("aggressive_walk", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) // to idle
        {
            foreach (Animator a in animator)
            {
                a.SetBool("aggressive", !a.GetBool("aggressive"));
                a.SetBool("normal_walk", false);
                a.SetBool("aggressive_walk", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) //
            foreach (Animator a in animator)
                a.SetTrigger("turn_l");

        if (Input.GetKeyDown(KeyCode.RightArrow)) //
            foreach (Animator a in animator)
                a.SetTrigger("turn_r");

        if (Input.GetKeyDown(KeyCode.UpArrow)) // 
            foreach (Animator a in animator)
                a.SetTrigger("turn");
        if (Input.GetKeyDown(KeyCode.DownArrow)) // 
            foreach (Animator a in animator)
                a.SetTrigger("turn2");

        if (Input.GetKeyDown("5")) // to idle
            foreach (Animator a in animator)
                a.SetTrigger("hit0");
        if (Input.GetKeyDown("6")) // to idle
            foreach (Animator a in animator)
                a.SetTrigger("hit1");

        if (Input.GetKeyDown("1")) // to idle
            foreach (Animator a in animator)
                a.SetTrigger("attack0");
        if (Input.GetKeyDown("2")) // to idle
            foreach (Animator a in animator)
                a.SetTrigger("attack1");
        if (Input.GetKeyDown("3")) // to idle
            foreach (Animator a in animator)
                a.SetTrigger("attack2");
        if (Input.GetKeyDown("4")) // to idle
            foreach (Animator a in animator)
                a.SetTrigger("attack3");

        if (Input.GetKeyDown("7")) // to idle
            foreach (Animator a in animator)
                a.SetTrigger("death0");
        if (Input.GetKeyDown("8")) // to idle
            foreach (Animator a in animator)
                a.SetTrigger("death1");
        if (Input.GetKeyDown("9")) // to idle
            foreach (Animator a in animator)
                a.SetTrigger("death2");
        if (Input.GetKeyDown("0")) // to idle
            foreach (Animator a in animator)
            {
                a.SetTrigger("death3");
                a.SetBool("normal_walk", false);
                a.SetBool("aggressive_walk", false);
            }

        if (Input.GetKeyDown(KeyCode.R)) // to idle
            foreach (Animator a in animator)
                a.SetTrigger("reset");


        if (Input.GetKey("a"))
        {
            yaw -= Time.deltaTime * RotateSpeed;
        }
        if (Input.GetKey("d"))
        {
            yaw += Time.deltaTime * RotateSpeed;
        }
        if (Input.GetKey("w") && Distance > 2f)
        {
            Distance -= Time.deltaTime * ZoomStep;
        }

        if (Input.GetKey("s") && Distance < 14f)
        {
            Distance += Time.deltaTime * ZoomStep;
        }
        if (Input.GetKey("q") && Pitch > 10f)
        {
            Pitch -= Time.deltaTime * RotateSpeed;
        }
        if (Input.GetKey("e") && Pitch < 75f)
        {
            Pitch += Time.deltaTime * RotateSpeed;
        }

        LookPoint = Vector3.MoveTowards(LookPoint, Target.position + Vector3.up * zOffset, TargetMoveSpeed * Time.deltaTime);

        TargetPos = GetPosition();
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, MoveSpeed * Time.deltaTime);
        transform.LookAt(LookPoint);
    }

    Vector3 GetPosition()
    {
        float y = Distance * Mathf.Sin(Pitch * Mathf.Deg2Rad);
        float r = Distance * Mathf.Cos(Pitch * Mathf.Deg2Rad);
        float x = r * Mathf.Cos(yaw * Mathf.Deg2Rad);
        float z = r * Mathf.Sin(yaw * Mathf.Deg2Rad);

        return (Target.position + new Vector3(x, y, z));
    }
}