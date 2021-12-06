using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

public class CollisionSystem : SystemBase
{
    private struct CollisionSystemJob : ICollisionEventsJob
    {
        public void Execute(CollisionEvent collisionEvent)
        {

        }
    }

    private struct TriggerSystemJob : ITriggerEventsJob
    {
        public void Execute(TriggerEvent triggerEvent)
        {
            
        }
    }

    protected override void OnUpdate()
    {

    }
}
