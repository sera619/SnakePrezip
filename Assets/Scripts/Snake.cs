using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Variablen deklarieren
    private Vector2 moveDirection = Vector2.up;
    public GameObject segmentPrefab;
    public List<GameObject> segments = new List<GameObject>();
    
    void Start()
    {
        // Schlangenkopf der Liste hinzufügen
        segments.Add(gameObject);
    }

    void Update()
    {
        // Richtung der Schlange mit WASD ändern
        if (Input.GetKeyDown("w") && moveDirection != Vector2.down)
        {
            moveDirection = Vector2.up;
        }
        if (Input.GetKeyDown("s") && moveDirection != Vector2.up)
        {
            moveDirection = Vector2.down;
        }
        if (Input.GetKeyDown("a") && moveDirection != Vector2.right)
        {
            moveDirection = Vector2.left;
        }
        if (Input.GetKeyDown("d") && moveDirection != Vector2.left)
        {
            moveDirection = Vector2.right;
        }
    }

    // Funktion, die den Kopf der Schlange bewegt
    private void HeadMovement()
    {
        transform.position = (Vector2)transform.position + moveDirection;
    }

    // Funktion, die die Segmente der Schlange bewegt
    private void SegmentMovement()
    {
        for(int i = segments.Count - 1; i>0; i--)
        {
            segments[i].transform.position = segments[i - 1].transform.position;
        }
    }

    private void FixedUpdate()
    {
        // Bewegt Kopf und Segmente in regelmäßigen Zeitintervallen
        SegmentMovement();
        HeadMovement();
    }

    // Erzeugt neues Segment an der Position des letzten Elements der Liste 'segments'
    public void ExpandSnake()
    {
        GameObject segment = Instantiate(segmentPrefab, segments[segments.Count - 1].transform.position, segmentPrefab.transform.rotation);
        segments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Wenn der Trigger des Food Objects getriggert wird, wird ExpandSnake() ausgeführt
        if(collision.gameObject.tag == "Food")
        {
            ExpandSnake();
        }
        else
        // wenn ein anderer Trigger ausgeführt wird (Segmente oder Wände) wird das Spiel neu gestartet
        {
            FindObjectOfType<GameManager>().RestartGame();
        }
    }
}
