using System.Collections.Generic;
using System.Linq;
using System;
using Domain.Common;
using System.Text.RegularExpressions;

namespace Domain.People
{
    public class Person : Entity
    {
        public string CPF { get; protected set; }
        public string Name { get; protected set; }
        public string CEP { get; protected set; }
        public string Address { get; protected set; }
        public string Number { get; protected set; }
        public string District { get; protected set; }
        public string Complement { get; protected set; }
        public string UF { get; protected set; }
        public string RG { get; protected set; }

        protected Person() : base() { }
        public Person(
            string name,
            string cpf,
            string cep,
            string address,
            string number,
            string district,
            string complement,
            string uf,
            string rg
            )
        {
            Name = name;
            CPF = cpf;
            CEP = cep;
            Address = address;
            Number = number;
            District = district;
            Complement = complement;
            UF = uf;
            RG = rg;
        }
        public Person(Guid id,
            string name,
            string cpf,
            string cep,
            string address,
            string number,
            string district,
            string complement,
            string uf,
            string rg
            )
        {
            Id = id;
            Name = name;
            CPF = cpf;
            CEP = cep;
            Address = address;
            Number = number;
            District = district;
            Complement = complement;
            UF = uf;
            RG = rg;
        }
        protected bool ValidateName()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }

            var words = Name.Split(' ');
            if (words.Length < 1)
            {
                return false;
            }

            foreach (var word in words)
            {
                if (word.Trim().Length < 2)
                {
                    return false;
                }
                if (word.Any(x => !char.IsLetter(x)))
                {
                    return false;
                }
            }
            return true;
        }

        protected bool ValidateCPF()
        {
            var cpf = CPF.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty); 
            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }

            if (cpf.Length != 11)
            {
                return false;
            }

            if (!cpf.All(char.IsNumber))
            {
                return false;
            }

            var first = cpf[0];
            if (cpf.Substring(1, 10).All(x => x == first))
            {
                return false;
            }

            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string temp;
            string digit;
            int sum;
            int rest;

            temp = cpf.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(temp[i].ToString()) * multiplier1[i];
            }

            rest = sum % 11;

            rest = rest < 2 ? 0 : 11 - rest;

            digit = rest.ToString();
            temp += digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(temp[i].ToString()) * multiplier2[i];
            }

            rest = sum % 11;

            rest = rest < 2 ? 0 : 11 - rest;

            digit += rest.ToString();

            if (cpf.EndsWith(digit))
            {
                return true;
            }

            return false;
        }

        protected bool ValidateCEP()
        {
            return Regex.IsMatch(
                CEP,
                @"^\d{5}-\d{3}$"
            );
        }
        public (List<string> errors, bool isValid) Validate()
        {
            var errs = new List<string>();

            if (!ValidateName())
            {
                errs.Add("Invalid name");
            }
            if (!ValidateCPF())
            {
                errs.Add("Invalid CPF");
            }
            if (!ValidateCEP())
            {
                errs.Add("Invalid CEP");
            }
            return (errs, errs.Count == 0);
        }
        
    }
}
