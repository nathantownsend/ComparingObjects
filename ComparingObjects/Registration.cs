// CREATED BY: Mike Williams
// CREATED DATE: 9/22/2014
// DO NOT MODIFY THIS CODE
// CHANGES WILL BE LOST WHEN THE GENERATOR IS RUN AGAIN
// GENERATION TOOL: Dalapi Code Generator (DalapiPro.com)


using System;
using System.Collections.Generic;
using System.Reflection;

namespace ComparingObjects
{
    /// <summary>
    /// Encapsulates a row of data in the Registration table
    /// </summary>
    public partial class RegistrationDO
    {

        public virtual Int32 RegistrationID {get; set;}
        public virtual Boolean? IsOpen {get; set;}
        public virtual Int32? IndexNumber {get; set;}
        public virtual Decimal? Value {get; set;}
        public virtual String Address1 {get; set;}
        public virtual String City {get; set;}
        public virtual String StateID {get; set;}
        public virtual String Zipcode {get; set;}
        public virtual String Phone {get; set;}


        public bool AreIdentical(RegistrationDO other)
        {
            return FindDifferences(other).Count == 0;
        }

        public List<KeyValuePair<string, string>> FindDifferences(RegistrationDO other)
        {
            List<KeyValuePair<string, string>> differences = new List<KeyValuePair<string, string>>();

            if (this.RegistrationID != other.RegistrationID)
                differences.Add(new KeyValuePair<string,string>("RegistrationId", DisplayDifferences(this.RegistrationID, other.RegistrationID)));
            if(this.IndexNumber != other.IndexNumber)
                differences.Add(new KeyValuePair<string,string>("IndexNumber", DisplayDifferences(this.IndexNumber, other.IndexNumber)));
            if(this.IsOpen != other.IsOpen)
                differences.Add(new KeyValuePair<string,string>("IsOpen", DisplayDifferences(this.IsOpen, other.IsOpen)));
            if(this.Value != other.Value)
                differences.Add(new KeyValuePair<string,string>("Value", DisplayDifferences(this.Value, other.Value)));
            if(this.Address1 != other.Address1)
                differences.Add(new KeyValuePair<string,string>("Address1", DisplayDifferences(this.Address1, other.Address1)));
            if(this.StateID != other.StateID)
                differences.Add(new KeyValuePair<string,string>("StateID", DisplayDifferences(this.StateID, other.StateID)));
            if(this.Zipcode != other.Zipcode)
                differences.Add(new KeyValuePair<string,string>("Zipcode", DisplayDifferences(this.Zipcode, other.Zipcode)));
            if (this.Phone != other.Phone)
                differences.Add(new KeyValuePair<string, string>("Phone", DisplayDifferences(this.Phone, other.Phone)));

            return differences;
        }

        private string DisplayDifferences(object A, object B)
        {
            return string.Format("Changed from '{0}' to '{1}'", A, B);
        }
    }
}