using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    [SerializeField]
    private int speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RepeatSpin());
    }

    private IEnumerator RepeatSpin()
    {
        while (true)
        {
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * speed));
            yield return new WaitForSeconds(0.04f);
        }
    }
}
