// <copyright file="ParkolasLogicTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Parkolo.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using Parkolo.Data;
    using Parkolo.Repository;

    /// <summary>
    /// ParkolasLogic Tests class.
    /// </summary>
    [TestFixture]
    public class ParkolasLogicTests
    {
        private Mock<ISzemelyRepository> peopleRepo;
        private Mock<IParkolasRepository> parkolasRepo;
        private IList<MalePeople> expectedMalePeople;
        private IList<PaidMoreThan2000> expectedPeople;

        /// <summary>
        /// Tests add Szemely.
        /// </summary>
        [Test]
        public void TestGetOneSzemelyByID()
        {
            Mock<ISzemelyRepository> mockedpeopleRepo = new Mock<ISzemelyRepository>(MockBehavior.Loose);
            Mock<IParkolasRepository> mockedparkolasRepo = new Mock<IParkolasRepository>(MockBehavior.Loose);

            IList<Szemely> people = new List<Szemely>()
            {
                new Szemely() { Nev = "Nagy András", SzemelyId = 1, Nem = "Ferfi" },
                new Szemely() { Nev = "Nagy Attila", SzemelyId = 2, Nem = "Ferfi" },
                new Szemely() { Nev = "Kiss Bernadett", SzemelyId = 3, Nem = "No" },
                new Szemely() { Nev = "Kiss Dorottya", SzemelyId = 4, Nem = "No" },
            };
            mockedpeopleRepo.Setup(repo => repo.GetOne(2)).Returns(people[1]);

            ParkolasLogic logic = new ParkolasLogic(mockedparkolasRepo.Object, mockedpeopleRepo.Object);
            logic.GetById(2);

            mockedpeopleRepo.Verify(repo => repo.GetOne(2), Times.Once);
        }

        /// <summary>
        /// Tests Add new Parkolas.
        /// </summary>
        [Test]
        public void TestAddParkolas()
        {
            Mock<ISzemelyRepository> mockedpeopleRepo = new Mock<ISzemelyRepository>(MockBehavior.Loose);
            Mock<IParkolasRepository> mockedparkolasRepo = new Mock<IParkolasRepository>(MockBehavior.Loose);

            Parkolas p1 = new Parkolas() { ParkoloId = 1, SzemelyId = 1, ParkolohelySzam = 2, Rendszam = "ABC123" };
            mockedparkolasRepo.Setup(repo => repo.InsertOne(p1));

            ParkolasLogic logic = new ParkolasLogic(mockedparkolasRepo.Object, mockedpeopleRepo.Object);
            logic.AddParkolas(p1);

            mockedparkolasRepo.Verify(repo => repo.InsertOne(p1), Times.Once);
        }

        /// <summary>
        /// Tests change person name.
        /// </summary>
        [Test]
        public void TestChangePersonName()
        {
            Mock<ISzemelyRepository> mockedpeopleRepo = new Mock<ISzemelyRepository>(MockBehavior.Loose);
            Mock<IParkolasRepository> mockedparkolasRepo = new Mock<IParkolasRepository>(MockBehavior.Loose);

            Szemely person = new Szemely() { SzemelyId = 1, Nev = "Nagy András" };
            mockedpeopleRepo.Setup(repo => repo.ChangeName(1, "Nagy Attila"));

            ParkolasLogic logic = new ParkolasLogic(mockedparkolasRepo.Object, mockedpeopleRepo.Object);
            logic.ChangeName(1, "Nagy Attila");

            mockedpeopleRepo.Verify(repo => repo.ChangeName(1, "Nagy Attila"), Times.Once);
        }

        /// <summary>
        /// MalePeople Tests.
        /// </summary>
        [Test]
        public void TestMalePeople()
        {
            var parkolaslogic = this.CreateParkolasLogic();
            var actualMalePeople = parkolaslogic.MalePeopleHaveParking();

            Assert.That(actualMalePeople, Is.EquivalentTo(this.expectedMalePeople));
            this.peopleRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.parkolasRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        /// <summary>
        /// PeoplePaidMoreThan200 Tests.
        /// </summary>
        [Test]
        public void TestPeoplePaid2000()
        {
            var parkolaslogic = this.CreateParkolasLogic();
            var actualPeople = parkolaslogic.PeoplePaidMoreThan2000();

            Assert.That(actualPeople, Is.EquivalentTo(this.expectedPeople));
            this.peopleRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.parkolasRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        private ParkolasLogic CreateParkolasLogic()
        {
            this.peopleRepo = new Mock<ISzemelyRepository>(MockBehavior.Loose);
            this.parkolasRepo = new Mock<IParkolasRepository>(MockBehavior.Loose);

            Parkolas p1 = new Parkolas() { ParkolohelySzam = 1, EltoltottIdo = 10, Koltseg = 250, Rendszam = "ABC123", SzemelyId = 1 };
            Parkolas p2 = new Parkolas() { ParkolohelySzam = 2, EltoltottIdo = 8, Koltseg = 250, Rendszam = "CBA321", SzemelyId = 2 };
            Parkolas p3 = new Parkolas() { ParkolohelySzam = 3, EltoltottIdo = 6, Koltseg = 250, Rendszam = "CBA333", SzemelyId = 3 };

            List<Parkolas> parking = new List<Parkolas>() { p1, p2, p3 };

            Szemely person1 = new Szemely() { SzemelyId = 1, Nem = "Ferfi", Nev = "Nagy András", SzuletesiIdo = DateTime.Now, };
            Szemely person2 = new Szemely() { SzemelyId = 2, Nem = "Ferfi", Nev = "Nagy Attila", SzuletesiIdo = DateTime.Now };
            Szemely person3 = new Szemely() { SzemelyId = 3, Nem = "No", Nev = "Kiss Dorottya", SzuletesiIdo = DateTime.Now };
            List<Szemely> people = new List<Szemely>() { person1, person2, person3 };

            this.expectedMalePeople = new List<MalePeople>()
            {
                new MalePeople() { Name = "Nagy András", Gender = "Ferfi" },
                new MalePeople() { Name = "Nagy Attila", Gender = "Ferfi" },
            };
            this.expectedPeople = new List<PaidMoreThan2000>()
            {
                new PaidMoreThan2000() { Name = "Nagy András", ParkingFee = 2500, LicensePlateNumber = "ABC123" },
            };

            this.peopleRepo.Setup(repo => repo.GetAll()).Returns(people.AsQueryable());
            this.parkolasRepo.Setup(repo => repo.GetAll()).Returns(parking.AsQueryable());

            return new ParkolasLogic(this.parkolasRepo.Object, this.peopleRepo.Object);
        }
    }
}