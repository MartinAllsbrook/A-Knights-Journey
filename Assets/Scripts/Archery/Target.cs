using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] Collider2D colliderObject;
    [SerializeField] float deployTime = 0.2f;
    [SerializeField] float retractTime = 0.05f;

    [SerializeField] Vector3 downPosition = new Vector3(0f, 0f, 0f);
    [SerializeField] Vector3 upPosition = new Vector3(0f, 1f, 0f);

    bool isDeployed = false;
    public bool IsDeployed => isDeployed;

    public void DeployTarget()
    {
        colliderObject.enabled = true;
        ArcheryController.Instance.TallyTarget();
        isDeployed = true;
        StopAllCoroutines();
        StartCoroutine(ShowTargetRoutine());
    }

    public void RetractTarget()
    {
        colliderObject.enabled = false;
        isDeployed = false;
        StopAllCoroutines();
        StartCoroutine(RetractTargetRoutine());
    }

    IEnumerator ShowTargetRoutine()
    {
        float elapsedTime = 0f;
        

        while (elapsedTime < deployTime)
        {
            Vector3 startingPos = transform.position + downPosition;
            Vector3 endPos = transform.position + upPosition;

            targetTransform.position = Vector3.Lerp(startingPos, endPos, elapsedTime / deployTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetTransform.position = transform.position + upPosition;
    }

    IEnumerator RetractTargetRoutine()
    {
        float elapsedTime = 0f;
        

        while (elapsedTime < retractTime)
        {
            Vector3 startingPos = transform.position + upPosition;
            Vector3 endPos = transform.position + downPosition;

            targetTransform.position = Vector3.Lerp(startingPos, endPos, elapsedTime / retractTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetTransform.position = transform.position + downPosition;
    }
}
