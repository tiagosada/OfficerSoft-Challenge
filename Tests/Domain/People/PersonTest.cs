using Xunit;
using Domain.People;
using System;

namespace Tests.Domain.People
{
    public class PersonTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        [InlineData("A B C")]
        [InlineData("Jonas 1326")]
        [InlineData("J0NAS")]
        public void Should_return_false_when_person_name_invalid(string name)
        {
            var person = new Person(
                name,
                "61668507005",
                "47999992222",
                "St.Sesame",
                "007",
                "America",
                "next to Elmo's house",
                "USA Governament",
                "444444"
                );
            
            var personIsValid = person.Validate().isValid;

            Assert.False(personIsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        [InlineData("4444444444")]
        [InlineData("123")]
        [InlineData("616.685.070-05")]
        public void Should_return_false_when_person_cpf_invalid(string cpf)
        {
            var person = new Person(
                "Kermet Ramos",
                cpf,
                "47999992222",
                "St.Sesame",
                "007",
                "America",
                "next to Elmo's house",
                "USA Governament",
                "444444"
                );
            
            var personIsValid = person.Validate().isValid;

            Assert.False(personIsValid);
        }
    }
}
