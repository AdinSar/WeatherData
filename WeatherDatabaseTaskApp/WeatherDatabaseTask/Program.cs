using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using WeatherDatabaseTask.Models;

namespace WeatherDatabaseTask
{
    class Program
    {
        static void Main(string[] args)
        {
            
            bool on = true;
            while (on)
            {
                try
                {
                    Console.WriteLine(@"1. Medeltemperatur för valt datum");
                    Console.WriteLine(@"2. Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
                    Console.WriteLine(@"3. Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
                    Console.WriteLine(@"4. Sortering av minst till störst risk för mögel");
                    Console.WriteLine(@"5. Datum för meteorologisk Höst");
                    Console.WriteLine(@"6. Datum för meteorologisk Vinter ");
                    int Menuinput = int.Parse(Console.ReadLine());
                    switch (Menuinput)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine(@"Medeltemperatur för valt datum");
                            Console.WriteLine(@"1. Inne");
                            Console.WriteLine(@"2. Ute");
                            int inOrOutinput1 = int.Parse(Console.ReadLine());
                            if (inOrOutinput1 == 1)
                            {
                                Console.Clear();
                                Console.WriteLine(@"Medeltemperatur för valt datum");
                                Console.WriteLine();
                                InAverageTemperatureSearch();
                                Console.ReadKey();
                                Console.Clear();

                            }
                            else if (inOrOutinput1 == 2)
                            {
                                Console.Clear();
                                Console.WriteLine(@"Medeltemperatur för valt datum");
                                Console.WriteLine();
                                OutAverageTemperatureSearch();
                                Console.ReadKey();
                                Console.Clear();
                            }
                          
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine(@"Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
                            Console.WriteLine(@"1. Inne");
                            Console.WriteLine(@"2. Ute");
                            int inOrOutinput2 = int.Parse(Console.ReadLine());
                            if (inOrOutinput2 == 1)
                            {
                                Console.Clear();
                                Console.WriteLine(@"Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
                                Console.WriteLine();
                                InHotToColdByDayAccordingToAverageTemperaturePerDay();
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else if (inOrOutinput2 == 2)
                            {
                                Console.Clear();
                                Console.WriteLine(@"Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
                                Console.WriteLine();
                                OutHotToColdByDayAccordingToAverageTemperaturePerDay();
                                Console.ReadKey();
                                Console.Clear();
                            }
                          
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine(@"Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
                            Console.WriteLine(@"1. Inne");
                            Console.WriteLine(@"2. Ute");
                            int inOrOutinput3 = int.Parse(Console.ReadLine());
                            if (inOrOutinput3 == 1)
                            {
                                Console.Clear();
                                Console.WriteLine(@"Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
                                Console.WriteLine();
                                InHighestToLowestHumitityByDayAccordingToAverageHumidityPerDay();
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else if (inOrOutinput3 == 2)
                            {
                                Console.Clear();
                                Console.WriteLine(@"Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
                                Console.WriteLine();
                                OutHighestToLowestHumitityByDayAccordingToAverageHumidityPerDay();
                                Console.ReadKey();
                                Console.Clear();
                            }
                            
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine(@"Sortering av minst till störst risk för mögel");
                            Console.WriteLine(@"1. Inne");
                            Console.WriteLine(@"2. Ute");
                            int inOrOutinput4 = int.Parse(Console.ReadLine());
                            if (inOrOutinput4 == 1)
                            {
                                Console.Clear();
                                Console.WriteLine(@"Sortering av minst till störst risk för mögel");
                                Console.WriteLine();
                                InLowestToHighestMoldriskByDay();
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else if (inOrOutinput4 == 2)
                            {
                                Console.Clear();
                                Console.WriteLine(@"Sortering av minst till störst risk för mögel");
                                Console.WriteLine();
                                OutLowestToHighestMoldriskByDay();
                                Console.ReadKey();
                                Console.Clear();
                            }
                            
                            break;
                        case 5:
                            Console.Clear();
                            Console.WriteLine(@"Datum för meteorologisk Höst");
                            GetStartOfAutumn();
                            Console.ReadKey();
                            Console.Clear();

                            break;
                        case 6:
                            Console.Clear();
                            Console.WriteLine(@"Datum för meteorologisk Vinter");
                            GetStartOfWinter();
                            Console.ReadKey();
                            Console.Clear();

                            break;
                        default:
                            break;
                    }

                }
                catch (Exception)
                {
                    Console.Clear();
                    
                    Console.WriteLine();
                }


            }






            //AddDataToDatabase();
            //InAverageTemperatureSearch();
            //OutAverageTemperatureSearch();
            //OutHotToColdByDayAccordingToAverageTemperaturePerDay();
            //InHotToColdByDayAccordingToAverageTemperaturePerDay();
            //OutHighestToLowestHumitityByDayAccordingToAverageHumidityPerDay();
            //InHighestToLowestHumitityByDayAccordingToAverageHumidityPerDay();
            //OutLowestToHighestMoldriskByDay();
            //InLowestToHighestMoldriskByDay();
            //GetStartOfAutumn();
            //GetStartOfWinter();



        }

        private static void GetStartOfWinter()
        {
            using (var db = new EFContext())
            {

                List<WeatherData> weatherDatas = db.WeatherDatas.ToList();
                var set1 = weatherDatas
                    .Where(w => w.InOrOut == "Ute")
                    .GroupBy(w => new { year = w.Date.Year, month = w.Date.Month, day = w.Date.Day })
                    .Select(w => new { yeardate = w.Key.year, monthdate = w.Key.month, w.Key.day, AverageTemperature = w.Average(a => a.Temperature) });

                int startDay = 0;
                int fiveCount = 0;

                foreach (var item in set1)
                {
                    if (item.monthdate >= 8 && item.AverageTemperature < 0.05)
                    {
                        if (startDay == 0)
                        {
                            startDay = item.day;
                            fiveCount++;
                        }
                        else if (startDay != 0 && startDay == item.day - 1)
                        {
                            fiveCount++;
                        }
                        else if (startDay != 0 && startDay == item.day - 2)
                        {
                            fiveCount++;
                        }
                        else if (startDay != 0 && startDay == item.day - 3)
                        {
                            fiveCount++;
                        }
                        else if (startDay != 0 && startDay == item.day - 4)
                        {
                            fiveCount++;
                        }
                        else if (fiveCount == 5)
                        {
                            Console.WriteLine(item.yeardate + "/" + item.monthdate + "/" + (item.day-5) + " är starten av den meteorologiska vintern");
                            break;

                        }
                        else
                        {
                            startDay = item.day;
                            fiveCount = 1;
                        }


                    }

                }
                if (fiveCount < 5)
                {
                    Console.WriteLine("Ingen meteorologisk vinter");
                }





            }
            Console.ReadKey();

        }

        private static void GetStartOfAutumn()
        {
            using (var db = new EFContext())
            {

                List<WeatherData> weatherDatas = db.WeatherDatas.ToList();
                var set1 = weatherDatas
                    .Where(w => w.InOrOut == "Ute")
                    .GroupBy(w => new { year = w.Date.Year, month = w.Date.Month, day = w.Date.Day })
                    .Select(w => new { yeardate = w.Key.year, monthdate = w.Key.month, w.Key.day, AverageTemperature = w.Average(a => a.Temperature) });

                int startDay = 0;
                int fiveCount = 0;

                foreach (var item in set1)
                {
                    if (item.monthdate >= 8 && item.AverageTemperature >= 0 && item.AverageTemperature < 10)
                    {
                        if (startDay == 0)
                        {
                            startDay = item.day;
                            fiveCount++;
                        }
                        else if (startDay != 0 && startDay == item.day - 1)
                        {
                            fiveCount++;
                        }
                        else if (startDay != 0 && startDay == item.day - 2)
                        {
                            fiveCount++;
                        }
                        else if (startDay != 0 && startDay == item.day - 3)
                        {
                            fiveCount++;
                        }
                        else if (startDay != 0 && startDay == item.day - 4)
                        {
                            fiveCount++;
                        }
                        else if (fiveCount == 5)
                        {
                            Console.WriteLine(item.yeardate + "/" + item.monthdate + "/" + (item.day-5) + " är starten av den meteorologiska hösten");
                            break;

                        }
                        else
                        {
                            startDay = item.day;
                            fiveCount = 1;
                        }


                    }

                }
                if (fiveCount < 5)
                {
                    Console.WriteLine("Ingen meteorologisk höst");
                }





            }
            Console.ReadKey();
        }

        private static void OutAverageTemperatureSearch()
        {
            Console.Clear();
            try
            {

                Console.WriteLine(@"Välj datum (yyyymmdd)");


                string dateInput = Console.ReadLine();
                DateTime date = DateTime.ParseExact(dateInput, "yyyyMMdd", CultureInfo.InvariantCulture);



                using (var db = new EFContext())
                {

                    List<WeatherData> weatherDatas = db.WeatherDatas.ToList();
                    var set1 = weatherDatas
                        .Where(w => w.InOrOut == "Ute" && w.Date.Date == date.Date)
                        .GroupBy(w => new { year = w.Date.Year, month = w.Date.Month, day = w.Date.Day })
                        .Select(w => new { yeardate = w.Key.year, monthdate = w.Key.month, w.Key.day, AverageTemperature = w.Average(a => a.Temperature) });

                    foreach (var item in set1)
                    {
                        Console.WriteLine(item.yeardate + "/" + item.monthdate + "/" + item.day + " = " + Math.Round(item.AverageTemperature, 2));
                    }

                }


            }
            catch (Exception)
            {

                Console.WriteLine("Fel inmatning");
            }
        }

        private static void InAverageTemperatureSearch()
        {
            Console.Clear();
            try
            {

                Console.WriteLine(@"Välj datum (yyyymmdd)");


                string dateInput = Console.ReadLine();
                DateTime date = DateTime.ParseExact(dateInput, "yyyyMMdd", CultureInfo.InvariantCulture);



                using (var db = new EFContext())
                {

                    List<WeatherData> weatherDatas = db.WeatherDatas.ToList();
                    var set1 = weatherDatas
                        .Where(w => w.InOrOut == "Inne" && w.Date.Date == date.Date)
                        .GroupBy(w => new { year = w.Date.Year, month = w.Date.Month, day = w.Date.Day })
                        .Select(w => new { yeardate = w.Key.year, monthdate = w.Key.month, w.Key.day, AverageTemperature = w.Average(a => a.Temperature) });

                    foreach (var item in set1)
                    {
                        Console.WriteLine(item.yeardate + "/" + item.monthdate + "/" + item.day + " = " + Math.Round(item.AverageTemperature, 2));
                    }

                }


            }
            catch (Exception)
            {

                Console.WriteLine("Fel inmatning");
            }



        }

        private static void InLowestToHighestMoldriskByDay()
        {
            using (var db = new EFContext())
            {

                List<WeatherData> weatherDatas = db.WeatherDatas.ToList();
                var set1 = weatherDatas
                    .Where(w => w.InOrOut == "Inne")
                    .GroupBy(w => new { year = w.Date.Year, month = w.Date.Month, day = w.Date.Day })
                    .Select(w => new
                    {
                        yeardate = w.Key.year,
                        monthdate = w.Key.month,
                        w.Key.day,
                        AverageHumidity = w.Average(a => a.Humidity),
                        AverageTemperature = w.Average(a => a.Temperature),
                        Moldrisk = ((w.Average(a => a.Humidity) - 78) * ((w.Average(a => a.Temperature)) / 15) / 0.22)
                    })
                    .OrderBy(w => w.Moldrisk);

                foreach (var item in set1)
                {

                    double result = item.Moldrisk;
                    if (result < 0)
                    {
                        result = 0;
                    }
                    if (result > 100)
                    {
                        result = 100;
                    }

                    Console.WriteLine(item.yeardate + "/" + item.monthdate + "/" + item.day + " Mögelrisk = " + result + "%");

                }


            }
        }

        private static void OutLowestToHighestMoldriskByDay()
        {
            using (var db = new EFContext())
            {

                List<WeatherData> weatherDatas = db.WeatherDatas.ToList();
                var set1 = weatherDatas
                    .Where(w => w.InOrOut == "Ute")
                    .GroupBy(w => new { year = w.Date.Year, month = w.Date.Month, day = w.Date.Day })
                    .Select(w => new
                    {
                        yeardate = w.Key.year,
                        monthdate = w.Key.month,
                        w.Key.day,
                        AverageHumidity = w.Average(a => a.Humidity),
                        AverageTemperature = w.Average(a => a.Temperature),
                        Moldrisk =((w.Average(a => a.Humidity)-78) * ((w.Average(a => a.Temperature)) / 15) / 0.22)
                    })
                    .OrderBy(w => w.Moldrisk);

                foreach (var item in set1)
                {
                    double result = item.Moldrisk;
                    if (result < 0)
                    {
                        result = 0;
                    }
                    if (result > 100)
                    {
                        result = 100;
                    }

                    Console.WriteLine(item.yeardate + "/" + item.monthdate + "/" + item.day + " Mögelrisk = " + result + "%");

                }


            }

        }

        private static void InHighestToLowestHumitityByDayAccordingToAverageHumidityPerDay()
        {
            using (var db = new EFContext())
            {

                List<WeatherData> weatherDatas = db.WeatherDatas.ToList();
                var set1 = weatherDatas
                    .Where(w => w.InOrOut == "Inne")
                    .GroupBy(w => new { year = w.Date.Year, month = w.Date.Month, day = w.Date.Day })
                    .Select(w => new { yeardate = w.Key.year, monthdate = w.Key.month, w.Key.day, AverageHumidity = w.Average(a => a.Humidity) })
                    .OrderBy(w => w.AverageHumidity);


                foreach (var item in set1)
                {
                    Console.WriteLine(item.yeardate + "/" + item.monthdate + "/" + item.day + " = " + Math.Round(item.AverageHumidity, 2));
                }


            }
        }

        private static void OutHighestToLowestHumitityByDayAccordingToAverageHumidityPerDay()
        {
            using (var db = new EFContext())
            {

                List<WeatherData> weatherDatas = db.WeatherDatas.ToList();
                var set1 = weatherDatas
                    .Where(w => w.InOrOut == "Ute")
                    .GroupBy(w => new { year = w.Date.Year, month = w.Date.Month, day = w.Date.Day })
                    .Select(w => new { yeardate = w.Key.year, monthdate = w.Key.month, w.Key.day, AverageHumidity = w.Average(a => a.Humidity) })
                    .OrderBy(w => w.AverageHumidity);


                foreach (var item in set1)
                {
                    Console.WriteLine(item.yeardate + "/" + item.monthdate + "/" + item.day + " = " + Math.Round(item.AverageHumidity, 2));
                }


            }
        }

        private static void InHotToColdByDayAccordingToAverageTemperaturePerDay()
        {
            using (var db = new EFContext())
            {

                List<WeatherData> weatherDatas = db.WeatherDatas.ToList();
                var set1 = weatherDatas
                    .Where(w => w.InOrOut == "Inne")
                    .GroupBy(w => new { year = w.Date.Year, month = w.Date.Month, day = w.Date.Day })
                    .Select(w => new { yeardate = w.Key.year, monthdate = w.Key.month, w.Key.day, AverageTemperature = w.Average(a => a.Temperature) })
                    .OrderByDescending(w => w.AverageTemperature);


                foreach (var item in set1)
                {
                    Console.WriteLine(item.yeardate + "/" + item.monthdate + "/" + item.day + " = " + Math.Round(item.AverageTemperature, 2));
                }


            }
        }

        private static void OutHotToColdByDayAccordingToAverageTemperaturePerDay()
        {
            using (var db = new EFContext())
            {

                List<WeatherData> weatherDatas = db.WeatherDatas.ToList();
                var set1 = weatherDatas
                    .Where(w => w.InOrOut == "Ute")
                    .GroupBy(w => new { year = w.Date.Year, month = w.Date.Month, day = w.Date.Day })
                    .Select(w => new { yeardate = w.Key.year, monthdate = w.Key.month, w.Key.day, AverageTemperature = w.Average(a => a.Temperature) })
                    .OrderByDescending(w => w.AverageTemperature);

                foreach (var item in set1)
                {
                    Console.WriteLine(item.yeardate + "/" + item.monthdate + "/" + item.day + " " + Math.Round(item.AverageTemperature, 2));
                }





            }
        }

        public static void AddDataToDatabase()
        {
            int count = 0;
            string WeatherDataFileInfo = File.ReadAllText("WeatherInfo.txt");
            string[] dataLines = WeatherDataFileInfo.Split('\n');

            foreach (string data in dataLines)
            {
                string[] keyValue = data.Split(',');

                if (keyValue.Length is 4)
                {
                    using (var db = new EFContext())
                    {




                        WeatherData weatherData = new WeatherData();

                        weatherData.Date = Convert.ToDateTime(keyValue[0]);
                        weatherData.InOrOut = keyValue[1];
                        weatherData.Temperature = Double.Parse(keyValue[2].Replace(".", ","));
                        weatherData.Humidity = Convert.ToInt32(keyValue[3].Trim(new Char[] { 'r', '/' }));



                        db.Add(weatherData);
                        db.SaveChanges();

                        Console.WriteLine("data added " + count);
                        count++;




                    }

                }
            }
        }

    }
}
