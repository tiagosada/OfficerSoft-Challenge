using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Users;

namespace Domain.People
{
    public class PeopleService : Service<Person>, IPeopleService
    {
        private readonly IPeopleRepository _repository;
        private readonly IUsersService _usersService;
        
        public PeopleService(IPeopleRepository repository, IUsersService usersService) : base(repository)
        {
            _repository = repository;
            _usersService = usersService;
        }

        public CreatedEntityDTO Create(
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
            if (_repository.Get(x => x.CPF == cpf ) != null)
            {
                return new CreatedEntityDTO(new List<string>{"Person already exists"});
            }
            

            var person = new Person(
                name,
                cpf,
                cep,
                address,
                number,
                district,
                complement,
                uf,
                rg
            );
            
            var personVal = person.Validate();
            if (!personVal.isValid)
            {
                return new CreatedEntityDTO(personVal.errors);
            }

            _repository.Add(person);
            return new CreatedEntityDTO(person.Id);
        }

        public override bool Remove(Guid id)
        {
            var person = _repository.Get(x => x.Id == id);
            if (person == null) { return false; }

            if (_repository.Get(x => x.Id == person.Id) != null)
            {
                _repository.Remove(person);
            }
            
            return true;
        }

        public void Modify(Person person)
        {
            _repository.Modify(person);
        }
        public List<string> Edit(Guid id,
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
            var person = Get(id);
            if (person == null)
            {
                return new List<string>{"Person not found"};
            }

            if (name == null)
            {
                name = person.Name;
            }
            if (cpf == null)
            {
                cpf = person.CPF;
            }
            if (cep == null)
            {
                cep = person.CEP;
            }
            if (address == null)
            {
                address = person.Address;
            }
            if (number == null)
            {
                number = person.Number;
            }
            if (district == null)
            {
                district = person.District;
            }
            if (district == null)
            {
                district = person.District;
            }
            if (complement == null)
            {
                complement = person.Complement;
            }
            if (uf == null)
            {
                uf = person.UF;
            }
            if (rg == null)
            {
                rg = person.RG;
            }
            
            var updatedPerson = new Person(id,
                name,
                cpf,
                cep,
                address,
                number,
                district,
                complement,
                uf,
                rg);

            Modify(updatedPerson);

            return new List<string>{"Person updated"};
        }
    }
}