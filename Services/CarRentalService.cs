using CarRentalSystem.Models;
using CarRentalSystem.Repositories;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace CarRentalSystem.Services
{
    public class CarRentalService: ICarRentalService
    {
        private readonly ICarRentalRepository _carRentalRepo;
        private readonly ICarsRepository _carsRepository;
        private readonly ICarsService _carsService;
        private readonly IUserService _userService;



        public CarRentalService(ICarRentalRepository carRentalRepo, ICarsService carsService, IUserService userService, ICarsRepository carsRepository)
        {
            _carRentalRepo = carRentalRepo;
            _carsService = carsService;
            _userService = userService;
            _carsRepository = carsRepository;
        }

        public static async Task SendRentalConfirmationEmail(User user, CarRental carRental)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress($"{Environment.GetEnvironmentVariable("SENDGRID_SENDER_EMAIL")}", "Car Rental System");
            var subject = "Car Rental Confimation";
            var to = new EmailAddress($"{user.Email}", $"{user.Name}");
            var plainTextContent = "";

            // Mail body
            var htmlContent = "<p>Your car rental was successful. Please find the details for the rental below:</p>" + "<p>"+ $"<strong>Name:</strong> {user.Name}" + "<br>" + $"<strong>Car:</strong> {carRental.Car.Manufacturer} {carRental.Car.Model}" + "<br>" + $"<strong>Rented On:</strong>  {carRental.RentedOn.ToShortDateString()}" + "<br>" + $"<strong>Rented Till:</strong>  {carRental.RentedTill.ToShortDateString()}" + "<br>" + $"<strong>Amount:</strong>  ₹{carRental.Amount}" + "</p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine($"Response from SendGrid: {response.StatusCode}");
            return;
        }

        public Task<List<CarRental>> GetAllCarRentals()
        {
            return _carRentalRepo.GetAll();
        }

        public async Task<CarRental> AddCarRental(CarRentalDTO carRental)
        {
            // Create a CarRental Object
            var user = await _userService.GetUserById(carRental.UserId);
            var car = await _carsService.GetCarById(carRental.CarId);

            var newRental = new CarRental()
            {
                User = user,
                Car = car,
                RentedOn = carRental.RentedOn,
                Days = carRental.Days,
                RentedTill = carRental.RentedOn.AddDays(carRental.Days),
                Amount = carRental.Days * car.PricePerDay
            };

            await _carRentalRepo.Add(newRental);

            // Update the status of the car to unavailable
            car.IsAvailable = false;
            await _carsRepository.Update(car);

            // Send the mail
            await SendRentalConfirmationEmail(user, newRental);

            return newRental;
        }

    }
}
