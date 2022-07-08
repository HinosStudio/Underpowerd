using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScanWeapon : Weapon {
    [SerializeField] private float range = 3.0f;
    [SerializeField] private TrailRenderer bulletTrail;

    private readonly List<HitDetector> _targets = new List<HitDetector>();

    protected override HitDetector[] QueryTargets() {
        _targets.Clear();
        var point = transform.forward * range;

        if(Physics.Raycast(Position + Vector3.up * 0.5f, Forward, out RaycastHit hit, range, Physics.AllLayers, QueryTriggerInteraction.Ignore)) {
            Debug.Log(hit.transform.name);
            point = hit.point;

            _targets.Add(hit.transform.GetComponent<HitDetector>());
        }

        return _targets.ToArray(); 

        // TrailRenderer trail = Instantiate(bulletTrail, m_Transform.position + Vector3.up * 0.5f, Quaternion.identity);
        // StartCoroutine(SpawnTrail(trail, point));
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 point) {
        var time = 0.0f;
        var startPosition = trail.transform.position;
        var prop = Vector3.Distance(startPosition, point) / range;

        while (time < 1) {
            trail.transform.position = Vector3.Lerp(startPosition, point, time);
            time += Time.deltaTime / (trail.time * prop);
            yield return null;
        }

        trail.transform.position = point;
        Destroy(trail.gameObject, trail.time);
    }
}
