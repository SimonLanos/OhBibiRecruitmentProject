using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsGizmosTester : MonoBehaviour
{
    public GameObject prode;
    public int numberOfProdes;
    public float scaleFactor;
    List<Renderer> objects = new List<Renderer>();
    public Transform manip;
    public Vector3 attackOffset;
    public Vector3 attackExtent;

    private void Start()
    {
        for(int x = 0; x < numberOfProdes; x++)
        {
            for (int y = 0; y < numberOfProdes; y++)
            {
                for (int z = 0; z < numberOfProdes; z++)
                {
                    var newProde = Instantiate(prode, transform.position + new Vector3(x, y, z), Quaternion.identity, transform);
                    newProde.transform.localScale *= scaleFactor;
                    objects.Add(newProde.GetComponent<Renderer>());
                }
            }
        }
        prode.SetActive(false);
    }

    public void FixedUpdate()
    {
        for(int i = 0; i < objects.Count; i++)
        {
            objects[i].material.color = Color.white;
        }
        Collider[] colliders = Physics.OverlapBox(manip.transform.position + manip.transform.right * attackOffset.x + manip.transform.up * attackOffset.y + manip.transform.forward * attackOffset.z, attackExtent, manip.transform.rotation);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = manip.transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackOffset, attackExtent*2f);
    }
}
