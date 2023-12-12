using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ScottPlot;

namespace METHODS_LAB_10;

public class NumericMethods
{
    static ObservableCollection<DataItem> DataCollection = new();
    static double Function(double x, double y)
    {
        return 0.5 * (x - 1) * Math.Exp(x) * Math.Pow(y, 2) - x * y;
    }
    static double RungeKuttaStep(double x, double y, double h)
    {
        double k1 = h * Function(x, y);
        double k2 = h * Function(x + 0.5 * h, y + 0.5 * k1);
        double k3 = h * Function(x + 0.5 * h, y + 0.5 * k2);
        double k4 = h * Function(x + h, y + k3);

        return y + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
    }
    static double EulerStep(double x, double y, double h)
    {
        return y + h * Function(x, y);
    }
    static double ExactFunction(double x)
    {
        return 2 * Math.Exp(-0.25 * Math.Pow(x, 2) + x);
    }

    
    
    
    public static void RungeKutta(TextBox TBRK, ScottPlot.WpfPlot plot)
    {
        double a = 0;
        double b = 2;
        double y0 = 2;
        double h = 0.1; // начальное предположение для шага

        double tolerance = 1e-5;
        double currentY = 0, newY = 0;

        while (true)
        {
            currentY = y0;

            for (double x = a; x <= b; x += h)
            {
                newY = RungeKuttaStep(x, currentY, h);
                currentY = newY;
            }

            if (Math.Abs(currentY - newY) < tolerance)
                break;

            h /= 2; // уменьшаем шаг вдвое
        }

        TBRK.Text = h.ToString();

        // Теперь строим приближенную интегральную кривую с найденным шагом
        double[] xValues = new double[(int)((b - a) / h) + 1];
        double[] yValues = new double[xValues.Length];

        xValues[0] = a;
        yValues[0] = y0;

        for (int i = 1; i < xValues.Length; i++)
        {
            xValues[i] = xValues[i - 1] + h;
            yValues[i] = RungeKuttaStep(xValues[i - 1], yValues[i - 1], h);
        }

        // Строим график
        plot.Plot.PlotScatter(xValues, yValues, label: "метод Рунге–Кутта");
        plot.Plot.XLabel("x");
        plot.Plot.YLabel("y");
        plot.Plot.Legend();
        plot.Render();
    }
    
    
    
    public static void Euler(TextBox TBE, ScottPlot.WpfPlot plot)
    {
        double a = 0;
        double b = 2;
        double y0 = 2;
        double h = 0.1; // начальное предположение для шага

        double tolerance = 1e-5;
        double currentY = 0, newY = 0;

        while (true)
        {
            currentY = y0;

            for (double x = a; x <= b; x += h)
            {
                newY = EulerStep(x, currentY, h);
                currentY = newY;
            }

            if (Math.Abs(currentY - newY) < tolerance)
                break;

            h /= 2; // уменьшаем шаг вдвое
        }

        TBE.Text = h.ToString();
        
        // Теперь строим график метода Эйлера
        double[] xValues = new double[(int)((b - a) / h) + 1];
        double[] yValues = new double[xValues.Length];

        xValues[0] = a;
        yValues[0] = y0;

        for (int i = 1; i < xValues.Length; i++)
        {
            xValues[i] = xValues[i - 1] + h;
            yValues[i] = EulerStep(xValues[i - 1], yValues[i - 1], h);
        }

        // Строим график
        plot.Plot.PlotScatter(xValues, yValues, label: "метод Эйлера");
        plot.Plot.XLabel("x");
        plot.Plot.YLabel("y");
        plot.Plot.Legend();
        plot.Render();
    }
    
    
    
    public static void Exact(TextBox TBMDRK, TextBox TBMDE, ScottPlot.WpfPlot plot, DataGrid dataGrid)
    {
        double a = 0;
        double b = 2;
        double y0 = 2;
        double h = 0.1;

        // Строим график точного решения
        double[] xExact = new double[(int)((b - a) / h) + 1];
        double[] yExact = new double[xExact.Length];

        for (int i = 0; i < xExact.Length; i++)
        {
            xExact[i] = a + i * h;
            yExact[i] = ExactFunction(xExact[i]);
        }
        
        
        // Вычисляем значения приближенного решения (метод Рунге–Кутта)
        double[] xValues1 = new double[(int)((b - a) / h) + 1];
        double[] yApproximate = new double[xValues1.Length];

        xValues1[0] = a;
        yApproximate[0] = 2;

        for (int i = 1; i < xValues1.Length; i++)
        {
            xValues1[i] = xValues1[i - 1] + h;
            yApproximate[i] = RungeKuttaStep(xValues1[i - 1], yApproximate[i - 1], h);
        }

        
        // Находим максимальное отклонение
        double maxDeviation1 = 0;

        for (int i = 0; i < xValues1.Length; i++)
        {
            double deviation = Math.Abs(yApproximate[i] - yExact[i]);
            maxDeviation1 = Math.Max(maxDeviation1, deviation);
        }

        
        // Вычисляем значения приближенного решения (метод Эйлера)
        double[] xValues = new double[(int)((b - a) / h) + 1];
        double[] yValues = new double[xValues.Length];

        xValues[0] = a;
        yValues[0] = y0;

        for (int i = 1; i < xValues.Length; i++)
        {
            xValues[i] = xValues[i - 1] + h;
            yValues[i] = EulerStep(xValues[i - 1], yValues[i - 1], h);
        }
        
        
        // Находим максимальное отклонение
        double maxDeviation2 = 0;

        for (int i = 0; i < xValues.Length; i++)
        {
            double deviation = Math.Abs(yValues[i] - yExact[i]);
            maxDeviation2 = Math.Max(maxDeviation2, deviation);
        }
        
        TBMDRK.Text = maxDeviation1.ToString();
        TBMDE.Text = maxDeviation2.ToString();
        
        plot.Plot.PlotScatter(xExact, yExact, label: "Точное решение задачи Коши");
        plot.Plot.XLabel("x");
        plot.Plot.YLabel("y");
        plot.Plot.Legend();
        plot.Plot.Render();
        
        
        for (int i = 0; i < yValues.Length; i++)
        {
            DataItem item = new DataItem
            {
                RungeKuttaValue = yApproximate[i],
                EulerValue = yValues[i],
                ExactValue = yExact[i]
            };

            DataCollection.Add(item);
        }
        dataGrid.ItemsSource = DataCollection;
    }
}