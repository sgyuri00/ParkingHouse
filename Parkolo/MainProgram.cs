// <copyright file="MainProgram.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Parkolo.Program
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ConsoleTools;
    using Parkolo.Data;
    using Parkolo.Logic;
    using Parkolo.Repository;
    //using Parkolo.Kliens;

    /// <summary>
    /// Main Program.
    /// </summary>
    public class MainProgram
    {
        private static void Main(string[] args)
        {
            //RestService rest = new RestService("http://localhost:45793/", "Auto");

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Factory factory = new Factory();
            ParkolasLogic parkolasLogic = factory.CreateParkolasLogic();
            CarLogic autologic = factory.CreateCarLogic();

            var menu = new ConsoleMenu()
                .Add(">> GetAllParkolas", () => ListAllParkolas(parkolasLogic))
                .Add(">> GetAllSzemely", () => ListAllSzemely(parkolasLogic))
                .Add(">> GetAllAuto", () => ListAllAuto(autologic))
                .Add(">> GetAllParkolo", () => ListAllParkolo(autologic))
                .Add(">> Get Car Data By License Plate Number", () => GetByPlateNum(autologic))
                .Add(">> Get Spot Data By Number", () => GetBySpotId(autologic))
                .Add(">> Get Person Data By ID", () => GetByPersonId(parkolasLogic))
                .Add(">> Get Parking Data By License Plate Number", () => GetByRendszam(parkolasLogic))
                .Add(">> Change Car Spot", () => ChangeSpot(parkolasLogic))
                .Add(">> Change Spot Size", () => ChangeSpotSize(autologic))
                .Add(">> Change Person Name", () => ChangePersonName(parkolasLogic))
                .Add(">> Change Fuel Type", () => ChangeFuel(autologic))
                .Add(">> Add new Auto", () => AddCar(autologic))
                .Add(">> Add new Parkolo", () => AddParkolo(autologic))
                .Add(">> Add new Szemely", () => AddPerson(parkolasLogic))
                .Add(">> Add new Parkolas", () => AddParkolas(parkolasLogic))
                .Add(">> Delete Auto", () => DeleteCar(autologic))
                .Add(">> Delete Parkolo", () => DeleteParkolo(autologic))
                .Add(">> Delete Szemely", () => DeletePerson(parkolasLogic))
                .Add(">> Delete Parkolas", () => DeleteParkolas(parkolasLogic))
                .Add(">> (NON CRUD) People paid more than 2000", () => PeopleAndFees(parkolasLogic))
                .Add(">> (NON CRUD) People who have data in parkings and men", () => MalePeople(parkolasLogic))
                .Add(">> (NON CRUD) Used parking spots", () => UsedSpots(autologic))
                .Add(">> (NON CRUD) Get Parking fee by vehicle", () => ParkingFeeByVehicle(autologic))
                .Add(">> (NON CRUD - ASYNC)People paid more than 2000", () =>
                {
                    var result = parkolasLogic.PeoplePaidMoreThan2000Async().Result;
                    foreach (var item in result)
                    {
                        Console.WriteLine(item);
                    }

                    Console.ReadLine();
                })
                .Add(">> (NON CRUD - ASYNC) People who have data in parkings and men", () =>
                {
                    var result = parkolasLogic.MalePeopleHaveParkingAsync().Result;
                    foreach (var item in result)
                    {
                        Console.WriteLine(item);
                    }

                    Console.ReadLine();
                })
                .Add(">> (NON CRUD - ASYNC) Used parking spots", () =>
                {
                    var result = autologic.UsedSpotsSizeOver13Async().Result;
                    foreach (var item in result)
                    {
                        Console.WriteLine(item);
                    }

                    Console.ReadLine();
                })
                .Add(">> (NON CRUD - ASYNC) Get Parking fee by vehicle", () =>
                {
                    var result = autologic.ParkingFeeAsync().Result;
                    foreach (var item in result)
                    {
                        Console.WriteLine(item);
                    }

                    Console.ReadLine();
                })
                .Add(">> EXIT", ConsoleMenu.Close);

            menu.Show();
        }

        private static void ListAllParkolas(ParkolasLogic logic)
        {
            Console.WriteLine("All parkolas");
            logic.GetAllParkolas().ToList().ForEach(x => Console.WriteLine($"Parkolo ID: {x.ParkoloId}, license plate number: {x.Rendszam}, person ID: {x.SzemelyId}, parking spot: {x.ParkolohelySzam}, fee zone: {x.Koltseg}, time spent: {x.EltoltottIdo}"));
            Console.ReadLine();
        }

        private static void GetByRendszam(ParkolasLogic logic)
        {
            Console.WriteLine("ENTER LICENCE PLATE NUMBER HERE: ");
            string rendszam = Console.ReadLine();

            var q = logic.GetParkolasByRendszam(rendszam);
            Console.WriteLine("Selected parking data");
            Console.WriteLine("Person ID: " + q.SzemelyId + ", License plate number: " + q.Rendszam + ", Parking spot: " + q.ParkolohelySzam + ", Parking fee zone: " + q.Koltseg + ", Time spent(h): " + q.EltoltottIdo + ", Parking ID: " + q.ParkoloId);
            Console.ReadLine();
        }

        private static void ChangeSpot(ParkolasLogic logic)
        {
            Console.WriteLine("ENTER LICENCE PLATE NUMBER HERE: ");
            string rendszam = Console.ReadLine().ToString();

            Console.WriteLine("ENTER NEW SPOT HERE: ");
            int newSpot = int.Parse(Console.ReadLine());

            logic.ChangeParkolasSpot(rendszam, newSpot);
        }

        private static void AddParkolas(ParkolasLogic logic)
        {
            try
            {
                Console.WriteLine("Enter id here:");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter license plate number:");
                string plateNum = Console.ReadLine();
                Console.WriteLine("Enter person ID:");
                int peresonId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter spot number:");
                int spotNum = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter fee zone:");
                int fee = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter time spent:");
                int timeSpent = int.Parse(Console.ReadLine());
                Parkolas parking = new Parkolas()
                {
                    ParkoloId = id,
                    Rendszam = plateNum,
                    SzemelyId = peresonId,
                    ParkolohelySzam = spotNum,
                    Koltseg = fee,
                    EltoltottIdo = timeSpent,
                };
                logic.AddParkolas(parking);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Console.WriteLine("Already exists a parking with this ID!");
            }
            catch (System.InvalidOperationException)
            {
                Console.WriteLine("Already exists a parking with this ID!");
            }

            Console.ReadLine();
        }

        private static void DeleteParkolas(ParkolasLogic logic)
        {
            Console.WriteLine("Enter license plate number to delete from parking here: ");
            string id = Console.ReadLine();
            logic.DeleteParking(id);
        }

        private static void ListAllParkolo(CarLogic logic)
        {
            Console.WriteLine("ALL Parkolo");
            logic.GetAllParkolo().ToList().ForEach(x => Console.WriteLine($"Parking spot number: {x.ParkolohelySzam}, Size: {x.Meret}, IsElectric: {x.ElektromosE}"));
            Console.ReadLine();
        }

        private static void AddParkolo(CarLogic logic)
        {
            try
            {
                Console.WriteLine("Enter id here:");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Size:");
                int size = int.Parse(Console.ReadLine());
                Console.WriteLine("Is Electric?(Igen/Nem):");
                string type = Console.ReadLine();
                ParkoloSpots parkolo = new ParkoloSpots()
                {
                    ParkolohelySzam = id,
                    Meret = size,
                    ElektromosE = type,
                };
                logic.AddParkolo(parkolo);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Console.WriteLine("Already exists a parking spot with this ID!");
            }
            catch (System.InvalidOperationException)
            {
                Console.WriteLine("Already exists a parking spot with this ID!");
            }

            Console.ReadLine();
        }

        private static void DeleteParkolo(CarLogic logic)
        {
            Console.WriteLine("Enter parking spot number here: ");
            int id = int.Parse(Console.ReadLine());
            logic.DeleteParkolo(id);
        }

        private static void ChangeSpotSize(CarLogic logic)
        {
            Console.WriteLine("ENTER SPOT NUMBER NUMBER HERE: ");
            int spot = int.Parse(Console.ReadLine());

            Console.WriteLine("ENTER NEW SIZE HERE: ");
            int newSize = int.Parse(Console.ReadLine());

            logic.ChangeSpotSize(spot, newSize);
        }

        private static void GetBySpotId(CarLogic logic)
        {
            Console.WriteLine("Enter spot number here: ");
            int spot = int.Parse(Console.ReadLine());

            var q = logic.GetBySpotId(spot);
            Console.WriteLine("Selected person's data:");
            Console.WriteLine("Spot ID: " + q.ParkolohelySzam + ", Size: " + q.Meret + ", IsElectric : " + q.ElektromosE);

            Console.ReadLine();
        }

        private static void ListAllAuto(CarLogic logic)
        {
            Console.WriteLine("ALL AUTO");
            logic.GetAllAuto().ToList().ForEach(x => Console.WriteLine("Márka: " + x.Marka + ", Rendszám: " + x.Rendszam + ", Gyártási év: " + x.GyartasiEv + ", Üzemanyag: " + x.Uzemanyag));
            Console.ReadLine();
        }

        private static void AddCar(CarLogic logic)
        {
            try
            {
                Console.WriteLine("License Plate Number:");
                string plate = Console.ReadLine();
                Console.WriteLine("Brand:");
                string brand = Console.ReadLine();
                Console.WriteLine("Year:");
                int year = int.Parse(Console.ReadLine());
                Console.WriteLine("Fuel type:");
                string fuel = Console.ReadLine();
                Auto auto = new Auto();
                auto.Rendszam = plate;
                auto.Marka = brand;
                auto.GyartasiEv = year;
                auto.Uzemanyag = fuel;

                logic.AddCar(auto);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Console.WriteLine("Already exists a car with this license plate number!");
            }
            catch (System.InvalidOperationException)
            {
                Console.WriteLine("Already exists a car with this license plate number!");
            }

            Console.ReadLine();
        }

        private static void DeleteCar(CarLogic logic)
        {
            Console.WriteLine("Enter license plate number here: ");
            string plateNum = Console.ReadLine();
            logic.DeleteCar(plateNum);
        }

        private static void ChangeFuel(CarLogic logic)
        {
            Console.WriteLine("Enter license plate number here: ");
            string plateNum = Console.ReadLine();
            Console.WriteLine("Enter fuel type here: ");
            string fuel = Console.ReadLine();
            logic.ChangeFuel(plateNum, fuel);
        }

        private static void GetByPlateNum(CarLogic logic)
        {
            Console.WriteLine("Enter license plate number here: ");
            string plateNum = Console.ReadLine();
            var q = logic.GetByPlateNum(plateNum);
            Console.WriteLine("Selected car's data:");
            Console.WriteLine("License Plate Number: " + q.Rendszam + ", Brand: " + q.Marka + ", Year: " + q.GyartasiEv + ", Fuel type: " + q.Uzemanyag);
            Console.ReadLine();
        }

        private static void ListAllSzemely(ParkolasLogic logic)
        {
            Console.WriteLine("Details of each person");
            logic.GetAllSzemely().ToList().ForEach(x => Console.WriteLine("Person ID: " + x.SzemelyId + " Full name: " + x.Nev + " Gender: " + x.Nem + " Date of birth: " + ((DateTime)x.SzuletesiIdo).ToString("yyyy-MM-dd")));
            Console.ReadLine();
        }

        private static void AddPerson(ParkolasLogic parkolasLogic)
        {
            try
            {
                Console.WriteLine("Enter person id here: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter full name here: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter gender here: ");
                string gender = Console.ReadLine();
                Console.WriteLine("Enter date of birth here: ");
                DateTime? dateofBirth = DateTime.Parse(Console.ReadLine());

                parkolasLogic.AddPerson(id, name, gender, dateofBirth);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Console.WriteLine("Already exists a person with this ID!");
            }
            catch (System.InvalidOperationException)
            {
                Console.WriteLine("Already exists a person with this ID!");
            }

            Console.ReadLine();
        }

        private static void DeletePerson(ParkolasLogic parkolasLogic)
        {
            Console.WriteLine("Enter person id here: ");
            int id = int.Parse(Console.ReadLine());
            parkolasLogic.DeletePerson(id);
        }

        private static void ChangePersonName(ParkolasLogic parkolasLogic)
        {
            Console.WriteLine("Enter person id: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter new name here: ");
            string newName = Console.ReadLine().ToString();

            parkolasLogic.ChangeName(id, newName);
        }

        private static void GetByPersonId(ParkolasLogic logic)
        {
            Console.WriteLine("Enter person id here: ");
            int id = int.Parse(Console.ReadLine());

            var q = logic.GetById(id);
            Console.WriteLine("Selected person's data:");
            Console.WriteLine("Person ID: " + q.SzemelyId + ", Full name: " + q.Nev + ", Gender: " + q.Nem + ",Date of birth: " + ((DateTime)q.SzuletesiIdo).ToString("yyyy - MM - dd"));

            Console.ReadLine();
        }

        private static void PeopleAndFees(ParkolasLogic parkolasLogic)
        {
            Console.WriteLine("People who paid more than 2000 HUF:");
            var q1 = parkolasLogic.PeoplePaidMoreThan2000();
            Console.WriteLine(string.Join("\n", q1));

            Console.ReadLine();
        }

        private static void MalePeople(ParkolasLogic parkolasLogic)
        {
            Console.WriteLine("People who have data in parkings and men:");
            var q2 = parkolasLogic.MalePeopleHaveParking();
            Console.WriteLine(string.Join("\n", q2));
            Console.ReadLine();
        }

        private static void UsedSpots(CarLogic logic)
        {
            Console.WriteLine("Used parking spots:");
            var q3 = logic.UsedSpotsSizeOver13();
            Console.WriteLine(string.Join("\n", q3));
            Console.ReadLine();
        }

        private static void ParkingFeeByVehicle(CarLogic logic)
        {
            Console.WriteLine("Parking fees by vehicles:");
            var q4 = logic.ParkingFee();
            Console.WriteLine(string.Join("\n", q4));
            Console.ReadLine();
        }
    }
}
