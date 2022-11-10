var context = new ValidationContext(columnContactEmail);
                var results = new List<ValidationResult>();
                var emailAttributes = new List<ValidationAttribute>();
                emailAttributes.Add(new EmailAddressAttribute());
                
                var isEmailValid = Validator.TryValidateValue(columnContactEmail, context, results, emailAttributes);