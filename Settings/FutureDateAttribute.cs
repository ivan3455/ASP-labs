using System.ComponentModel.DataAnnotations; // Підключення бібліотеки для атрибутів валідації даних

namespace AspNetCoreConsultationForm.Settings
{
    public class FutureDateAttribute : ValidationAttribute // Атрибут валідації для перевірки майбутньої дати і невходження у вихідні
    {
        public override string FormatErrorMessage(string name)
        {
            return "The desired date should be in the future and not fall on a weekend!";
        }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext
        )
        {
            if (value is DateTime date) // Перевірка, чи значення є типом DateTime
            {
                if (date.Date <= DateTime.Now.Date) // Перевірка, чи обрана дата не є минулою або сьогоднішньою
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }

                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) // Перевірка, чи обрана дата не вихідний
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }

                return ValidationResult.Success; // Повернення успішного результату валідації, якщо всі перевірки успішні
            }

            return new ValidationResult("Invalid date type.");
        }
    }
}
