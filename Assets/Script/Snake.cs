using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.zero;

    private List<Transform> _segments = new List<Transform>();

    public Transform segmentPrefab;

    public int initialSize = 5;

    public GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
        ResetState();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }
    }
    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y) + direction.y, 0.0f);
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    public void ResetState()
    {
        for ( int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);     
        }
        _segments.Clear();
        _segments.Add(this.transform);

        for ( int i = 1; i < this.initialSize; i++) 
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Food")
        {
            Grow();
        }
        else if ( collision.tag =="Obstacle")
        {
            ResetState();
            gameManager.appleCurrentScore = gameManager.appleCurrentScore - 10;
            if( gameManager.appleCurrentScore <= 0 )
            {
                gameManager.appleCurrentScore = 0;
            }
        }
        else if( collision.tag =="Trap")
        {
            ResetState();
            GameManager.Instance.GameOver();
        }
    }


}
