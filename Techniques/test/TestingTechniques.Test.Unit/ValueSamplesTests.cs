using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using TestingTechniques;

namespace TestingTechniques.Test.Unit
{
    public class ValueSamplesTests
    {
        private readonly ValueSamples _sut = new();

        [Fact]
        public void StringAssertionExample()
        {
            var fullName = _sut.FullName;

            fullName.Should().Be("Okan Akın");
            fullName.Should().NotBeEmpty();
            fullName.Should().StartWith("Okan");
            fullName.Should().EndWith("Akın");
        }

        [Fact]
        public void NumberAssertionExample()
        {
            var age = _sut.Age;

            age.Should().Be(23);
            age.Should().BePositive();
            age.Should().BeGreaterThan(18);
            age.Should().BeLessThanOrEqualTo(30);
            age.Should().BeInRange(20,25);
        }

        [Fact]
        public void DateAssertionExample()
        {
            var dateOfBirth = _sut.DateOfBirth;

            dateOfBirth.Should().Be(new DateOnly(2002, 11, 17));
            dateOfBirth.Should().BeBefore(DateOnly.FromDateTime(DateTime.Now));
            dateOfBirth.Should().BeAfter(new DateOnly(1900, 1, 1));
        }

        [Fact]
        public void ObjectAssertionExample()
        {
            var expected = new User
            {
                FullName = "Okan Akın",
                Age = 23,
                DateOfBirth = new(2002, 11, 17)
            };

            var user = _sut.AppUser;

            user.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void EnumerableAssertionExample()
        {
            var expected = new User
            {
                FullName = "Okan Akın",
                Age = 23,
                DateOfBirth = new(2002, 11, 17)
            };

            var users = _sut.Users.As<User[]>();

            users.Should().ContainEquivalentOf(expected);
            users.Should().HaveCount(3);
            users.Should().Contain(x => x.FullName.StartsWith("Okan") && x.Age > 5);
        }

        [Fact]
        public void EnumerableNumberAssertionExample()
        {
            var numbers = _sut.Numbers.As<int[]>();

            numbers.Should().Contain(5);
        }

        [Fact]
        public void ExceptionThrownAssertionExample()
        {
            Action result = () => _sut.Divide(1, 0);

            result.Should().Throw<DivideByZeroException>().WithMessage("Attempted to divide by zero.");
        }

        [Fact]
        public void EventRaisedAssertionExample()
        {
            var monitorSubject = _sut.Monitor();

            _sut.RaiseExampleEvent();

            monitorSubject.Should().Raise("ExampleEvent");
        }

        [Fact]

        public void TestingInternalMembersExample()
        {
            var number = _sut.InternalSecretNumber;
            number.Should().Be(42);
        }
}
}
