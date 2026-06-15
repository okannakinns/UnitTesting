using CalculatorLibrary;
using Xunit;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace CalculatorLibraryTests
{
    public class CalculatorTests : IAsyncLifetime
    {
        private readonly Calculator _sut = new();
        private readonly ITestOutputHelper _outputHelper;

        public CalculatorTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
            _outputHelper.WriteLine("Hello from the ctor.");
        }

        [Theory]
        [InlineData(5, 5, 10)]
        [InlineData(-5, 5, 0)]
        [InlineData(-15, -5, -20)]
        public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
        {
            
            //Act
            var result = _sut.Add(a,b);

            _outputHelper.WriteLine("Hello from the add method.");
            //Assert
            //Assert.Equal(expected, result);
            result.Should().Be(expected);
        }


        [Theory]
        [InlineData(5, -5, 10)]
        [InlineData(15, -5, 20)]
        [InlineData(-5, -5, 0)]
        [InlineData(-15, -5, -10)]
        [InlineData(5, -10, 15)]
        public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
        {

            //Act
            var result = _sut.Subtract(a,b);

            _outputHelper.WriteLine("Hello from the subtract method.");
            //Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(5, 5, 25)]
        [InlineData(50, 0, 0)]
        [InlineData(-5, 5, -25)]
        public void Multiply_ShouldMultiplyTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
        {
            //Act
            var result = _sut.Multiply(a, b);

            //Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(5, 5, 1)]
        [InlineData(15, 5, 3)]
        [InlineData(0, 0, -25, Skip = "Ignore edildi!")]
        public void Divide_ShouldDivideTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
        {
            //Act
            var result = _sut.Divide(a, b);

            //Assert
            result.Should().Be(expected);
        }

        public async Task InitializeAsync()
        {
            _outputHelper.WriteLine("Hello from the initialize async");
            Task.Delay(1);
        }

        public async Task DisposeAsync()
        {
            _outputHelper.WriteLine("Hello from the dispose async");
            Task.Delay(1);
        }
    }
}
