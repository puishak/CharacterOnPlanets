using UnityEngine;

public class TPCamera : MonoBehaviour
{
    private GameObject m_target;

    [Header("Look from Behind")]
    [Range(0, 10)]
    [SerializeField] private float m_distance = 5;
    [Range(0, 10)]
    [SerializeField] private float m_height = 2;
    [Range(0, 45)]
    [SerializeField] private float m_angleOffset = 25;
    [Range(0, 180)]
    [SerializeField] private float m_maxAngleUp = 70;
    [Range(0, 180)]
    [SerializeField] private float m_maxAngleDown = 90;

    [Header("Smoothness")]
    [Range(0, 10)]
    [SerializeField] private float m_movementSmoothness = 2;
    [Range(0, 10)]
    [SerializeField] private float m_turnSmoothness = 2;

    public float Angle { get; set; }
    public Vector3 forward()
    {
        return transform.forward;
    }

    private float m_aMaxDistance;
    private float m_bMaxDistance;


    private void Start()
    {
        m_target = GameObject.FindGameObjectWithTag("Player");
        m_aMaxDistance = m_distance;
    }
    public void changeTarget(GameObject gb)
    {
        m_target = gb;
    }

    private void FixedUpdate()
    {
        Vector3 _desiredPosition;
        Quaternion _desiredRotation;
        Vector3 _position;
        int i = 0;

        do
        {
            i++;
            Vector3 upTarget = m_target.transform.up.normalized;

            Vector3 offset = m_target.transform.forward.normalized * m_distance;
            offset -= upTarget * m_height;
            offset = CalculateAngle(offset, upTarget);

            _desiredPosition = m_target.transform.position - offset;

            Vector3 orientation = (m_target.transform.position - transform.position).normalized;
            _desiredRotation = Quaternion.LookRotation(orientation, upTarget);
            _desiredRotation *= Quaternion.Euler(-m_angleOffset, 0, 0);

            _position = Vector3.Lerp(transform.position, _desiredPosition, Time.deltaTime * (10 - m_movementSmoothness));
        } while (!checkPosition(_position) && i < 20);
        transform.position = _position;
        transform.rotation = Quaternion.Slerp(transform.rotation, _desiredRotation, Time.deltaTime * (10 - m_turnSmoothness));
    }

    private Vector3 CalculateAngle(Vector3 _offset, Vector3 _upTarget)
    {
        Angle = Mathf.Clamp(Angle, -m_maxAngleDown, m_maxAngleUp);
        Vector3 axis = Vector3.Cross(_upTarget, _offset);
        _offset = Quaternion.AngleAxis(Angle, axis) * _offset;
        return _offset;
    }

    private bool checkPosition(Vector3 _position)
    {
        Vector3 direction = _position - transform.position;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction.normalized, out hit, direction.magnitude))
        {
            if (Vector3.Dot(direction.normalized, (m_target.transform.position - transform.position).normalized) > 0.99)
            {
                return true;
            }
            if (m_distance > 0) m_distance -= Time.deltaTime;
            return false;
        }
        else
        {
            if (m_distance < m_aMaxDistance) m_distance += Time.deltaTime;
            return true;
        }
    }

    public Vector3 viewportCenter(float maxDistance)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance) && hit.transform != m_target.transform)
        {

            return hit.point;
        }
        else
        {
            return transform.position + transform.forward * maxDistance;
        }
    }
}
