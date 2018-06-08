
using System.Collections.Generic;
using wasteless.EntityModel;
using wasteless.Services;

namespace wasteless.Models.DataTransferObjects
{
    public class UserConcreteDto : User
    {
        public List<ProductsConcreteDto> ProductsConcrete { get; set; }

        EmailService emailService = new EmailService();

        public UserConcreteDto(User user) : base()
        {
            if (user != null)
            {
                UserID = user.UserID;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Email = user.Email;
                Password = user.Password;
                IsAdmin = user.IsAdmin;
                CreatedDate = user.CreatedDate;
                ident = user.ident;

                ProductsConcrete = DBService.ProductsToConcrete(DBService.GetProductsForUser(UserID));
            }
        }

        public bool HasRequiredValues => UserID != default(int) && emailService.IsValidEmail(Email?.ToString()) && !string.IsNullOrWhiteSpace(Password);
    }
}