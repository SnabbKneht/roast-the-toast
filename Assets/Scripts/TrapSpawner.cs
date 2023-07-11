using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    [field: SerializeField] private GameObject ToasterPrefab { get; set; }
    [field: SerializeField] private GameObject CarPrefab { get; set; }
    [field: SerializeField] private float CooldownDuration { get; set; }
    
    private bool IsOnCooldown { get; set; }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && !IsOnCooldown)
        {
            Instantiate(ToasterPrefab, transform.position, Quaternion.identity);
            StartCoroutine(Cooldown());
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && !IsOnCooldown)
        {
            Instantiate(CarPrefab, transform.position, Quaternion.identity);
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        IsOnCooldown = true;
        yield return new WaitForSeconds(CooldownDuration);
        IsOnCooldown = false;
    }
}
