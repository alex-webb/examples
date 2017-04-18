using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Management.Automation;

namespace PSExample
{
    [Cmdlet(VerbsCommon.Get, "Salutation")]
    public class GetSalutation : PSCmdlet
    {
        private string[] nameCollection;

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "Name to get salutation for."
        )]
        [Alias("Person", "FirstName")]
        public string[] Name
        {
            get { return nameCollection; }
            set { nameCollection = value; }
        }

        protected override void ProcessRecord()
        {
            //foreach (string name in nameCollection)
            //{
            //    WriteVerbose("Creating salutation for " + name);
            //    string salutation = "Hello, " + name;
            //    WriteObject(salutation);
            //}

            //List<string> cars = new List<string>()
            //{
            //    "Porsche","Ferrari","Mercedes","Fiat","Montego","TVR","Lotus","Ariel","Morgan"
            //};

            List<MyData> cars = new List<MyData>()
            {
                new MyData(1,"Alex", DateTime.Now),
                new MyData(2,"Chris", DateTime.Now.AddMilliseconds(19)),
                new MyData(3,"Phill", DateTime.Now.AddMinutes(1)),
                new MyData(4,"Matt", DateTime.Now),
                new MyData(5,"Lee", DateTime.Now.AddSeconds(100))
            };

            WriteObject(cars, true);

        }

    }

    public struct MyData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime When { get; set; }

        public MyData(int id, string name, DateTime when)
        {
            Id = id;
            Name = name;
            When = when;
        }
    }
}
