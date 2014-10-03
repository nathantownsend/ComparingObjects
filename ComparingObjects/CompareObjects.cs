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
                {
                    //GetListDifferences(ref differences, property, ValueA, ValueB, DescriptionA, DescriptionB);
                    //if(!ListsMatch(ValueA, ValueB)){
                    if (!CompareLists(ValueA, ValueB))
                    {
                        KeyValuePair<string, string> notTheSame = new KeyValuePair<string, string>(property.Name, "changed");
                        differences.Add(notTheSame);
                    }
                }
                else
                {
                    GetBasicDifference(ref differences, property, ValueA, ValueB);
                }
            }

            return differences;
        }



        private static bool CompareLists(object ValueA, object ValueB)
        {
            int ACount = (ValueA as IEnumerable<object>).Count();
            int BCount = (ValueB as IEnumerable<object>).Count();

            // if the lists contain different numbers of items they can't match
            if (ACount != BCount)
                return false;

            // are all items from list also in list b
            bool match = ListsMatch(ValueA, ValueB);
            if (!match)
                return false;

            // there is a rare possibility that it could pass the previous test and still be different: for example ValueA has A,B,B,C; ValueB has A,B,C,D - since A,B,C are in ValueB it passes

            // are all items from list b also in list a
            return ListsMatch(ValueB, ValueA);
            
        }


        private static bool ListsMatch(object ValueA, object ValueB)
        {
            // COMPARE A TO B
            foreach (object a in ValueA as IEnumerable<object>)
            {
                bool hasMatch = false;

                // compare each item in List A to each item in List B
                foreach (object b in ValueB as IEnumerable<object>)
                {
                    if (!hasMatch)
                    {
                        List<KeyValuePair<string, string>> compare = FindDifferences(a, b, "", "");

                        // if there are no differences, then a match has been found an no need to look further
                        if (compare.Count == 0)
                            hasMatch = true;
                    }
                }
                
                // if a match wasn't found then there is a difference
                if (!hasMatch)
                    return false;
            }


            // if nothing differs return true
            return true;
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
                    differences.Add(new KeyValuePair<string, string>(PropertyName, string.Format("Item {0} in {1} did not have a match in {2}", index, DescriptionA, DescriptionB)));

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
            return "changed";
            //return string.Format("changed from '{0}' to '{1}'", A, B);
        }

    }
}
