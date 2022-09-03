using System;
using UnityEngine;
using UnityEngine.Jobs;
using Zenject;

namespace Farm.Billboard
{
    public class BillboardSystem : ITickable, IDisposable
    {
        private Transform _camera;
        private TransformAccessArray _billboards;

        public BillboardSystem()
        {
            _billboards = new(1000);

            _camera = Camera.main.transform;
        }

        public void Tick()
        {
            var job = new BillboardJob()
            {
                ClampRotation = new(50f, 0f, 0f),
                CameraPos = _camera.position
            };

            var jobHandle = job.Schedule(_billboards);
            jobHandle.Complete();
        }

        public void Dispose()
        {
            _billboards.Dispose();
        }

        public void RegisterBillboard(Transform billboard)
        {
            _billboards.Add(billboard);
        }

        public void UnregisterBillboard(Transform transform)
        {
            if (!_billboards.isCreated) return;

            for (int i = 0; i < _billboards.length; i++)
            {
                var item = _billboards[i];
                if (item == transform)
                {
                    _billboards.RemoveAtSwapBack(i);
                    break;
                }
            }
        }
    }
}
