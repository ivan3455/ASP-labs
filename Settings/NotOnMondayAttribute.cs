using System;
using System.ComponentModel.DataAnnotations; // Підключення бібліотеки для атрибутів валідації
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; // Підключення бібліотеки для валідації на клієнтському боці

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)] // Вказує на використання атрибута для властивостей із певними обмеженнями
public class NotOnMondayAttribute : ValidationAttribute, IClientModelValidator // Атрибут валідації для перевірки, чи не є день понеділком
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var selectedProduct = validationContext
            .ObjectInstance
            .GetType()
            .GetProperty("SelectedProduct")
            .GetValue(validationContext.ObjectInstance, null);
        // Отримання значення властивості "SelectedProduct" з об'єкта моделі
        var consultationDate = (DateTime)value; // Конвертація значення в DateTime

        if (
            selectedProduct != null
            && selectedProduct.ToString() == "Основи"
            && consultationDate.DayOfWeek == DayOfWeek.Monday
        )
        {
            return new ValidationResult(ErrorMessage); // Повернення помилки, якщо обраний продукт - "Основи" і день - понеділок
        }

        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        context.Attributes.Add("data-val-notonmonday", ErrorMessage);
        // Додавання атрибуту для клієнтської валідації на стороні клієнта (JavaScript)
    }
}
