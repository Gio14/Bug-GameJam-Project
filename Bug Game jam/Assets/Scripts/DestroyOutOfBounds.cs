using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 16;
    private float lowerBound = -10;
    [SerializeField]private int pointValue;
    public GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            gameManager.AntStomp();
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            
        }

    }



    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
        

        if (transform.position.x > topBound)
        {
            // Instead of destroying the projectile when it leaves the screen
            //Destroy(gameObject);

            // Just deactivate it
            Destroy(gameObject);

        }
        else if (transform.position.x < lowerBound)
        {
            
            
            Destroy(gameObject);
        }

    }
}
