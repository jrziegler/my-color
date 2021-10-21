using FluentAssertions;
using MyColor.Domain.Entities;
using System;
using Xunit;

namespace MyColor.Domain.Tests
{
    public class PersonUnitTest1
    {
        [Fact(DisplayName = "Create Person with valid state")]
        public void CreatePerson_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Person(1, "Tony", "Stark", "10210", "California", "Red");
            action.Should()
                .NotThrow<MyColor.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Person with null name value")]
        public void CreatePerson_WithNullNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Person(1, null, "Stark", "10210", "California", "Red");
            action.Should()
                .Throw<MyColor.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. Name is required.");
        }

        [Fact(DisplayName = "Create Person missing name value")]
        public void CreatePerson_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Person(1, "", "Stark", "10210", "California", "Red");
            action.Should()
                .Throw<MyColor.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. Name is required.");
        }

        [Fact(DisplayName = "Create Person with null lastname value")]
        public void CreatePerson_WithNullLastnameValue_DomainExceptionRequiredLastname()
        {
            Action action = () => new Person(1, "Tony", null, "10210", "California", "Red");
            action.Should()
                .Throw<MyColor.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid lastname. Lastname is required.");
        }

        [Fact(DisplayName = "Create Person missing lastname value")]
        public void CreatePerson_MissingLastnameValue_DomainExceptionRequiredLastname()
        {
            Action action = () => new Person(1, "Tony", "", "10210", "California", "Red");
            action.Should()
                .Throw<MyColor.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid lastname. Lastname is required.");
        }

        [Fact(DisplayName = "Create Person with null zipcode value")]
        public void CreatePerson_WithNullZipcodeValue_ResultObjectValidState()
        {
            Action action = () => new Person(1, "Tony", "Stark", null, "California", "Red");
            action.Should()
                .NotThrow<MyColor.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Person missing zipcode value")]
        public void CreatePerson_MissingZipcodeValue_ResultObjectValidState()
        {
            Action action = () => new Person(1, "Tony", "Stark", "", "California", "Red");
            action.Should()
                .Throw<MyColor.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Person with null zipcode value")]
        public void CreatePerson_WithNullZipcodeValue_NoNullReferenceException()
        {
            Action action = () => new Person(1, "Tony", "Stark", null, "California", "Red");
            action.Should()
                .NotThrow<NullReferenceException>();
        }

        [Fact(DisplayName = "Create Person with zipcode shorter as 5 characters")]
        public void CreatePerson_WithShorterZipcode_DomainExceptionInvalidZipcode()
        {
            Action action = () => new Person(1, "Tony", "Stark", "1210", "California", "Red");
            action.Should()
                .Throw<MyColor.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid zipcode. Zipcode must have at least 5 characters.");
        }

        [Fact(DisplayName = "Create Person with null city value")]
        public void CreatePerson_WithNullCityValue_ResultObjectValidState()
        {
            Action action = () => new Person(1, "Tony", "Stark", "10210", null, "Red");
            action.Should()
                .NotThrow<MyColor.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Person missing city value")]
        public void CreatePerson_MissingCityValue_ResultObjectValidState()
        {
            Action action = () => new Person(1, "Tony", "Stark", "10210", "", "Red");
            action.Should()
                .NotThrow<MyColor.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Person with null color value")]
        public void CreatePerson_WithNullColorValue_ResultObjectValidState()
        {
            Action action = () => new Person(1, "Tony", "Stark", "10210", "California", null);
            action.Should()
                .NotThrow<MyColor.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Person missing color value")]
        public void CreatePerson_MissingColorValue_ResultObjectValidState()
        {
            Action action = () => new Person(1, "Tony", "Stark", "10210", "California", "");
            action.Should()
                .NotThrow<MyColor.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Person with negative id")]
        public void CreatePerson_WithNegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Person(-1, "Tony", "Stark", "10210", "California", "Red");
            action.Should()
                .Throw<MyColor.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid id: -1. Id must be greater than 0.");
        }
    }
}
