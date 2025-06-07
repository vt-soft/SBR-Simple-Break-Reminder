using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace SBR
{

    // Methods for saving and loading data from json files.    
    public static class JsonMethods
    {
        
        private static string FileNameConfig; // Path to the config (+Alarm) file
        private static DataConfig? cData;

        private static string FileNameTasks;  // Path to the TODO tasks file
        private static DataToDoList? tData;


        static JsonMethods()
        {

            // We are storing config.json tasks.json file in the LocalApplicationData folder. This is a good place for storing user-specific data.

            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SBR_vtsoft");

            // Check if the directory (for the config file) exists, if not, create it.
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath); 
            }

            FileNameConfig = Path.Combine(appDataPath, "config.json");
            FileNameTasks  = Path.Combine(appDataPath, "tasks.json");
        }


        // *****************************************************************************
        // Methods for config file (Alarm) - DataConfig class:

        /// <summary>
        /// FileSave method will convert DataConfig object to a json file and will save it to your hard drive. This method is called every 10 minutes.
        /// </summary>
        public static void FileSaveConfig(DataConfig cData)
        {
            UpdateMonthlyChartData(cData);

            // This code will ensure that json file is user-friendly formatted.
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;

            // Serialize cData object to a json string.
            var jsonContentConfig = JsonSerializer.Serialize(cData, options);
       
            // The boolean value in the StreamWriter constructor is set to false, so the file will be overwritten.
            using (var writer = new StreamWriter(FileNameConfig, false))
            {
                writer.Write(jsonContentConfig);
            }
        }


        /// <summary>
        /// UpdateMonthlyChartData method will update the monthly data in the chart. 
        /// It will calculate the average values for the last 31 days, but only in current month.
        /// But only only if there are at least 3 working days in the last 31 days
        /// </summary>
        /// <param name="cData"></param>
        public static void UpdateMonthlyChartData(DataConfig cData)
        {
            // For the Statistics form/chart.

            int totalTime = 0;
            int idleTime = 0;
            int workingTime = 0;
            float ignoredBreaks = 0;
            int numberOfWorkingDays = 0;

            // Counting the number of working days in the last 31 days, but only in current month.
            foreach (var day in cData.Days)
            {
                // Parse the day date to a DateTime object
                DateTime dayDate = DateTime.ParseExact(day.DayDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                if (dayDate.Year == DateTime.Today.Year && dayDate.Month == DateTime.Today.Month)
                {
                    if (day.DayTotalTime!=0) // Skipping days with zero Total time
                    {
                        numberOfWorkingDays++;
                    }
                }
            }

            // We are updating monthly data in the chart only if there are at least 3 working days in the last 31 days, but only in current month.
            if (numberOfWorkingDays >= 3)
            {
                foreach (var day in cData.Days)
                {
                    // Parse the day date to a DateTime object
                    DateTime dayDate = DateTime.ParseExact(day.DayDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    if (dayDate.Year == DateTime.Today.Year && dayDate.Month == DateTime.Today.Month)
                    {
                        if (day.DayTotalTime!=0) // Skipping days with zero Total time
                        {
                            totalTime += day.DayTotalTime;
                            idleTime += day.DayIdleTime;
                            workingTime += day.DayWorkingTime;
                            ignoredBreaks += day.DayIgnoredBreaks;
                        }
                    }
                }
            }

            // Now we calculate the averages.
            if (numberOfWorkingDays >= 3)
            {
                totalTime /= numberOfWorkingDays;
                idleTime /= numberOfWorkingDays;
                workingTime /= numberOfWorkingDays;
                ignoredBreaks /= numberOfWorkingDays;

                // And now we save them into cData object.
                cData.Months[0].MonthTotalTime = totalTime;
                cData.Months[0].MonthIdleTime = idleTime;
                cData.Months[0].MonthWorkingTime = workingTime;
                cData.Months[0].MonthIgnoredBreaks = ignoredBreaks;
            }
        }

        /// <summary>
        /// FileLoad method will read the json file from your hard drive and will convert it to a DataConfig object.
        /// </summary>
        public static DataConfig FileLoadConfig()
        {
            if (File.Exists(FileNameConfig))
            {
                using (var reader = new StreamReader(FileNameConfig))
                {
                    // Read the json file and convert it to a string.
                    string jsonContent = reader.ReadToEnd();

                    // Deserialize the json string to a DataConfig object.
                    cData = JsonSerializer.Deserialize<DataConfig>(jsonContent);                
                }
            }
            else // File not exists. At least we will prepare default data for writing into this file.
            {
                cData = DefaultJsonDataConfig();    
            }


            // Shift data if today is not equal to the last day from the config file.
            if (DateTime.Today.ToString("yyyy-MM-dd")!= cData.Days[0].DayDate)
            {
                ShiftDataDays();
            }

            // Shift data if today's month is not equal to the last month from the config file.
            if (DateTime.Today.ToString("yyyy-MM")!= cData.Months[0].MonthDate)
            {
                ShiftDataMonths();
            }

            return cData;
        }


        /// <summary>
        /// DefaultJsonData method will prepare default data for the json file.
        /// </summary>
        /// <returns></returns>
        private static DataConfig DefaultJsonDataConfig()
        {
            // In case config.json file is not found (not exists), we will prepare default data for new file.
            var defaultData = new DataConfig();
          
            defaultData.LanguageCode = "en-gb";
            defaultData.DarkMode = true;

            defaultData.AlarmTime1 = 30;
            defaultData.AlarmTime2 = 0;
            defaultData.AlarmTime3 = 0;
            defaultData.AlarmTime4 = 0;
            defaultData.SelectedAlarm = 1;

            defaultData.StartUp = true;
            defaultData.PlaySound = true;
            defaultData.PlaySoundRButton = "rdoS1";
            defaultData.Emoticons = true;
            defaultData.Pomodoro = false;
            defaultData.PomodoroLongBreak = false;

            defaultData.SelectedColor = ColorTranslator.ToHtml(Color.Orange);
            defaultData.SelectedRadioButton = "rdoOrange";
            defaultData.CustomColor = "#0080C0";
            defaultData.HidePriorityColumn = false;   // Hide priority, status and tag column in the ToDo list

            // Initialize daily data for the last 31 days.
            defaultData.Days = new List<Day>(); 
            for (int i = 0; i >= -30; i--)  
            {
                defaultData.Days.Add(new Day
                {
                    DayDate = DateTime.Today.AddDays(i).ToString("yyyy-MM-dd"), // 0 = today, -1 = yesterday, -2 = day before yesterday, etc.
                    DayTotalTime = 0,
                    DayIdleTime = 0,
                    DayIgnoredBreaks = 0
                });
            }

            // Initialize monthly data for the last 14 months.
            defaultData.Months = new List<Month>();
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddMonths(-13); 
          
            for (DateTime date = startDate; date >= endDate; date = date.AddMonths(-1))
            {
                defaultData.Months.Add(new Month
                {
                    MonthDate = date.ToString("yyyy-MM"),
                    MonthTotalTime = 0,
                    MonthIdleTime = 0,
                    MonthWorkingTime = 0,
                    MonthIgnoredBreaks = 0
                });
            }

            return defaultData;
        }


        /// <summary>
        /// Method will shift cData in the Days list by the number of days between today and the date of the first day in the list.
        /// </summary>
        private static void ShiftDataDays()
        {
            // This parse method should be safe, no matter what culture is set on the computer. 
            int diffDays = (DateTime.Today - DateTime.ParseExact(cData.Days[0].DayDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)).Days;

            if (diffDays>31)
                diffDays=31;

         
            if (diffDays <=0) // just in case :)
            {
                MessageBox.Show("Chaos with dates in config file. Program will be terminated. Fix or delete the config file.");
                Environment.Exit(0);
            }

            // Shift data in Days list by the diffDays value.
            for (int i = cData.Days.Count-1; i>=0; i--)
            {
                if (i - diffDays >= 0)
                {
                    cData.Days[i].DayDate = cData.Days[i - diffDays].DayDate;
                    cData.Days[i].DayTotalTime = cData.Days[i - diffDays].DayTotalTime;
                    cData.Days[i].DayIdleTime = cData.Days[i - diffDays].DayIdleTime;
                    cData.Days[i].DayWorkingTime = cData.Days[i - diffDays].DayWorkingTime;
                    cData.Days[i].DayIgnoredBreaks = cData.Days[i - diffDays].DayIgnoredBreaks;
                }
            }
            // Now we need to update (set to 0) the first diffDays days in the list (which were not shifted in previous cycle).
            for (int i = 0; i < diffDays; i++)
            {
                cData.Days[i].DayDate = DateTime.Today.AddDays(-i).ToString("yyyy-MM-dd");
                cData.Days[i].DayTotalTime = 0;
                cData.Days[i].DayIdleTime = 0;
                cData.Days[i].DayWorkingTime = 0;
                cData.Days[i].DayIgnoredBreaks = 0;
            }
        }


        /// <summary>
        /// Method will shift cData in the Months list by the number of months between this month and the first month in the list.
        /// </summary>
        private static void ShiftDataMonths()
        {
            // Calculate the difference in months between today and the first month in the list
            int diffMonths = ((DateTime.Today.Year - DateTime.ParseExact(cData.Months[0].MonthDate, "yyyy-MM", CultureInfo.InvariantCulture).Year) * 12) +
                              (DateTime.Today.Month - DateTime.ParseExact(cData.Months[0].MonthDate, "yyyy-MM", CultureInfo.InvariantCulture).Month);

            if (diffMonths > 14)
                diffMonths = 14;

            if (diffMonths <= 0) // Just in case :)
            {
                MessageBox.Show("Chaos with dates in config file. Program will be terminated. Fix or delete the config file.");
                Environment.Exit(0);
            }

            // Shift data in Months list by the diffMonths value
            for (int i = cData.Months.Count - 1; i >= 0; i--)
            {
                if (i - diffMonths >= 0)
                {
                    cData.Months[i].MonthDate = cData.Months[i - diffMonths].MonthDate;
                    cData.Months[i].MonthTotalTime = cData.Months[i - diffMonths].MonthTotalTime;
                    cData.Months[i].MonthIdleTime = cData.Months[i - diffMonths].MonthIdleTime;
                    cData.Months[i].MonthWorkingTime = cData.Months[i - diffMonths].MonthWorkingTime;
                    cData.Months[i].MonthIgnoredBreaks = cData.Months[i - diffMonths].MonthIgnoredBreaks;
                }
            }

            // Update the first diffMonths months in the list (which were not shifted in the previous cycle)
            for (int i = 0; i < diffMonths; i++)
            {
                cData.Months[i].MonthDate = DateTime.Today.AddMonths(-i).ToString("yyyy-MM");
                cData.Months[i].MonthTotalTime = 0;
                cData.Months[i].MonthIdleTime = 0;
                cData.Months[i].MonthWorkingTime = 0;
                cData.Months[i].MonthIgnoredBreaks = 0;
            }
        }



        // *****************************************************************************
        // Methods for TODO tasks file  - DataToDoList class:



        /// <summary>
        /// FileSave method will convert DataToDoLis object to a json file and will save it to your hard drive. 
        /// This method is called only if any task was added, edited or finished.
        /// </summary>
        public static void FileSaveTasks(DataToDoList tData)
        {
           
            // This code will ensure that json file is user-friendly formatted.
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;

            // Serialize cData object to a json string.
            var jsonContentTasks = JsonSerializer.Serialize(tData, options);

            // The boolean value in the StreamWriter constructor is set to false, so the file will be overwritten.
            using (var writer = new StreamWriter(FileNameTasks, false))
            {
                writer.Write(jsonContentTasks);
            }
        }


        /// <summary>
        /// FileLoad method will read the json file from your hard drive and will convert it to a DataToDoList object.
        /// </summary>
        public static DataToDoList FileLoadTasks()
        {
            if (File.Exists(FileNameTasks))
            {
                using (var reader = new StreamReader(FileNameTasks))
                {
                    // Read the json file and convert it to a string.
                    string jsonContentTasks = reader.ReadToEnd();

                    // Deserialize the json string to a DataConfig object.
                    tData = JsonSerializer.Deserialize<DataToDoList>(jsonContentTasks);
                }
            }
            else // File not exists. At least we will prepare new empty object which we will later save to this file.
            {
                tData = new DataToDoList();
                tData.ToDoTasks = new List<ToDoTask>();
            }

            return tData;
        }





    }
}
