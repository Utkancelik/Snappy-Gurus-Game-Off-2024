using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    public GameObject lightOn;
    public GameObject correctEffect; // Doğru birleşme efekti için eklenen GameObject
    public GameObject wrongEffect;   // Yanlış birleşme efekti için eklenen GameObject
    Vector3 startPoint;
    Vector3 startPosition;
    private bool isDragging = true;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        if (!isDragging)
        {
            // If not allowed to drag, reset position and return
            UpdateWire(startPosition);
            return;
        }

        // mouse position to world point
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        // check for nearby connection points
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .2f);
        foreach (Collider2D collider in colliders)
        {
            // make sure the other object is a wire and not my own collider
            Wire otherWire = collider.GetComponent<Wire>();
            if (otherWire != null && collider.gameObject != gameObject)
            {
                // check if the wires are same color
                if (transform.parent.name.Equals(collider.transform.parent.name))
                {
                    // update wire to the connection point position
                    UpdateWire(collider.transform.position);

                    // count connection
                    Main.Instance.SwitchChange(1);

                    // play correct effect
                    if (correctEffect != null)
                    {
                        Instantiate(correctEffect, collider.transform.position, Quaternion.identity);
                        AudioManager.Instance.PlaySFXClip("CorrectConnectCable");
                    }

                    // finish step
                    otherWire.Done();
                    Done();
                }
                else
                {
                    // play wrong effect
                    if (wrongEffect != null)
                    {
                        Instantiate(wrongEffect, newPosition, Quaternion.identity);
                        AudioManager.Instance.PlaySFXClip($"FailConnectCable{Random.Range(1,3)}");
                    }

                    // prevent further dragging
                    isDragging = false;

                    // reset wire position
                    UpdateWire(startPosition);
                }
                return;
            }
        }

        // update wire
        UpdateWire(newPosition);
    }

    void Done()
    {
        // turn on light
        lightOn.SetActive(true);

        // destroy the script
        Destroy(this);
    }

    private void OnMouseUp()
    {
        // reset dragging flag
        isDragging = true;

        // reset wire position
        UpdateWire(startPosition);
    }

    void UpdateWire(Vector3 newPosition)
    {
        // update position
        transform.position = newPosition;

        // update direction
        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        // update scale
        float dist = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }
}
