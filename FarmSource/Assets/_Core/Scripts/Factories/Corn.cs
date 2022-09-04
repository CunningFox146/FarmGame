using UnityEngine;
using Zenject;

namespace Farm.GrowSystem
{
    public class Corn : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Corn> { }
    }
}
