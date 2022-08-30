using UnityEngine;
using UnityEngine.Jobs;

namespace Farm.Billboard
{
    public struct BillboardJob : IJobParallelForTransform
    {
        public Vector3 CameraPos { get; set; }

        public void Execute(int index, TransformAccess transform)
        {
            var lookPos = CameraPos - transform.position;
            lookPos.x = 0;
            transform.rotation = Quaternion.LookRotation(lookPos) * Quaternion.AngleAxis(-180f, Vector3.up);
        }
    }
}
