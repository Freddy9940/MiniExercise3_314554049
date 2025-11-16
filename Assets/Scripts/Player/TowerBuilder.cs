using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public GameObject[] towerPrefabs;
    public GameObject towerMenu;
    public int towerIndex;
    public float maxDistance;
    public Transform cam;
    RaycastHit hit;
    public Vector3 pos;

    void Update()
    {
        Ray ray = new Ray(cam.position, cam.forward);
        if (Physics.Raycast(ray, out var hit, maxDistance))
        {
            var respawner = hit.collider.GetComponent<TowerRespawner>();
            if (respawner != null && Input.GetKeyDown(KeyCode.E))
            {
                pos = hit.transform.position;
                towerMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void BuildTower() 
    {
        Instantiate(towerPrefabs[towerIndex], pos, Quaternion.identity);
    }
}
