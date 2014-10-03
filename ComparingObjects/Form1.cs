using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComparingObjects
{
    public partial class Form1 : Form
    {
        RegistrationDO model = new RegistrationDO()
        {
            PermitKey = 1,
            Address1 = "123",
            City = "Helena",
            Value = 123.33m,
            IsOpen = null,
            IndexNumber = null,
            Phone = "444-444-4444",
            RegistrationID = 1,
            StateID = "MT",
            Zipcode = "59601"
        };

        RegistrationDO changed = new RegistrationDO()
        {
            PermitKey = 2,
            Address1 = "1234",
            City = "Helena",
            Value = 123.33m,
            IsOpen = false,
            IndexNumber = 42,
            Phone = "(444) 444-4444",
            RegistrationID = 1,
            StateID = "MT",
            Zipcode = "59601"
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        int timesToRun = 10000;

        /// <summary>
        /// This method of comparison uses the FindDifferences method that could be generated as part of the Data Object class in Dalapi.
        /// </summary>
        /*
        private void LowLevelComparison()
        {
            DateTime A = DateTime.Now;
            for (int i = 0; i < timesToRun; i++)
                model.FindDifferences(changed);
            DateTime B = DateTime.Now;

            MessageBox.Show("Low level speed test = " + B.Subtract(A).Milliseconds.ToString() + " Milliseconds to run " + timesToRun.ToString() + "times");

            List<KeyValuePair<string, string>> changes = model.FindDifferences(changed);
            DisplayDifferences(changes, "Low Level Comparison");
        }*/


        /// <summary>
        /// This method of comparison uses the CompareObjects.FindDifferences method to generically compare objects and list their differences. 
        /// </summary>
        private void HighLevelComparison()
        {

            //List<KeyValuePair<string, string>> changes = CompareObjects.FindDifferences(model, changed);
            //DisplayDifferences(changes, "High Level Comparison");
            
            ExampleBO first = new ExampleBO() { Name = "Operator", PermitKey = 1, Registration = new List<RegistrationDO>() };
            first.Registration.Add(model);
            first.Registration.Add(changed);

            ExampleBO second = new ExampleBO() { Name = "Operator", PermitKey = 2, Registration = new List<RegistrationDO>() };
            //second.Registration.Add(changed);
            second.Registration.Add(model);
            

            List<KeyValuePair<string, string>> changes = CompareObjects.FindDifferences(first, second, "Permit 1", "Permit 2");
            DisplayDifferences(changes, "Comparison involving nested objects");
            
        }


        /// <summary>
        /// writes the change set to a popup
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="ComparisonType"></param>
        private void DisplayDifferences(List<KeyValuePair<string, string>> changes, string ComparisonType)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(ComparisonType);
            sb.AppendLine();

            sb.AppendLine("Are objects identical: " + (changes.Count == 0 ? "Yes" : "No"));
            sb.AppendLine();

            foreach (KeyValuePair<string, string> change in changes)
                sb.AppendLine(string.Format("{0} {1}", change.Key, change.Value));

            MessageBox.Show(sb.ToString());
        }


        private void button1_Click(object sender, EventArgs e)
        {

            //LowLevelComparison();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HighLevelComparison();
        }
    }
}
