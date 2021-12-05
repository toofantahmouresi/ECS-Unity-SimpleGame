using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class MovableSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;
        Entities.ForEach((ref Movable mov, ref Translation translation, ref Rotation rot) =>
        {
            translation.Value += mov.speed * mov.direction* dt;
        }).Schedule();
    }
}
