# CBookingProject
Web platform for the administration of a hotel complex and web api for the integration of the hotel's seasonal offers in any mobile or web manager.

# Content

This Project has 3 projects in the same solution which I detail below:

CBookingProject.WebAdmin: Web project that allows hotel administrators to enter information about their facilities, information such as Types of rooms, Rooms, Availability of rooms and prices are just some of the features that you can find in this project.

CBookingProject.Data: Shared project to handle the Database entities, this project is shared between the web administration and the API, its responsibility is to store the entities, datacontext and the migration files necessary to restore the database.

CBookingProject.API: API for the user's clients, in this project you will find the communication apis to show the offers, rooms and availabilities to the end users, so that they can create a reservation, modify it, cancel it or verify the reservation. hotel availability.

See image for project reference.

![image](https://user-images.githubusercontent.com/78773393/138623447-c3e83eba-2548-4e3a-98f5-acdda8bae3f4.png)

# 🚀 Installation

1. Clone this project.
2. Go to the API and WebAdmin projects
3. In these projects you must modify the connection string to the database and set it to point to your own database manager.
4. It is not necessary to carry out migrations, the project already has a seed file, in charge of creating the DB when executing the WebAdmin project.
5. In case this file fails, the following commands should be run in the package manager console:

  •	Add-migration AddTables.
  •	Update-database.
  
# 🦀 Database Diagram

![image](https://user-images.githubusercontent.com/78773393/138623628-1c628d36-de7e-4f32-ab37-b45ccb2a6887.png)

# 🧾 License

The MIT License (MIT)

# 🧾 Application operation

When executing the project the first time, the seed file will create the hotel of the application, in addition to this, the reservation status will also be created.

The administrator has the task of entering the information to be handled in the application in the following order:

Type of room: types of rooms to be managed within the complete hotel.

Rooms: Rooms available at the hotel, in this window the information of the room is entered, the type of room is assigned, the maximum number of people allowed in the room and the number of rooms available is specified, for example:

{
RoomName: Deluxe Suite,
RoomType: Simple rooms on the first floor.
Number of People per room: 2 people
Number of available rooms of this type: range (1-10000).
Room Description: This is an ocean view room.
}

Room availability: Room availability maintenance allows administrators to manage the seasons in which a room type x is offered to the public, example:

The hotel determined that all Deluxe suites in the hotel will be offered for reservations from November 1, 2021 to December 15, 2021.

Adding to this information the administrator can configure the information of maximum days allowed per guest, minimum days required to make a reservation and maximum days in which the reservation can be made.

Information example:

Room availability
{
Description: Fall 2021
Applicable to the type of room: Simple rooms (in our example the Deluxe rooms are classified with this type of room).
Date from and Date to: start and end dates of this season where those rooms will be on offer.
Minimum days required for a reservation: whole value that allows you to control how many days in advance the system will accept reservations in this season.
Maximum days required for a reservation: integer value that allows controlling how many days exist between the day of the reservation and the current day example: If we set this value as 15 it means that users will only be able to reserve 15 days or less before their accommodation
Maximum number of days allowed: Maximum number of days that a user can reserve.
}

Prices by availability: in this window the user will be able to control the prices by seasons, that is, they will be able to place a price on the Deluxe rooms for the season created previously Fall 2021.

# 🧾 API performance:

The api implements the Swagger tool which allows you to view all the methods available in the API and the parameters required for each one.
Swagger also allows queries and tests without the need to resort to postman.
The methods contained in the API are:

![image](https://user-images.githubusercontent.com/78773393/138623885-ae9f22d7-7a04-4418-8963-eda21ca0240e.png)

# 🧾 Preview

![image](https://user-images.githubusercontent.com/78773393/138624847-093ef73f-1734-4462-8c0d-78a62a9ef4dd.png)

Thanks for reading this description up to here.
