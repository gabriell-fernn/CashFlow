﻿using Bogus;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Cryptography;

namespace CommonTestUtilities.Entities
{
    public class UserBuilder
    {
        public static User Build() 
        { 
            var passwordEncripter = new PasswordEncripterBuilder().Build();

            var user = new Faker<User>()
                .RuleFor(u => u.Id, _ => 1)
                .RuleFor(u => u.Name, f => f.Person.FirstName)
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.Password, (_, user) => passwordEncripter.Encrypt(user.Password))
                .RuleFor(u => u.UserIdentifier, _ => Guid.NewGuid());

            return user;
        }
    }
}
