using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RailLine : MonoBehaviour
{
    [SerializeField] float cartSpeed = 2f;
    [SerializeField] float spawnInterval = 0.5f;
    [SerializeField] bool leftToRight = true;
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;
    [SerializeField] Target cartPrefab;

    List<Target> carts = new List<Target>();

    public Vector3 GetLeftPoint() => leftPoint.position;
    public Vector3 GetRightPoint() => rightPoint.position;

    void Update()
    {
        for (int i = carts.Count - 1; i >= 0; i--)
        {
            Target cart = carts[i];
            if (leftToRight)
            {
                cart.transform.position = Vector3.MoveTowards(cart.transform.position, rightPoint.position, cartSpeed * Time.deltaTime);
                if (Vector3.Distance(cart.transform.position, rightPoint.position) < 0.01f)
                {
                    Destroy(cart.gameObject);
                    carts.RemoveAt(i);
                }
            }
            else
            {
                cart.transform.position = Vector3.MoveTowards(cart.transform.position, leftPoint.position, cartSpeed * Time.deltaTime);
                if (Vector3.Distance(cart.transform.position, leftPoint.position) < 0.01f)
                {
                    Destroy(cart.gameObject);
                    carts.RemoveAt(i);
                }
            }
        }     
    }

    public void SpawnCart()
    {
        Target newCart;

        if (leftToRight)
        {
            newCart = Instantiate(cartPrefab, leftPoint.position, Quaternion.identity);
        }
        else
        {
            newCart = Instantiate(cartPrefab, rightPoint.position, Quaternion.identity);
        }

        carts.Add(newCart);
        newCart.DeployTarget();
    }
    
    public void SpawnCarts(int count)
    {
        StartCoroutine(SpawnCartsRoutine(count, spawnInterval));
    }

    IEnumerator SpawnCartsRoutine(int count, float interval)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnCart();
            yield return new WaitForSeconds(interval);
        }
    }

}