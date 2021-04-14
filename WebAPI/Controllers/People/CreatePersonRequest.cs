using System;

namespace WebAPI.Controllers.People
{
    public class CreatePersonRequest
    {
        public string CPF { get;  set; }
        public string Name { get;  set; }
        public string CEP { get;  set; }
        public string Address { get;  set; }
        public int Number { get;  set; }
        public string District { get;  set; }
        public string Complement { get;  set; }
        public string UF { get;  set; }
        public string RG { get;  set; }
    }
}