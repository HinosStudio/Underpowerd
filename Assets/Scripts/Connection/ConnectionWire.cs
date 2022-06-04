using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ConnectionWire : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField, Min(1)] private int sections;
    [SerializeField, Min(0.01f)] private float damping;
    [SerializeField, Min(1)] private int iterations;
    [SerializeField] private float pointSpacing = 0.2f;

    private Vector3[] m_Points;
    private Vector3[] m_PointsLastUpdate;

    private Transform m_Transform;
    private LineRenderer m_LineRenderer;

    private void Awake() {
    }

    private void FixedUpdate() {
        int first = 0, last = m_Points.Length - 1;

        // Update anchor points
        m_Points[first] = m_Transform.position;
        m_Points[last] = target.position;

        // Update sections
        for (int i = first + 1; i < last; ++i) {
            var pos = m_Points[i];
            var swing = (m_Points[i] - m_PointsLastUpdate[i]) * damping;
            var gravity = Physics.gravity * Time.deltaTime * Time.deltaTime;
            m_Points[i] += swing + gravity ;
            m_PointsLastUpdate[i] = pos;
        }

        for(int i = 0; i < iterations; ++i) {
            ConstrainConnections();
            ConstrainCollisions();
        }
    }

    private void LateUpdate() {
        m_LineRenderer.SetPositions(m_Points);
    }

    private void ConstrainConnections() {
        for (int i = 0; i < m_Points.Length - 1; ++i) {
            var center = (m_Points[i] + m_Points[i + 1]) * 0.5f;
            var offset = m_Points[i] - m_Points[i + 1];
            var length = offset.magnitude;
            var dir = offset / length;

            if(i != 0) {
                m_Points[i] = center + dir * pointSpacing / 2;
            }
            if(i + 1 != m_Points.Length - 1) {
                m_Points[i + 1] = center - dir * pointSpacing / 2;
            }
        }
    }

    private void ConstrainCollisions() {
        int first = 0, last = m_Points.Length - 1;
        for (int i = first + 1; i < last; ++i) {
            if (m_Points[i].y < 0.1f) m_Points[i].y = 0.1f;
        }
    }

    public void Initialize(Transform target) {
        m_Transform = GetComponent<Transform>();
        m_LineRenderer = GetComponent<LineRenderer>();

        m_Points = new Vector3[sections];
        m_PointsLastUpdate = new Vector3[sections];

        m_LineRenderer.positionCount = sections;

        this.target = target;

        // Set initial position points
        var diff = (target.position - m_Transform.position) / (sections - 1);
        for (int i = 0; i < m_Points.Length; ++i) {
            m_Points[i] = m_PointsLastUpdate[i] = m_Transform.position + diff * i;
        }

        this.gameObject.SetActive(true);
    }
}
