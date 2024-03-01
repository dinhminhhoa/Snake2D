using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private GameObject trapPrefabs;

    public BoxCollider2D trapArea;

    [SerializeField] private float timeOnMap;
    [SerializeField] private float respawn;
    
    private void Start()
    {
        RandomizePosition();
    }
    private void Update()
    {
        ReSpawnTrap();
    }
    private void RandomizePosition()
    {
        Bounds bounds = this.trapArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    public void ReSpawnTrap()
    {
        timeOnMap -= Time.deltaTime;
        if( timeOnMap <= 0) 
        {
            RandomizePosition();
            timeOnMap = respawn;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            GameManager.Instance.GameOver();
            
        }
    }


}
