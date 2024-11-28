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

## Endpoints
### Users
#### Registering A New User
To register a new user, send a POST request to the endpoint `/api/user/register`. The request body must contain the following fields:

- name: `string` - The name of the user
- email: `string` - The email ID of the user (must be in valid email format)
- password: `string` - Password of the user. The password must satisfy the following requirements:
	- Must be at least 6 characters long
	- Must contain at least one special character
	- Must contain at least one lower case alphabet
	- Must contain at least one upper case alphabet
	- Must contain at least one digit
- role: `string` - User's role. Can be either `User` or `Admin`

Sample request:
```
{
    "name": "Karthik",
    "email": "karthik@gmail.com",
    "password": "Karthik123#",
    "role": "User"
}
```


**Response**: The created user object with the fields:
- `id`: Unique ID of the user in the database
- `name`: Name of the user
- `email`: User's email address 
- `password`: The hash of user's password as a hexadecimal string
- `role`: User's role

Sample response:
```
{
    "id": 13,
    "name": "Karthik",
    "email": "karthik@gmail.com",
    "password": "B0735682572C91A19B749F33FC22B26CCFADB05234194EA93FF8E4A2800F6AF4",
    "role": "User"
}
```

#### Getting All Users
To get all the users in the system, send a `GET` request to the endpoint `/api/user`. **You must have `Admin` role to access the endpoint**. On accessing the endpoint without a JWT token or with a `User` role, the `401` status code is returned.

**Response**: Array of users with `id`, `name`, `email`, `password` (hashed) and `role` fields.

Sample response:

```
[
    {
        "id": 1,
        "name": "John",
        "email": "john@gmail.com",
        "password": "B4B597C714A8F49103DA4DAB0266AF0EE0AE4F8575250A84855C3D76941CD422",
        "role": "Admin"
    },
    {
        "id": 2,
        "name": "Peter",
        "email": "peter@gmail.com",
        "password": "FD82F0E95C8034CFEACD4FB4D2853D50749364F1C98F780158AA3196FED7D0D7",
        "role": ""
    },
    {
        "id": 3,
        "name": "Sarah",
        "email": "sarah@gmail.com",
        "password": "857AAC1DFEAE126D742174B899C3582ECB9FEE8EBF11DCE7BB46B035312A4148",
        "role": "User"
    }
]
```

#### Authentication
To sign in a user, send a `POST` request to the endpoint `/api/auth/login` with the following parameters:

- email: `string` - The email address used while registering the user
- password: `string`- The password used while registering the user

**Response**: A single `string` representing the JWT token that can be passed in requests to authenticate the user. The token will be valid for 60 minutes. 

### Cars
#### Get All Cars
To get the list of all cars in the database, send a `GET` request to the endpoint `/api/cars/`. 

**NOTE**: You must be authenticated to access the endpoint. That is, the request must contain a valid JWT token.

**Response**: Array of all cars in the database where each car object has the following properties:

- id: `int`: The unique ID of the car
- manufacturer: `string`: Car manufacturing company
- model: `string`: Model of the car
- year: `int`: Year of Release-to-Manufacturing (RTM)
- pricePerDay: `decimal`: Price of rent per day
- isAvailable: `boolean`: If the car is avilable to rent

Sample response:
```
[
    {
        "id": 1,
        "manufacturer": "Tesla",
        "model": "Model S",
        "year": 2012,
        "pricePerDay": 3400.00,
        "isAvailable": true
    },
    {
        "id": 2,
        "manufacturer": "Audi",
        "model": "A4",
        "year": 1994,
        "pricePerDay": 3200.00,
        "isAvailable": false
    },
]
```

#### Get a car by its ID
To get a specific car, send a `GET` request to the endpoint `api/cars/{id}`, and replace `{id}` with the `id` of the car.

**Response**: An object with the properties of the car:

- id: `int`: The unique ID of the car
- manufacturer: `string`: Car manufacturing company
- model: `string`: Model of the car
- year: `int`: Year of Release-to-Manufacturing (RTM)
- pricePerDay: `decimal`: Price of rent per day
- isAvailable: `boolean`: If the car is avilable to rent

Sample response: Sending a request to `/api/cars/3` gets the following response (from the sample data): 

```
{
    "id": 3,
    "manufacturer": "Hyundai",
    "model": "Elantra",
    "year": 1990,
    "pricePerDay": 5000.00,
    "isAvailable": true
}
```

#### Add a car
To add a car, send a `POST` request to the endpoint `/api/cars` with the body containing the following parameters in JSON:

- manufacturer: `string`: Car manufacturing company
- model: `string`: Model of the car
- year: `int`: Year of Release-to-Manufacturing (RTM)
- pricePerDay: `decimal`: Price of rent per day
- isAvailable: `boolean`: If the car is avilable to rent

The `id` parameter is not required and is automatically generated.

#### Updating a car
To update the properties of a car, send a `PUT` request to the endpoint `/api/cars/{id}`, replacing `{id}` with the `id` of the car. In the request body, specify the properties to be updated. 

For example, the following request to `/api/cars/2` updates the availability to false and price per day to 3200:

```
{
    "isAvailable": true,
    "pricePerDay": 3200.00
}
```

**Response**: Updated car object
```
{
    "id": 2,
    "manufacturer": "Audi",
    "model": "A4",
    "year": 1994,
    "pricePerDay": 3200.00,
    "isAvailable": true
}
```

### Car Rentals
#### Get all rentals
To get list of all rentals, send a `GET` request to the endpoint `/api/rentals`. 

**NOTE**: You must have `Admin` role to access the endpoint.

**Response**: Array of rentals

Sample response:
```
[
    {
        "id": 1,
        "userId": 13,
        "carId": 2,
        "rentedOn": "2024-11-25",
        "rentedTill": "2024-11-28",
        "days": 3,
        "amount": 9600.00
    }
]
```


#### Add a rental
To add a rental, send a `POST` request to the endpoint `api/rentals/` with the request body containing the following parameters:

- UserId: `int` - The `id` of the user renting the car
- CarId: `int` - The `id` of the car to rent
- RentedOn: `date` - The date of rental in the format YYYY-MM-DD
- Days: `int` - The number of days for which the car is rented (in the range 1 to 14)

**NOTE**: You must be authenticated to access this endpoint. The id of the user accessing the endpoint (obtained through JWT token) must be same as the id `UserId` parameter in the request.

Sample request:
```
{
    "UserId": 13,
    "CarId": 2,
    "RentedOn": "2024-11-25",
    "days": 3
}
```

**Response**: The created car rental object, containing the following properties: 

- id: `int` - The  unique identifer of the rental
- UserId: `int` - The `id` of the user renting the car
- CarId: `int` - The `id` of the car to rent
- RentedOn: `date` - The date of rental in the format YYYY-MM-DD
- Days: `int` - The number of days for which the car is rented (in the range 1 to 14)
- RentalTill: `date` - The date till which rental is valid (automatically calculated)
- amount: `decimal` - The total amount to be paid for the rental

Sample response:
```
{
    "id": 1,
    "userId": 13,
    "carId": 2,
    "rentedOn": "2024-11-25",
    "rentedTill": "2024-11-28",
    "days": 3,
    "amount": 9600.00
}
```

On successful car rental, an email is sent to the email address of the user that has rented the car. SendGrid email service is used for sending the mails. 

**NOTE**: Outlook inboxes often block SendGrid mails, hence it is recommended to use other email clients for registering the users.

### Transactions Logging
All requests to the endpoints (excluding those that are blocked due to authentication failure) are logged in the database using logging filters.

## Running Locally
To run the API locally, clone this repository or download the zip file and extract to a folder. 
1. Open Visual Studio
2. If not already installed, install   the **ASP.NET and web development** workload from Visual Studio Installer
3. In Visual Studio, click on **Open a project or solution** and select the `CarRentalSystem.sln` file
4. Rename the `.env.sample` file to `.env`. This file holds the environemnt variables. Fill your own values in the fields and save the file.
5. Create database migration using the command `Add-Migration <migration name>` in the NuGet Package Manager.
6. Update the database using the command `Update-Database`
5. Click on the **Start** button to run the program.

**NOTE**: All the fields in the `.env` must be filled with valid values (including SendGrid API key) for the application to run properly.