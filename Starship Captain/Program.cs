using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;


namespace Starship_Captain
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Home World: 123.123.99.1 X & 098.098.11.1 Y & 456.456.99.9 Z
            // Your coordinate system ranges from 000.000.00.0 to 999.999.99.9, and you live in a three-dimensional universe.
            // - In order to inhabit a planet, you have to colonize more than 50% of its surface.
            // - Colonization takes place at exactly 0.043 seconds per square kilometer.
            // - Due to the strange construction of the star system you live in, travel time between any planet and its immediate neighbor is always 10 minutes, 
            //   except if the neighbor is a space monster, in which case travel time doubles because you have to dodge the monster.

            string HomeWorld = "123.123.99.1 X & 098.098.11.1 Y & 456.456.99.9 Z";
            double colonizationRate = 0.043;
            int travelTime = 10;

            CreateUniverse();

            GeneratePoints(HomeWorld);
            

            Console.WriteLine("Planets inhabited: " + ReadStepOne(HomeWorld, colonizationRate, travelTime)[0]);
            Console.WriteLine("Flight path taken: The nearest planet to the home planet in order.");
            Console.WriteLine("Surface Area colonized in the 24 hour period: " + ReadStepOne(HomeWorld, colonizationRate, travelTime)[1]);


            Console.ReadLine();

        }

        static void CreateUniverse()
        { 
            
            IEnumerable<int> xCoords = Enumerable.Range(0, 999999999);
            IEnumerable<int> yCoords = Enumerable.Range(0, 999999999);
            IEnumerable<int> zCoords = Enumerable.Range(0, 999999999);
        }

        static void GeneratePoints(string HomeWorld)
        {
            // Random number generator in C#
            Random random = new Random();
            // Create array/matrix with 5 columns and 15000 rows to store the generated points 
            // column 1 = coordinates
            // column 2 = very hungry space monster/planet
            // column 3 = habitable planet or not
            // column 4 = habitable planet surface area
            // column 5 = distance from homeworld
            string[,] holder = new string[15000, 5];
            // Take away the non-digit characters from HomeWorld string and store result
            string HomeWorldCoords =  new string(HomeWorld.Where(c => char.IsDigit(c)).ToArray());
            string locations = "";
            DataTable dt = new DataTable { TableName = "MyTableName" };
            // Generate the random points and concatenate them to form 15000 single coordinates
            for (int i = 0; i < 15000; i++)
            {
                // X coordinate
                // First number to be generated below 1000 concatenated with a fullstop
                
                holder[i, 0] = random.Next(1000).ToString().PadLeft(3, '0') + ".";
                // Second number to be generated below 1000 
                holder[i, 0] = holder[i, 0] + random.Next(1000).ToString().PadLeft(3, '0') + ".";
                // Third number to be generated below 100
                holder[i, 0] = holder[i, 0] + random.Next(100).ToString().PadLeft(2, '0') + ".";
                // Last number to be generated below 10
                holder[i, 0] = holder[i, 0] + random.Next(10).ToString() + " X & ";

                // Y coordinate
                // First number to be generated below 1000 concatenated with a fullstop
                holder[i, 0] = holder[i, 0] + random.Next(1000).ToString().PadLeft(3, '0') + ".";
                // Second number to be generated below 1000 
                holder[i, 0] = holder[i, 0] + random.Next(1000).ToString().PadLeft(3, '0') + ".";
                // Third number to be generated below 100
                holder[i, 0] = holder[i, 0] + random.Next(100).ToString().PadLeft(2, '0') + ".";
                // Last number to be generated below 10
                holder[i, 0] = holder[i, 0] + random.Next(10).ToString() + " Y & ";

                // Z coordinate
                // First number to be generated below 1000 concatenated with a fullstop
                holder[i, 0] = holder[i, 0] + random.Next(1000).ToString().PadLeft(3, '0') + ".";
                // Second number to be generated below 1000 
                holder[i, 0] = holder[i, 0] + random.Next(1000).ToString().PadLeft(3, '0') + ".";
                // Third number to be generated below 100
                holder[i, 0] = holder[i, 0] + random.Next(100).ToString().PadLeft(2, '0') + ".";
                // Last number to be generated below 10
                holder[i, 0] = holder[i, 0] + random.Next(10).ToString() + " Z";
                // Decider if a point is a planet or a very hungry space monster
                // Changes to be a very hungry space monster is => 1/11 (includes 0 and excludes 11)
                // Marks second column as 0 if it's a planet and 1 if it's a very hungry space monster
                if (random.Next(11) < 1)
                {
                    holder[i, 1] = "1";
                    holder[i, 2] = "0";
                    holder[i, 3] = "0";
                }
                else
                {
                    holder[i, 1] = "0";
                    // Check if planet is habitable or not. Chances to be habitable => 1/3 (includes 0 excludes 3)
                    // If 3rd column is 1, planet is habitable, else not.
                    if (random.Next(3) < 1)
                    {
                        holder[i, 2] = "1";
                        // Generate random surface area for habitable planet from range 1 mil to 100 mil
                        holder[i, 3] = ((int)((random.NextDouble() * 99000000) + 1000000)).ToString();
                    }
                    else
                    {
                        holder[i, 2] = "0";
                        holder[i, 3] = "0";
                    }
                }
                // Calculate the distance from the homeworld using the coordinates
                locations = new string(holder[i,0].Where(c => char.IsDigit(c)).ToArray());
                holder[i, 4] = ( Math.Sqrt(Math.Pow(Convert.ToInt32(locations.Substring(0, 3)) - Convert.ToInt32(HomeWorldCoords.Substring(0, 3)), 2) +
                                            Math.Pow(Convert.ToInt32(locations.Substring(3, 3)) - Convert.ToInt32(HomeWorldCoords.Substring(3, 3)), 2) +
                                            Math.Pow(Convert.ToInt32(locations.Substring(6, 2)) - Convert.ToInt32(HomeWorldCoords.Substring(6, 2)), 2) +
                                            Math.Pow(Convert.ToInt32(locations.Substring(8, 1)) - Convert.ToInt32(HomeWorldCoords.Substring(8, 1)), 2) +
                                            Math.Pow(Convert.ToInt32(locations.Substring(9, 3)) - Convert.ToInt32(HomeWorldCoords.Substring(9, 3)), 2) +
                                            Math.Pow(Convert.ToInt32(locations.Substring(12, 3)) - Convert.ToInt32(HomeWorldCoords.Substring(12, 3)), 2) +
                                            Math.Pow(Convert.ToInt32(locations.Substring(15, 2)) - Convert.ToInt32(HomeWorldCoords.Substring(15, 2)), 2) +
                                            Math.Pow(Convert.ToInt32(locations.Substring(17, 1)) - Convert.ToInt32(HomeWorldCoords.Substring(17, 1)), 2) +
                                            Math.Pow(Convert.ToInt32(locations.Substring(18, 3)) - Convert.ToInt32(HomeWorldCoords.Substring(18, 3)), 2) +
                                            Math.Pow(Convert.ToInt32(locations.Substring(21, 3)) - Convert.ToInt32(HomeWorldCoords.Substring(21, 3)), 2) +
                                            Math.Pow(Convert.ToInt32(locations.Substring(24, 2)) - Convert.ToInt32(HomeWorldCoords.Substring(24, 2)), 2) +
                                            Math.Pow(Convert.ToInt32(locations.Substring(26, 1)) - Convert.ToInt32(HomeWorldCoords.Substring(26, 1)), 2)
                                            )).ToString();

                
            }
            // Add Columns to the DataTable
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Column3", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Column4", typeof(Int32)));
            dt.Columns.Add("Column5", typeof(Double));

            // Populate the DataTable from the array previous used (holder)
            for (var y = 0; y < holder.GetLength(0); ++y)
            {
                DataRow row = dt.NewRow();
                for (var x = 0; x < holder.GetLength(1); ++x)
                {
                    row[x] = holder[y, x];
                    if (x == 5)
                    {
                        row["Column5"] = Convert.ToDouble(holder[y, x]);
                    }
                }
                dt.Rows.Add(row);
            }
            // Sort on the fifth column ascending to get the closest distances first
            DataView dv = dt.DefaultView;
            dv.Sort = "Column5 ASC";
            DataTable dts = dv.ToTable();


            // Writing the results of the 15000 locations, .txt file was chosen because it was indicated in the question
            // Use MyDocuments since it's usuable on any computer with Windows
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Starship.txt")))
            {
                // Include headings for easy to read data
                outputFile.Write("Coordinates Monster Habitable Surface_Area\t Distance_from_Homeworld\n");

                // Output to a text file the details of the Coordinates, Monster, Habitable planet, Surface Area and Distance from Homeworld
                foreach (DataRow row in dts.Rows)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        outputFile.Write(row[j] + "\t");
                    }
                    outputFile.WriteLine();
                }
            }


            return;
        }
        static double[] ReadStepOne(string HomeWorldCoords, double cRate, int travelTime)
        {
            // Get path for MyDocuments and read lines from text files
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            string[] lines = File.ReadAllLines(path + "//" + "Starship.txt");

            // total minutes available
            int totalTimeMins = 24 * 60;
            // Planets inhabited counter
            int planetInhabited = 0;
            double surfaceAreaColonized = 0;
            // get parts of lines seperated by tab
            string[] parts = new string[5];
            // returnable ints
            double[] returnVals = new double[2];

            foreach (string line in lines)
            {
                parts = line.Split('\t');
                // if monster, subtract 30 minutes - 10 minutes to get there and 20 minutes to get to next planet
                if (parts[1] == "1")
                {
                    totalTimeMins -= travelTime * 3;
                }
                // if planet but uninhabitable - 10 minutes to get there and 10 minutes to the next planet
                else if (parts[1] == "0" && parts[2] == "0")
                {
                    totalTimeMins -= travelTime * 2;
                }
                // if planet and habitable - 10 minutes to get there
                // if total seconds available to colonize planet is enough when getting there and at 0.043 km per second
                // and total millions of kilometer surface then add to planet inhabited, else go to next
                else if (parts[1] == "0" && parts[2] == "1")
                {
                    if ((totalTimeMins * 60 * cRate - Convert.ToInt32(parts[3]) / 2) <= 0 && totalTimeMins > 0)
                    {
                        planetInhabited++;
                        // Add surface area colonized to total
                        if(totalTimeMins * cRate * 60 <= Convert.ToInt32(parts[3]))
                        {
                            surfaceAreaColonized += totalTimeMins * cRate * 60;
                        }
                        else
                        {
                            surfaceAreaColonized += Convert.ToInt32(parts[3]);
                        }
                        
                    }
                    else if (totalTimeMins > 0)
                    {
                        // add total covered surface area colonized 
                        surfaceAreaColonized += totalTimeMins * cRate * 60;
                    }
                    totalTimeMins -= travelTime;
                }
            }
            returnVals[0] = planetInhabited;
            returnVals[1] = surfaceAreaColonized;
            return returnVals;
        }
    }
}
