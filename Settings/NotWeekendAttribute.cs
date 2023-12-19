using System;
using System.ComponentModel.DataAnnotations; // Підключення бібліотеки для атрибутів валідації

namespace AspNetCoreConsultationForm.Settings
{
    public class NotWeekendAttribute : ValidationAttribute // атрибут валідації для перевірки, чи не є день вихідним
    {
        public override bool IsValid(object value) // Перевірка на валідність значення
        {
            DateTime date = (DateTime)value; // Конвертація значення в DateTime

            // Перевірка, чи обрана дата не є суботою або неділею
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}
