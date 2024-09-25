// <copyright file="CarLogicTests.cs" company="PlaceholderCompany">
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
    /// Class for tests.
    /// </summary>
    [TestFixture]
    public class CarLogicTests
    {
        private Mock<IAutoRepository> carRepo;
        private Mock<IParkolasRepository> parkolasRepo;
        private Mock<IParkoloRepository> parkoloRepo;
        private IList<UsedParkingSpots> expectedUsedSpots;
        private IList<SumCost> expectedsumCost;

        /// <summary>
        /// Car GetAll() test.
        /// </summary>
        [Test]
        public void GetAllAuto()
        {
            Mock<IAutoRepository> mockedcarRepo = new Mock<IAutoRepository>(MockBehavior.Loose);
            Mock<IParkolasRepository> mockedparkolasRepo = new Mock<IParkolasRepository>(MockBehavior.Loose);
            Mock<IParkoloRepository> mockedparkoloRepo = new Mock<IParkoloRepository>(MockBehavior.Loose);

            IList<Auto> cars = new List<Auto>()
            {
                new Auto() { Rendszam = "ABC123", Marka = "Honda" },
                new Auto() { Rendszam = "CDR067", Marka = "Volkswagen" },
                new Auto() { Rendszam = "DGK309", Marka = "Nissan" },
                new Auto() { Rendszam = "DWH356", Marka = "Suzuki" },
                new Auto() { Rendszam = "HSK274", Marka = "Volkswagen" },
                new Auto() { Rendszam = "KSU096", Marka = "Nissan" },
            };
            mockedcarRepo.Setup(repo => repo.GetAll()).Returns(cars.AsQueryable());
            CarLogic logic = new CarLogic(mockedcarRepo.Object, mockedparkolasRepo.Object, mockedparkoloRepo.Object);
            logic.GetAllAuto();
            mockedcarRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        /// <summary>
        /// Tests Delete parkolo entity.
        /// </summary>
        [Test]
        public void DeleteParkoloSpot()
        {
            Mock<IParkoloRepository> mockedparkoloRepo = new Mock<IParkoloRepository>(MockBehavior.Loose);
            Mock<IAutoRepository> mockedcarRepo = new Mock<IAutoRepository>(MockBehavior.Loose);
            Mock<IParkolasRepository> mockedparkolasRepo = new Mock<IParkolasRepository>(MockBehavior.Loose);

            IList<ParkoloSpots> spots = new List<ParkoloSpots>()
            {
                new ParkoloSpots() { ParkolohelySzam = 1, Meret = 13, ElektromosE = "False" },
                new ParkoloSpots() { ParkolohelySzam = 2, Meret = 14, ElektromosE = "False" },
                new ParkoloSpots() { ParkolohelySzam = 3, Meret = 15, ElektromosE = "True" },
            };
            List<ParkoloSpots> expectedspots = new List<ParkoloSpots>() { spots[0], spots[2] };
            mockedparkoloRepo.Setup(repo => repo.Delete(spots[1]));
            mockedparkoloRepo.Setup(repo => repo.GetOne(2)).Returns(spots[1]);

            CarLogic logic = new CarLogic(mockedcarRepo.Object, mockedparkolasRepo.Object, mockedparkoloRepo.Object);

            logic.DeleteParkolo(2);

            mockedparkoloRepo.Verify(repo => repo.GetOne(2), Times.Once);
            mockedparkoloRepo.Verify(repo => repo.Delete(spots[1]), Times.Once);
        }

        /// <summary>
        /// Non crud test.
        /// </summary>
        [Test]
        public void TestSumCost()
        {
            var logic = this.CreateCarLogic();
            var actualSumCost = logic.ParkingFee();

            Assert.That(actualSumCost, Is.EquivalentTo(this.expectedsumCost));
            this.carRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.parkoloRepo.Verify(repo => repo.GetAll(), Times.Never);
            this.parkolasRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        /// <summary>
        /// Used parking spots over 13.
        /// </summary>
        [Test]
        public void TestUsedParkingSpots()
        {
            var carlogic = this.CreateCarLogic();
            var actualUsedParkingSpots = carlogic.UsedSpotsSizeOver13();

            Assert.That(actualUsedParkingSpots, Is.EquivalentTo(this.expectedUsedSpots));
            this.carRepo.Verify(repo => repo.GetAll(), Times.Never);
            this.parkoloRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.parkolasRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        private CarLogic CreateCarLogic()
        {
            this.carRepo = new Mock<IAutoRepository>(MockBehavior.Loose);
            this.parkolasRepo = new Mock<IParkolasRepository>(MockBehavior.Loose);
            this.parkoloRepo = new Mock<IParkoloRepository>(MockBehavior.Loose);

            List<ParkoloSpots> parkSpots = new List<ParkoloSpots>()
            {
                new ParkoloSpots() { ParkolohelySzam = 1, Meret = 15, ElektromosE = "False" },
                new ParkoloSpots() { ParkolohelySzam = 2, Meret = 15, ElektromosE = "False" },
                new ParkoloSpots() { ParkolohelySzam = 3, Meret = 13, ElektromosE = "False" },
                new ParkoloSpots() { ParkolohelySzam = 4, Meret = 13, ElektromosE = "False" },
            };

            Parkolas p1 = new Parkolas() { ParkolohelySzam = 1, EltoltottIdo = 10, Koltseg = 250, Rendszam = "ABC123", Szemely = new Szemely() { Nev = "Nagy András" } };
            Parkolas p2 = new Parkolas() { ParkolohelySzam = 2, EltoltottIdo = 11, Koltseg = 250, Rendszam = "CBA321", Szemely = new Szemely() { Nev = "Nagy Attila" } };
            Parkolas p3 = new Parkolas() { ParkolohelySzam = 3, EltoltottIdo = 6, Koltseg = 250, Rendszam = "AAA123", Szemely = new Szemely() { Nev = "Kiss Dorottya" } };
            Parkolas p4 = new Parkolas() { ParkolohelySzam = 4, EltoltottIdo = 8, Koltseg = 250, Rendszam = "BBB123", Szemely = new Szemely() { Nev = "Fekete Zsolt" } };
            List<Parkolas> parking = new List<Parkolas>() { p1, p2, p3, p4 };

            Auto car1 = new Auto() { Rendszam = "ABC123" };
            Auto car2 = new Auto() { Rendszam = "CBA321" };
            Auto car3 = new Auto() { Rendszam = "AAA123" };
            Auto car4 = new Auto() { Rendszam = "BBB123" };
            List<Auto> cars = new List<Auto>() { car1, car2, car3, car4 };

            this.expectedsumCost = new List<SumCost>()
            {
                new SumCost() { LicensePlateNumber = "ABC123", Sum = 2500 },
                new SumCost() { LicensePlateNumber = "CBA321", Sum = 2750 },
                new SumCost() { LicensePlateNumber = "AAA123", Sum = 1500 },
                new SumCost() { LicensePlateNumber = "BBB123", Sum = 2000 },
            };

            this.expectedUsedSpots = new List<UsedParkingSpots>()
            {
                new UsedParkingSpots() { Number = 2, Size = 15, IsElectric = "False", PersonName = "Nagy András" },
                new UsedParkingSpots() { Number = 1, Size = 15, IsElectric = "False", PersonName = "Nagy Attila" },
            };

            this.parkolasRepo.Setup(repo => repo.GetAll()).Returns(parking.AsQueryable());
            this.carRepo.Setup(repo => repo.GetAll()).Returns(cars.AsQueryable());
            this.parkoloRepo.Setup(repo => repo.GetAll()).Returns(parkSpots.AsQueryable());

            return new CarLogic(this.carRepo.Object, this.parkolasRepo.Object, this.parkoloRepo.Object);
        }
    }
}