using Xunit;
using Moq;
using Domain.People;
using Domain.Common;
using System;
using Domain.Users;

namespace Tests.Domain.People
{
    public class PeopleServiceTest
    {
        private Mock<IPeopleRepository> _peopleRepository;
        private Mock<IUsersService> _usersService;
        private PeopleService _peopleService;
        
        public PeopleServiceTest()
        {
            _peopleRepository = new Mock<IPeopleRepository>();
            _usersService = new Mock<IUsersService>();
            _peopleService = new PeopleService(_peopleRepository.Object, _usersService.Object);
        }

        [Fact]
        public void should_not_create_when_person_already_exists()
        {
            var cpf = "61668507005";
            var rg = "4544";
            _peopleRepository.Setup(x => x.Get(It.IsAny<Func<Person,bool>>())).Returns(new Person(
                "Elmo Ramos",
                cpf,
                "14134243324",
                "St.Sesame",
                "005",
                "America",
                "next to Kermet's house",
                "USA Governament",
                rg
                ));
            
            var resp = _peopleService.Create(
                "Kermet Frog",
                cpf,
                "47999992222",
                "St.Sesame",
                "007",
                "America",
                "next to Elmo's house",
                "USA Governament",
                rg
                );

            Assert.False(resp.IsValid);

            _peopleRepository.Verify(
                x => x.Add(It.IsAny<Person>()),
                Times.Never()
            );
        }

        [Fact]
        public void should_not_create_person_when_cpf_already_in_use()
        {
            var cpf = "61668507005";
            _peopleRepository.SetupSequence(x => x.Get(It.IsAny<Func<Person,bool>>()))
                .Returns(null as Person)
                .Returns(new Person(
                "Kermet Frog",
                cpf,
                "47999992222",
                "St.Sesame",
                "007",
                "America",
                "next to Elmo's house",
                "USA Governament",
                "0000000")
                );
            
            var resp = _peopleService.Create(
                "Elmo Ramos",
                cpf,
                "4799999222",
                "St.Sesame",
                "005",
                "America",
                "next to Kermit's house",
                "USA Governament",
                "4444"
                );

            Assert.False(resp.IsValid);

            _peopleRepository.Verify(
                x => x.Add(It.IsAny<Person>()),
                Times.Never()
            );
        }

        [Fact]
        public void should_not_create_person_when_has_validation_errors()
        {
            var resp = _peopleService.Create(
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
                );

            Assert.False(resp.IsValid);

            _peopleRepository.Verify(
                x => x.Add(It.IsAny<Person>()),
                Times.Never()
            );
        }
        
        [Fact]
        public void should_create_person_when_is_valid()
        {
            _usersService.Setup(x => x.Create("444444", "61668507005")).Returns(new CreatedEntityDTO(Guid.NewGuid()));
            _usersService.Setup(x => x.Get(It.IsAny<Guid>())).Returns(new User("444444", "61668507005"));
            
            var resp = _peopleService.Create(
                "Kermet Frog",
                "52774227062",
                "72830-040",
                "St.Sesame",
                "007",
                "America",
                "next to Elmo's house",
                "USA Governament",
                "0000000"
                );

            Assert.True(resp.IsValid);
            _peopleRepository.Verify(x => x.Add(It.Is<Person>(x => 
                x.Name == "Kermet Frog" &&
                x.CPF == "52774227062" &&
                x.CEP == "72830-040" &&
                x.Address == "St.Sesame" &&
                x.Number == "007" &&
                x.District == "America" &&
                x.Complement == "next to Elmo's house" &&
                x.UF == "USA Governament" &&
                x.RG == "0000000"
            )), Times.Once());
        }

    }
}
