using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.MailServices;
using Domain.MailServices.Templates;
using Domain.Users;

namespace Domain.People
{
    public class PeopleService : Service<Person>, IPeopleService
    {
        private readonly IPeopleRepository _repository;
        private readonly IUsersService _usersService;
        
        public StudentsService(IPeopleRepository repository, IUsersService usersService) : base(repository)
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
    }
}