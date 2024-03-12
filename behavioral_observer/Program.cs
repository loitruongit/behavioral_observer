using System;
using System.Collections.Generic;

// Subject: Object being observed in the stock market context
public class StockMarket
{
    private List<IObserver> investors = new List<IObserver>();
    private decimal stockPrice;

    public decimal StockPrice
    {
        get { return stockPrice; }
        set
        {
            if (stockPrice != value)
            {
                stockPrice = value;
                NotifyInvestors();
            }
        }
    }

    public void RegisterInvestor(IObserver investor)
    {
        investors.Add(investor);
    }

    public void UnregisterInvestor(IObserver investor)
    {
        investors.Remove(investor);
    }

    private void NotifyInvestors()
    {
        foreach (var investor in investors)
        {
            investor.Update(this);
        }
    }
}

// Observer: Interface for investors
public interface IObserver
{
    void Update(StockMarket stockMarket);
}

// ConcreteObserver: Specific investor
public class Investor : IObserver
{
    private string name;

    public Investor(string name)
    {
        this.name = name;
    }

    public void Update(StockMarket stockMarket)
    {
        Console.WriteLine($"{name} received notification: Stock price is now {stockMarket.StockPrice}");
    }
}

class Program
{
    static void Main()
    {
        StockMarket stockMarket = new StockMarket();

        Investor investor1 = new Investor("Investor 1");
        Investor investor2 = new Investor("Investor 2");

        stockMarket.RegisterInvestor(investor1);
        stockMarket.RegisterInvestor(investor2);

        // Change in stock price, notifying all investors
        stockMarket.StockPrice = 150.50m;

        // Output:
        // Investor 1 received notification: Stock price is now 150.50
        // Investor 2 received notification: Stock price is now 150.50
    }
}
