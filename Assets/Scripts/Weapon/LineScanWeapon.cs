using System.Collections;
using UnityEngine;

public class LineScanWeapon : Weapon {
    [SerializeField] private TrailRenderer bulletTrail;

    protected override void Shoot() {
        var point = transform.forward * Range;

        if(Physics.Raycast(m_Transform.position + Vector3.up, m_Transform.forward, out RaycastHit hit, Range, Physics.AllLayers, QueryTriggerInteraction.Ignore)) {
            Debug.Log(hit.transform.name);
            point = hit.point;
        }

        TrailRenderer trail = Instantiate(bulletTrail, m_Transform.position + Vector3.up * 0.5f, Quaternion.identity);
        StartCoroutine(SpawnTrail(trail, point));
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 point) {
        var time = 0.0f;
        var startPosition = trail.transform.position;
        var prop = Vector3.Distance(startPosition, point) / Range;

        while (time < 1) {
            trail.transform.position = Vector3.Lerp(startPosition, point, time);
            time += Time.deltaTime / (trail.time * prop);
            yield return null;
        }

        trail.transform.position = point;
        Destroy(trail.gameObject, trail.time);
    }
}
