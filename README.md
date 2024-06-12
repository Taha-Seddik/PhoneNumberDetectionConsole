# Intro 
This project is Phone Number Detection Console Application.

# Environment 
- .NET 8
- C#
- XUnit

# How to run 
- It can be run in debug mode so it shows this 

![image](https://github.com/Taha-Seddik/PhoneNumberDetection/assets/16271638/ceb198b0-e612-486e-a7fa-9f90a18828e5)

- Or with arguments by running this command : 

dotnet run --arg1 "./sampleInputText.txt"

( I prepared a testing file sampleInputText.txt )

![image](https://github.com/Taha-Seddik/PhoneNumberDetection/assets/16271638/e3e00716-2164-4dd1-aeb3-632714d78052)


# Solution projects 
- TechnicalTest : 
  - Extensions Folder : Contains extension to display enum type
  - Models: Where I did put main class and enums 
  - Services: Contains PhoneNumberParserService that handles parsing phone numbers
  - Utils: Contains Utility functions 
  - App.cs Where I did put the main logic
  - Program.cs : entry point where I did inject PhoneNumberParserService
- TechnicalTest.Test: Contains Unit tests  
