using System;
using System.Collections.Generic;

namespace RESTfullAPIRoComputer
{
    public partial class PersonData
    {
        public int Id { get; set; }
        public decimal Hastighed { get; set; }
        public decimal Acceleration { get; set; }
        public string Tid { get; set; }
        public string FkEmail { get; set; }

        public virtual Person FkEmailNavigation { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} ID: {Hastighed} ID: {Acceleration} ID: {Tid} ID: {FkEmail}"; //lol
        }
    }
}