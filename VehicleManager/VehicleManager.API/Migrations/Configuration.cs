namespace VehicleManager.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
	using VehicleManager.API.Models;

	internal sealed class Configuration : DbMigrationsConfiguration<VehicleManager.API.Data.VehicleManagerDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VehicleManager.API.Data.VehicleManagerDataContext context)
        {
			string[] colors = new string[] { "Green", "Red", "Yellow", "Blue", "Hot Pink", "White", "Black", "Silver", "Gold" };
			string[] makes = new string[] { "Toyota", "Ford", "Dodge", "Honda" };
			string[] models = new string[] { "ModelZ", "Sport", "Hemi", "ModelX", "Luxury" };
			string[] vehicleTypes = new string[] { "Hybrid", "SUV", "MiniVan", "Truck", "Car" };

			if (context.Customers.Count() == 0)
			{
				for (int i = 0; i < 100; i++)
				{
					context.Customers.Add(new Models.Customer
					{
						EmailAddress = Faker.InternetFaker.Email(),
						FirstName = Faker.NameFaker.FirstName(),
						LastName = Faker.NameFaker.LastName(),
						Telephone = Faker.PhoneFaker.Phone(),
					});
				}

				context.SaveChanges();
			}

			if (context.Vehicles.Count() == 0)
			{
				for (int i = 0; i < 100; i++)
				{
					context.Vehicles.Add(new Vehicle
					{
						Make = Faker.ArrayFaker.SelectFrom(makes),
						Model = Faker.ArrayFaker.SelectFrom(models),
						Color = Faker.ArrayFaker.SelectFrom(colors),
						RetailPrice = Faker.NumberFaker.Number(15000, 50000),
						VehicleType = Faker.ArrayFaker.SelectFrom(vehicleTypes),
						Year = Faker.DateTimeFaker.DateTime().Year
					});
				}

				context.SaveChanges();
			}

			if (context.Sales.Count() == 0)
			{
				for (int i = 0; i < 100; i++)
				{
					var vehicle = context.Vehicles.Find(Faker.NumberFaker.Number(1, 100));
					var invoiceDate = Faker.DateTimeFaker.DateTime();

					context.Sales.Add(new Sale
					{
						Customer = context.Customers.Find(Faker.NumberFaker.Number(1,100)),
						Vehicle = vehicle,
						InvoiceDate = invoiceDate,
						PaymentReceivedDate = invoiceDate.AddDays(Faker.NumberFaker.Number(1,14)),
						SalePrice = vehicle.RetailPrice
					});
				}

				context.SaveChanges();
			}
		}
    }
}
