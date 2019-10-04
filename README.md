# Rentabike
## Requirements:
###### A company rents bikes under following options: 
###### 1. Rental by hour, charging $5 per hour 
###### 2. Rental by day, charging $20 a day 
###### 3. Rental by week, changing $60 a week 
###### 4. Family Rental, is a promotion that can include from 3 to 5 Rentals (of any type) with a discount 
###### of 30% of the total price 
###### 5. The values could change in the future if the client decides at any time 
###### 6: Implement a persistence model for the rents information 
###### 7. Implement a kind of message queue model to send and receive the information of the rents that the user creates 
###### 8. At the moment of the project, no providers for data persistance nor message exchange have been defined 

## Solution Design:
![alt text](https://github.com/pbravi/rentabike/blob/master/rentabike.png)
#### Solution is separated in 4 layers:
###### rentabike.model -> Contains model classes for manage Rentals
###### rentabike.model.strategies -> Contains Strategy model classes for manage Price calculation and Rental composite
###### rentabike.services -> Contains Services to help with Rental Building, Strategy use and Async Messages Processing
###### rentabike.data -> Contains a persitance implementation with Entity Framework
###### rentabike.test -> Contains unit test classes

## Development Practices:
#### Design Patterns used:
###### Composite	(Structural)
###### Strategy		(Behavioral)
###### Builder		(Creational)

## Unit Testing:
###### Unit tests were performed with MSTEST
###### For Unit Test Coverage Calculation was used DotCover(ReSharper) - Coverage: 89%
#### How to run tests:
###### 1.Open Test Explorer (Test->Windows->Test Explorer)
###### 2.Run all tests inside rentabike.test