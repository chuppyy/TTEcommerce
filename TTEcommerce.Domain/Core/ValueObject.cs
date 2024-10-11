using System;
using System.Collections.Generic;
using System.Linq;

namespace TTEcommerce.Domain.Core
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var valueObject = (ValueObject)obj;

            using (var thisValues = GetEqualityComponents().GetEnumerator())
            using (var otherValues = valueObject.GetEqualityComponents().GetEnumerator())
            {
                while (thisValues.MoveNext() && otherValues.MoveNext())
                {
                    if (ReferenceEquals(thisValues.Current, null) ^
                        ReferenceEquals(otherValues.Current, null))
                    {
                        return false;
                    }

                    if (thisValues.Current != null &&
                        !thisValues.Current.Equals(otherValues.Current))
                    {
                        return false;
                    }
                }

                return !thisValues.MoveNext() && !otherValues.MoveNext();
            }
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    return HashCode.Combine(current, obj);
                });
        }
    }
}
