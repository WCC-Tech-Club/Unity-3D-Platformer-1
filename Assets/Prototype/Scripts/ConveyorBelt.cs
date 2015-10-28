using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public ConveyorSegment prefab;
    public int size;
    public float offset;
    public float speed;

    private new Transform transform;

    private ConveyorSegment[] segments;
    private float end;

    void Awake()
    {
        transform = base.transform;
        
        segments = new ConveyorSegment[size];
        end = offset * size;

        for (int i = 0; i < size; i++)
        {
            Vector3 newSegmentPosition = transform.position + (transform.forward * offset * i);
            ConveyorSegment newSegment = Instantiate(prefab, newSegmentPosition, transform.rotation) as ConveyorSegment;
            newSegment.transform.parent = transform;
            segments[i] = newSegment;
        }
    }

    void FixedUpdate()
    {
        foreach (ConveyorSegment segment in segments)
        {
            Vector3 position = segment.Rigidbody.position;
            position += transform.forward * speed * Time.deltaTime;

            float distance = Vector3.Distance(position, transform.position);

            if (distance < end)
            {
                segment.Rigidbody.MovePosition(position);
            }
            else
            {
                position = transform.position;
                segment.Rigidbody.position = position;
                position += transform.forward * (distance - end);
                segment.Rigidbody.MovePosition(position);
            }
        }
    }
}
