using System;
using System.ComponentModel.DataAnnotations; // Підключення бібліотеки для атрибутів валідації
using AspNetCoreConsultationForm.Settings;

public class ConsultationModel
{
    [Required(ErrorMessage = "First and last name are required")] // Атрибут, що вказує на обов'язковість поля
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid Email")] // Атрибут, що перевіряє коректність формату Email
    public string Email { get; set; }

    [Required(ErrorMessage = "The desired consultation date is required")]
    [DataType(DataType.Date)] // Атрибут, що вказує на тип даних (дата)
    [FutureDate(ErrorMessage = "The desired date must be in the future")] // Атрибут, що перевіряє чи вибрана дата у майбутньому
    [NotOnMonday(ErrorMessage = "The desired date must not be a weekend")] //Аатрибут, що перевіряє чи не є вибрана дата понеділком
    public DateTime ConsultationDate { get; set; }

    [Required(ErrorMessage = "Please select a product")]
    public string SelectedProduct { get; set; }
}
