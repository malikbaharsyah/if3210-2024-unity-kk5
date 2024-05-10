using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireShotgun : MonoBehaviour
{
    EnemyShotgun shotgun;

    void Awake()
    {
        shotgun = GetComponentInChildren<EnemyShotgun>();
    }

    public void fireShotgun()
    {
        shotgun.Shoot();
    }
    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
