using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameDisplayer : MonoBehaviour
{
    [SerializeField] float offset = 1;

    Text playerNameText;
    Transform target;

    private void Awake()
    {
        playerNameText = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = target.transform.position + offset * Vector3.down;
        }
    }

    public void SetFollowing(Transform target)
    {
        this.target = target;
        playerNameText.text = target.name;
    }
}
