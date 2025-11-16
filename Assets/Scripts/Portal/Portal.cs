using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject dest;
    public ParticleSystem teleportEffect;   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = dest.transform.position;
            if (teleportEffect != null)
                Instantiate(teleportEffect, dest.transform.position, Quaternion.identity);
        }
    }
}
