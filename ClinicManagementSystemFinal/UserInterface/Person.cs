using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystemFinal.UserInterface
{
    public class Person
    {
        public string LoginId { get; init; }
        public string Name { get; init; }
        public string Role { get; init; }
        public string Specialization { get; init; }
        public string ImagePath { get; init; }
        public byte[] ImageBlob { get; init; }
        public string ClinicId { get; init; }
    }
}
