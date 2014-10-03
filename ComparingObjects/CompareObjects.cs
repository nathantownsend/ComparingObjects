using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ComparingObjects
{

    public class ComparisonResult
    {
        //public string PropertyName { get; set; }

        public string ObjectType { get; set; }

    }


    public class CompareObjects
    {

        private static string[] KeysToIgnore = new string[] { "PermitKey", "RegistrationID" };



        public static List<KeyValuePair<string, string>> FindDifferences(object A, object B, string DescriptionA, string DescriptionB)
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

                string type = property.PropertyType.ToString();

                // handle lists differently
                if (type.StartsWith("System.Collections.Generic.List"))
                    GetListDifferences(ref differences, property, ValueA, ValueB, DescriptionA, DescriptionB);
                else
                    GetBasicDifference(ref differences, property, ValueA, ValueB);
            }

            

            return differences;

        }

        /// <summary>
        /// Compares two lists of objects and finds differeences
        /// </summary>
        /// <param name="differences"></param>
        /// <param name="property"></param>
        /// <param name="ValueA"></param>
        /// <param name="ValueB"></param>
        private static void GetListDifferences(ref List<KeyValuePair<string, string>> differences, PropertyInfo property, object ValueA, object ValueB, string DescriptionA, string DescriptionB)
        {
            // match items from list a to items in list b
            CompareLists(ref differences, property.Name, ValueA, ValueB, DescriptionA, DescriptionB);

            // match items from list b to items in list a
            CompareLists(ref differences, property.Name, ValueB, ValueA, DescriptionB, DescriptionA);
        }


        /// <summary>
        /// Compares two lists and 
        /// </summary>
        /// <param name="differences"></param>
        /// <param name="PropertyName"></param>
        /// <param name="FirstList"></param>
        /// <param name="FirstListCaption"></param>
        /// <param name="SecondList"></param>
        /// <param name="SecondListCaption"></param>
        static void CompareLists(ref List<KeyValuePair<string, string>> differences, string PropertyName, object FirstList, object SecondList, string DescriptionA, string DescriptionB)
        {
            int index = 0;

            // loop through every item in list A
            foreach (object a in FirstList as IEnumerable<object>)
            {
                bool aHasMatchInB = false;

                // compare each item in List A to each item in List B
                foreach (object b in SecondList as IEnumerable<object>)
                {

                    if (!aHasMatchInB)
                    {
                        List<KeyValuePair<string, string>> compare = FindDifferences(a, b, DescriptionA, DescriptionB);

                        // if there are no differences, then a match has been found - stop comparing
                        if (compare.Count == 0)
                            aHasMatchInB = true;
                    }
                }

                // if a match was found then 
                if (!aHasMatchInB)
                    differences.Add(new KeyValuePair<string, string>(PropertyName, string.Format("Item {0} in {1} did not have an exact match in {2}", index, DescriptionA, DescriptionB)));

                index++;
            }

            
        }


        /// <summary>
        /// Compares two POCOs to find differences
        /// </summary>
        /// <param name="differences"></param>
        /// <param name="property"></param>
        /// <param name="ValueA"></param>
        /// <param name="ValueB"></param>
        private static void GetBasicDifference(ref List<KeyValuePair<string, string>> differences, PropertyInfo property, object ValueA, object ValueB)
        {
            if (KeysToIgnore.Contains(property.Name))
                return;

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




        private static string DisplayDifferences(object A, object B)
        {
            return string.Format("changed from '{0}' to '{1}'", A, B);
        }

    }
}
