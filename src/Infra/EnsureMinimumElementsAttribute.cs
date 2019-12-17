using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace IntelligentCheckout.Backend.Infra
{
    public class EnsureMinimumElementsAttribute : ValidationAttribute
    {
        private readonly int _minElements;
        public EnsureMinimumElementsAttribute(int minElements)
            => this._minElements = minElements;

        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
                return list.Count >= this._minElements;
            
            return false;
        }
    }
}
