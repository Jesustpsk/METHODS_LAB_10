using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace METHODS_LAB_10;

public class DataItem : INotifyPropertyChanged
{
    private double _rungeKuttaValue;
    public double RungeKuttaValue
    {
        get { return _rungeKuttaValue; }
        set
        {
            if (_rungeKuttaValue != value)
            {
                _rungeKuttaValue = value;
                OnPropertyChanged(nameof(RungeKuttaValue));
            }
        }
    }

    private double _eulerValue;
    public double EulerValue
    {
        get { return _eulerValue; }
        set
        {
            if (_eulerValue != value)
            {
                _eulerValue = value;
                OnPropertyChanged(nameof(EulerValue));
            }
        }
    }

    private double _exactValue;
    public double ExactValue
    {
        get { return _exactValue; }
        set
        {
            if (_exactValue != value)
            {
                _exactValue = value;
                OnPropertyChanged(nameof(ExactValue));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}