using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.People
{
    public interface IPeopleService : IService<Person>
    {
        CreatedEntityDTO Create(
            string name,
            string cpf,
            string cep,
            string address,
            string number,
            string district,
            string complement,
            string uf,
            string rg
            );
        void Modify(Person person);
        List<string> Edit(Guid id,
            string name,
            string cpf,
            string cep,
            string address,
            string number,
            string district,
            string complement,
            string uf,
            string rg
            );
    }
}