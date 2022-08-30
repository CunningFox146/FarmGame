using System.Collections.Generic;
using UnityEngine;
using Zenject;

// I wasn't able to make a billboard shader that would work with
// multiple instances of the same material, so I'll use this system for now
namespace Farm.Animations
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

        public void RegisterBillboard(Transform billboard)
        {
            _billboards.Add(billboard);
        }

        public void Tick()
        {
            foreach (Transform billboard in _billboards)
            {
                billboard.LookAt(_camera);
            }
        }
    }
}
