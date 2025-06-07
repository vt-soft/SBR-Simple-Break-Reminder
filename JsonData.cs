using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBR
{
    // Classes neccesary to store data in  JSON file
    // Data for config (alarm) file:


    public class Day
    {
        public string DayDate { get; set; }
        public int DayTotalTime { get; set; } // sec
        public int DayIdleTime { get; set; } // sec
        public int DayWorkingTime { get; set; }  // sec
        public int DayIgnoredBreaks { get; set; }
    }

    public class Month
    {
        public string MonthDate { get; set; }
        public int MonthTotalTime { get; set; } // sec
        public int MonthIdleTime { get; set; } // sec
        public int MonthWorkingTime { get; set; }  // sec
        public float MonthIgnoredBreaks { get; set; } // must be float here, because it is average value
    }


    public class DataConfig
    {
        public string LanguageCode { get; set; }
        public bool DarkMode { get; set; }
        public int AlarmTime1 { get; set; }  // Alarm time in minutes:
        public int AlarmTime2 { get; set; }
        public int AlarmTime3 { get; set; }
        public int AlarmTime4 { get; set; }
        public int SelectedAlarm { get; set; } // 1 - Aalarm1, 2 - Alarm2, 3 - Alarm3, 4 - Alarm4    
        public bool StartUp { get; set; }
        public bool PlaySound { get; set; }
        public string PlaySoundRButton { get; set; }
        public bool Emoticons { get; set; }
        public bool Pomodoro { get; set; }
        public bool PomodoroLongBreak { get; set; }
        public string SelectedColor { get; set; }   // There were troubles to deserialize Color type, so I have used string instead. 
        public string SelectedRadioButton { get; set; }
        public string CustomColor { get; set; }     // Our own color. 
        public bool HidePriorityColumn { get; set; } // Hide priority, status and tag column in the ToDo list
        public List<Day> Days { get; set; }
        public List<Month> Months { get; set; }
    }


    // Data for ToDo list file:

    public class ToDoTask
    {
        public string TaskId { get; set; }
        public string TaskDueDate { get; set; }
        public string TaskPriority { get; set; }
        public bool TaskArchived { get; set; } // true - task is archived, false - task is not archived
        public char TaskType { get; set; }  // N - normal, R - recurring
        public int TaskRecurrenceType { get; set; } // 0 - Not recurring task, 1 - daily, 2 - weekly, 3 - monthly
        public int TaskRecurrenceTime { get; set; } //  in days/weeks/moths based on TaskRecurrence
        public int TaskRecurrenceReminderTime { get; set; } // Remind (show in the ongoings tasks) in X day before task due date.
        public bool TaskRecurrenceShow { get; set; } // Show in the ongoing tasks
        public string TaskName { get; set; }
        public string TaskStatus { get; set; } // like InPorgress, 50% done, it is up to user how to  fill in this field
        public string TaskTag { get; set; }
        public string TaskNote { get; set; }
    }

    public class DataToDoList
    {
        public List<ToDoTask> ToDoTasks { get; set; }
    }

}
