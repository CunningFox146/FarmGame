using UnityEngine;

namespace Farm.Operations
{
    public abstract class Operation
    {
        public int Priority { get; protected set; }
        public GameObject Doer { get; set; }
        public GameObject Target { get; set; }

        public abstract bool IsValid();
        public abstract void Perform();
    }
}
