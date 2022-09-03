using Farm.Util;
using UnityEngine;
using UnityEngine.Jobs;

namespace Farm.Billboard
{
    public struct BillboardJob : IJobParallelForTransform
    {
        public Vector3 ClampRotation { get; set; }
        public Vector3 CameraPos { get; set; }

        public void Execute(int index, TransformAccess transform)
        {
            var lookPos = CameraPos - transform.position;
            var rotation = Quaternion.LookRotation(lookPos) * Quaternion.AngleAxis(-180f, Vector3.up);
            transform.rotation = QuaternionUtil.ClampRotation(rotation, ClampRotation);
        }
    }
}
