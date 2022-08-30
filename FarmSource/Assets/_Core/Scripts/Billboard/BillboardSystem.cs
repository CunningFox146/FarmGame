using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Farm.Billboard
{
    public class BillboardSystem : ITickable
    {
        private List<Transform> _billboards;
        private Transform _camera;

        public BillboardSystem()
        {
            _billboards = new();

            _camera = Camera.main.transform;
        }

        public void RegisterBillboard(Transform billboard) => _billboards.Add(billboard);

        public void UnregisterBillboard(Transform transform) => _billboards.Remove(transform);

        public void Tick()
        {
            foreach (Transform billboard in _billboards)
            {
                var targetPostition = new Vector3(billboard.position.x, _camera.position.y, _camera.position.z);
                billboard.LookAt(targetPostition);
                billboard.Rotate(Vector3.up * -180f);
            }
        }
    }
}
