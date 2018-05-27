using ServiceGUI.DataStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;


/// <summary>
/// an interface for the log model.
/// </summary>
public interface ILogModel : INotifyPropertyChanged
{
    ObservableCollection<LogLine> Logs 
    {
        get; set;
    }
}

