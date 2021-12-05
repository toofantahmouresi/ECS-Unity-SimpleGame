using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Mathematics;
using Unity.Transforms;

public class MovableSystem : SystemBase
{
    protected override void OnUpdate()
    {
        
        Entities.ForEach((ref PhysicsVelocity physicVel, in Movable mov) =>
        {
            var step = mov.direction * mov.speed;
            physicVel.Linear = step;
        }).Schedule();
    }
}
