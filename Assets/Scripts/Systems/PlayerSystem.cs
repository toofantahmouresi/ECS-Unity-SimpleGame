using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class PlayerSystem : SystemBase
{
   
    protected override void OnUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        // "in" KeyWord Reads the data
        // "ref" KeyWord can overwrite the data
        Entities.WithAll<Player>().ForEach((ref Movable mov) =>  // WithAll<>() will filter the enteties that have specific components
        {
            mov.direction = new float3(x, 0, y);

        }).Schedule();
    }
}
