using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;


[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class EnemySystem : SystemBase
{
    private Unity.Mathematics.Random rng = new Unity.Mathematics.Random(1234);
    protected override void OnUpdate()
    {

        var raycastChecker = new MoveRayCast() { pw = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld};
        rng.NextInt();
        var rngTemp = rng;
        Entities.ForEach((ref Movable mov, ref Enemy enemy, in Translation trns) =>
        {
            bool hitWall = raycastChecker.CheckRay(trns.Value, mov.direction, mov.direction);
            if (math.distance(trns.Value,enemy.previousCell)> 0.9 || hitWall)
            {
                enemy.previousCell = math.round(trns.Value);
                var validDir = new NativeList<float3>(Allocator.Temp); //if we use it inside forrach we could use .temp else we need to use .tempJobAlloacation

                if (!raycastChecker.CheckRay(trns.Value, new float3(0, 0, -1), mov.direction))
                    validDir.Add(new float3(0, 0, -1));
                if (!raycastChecker.CheckRay(trns.Value, new float3(0, 0, 1), mov.direction))
                    validDir.Add(new float3(0, 0, 1));
                if (!raycastChecker.CheckRay(trns.Value, new float3(-1, 0, 0), mov.direction))
                    validDir.Add(new float3(-1, 0, 0));
                if (!raycastChecker.CheckRay(trns.Value, new float3(1, 0, 0), mov.direction))
                    validDir.Add(new float3(1, 0, 0));
                if (validDir.Length > 0)
                    mov.direction = validDir[rngTemp.NextInt(validDir.Length)];

                validDir.Dispose();
                //perform raycast here!
                //raycastChecker.CheckRay(trns.Value,new float3(0,0,-1),mov.direction);

            }
        }).Schedule();
        this.CompleteDependency();
    }

    public struct MoveRayCast
    {
        [ReadOnly]public PhysicsWorld pw;
        public bool CheckRay(float3 pos, float3 direction, float3 currentDirection)
        {
            var ray = new RaycastInput()
            {
                Start = pos,
                End = pos + (direction * 0.9f),
                Filter = new CollisionFilter()
                {
                    GroupIndex = 0,
                    BelongsTo = 1u << 2,
                    CollidesWith = 1u << 1
                }
            };
            return pw.CastRay(ray);
        }
    }
}
