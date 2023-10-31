# LemonadeStandProject
A game where you can sell lemonade to customers to try to gain as much money as possible over the course of 7 days

1  Remove object from inventory     Solid      Inventory.cs lines: 40-97
    In this part of my code, I utilized the Single Responsibility Principle to set up seperate methods to remove different items from the inventory so that when that was needed in different parts of the code it was simple to call each method for a specific purpose instead of requiring an input or comparison to figure out what was trying to be called.

2  Weather Types                    soLid      WeatherTypes.cs, Cloudy.cs, PartlyCloudy.cs, Sunny.cs, Rainy.cs
    Here I used the Liskov Substitution Princliple to set up my weather types as different classes that were easily swappable within my code so that each day could randomize a new weather type and easily change between each class.

    