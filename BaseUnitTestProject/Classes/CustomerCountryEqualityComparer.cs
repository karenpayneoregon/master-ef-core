using System.Collections.Generic;
using BaseUnitTestProject.Models;

namespace BaseUnitTestProject.Classes
{
    /// <summary>
    /// Compare <see cref="CustomerCountry"/> on Id and Name properties
    /// </summary>
    public class CustomerCountryEqualityComparer : IEqualityComparer<CustomerCountry>
    {
        public bool Equals(CustomerCountry x, CustomerCountry y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;

            return x.Name == y.Name && x.Id == y.Id;
        }

        public int GetHashCode(CustomerCountry customerCountry)
        {
            int hashCodeName = customerCountry.Name == null ? 
                0 : 
                customerCountry.Name.GetHashCode();

            int hasCodeAge = customerCountry.Id.GetHashCode();

            return hashCodeName ^ hasCodeAge;
        }
    }
}