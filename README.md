# Car Rental System API
A REST API for a car rental system developed with ASP.NET Core framework.

The API supports the following operations:

1. Create/register new users of type User or Admin
2. Login users with JWT token
3. Manage available cars
4. Rent cars
5. Email notification on renting car (using SendGrid)
6. Secured endpoints for Admin-Only access

The project uses Microsoft SQL Server with Entity Core Framework for data storage.

## Models
The project consits of the following models:

1. **Car**: Represents a single car in the database
2. **CarRental**: Represents a single car rental in the database
3. **User**: Represents a single user in the system
4. **TransactionLog**: Represents a transaction log entry

Additionally, the following models are used for taking inputs from request body (Data Transfer Objects):

1. CarDTO: For taking input of car details
2. CarRentalDTO: For taking input of car rental details

### Car Model
The car model consists of the following properties:

|Property|Description|Type|Validation|
|---|---|---|---|
|Id|Unique identifier of the car|int|Required|
|Manufacturer|Manufacturing company|string|Required|
|Model|Model of the car|string|Required|
|Year|Year of RTM|int|Required|
|PricePerDay|Price per day for rent|decimal|Required, Precision (6,2), and in Range(500.00, 5000.00)|
|IsAvailable|Whether the car is available for rent|boolean|Required|

**Note**: The validation for `Year` is done in the controller instead of the model to dynamically check if the year is before or the current year. The check may also done in a validation attribute.


