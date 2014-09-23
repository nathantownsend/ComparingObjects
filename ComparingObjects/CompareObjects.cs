using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ComparingObjects
{
    public class CompareObjects
    {

        public static List<KeyValuePair<string, string>> FindDifferences(object A, object B)
        {
            

            // ensure the two objects are of the same type
            Type t = A.GetType();
            if (B.GetType() != t)
                throw new InvalidOperationException("The paramaters A and B must be of the same type");

            List<KeyValuePair<string, string>> differences = new List<KeyValuePair<string, string>>();

            // loop through each property for the type
            foreach (PropertyInfo property in t.GetProperties())
            {
                // the the property value for both objects
                object ValueA = property.GetValue(A, null);
                object ValueB = property.GetValue(B, null);

                // must handle null values differently than real values
                if (ValueA == null || ValueB == null)
                {
                    // A.Equals(B) errors with null values, but A == B works
                    if (ValueA != ValueB)
                    {
                        KeyValuePair<string, string> difference = new KeyValuePair<string, string>(property.Name, DisplayDifferences(ValueA, ValueB));
                        differences.Add(difference);
                    }
                }
                else
                {
                    // ValueA == ValueB doesn't work when the objects contain a value. Eg. if A = 1 and B = 1 then A==B is false, but A.Equals(B) is true. 
                    if (!ValueA.Equals(ValueB))
                    {
                        KeyValuePair<string, string> difference = new KeyValuePair<string, string>(property.Name, DisplayDifferences(ValueA, ValueB));
                        differences.Add(difference);
                    }
                }

            }

            return differences;

        }

        private static string DisplayDifferences(object A, object B)
        {
            return string.Format("changed from '{0}' to '{1}'", A, B);
        }

    }
}
