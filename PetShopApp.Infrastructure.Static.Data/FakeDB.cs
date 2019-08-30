using PetShopApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Infrastructure.Static.Data
{
    public static class FakeDB
    {
        public static IEnumerable<Pet> pets;
        public static int id = 1;


        public static void InitData()
        {
            Pet cat = new Pet()
            {
                Id = id++,
                Name = "Jazz",
                Type = "Cat",
                BirthDate = new DateTime(2008, 10, 14),
                SoldDate = new DateTime(2008, 12, 15),
                Color = "grey",
                PreviousOwner = "homeless/streetcat",
                Price = 5000
            };
            Pet dog = new Pet()
            {
                Id = id++,
                Name = "Sarri",
                Type = "Dog",
                BirthDate = new DateTime(2006, 10, 14),
                SoldDate = new DateTime(2007, 10, 15),
                Color = "Golden",
                PreviousOwner = "David Fabricius",
                Price = 50
            };

            Pet turtle = new Pet()
            {
                Id = id++,
                Name = "Leonardo",
                Type = "Turtle",
                BirthDate = new DateTime(2016, 10, 14),
                SoldDate = new DateTime(2017, 10, 15),
                Color = "Green",
                PreviousOwner = "Splinter",
                Price = 200
            };

            Pet Lobster = new Pet()
            {
                Id = id++,
                Name = "Rock lobster",
                Type = "Lobster",
                BirthDate = new DateTime(2014, 10, 14),
                SoldDate = new DateTime(2017, 10, 15),
                Color = "Brown",
                PreviousOwner = "The ocean",
                Price = 400
            };

            Pet Dog = new Pet()
            {
                Id = id++,
                Name = "SomeDog",
                Type = "Dog",
                BirthDate = new DateTime(2015, 10, 11),
                SoldDate = new DateTime(2017, 01, 15),
                Color = "Brown",
                PreviousOwner = "SomeBody",
                Price = 450
            };

            Pet Cat = new Pet()
            {
                Id = id++,
                Name = "SomeCat",
                Type = "Cat",
                BirthDate = new DateTime(2016, 11, 04),
                SoldDate = new DateTime(2017, 01, 15),
                Color = "Black",
                PreviousOwner = "SomeBody",
                Price = 600
            };
            pets = new List<Pet> { cat, dog, turtle, Lobster, Dog, Cat };
        }

    }
}
