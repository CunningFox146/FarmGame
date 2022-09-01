using System.Collections.Generic;

namespace Farm.Util
{
    public class SourceModifierList
    {
        private Dictionary<string, float> _modifiers = new();
        public float Modifier { get; private set; }

        public SourceModifierList()
        {
            Modifier = 1f;
        }

        public SourceModifierList(float startValue)
        {
            Modifier = startValue;
        }

        public void AddModifier(string name, float value)
        {
            _modifiers[name] = value;
            RecalculateModifier();
        }

        public void RemoveModifier(string name)
        {
            _modifiers.Remove(name);
            RecalculateModifier();
        }

        private void RecalculateModifier()
        {
            foreach (float modifier in _modifiers.Values)
            {
                Modifier *= modifier;
            }
        }
    }
}
